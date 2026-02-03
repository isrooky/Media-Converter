# 🎬 MediaConverter

MediaConverter is a simple Windows desktop app (WinForms) built around FFmpeg to convert, compress, cut videos and process images (channels, transparency, etc.).

The repository contains two different things, depending on what you are looking for 👇

## 📁 Repository structure
🔹 MediaConverter/

This folder contains:

  - The raw Windows Forms project

  - Source code (C#)

  - Visual Studio solution

  - FFmpeg is NOT included here

👉 This folder is intended for:

  - Developers

  - People who want to modify or build the app themselves

  - Learning / extending the code

### If you only want to use the app, you don’t need this folder.

## 🔹 MediaConverterApp/ ⭐ (what most users want)

This folder contains:

  - The ready-to-use application

  - MediaConverter.exe

  - A bundled compressed version of FFmpeg

👉 This is the folder you want if you just want to run the app.

# 🚀 How to use the app (MediaConverterApp)

1. Download the MediaConverterApp folder

2. Run MediaConverter.exe

3. Drag & drop a video or image into the app

4. Use the tools (convert, reduce, cut, extract, etc.)

# 🔧 About FFmpeg

FFmpeg binaries are included as compressed .zip files

  On the first use, the app automatically:

   Detects that FFmpeg is not extracted

   Decompresses the required tools (ffmpeg, ffprobe, ffplay)

This happens automatically and only once

No manual setup required

# ⬇️ Download (recommended)

👉 If you only want the app, download just the usable version:

### ➡️ Go to:
MediaConverterApp/

You can download the folder as a ZIP from GitHub and extract it anywhere on your PC.

You do NOT need Visual Studio to run the app.

---

# 🧠 Notes

Windows only

Portable app (no installer required)

FFmpeg is used under its respective license (LGPL/GPL depending on build)

### 🛠 For developers

If you want to build or modify the app:

Open the solution inside MediaConverter/

You must provide your own FFmpeg binaries for development

The project is a standard WinForms (.NET) application

### 📜 License

This project is provided as-is.
FFmpeg is distributed separately and follows its own license.
