using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.IO.Compression;
namespace MediaConverter
{
    public partial class Form1 : Form
    {
        private string inputPath = "";

        private readonly string[] videoExtensions =
        {
            ".mp4", ".mkv", ".avi", ".mov", ".wmv", ".webm", ".m4v"
        };

        private readonly string[] imageExtensions =
        {
            ".png", ".jpg", ".jpeg", ".bmp", ".webp", ".tif", ".tiff"
        };
        private Color selectedKeyColor = Color.White; // default
        private string GetToolBinDir()
        {
            // donde tú ya tienes ffmpeg\bin
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg", "bin");
        }

        private string EnsureToolExe(string exeNameWithoutExt)
        {
            // exeNameWithoutExt: "ffmpeg" | "ffprobe" | "ffplay"
            var binDir = GetToolBinDir();
            Directory.CreateDirectory(binDir);

            var exePath = Path.Combine(binDir, exeNameWithoutExt + ".exe");
            if (File.Exists(exePath))
                return exePath;

            // Si no existe el exe, probamos con el zip
            var zipPath = Path.Combine(binDir, exeNameWithoutExt + ".zip");
            if (!File.Exists(zipPath))
                return exePath; // devolverá una ruta que no existe; tu ValidateInputReady ya avisará

            // Extrae el zip dentro del mismo binDir
            // IMPORTANTE: el zip debe contener el .exe (idealmente en la raíz del zip)
            ZipFile.ExtractToDirectory(zipPath, binDir, overwriteFiles: true);

            // Caso común: el exe sale directo
            if (File.Exists(exePath))
                return exePath;

            // Caso alternativo: el zip trae subcarpetas (buscamos el exe dentro)
            var found = Directory.GetFiles(binDir, exeNameWithoutExt + ".exe", SearchOption.AllDirectories).FirstOrDefault();
            if (found != null)
                return found;

            return exePath; // no encontrado
        }


        public Form1()
        {
            InitializeComponent();

            // ===== Video convert =====
            FormatCB.DropDownStyle = ComboBoxStyle.DropDownList;
            FormatCB.Items.AddRange(new object[] { "mp4", "mkv", "avi", "webm", "m4v" });
            FormatCB.SelectedIndex = 0;

            // ===== Reduce (compress) =====
            ReduceCB.DropDownStyle = ComboBoxStyle.DropDownList;
            ReduceCB.Items.AddRange(new object[]
            {
                "Soft (better quality)",
                "Medium (balanced)",
                "Aggressive (lighter)"
            });
            ReduceCB.SelectedIndex = 0;

            // ===== Extract audio =====
            AudioCB.DropDownStyle = ComboBoxStyle.DropDownList;
            AudioCB.Items.AddRange(new object[] { "mp3", "m4a", "aac", "wav", "flac", "opus", "ogg" });
            AudioCB.SelectedIndex = 0;

            // ===== Trim UI (TrackBars) =====
            StartTrack.Minimum = 0;
            EndTrack.Minimum = 0;

            StartTrack.Scroll += StartTrack_Scroll;
            EndTrack.Scroll += EndTrack_Scroll;

            LBInicio.Text = "Start: 00:00:00";
            LBFin.Text = "End: 00:00:00";

            FormatImageCB.DropDownStyle = ComboBoxStyle.DropDownList;
            FormatImageCB.Items.AddRange(new object[] { "png", "jpg", "jpeg", "webp", "bmp", "tif", "tiff" });
            FormatImageCB.SelectedIndex = 0;
            ExtractChannelsCB.DropDownStyle = ComboBoxStyle.DropDownList;
            ExtractChannelsCB.Items.AddRange(new object[] { "All", "R", "G", "B", "A" });
            ExtractChannelsCB.SelectedIndex = 0;

            // Default color
            ColorPreviewPanel.BackColor = selectedKeyColor;

            // TrackBars (0..100 => 0.00..1.00)
            ToleranceTrack.Minimum = 0;
            ToleranceTrack.Maximum = 100;
            ToleranceTrack.TickFrequency = 5;
            ToleranceTrack.Value = 15;

            SoftnessTrack.Minimum = 0;
            SoftnessTrack.Maximum = 100;
            SoftnessTrack.TickFrequency = 5;
            SoftnessTrack.Value = 7;

            ToleranceTrack.Scroll += ToleranceTrack_Scroll;
            SoftnessTrack.Scroll += SoftnessTrack_Scroll;

            UpdateKeySettingsLabels();


        }
        private enum AppMode { None, Video, Image }

        private void SetMode(AppMode mode)
        {
            PanelVideos.Visible = (mode == AppMode.Video);
            PanelImages.Visible = (mode == AppMode.Image);
        }

        // =========================
        // DRAG & DROP
        // =========================

        private void PanelDragDrop_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void PanelDragDrop_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null) return;

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null || files.Length == 0) return;

            var path = files[0];

            if (Directory.Exists(path))
            {
                MessageBox.Show("Drag a file, not a folder.");
                return;
            }

            var ext = Path.GetExtension(path).ToLowerInvariant();

            inputPath = path;
            InputTB.Text = inputPath;

            // OutputTB WITHOUT extension
            OutputTB.Text = Path.GetFileNameWithoutExtension(inputPath) + "_Converted";
            // IMAGE MODE
            if (imageExtensions.Contains(ext))
            {
                SetMode(AppMode.Image);
                InitImageUI();
                return;
            }

            // VIDEO MODE
            if (videoExtensions.Contains(ext))
            {
                SetMode(AppMode.Video);
                InitVideoUI(path);
                return;
            }

            MessageBox.Show("Unsupported file type.");
            SetMode(AppMode.None);
        }
        private void InitVideoUI(string path)
        {

            // Default output format = input extension (if exists in dropdown)
            var ext = Path.GetExtension(path).ToLowerInvariant();
            SetComboTo(FormatCB, ext.TrimStart('.'));

            // Immediate UI state
            LBInicio.Text = "Start: (loading...)";
            LBFin.Text = "End: (loading...)";

            // Run ffprobe in background
            _ = InitTrimUIAsync();
        }

        private void InitImageUI()
        {
            var ext = Path.GetExtension(inputPath).ToLowerInvariant().TrimStart('.');
            SetComboTo(FormatImageCB, ext);

            ExtractChannelsCB.SelectedIndex = 0; // All
        }
        private string BuildExtractChannelArgs(string inputFilePath, string outputFilePath, string channelLetter)
        {
            // channelLetter: "r", "g", "b", "a"
            // Force RGBA so planes always exist (especially alpha)
            return $"-y -i \"{inputFilePath}\" " +
                   $"-filter_complex \"[0:v]format=rgba,extractplanes={channelLetter}[ch]\" " +
                   $"-map \"[ch]\" -frames:v 1 \"{outputFilePath}\"";
        }


        private async void ExtractChannelsBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputReady()) return;

            var inExt = Path.GetExtension(inputPath).ToLowerInvariant();
            if (!imageExtensions.Contains(inExt))
            {
                MessageBox.Show("Current file is not an image.");
                return;
            }

            var choice = (ExtractChannelsCB.SelectedItem?.ToString() ?? "All").ToUpperInvariant();

            var folder = Path.GetDirectoryName(inputPath)!;
            var baseName = Path.GetFileNameWithoutExtension(inputPath);

            try
            {
                Cursor = Cursors.WaitCursor;
                ExtractChannelsBtn.Enabled = false;

                if (choice == "ALL")
                {
                    var outR = Path.Combine(folder, $"{baseName}_R.png");
                    var outG = Path.Combine(folder, $"{baseName}_G.png");
                    var outB = Path.Combine(folder, $"{baseName}_B.png");
                    var outA = Path.Combine(folder, $"{baseName}_A.png");

                    await RunFfmpeg(BuildExtractChannelArgs(inputPath, outR, "r"), outR);
                    await RunFfmpeg(BuildExtractChannelArgs(inputPath, outG, "g"), outG);
                    await RunFfmpeg(BuildExtractChannelArgs(inputPath, outB, "b"), outB);
                    await RunFfmpeg(BuildExtractChannelArgs(inputPath, outA, "a"), outA);

                    MessageBox.Show("✅ Channels exported:\n" + outR + "\n" + outG + "\n" + outB + "\n" + outA);
                    return;
                }

                string ch = choice switch
                {
                    "R" => "r",
                    "G" => "g",
                    "B" => "b",
                    "A" => "a",
                    _ => "r"
                };

                var output = Path.Combine(folder, $"{baseName}_{choice}.png");
                await RunFfmpeg(BuildExtractChannelArgs(inputPath, output, ch), output);

                MessageBox.Show("✅ Channel exported:\n" + output);
            }
            finally
            {
                ExtractChannelsBtn.Enabled = true;
                Cursor = Cursors.Default;
            }
        }



        private string BuildImageConvertArgs(string input, string output)
        {
            // Para imágenes, suele bastar con: -y -i input output
            return $"-y -i \"{input}\" \"{output}\"";
        }
        private async void ConvertImageBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputReady()) return;

            // Asegurarnos de que el input actual es imagen
            var inExt = Path.GetExtension(inputPath).ToLowerInvariant();
            if (!imageExtensions.Contains(inExt))
            {
                MessageBox.Show("Current file is not an image.");
                return;
            }

            var ext = (FormatImageCB.SelectedItem?.ToString() ?? "png").TrimStart('.').ToLowerInvariant();

            var folder = Path.GetDirectoryName(inputPath)!;
            var baseName = GetOutputBaseNameOrDefault("_Converted"); // OutputTB sin extensión
            var outputPath = Path.Combine(folder, $"{baseName}.{ext}");

            var args = BuildImageConvertArgs(inputPath, outputPath);
            await RunFfmpeg(args, outputPath);
        }


        private void SetComboTo(ComboBox cb, string value)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (string.Equals(cb.Items[i]?.ToString(), value, StringComparison.OrdinalIgnoreCase))
                {
                    cb.SelectedIndex = i;
                    return;
                }
            }
        }

        // =========================
        // FFmpeg PATHS
        // =========================
        private string GetFfmpegPath()
        {
            return EnsureToolExe("ffmpeg");
        }

        private string GetFfprobePath()
        {
            return EnsureToolExe("ffprobe");
        }


        // =========================
        // VIDEO CONVERT
        // =========================

        private string BuildVideoConvertArgs(string input, string output)
        {
            var format = (FormatCB.SelectedItem?.ToString() ?? "mp4").ToLowerInvariant();

            return format switch
            {
                "mp4" or "mkv" or "m4v" =>
                    $"-y -i \"{input}\" -c:v libx264 -crf 23 -preset medium -c:a aac -b:a 128k \"{output}\"",

                "webm" =>
                    $"-y -i \"{input}\" -c:v libvpx-vp9 -crf 32 -b:v 0 -c:a libopus -b:a 128k \"{output}\"",

                "avi" =>
                    $"-y -i \"{input}\" -c:v mpeg4 -q:v 5 -c:a libmp3lame -q:a 4 \"{output}\"",

                _ =>
                    $"-y -i \"{input}\" -c:v libx264 -crf 23 -preset medium -c:a aac -b:a 128k \"{output}\""
            };
        }

        private async void ConvertBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputReady()) return;

            var baseName = GetOutputBaseNameOrDefault("_Converted");
            var ext = (FormatCB.SelectedItem?.ToString() ?? "mp4").TrimStart('.');
            var outputFolder = Path.GetDirectoryName(inputPath)!;
            var outputPath = Path.Combine(outputFolder, $"{baseName}.{ext}");

            await RunFfmpeg(BuildVideoConvertArgs(inputPath, outputPath), outputPath);
        }

        // =========================
        // REDUCE (compress)
        // =========================

        private string BuildReduceArgs(string input, string output)
        {
            var preset = ReduceCB.SelectedItem?.ToString() ?? "";
            var inExt = Path.GetExtension(input).ToLowerInvariant();

            (int crf, string speed, string ab) = preset switch
            {
                "Soft (better quality)" => (23, "medium", "128k"),
                "Medium (balanced)" => (26, "slow", "96k"),
                "Aggressive (lighter)" => (30, "veryslow", "64k"),
                _ => (23, "medium", "128k")
            };

            if (inExt == ".webm")
            {
                // Keep WebM: VP9 + Opus
                return $"-y -i \"{input}\" -c:v libvpx-vp9 -crf {Math.Min(crf + 6, 40)} -b:v 0 -c:a libopus -b:a {ab} \"{output}\"";
            }

            // For most containers: H.264 + AAC
            return $"-y -i \"{input}\" -c:v libx264 -crf {crf} -preset {speed} -c:a aac -b:a {ab} \"{output}\"";
        }

        private string BuildReducedOutputPath()
        {
            var folder = Path.GetDirectoryName(inputPath) ?? "";
            var baseName = Path.GetFileNameWithoutExtension(inputPath);
            var inputExt = Path.GetExtension(inputPath);

            var label = ReduceCB.SelectedItem?.ToString() switch
            {
                "Soft (better quality)" => "Soft",
                "Medium (balanced)" => "Medium",
                "Aggressive (lighter)" => "Aggressive",
                _ => "Soft"
            };

            return Path.Combine(folder, $"{baseName}_Reduced_{label}{inputExt}");
        }

        private async void ReduceBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputReady()) return;

            var outputPath = BuildReducedOutputPath();
            await RunFfmpeg(BuildReduceArgs(inputPath, outputPath), outputPath);
        }

        // =========================
        // EXTRACT AUDIO
        // =========================

        private string BuildExtractAudioArgs(string input, string output)
        {
            var fmt = (AudioCB.SelectedItem?.ToString() ?? "mp3").ToLowerInvariant();

            // -vn = no video
            return fmt switch
            {
                "mp3" => $"-y -i \"{input}\" -vn -c:a libmp3lame -q:a 2 \"{output}\"",
                "m4a" => $"-y -i \"{input}\" -vn -c:a aac -b:a 192k \"{output}\"",
                "aac" => $"-y -i \"{input}\" -vn -c:a aac -b:a 192k \"{output}\"",
                "wav" => $"-y -i \"{input}\" -vn -c:a pcm_s16le \"{output}\"",
                "flac" => $"-y -i \"{input}\" -vn -c:a flac \"{output}\"",
                "opus" => $"-y -i \"{input}\" -vn -c:a libopus -b:a 128k \"{output}\"",
                "ogg" => $"-y -i \"{input}\" -vn -c:a libvorbis -q:a 5 \"{output}\"",
                _ => $"-y -i \"{input}\" -vn -c:a libmp3lame -q:a 2 \"{output}\""
            };
        }

        private async void ExtractBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputReady()) return;

            var ext = (AudioCB.SelectedItem?.ToString() ?? "mp3").TrimStart('.');
            var outputFolder = Path.GetDirectoryName(inputPath)!;

            // OutputTB without extension; for audio append _Audio
            var baseName = OutputTB.Text.Trim();
            if (string.IsNullOrWhiteSpace(baseName))
                baseName = Path.GetFileNameWithoutExtension(inputPath);

            baseName = Path.GetFileNameWithoutExtension(baseName);
            if (!baseName.EndsWith("_Audio", StringComparison.OrdinalIgnoreCase))
                baseName += "_Audio";

            var outputPath = Path.Combine(outputFolder, $"{baseName}.{ext}");

            await RunFfmpeg(BuildExtractAudioArgs(inputPath, outputPath), outputPath);
        }

        // =========================
        // TRIM / CUT
        // =========================

        private async System.Threading.Tasks.Task InitTrimUIAsync()
        {
            if (string.IsNullOrWhiteSpace(inputPath)) return;

            double duration = await System.Threading.Tasks.Task.Run(async () =>
            {
                return await GetDurationSecondsAsync(inputPath);
            });

            if (duration <= 0)
            {
                if (!IsDisposed)
                {
                    BeginInvoke(new Action(() =>
                    {
                        LBInicio.Text = "Start: 00:00:00";
                        LBFin.Text = "End: (unknown duration)";
                    }));
                }
                return;
            }

            int max = (int)Math.Ceiling(duration);
            if (max < 1) max = 1;

            if (IsDisposed) return;

            BeginInvoke(new Action(() =>
            {
                StartTrack.Minimum = 0;
                EndTrack.Minimum = 0;

                StartTrack.Maximum = max;
                EndTrack.Maximum = max;

                StartTrack.Value = 0;
                EndTrack.Value = max;

                UpdateTrimLabels();
            }));
        }

        private void StartTrack_Scroll(object sender, EventArgs e)
        {
            if (StartTrack.Value > EndTrack.Value)
                EndTrack.Value = StartTrack.Value;

            UpdateTrimLabels();
        }

        private void EndTrack_Scroll(object sender, EventArgs e)
        {
            if (EndTrack.Value < StartTrack.Value)
                StartTrack.Value = EndTrack.Value;

            UpdateTrimLabels();
        }

        private void UpdateTrimLabels()
        {
            LBInicio.Text = "Start: " + TimeSpan.FromSeconds(StartTrack.Value).ToString(@"hh\:mm\:ss");
            LBFin.Text = "End: " + TimeSpan.FromSeconds(EndTrack.Value).ToString(@"hh\:mm\:ss");
        }

        private string BuildCutArgsFastCopy(string input, string output, TimeSpan start, TimeSpan end)
        {
            string ss = start.ToString(@"hh\:mm\:ss");
            string to = end.ToString(@"hh\:mm\:ss");
            return $"-y -ss {ss} -to {to} -i \"{input}\" -c copy \"{output}\"";
        }

        private async void CutBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputReady()) return;

            int startSec = StartTrack.Value;
            int endSec = EndTrack.Value;

            if (endSec <= startSec)
            {
                MessageBox.Show("End must be greater than Start");
                return;
            }

            var start = TimeSpan.FromSeconds(startSec);
            var end = TimeSpan.FromSeconds(endSec);

            var folder = Path.GetDirectoryName(inputPath)!;
            var baseName = Path.GetFileNameWithoutExtension(inputPath);
            var ext = Path.GetExtension(inputPath);

            var outputPath = Path.Combine(folder, $"{baseName}_Cut_{startSec}s_{endSec}s{ext}");
            var args = BuildCutArgsFastCopy(inputPath, outputPath, start, end);

            await RunFfmpeg(args, outputPath);
        }

        // =========================
        // EXTRACT FRAME @ StartTrack
        // =========================

        private string BuildExtractFrameArgs(string input, string output, TimeSpan time)
        {
            string ss = time.ToString(@"hh\:mm\:ss");
            return $"-y -ss {ss} -i \"{input}\" -frames:v 1 \"{output}\"";
        }

        private async void ExtractFrameBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputReady()) return;

            int startSec = StartTrack.Value;
            var time = TimeSpan.FromSeconds(startSec);

            var folder = Path.GetDirectoryName(inputPath)!;
            var baseName = Path.GetFileNameWithoutExtension(inputPath);

            var outputPath = Path.Combine(folder, $"{baseName}_Frame_{startSec}s.jpg");
            var args = BuildExtractFrameArgs(inputPath, outputPath, time);

            await RunFfmpeg(args, outputPath);
        }

        // =========================
        // ffprobe duration
        // =========================

        private async System.Threading.Tasks.Task<double> GetDurationSecondsAsync(string file)
        {
            var ffprobe = GetFfprobePath();
            if (!File.Exists(ffprobe)) return 0;

            var psi = new ProcessStartInfo
            {
                FileName = ffprobe,
                Arguments = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{file}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using var p = Process.Start(psi);
            if (p == null) return 0;

            var output = await p.StandardOutput.ReadToEndAsync();
            await p.WaitForExitAsync();

            if (double.TryParse(output.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double seconds))
                return seconds;

            return 0;
        }

        // =========================
        // HELPERS
        // =========================

        private bool ValidateInputReady()
        {
            if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
            {
                MessageBox.Show("Drag a video first.");
                return false;
            }

            if (!File.Exists(GetFfmpegPath()))
            {
                MessageBox.Show("ffmpeg.exe not found.");
                return false;
            }

            // ffprobe is optional (trim UI needs it to set duration properly)
            return true;
        }

        private string GetOutputBaseNameOrDefault(string defaultSuffix)
        {
            var baseName = OutputTB.Text.Trim();

            if (string.IsNullOrWhiteSpace(baseName))
                baseName = Path.GetFileNameWithoutExtension(inputPath) + defaultSuffix;

            baseName = Path.GetFileNameWithoutExtension(baseName);
            return baseName;
        }

        private async System.Threading.Tasks.Task RunFfmpeg(string args, string outputPath)
        {
            var ffmpegPath = GetFfmpegPath();
            if (!File.Exists(ffmpegPath))
            {
                MessageBox.Show("ffmpeg.exe not found.");
                return;
            }

            var psi = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            Cursor = Cursors.WaitCursor;

            try
            {
                using var p = new Process { StartInfo = psi };
                p.Start();

                // Consume outputs to avoid deadlocks
                p.BeginErrorReadLine();
                p.BeginOutputReadLine();

                await p.WaitForExitAsync();

                if (p.ExitCode == 0)
                    MessageBox.Show("✅ Done:\n" + outputPath);
                else
                    MessageBox.Show("❌ FFmpeg error (ExitCode " + p.ExitCode + ")");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Unused (safe to keep empty if wired in designer)
        private void ReduceCB_SelectedIndexChanged(object sender, EventArgs e) { }
        private void AudioCB_SelectedIndexChanged(object sender, EventArgs e) { }
        private void FormatCB_SelectedIndexChanged(object sender, EventArgs e) { }

        private void PickColorBtn_Click(object sender, EventArgs e)
        {
            using var dlg = new ColorDialog
            {
                AllowFullOpen = true,
                FullOpen = true,
                AnyColor = true,
                Color = selectedKeyColor
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                selectedKeyColor = dlg.Color;
                ColorPreviewPanel.BackColor = selectedKeyColor;
            }
        }
        private void ToleranceTrack_Scroll(object sender, EventArgs e)
        {
            UpdateKeySettingsLabels();
        }

        private void SoftnessTrack_Scroll(object sender, EventArgs e)
        {
            UpdateKeySettingsLabels();
        }

        private void UpdateKeySettingsLabels()
        {
            double tol = ToleranceTrack.Value / 100.0;  // 0.00..1.00
            double soft = SoftnessTrack.Value / 100.0;  // 0.00..1.00

            // Con 2 decimales queda muy legible
            LBTolerance.Text = $"Tolerance: {tol:0.00}";
            LBSoftness.Text = $"Softness: {soft:0.00}";
        }

        private async void CreatePNGBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputReady()) return;

            // asegurarnos de que es una imagen
            var inExt = Path.GetExtension(inputPath).ToLowerInvariant();
            if (!imageExtensions.Contains(inExt))
            {
                MessageBox.Show("Current file is not an image.");
                return;
            }

            // Color elegido -> 0xRRGGBB
            string hex = $"0x{selectedKeyColor.R:X2}{selectedKeyColor.G:X2}{selectedKeyColor.B:X2}";

            // Sliders 0..100 -> 0.00..1.00
            double tolerance = ToleranceTrack.Value / 100.0;
            double softness = SoftnessTrack.Value / 100.0;

            var folder = Path.GetDirectoryName(inputPath)!;
            var baseName = GetOutputBaseNameOrDefault("_Converted"); // OutputTB sin extensión
            var outputPath = Path.Combine(folder, $"{baseName}_Transparent.png");

            var args = BuildCreateTransparentPngArgs(inputPath, outputPath, hex, tolerance, softness);

            await RunFfmpeg(args, outputPath);
        }

        private string BuildCreateTransparentPngArgs(string input, string output, string hexColor, double tolerance, double softness)
        {
            // Forzamos RGBA para asegurar alpha, y aplicamos colorkey.
            // colorkey=0xRRGGBB:similarity:blend
            return $"-y -i \"{input}\" -vf \"format=rgba,colorkey={hexColor}:{tolerance.ToString("0.00", CultureInfo.InvariantCulture)}:{softness.ToString("0.00", CultureInfo.InvariantCulture)}\" \"{output}\"";
        }

    }
}
