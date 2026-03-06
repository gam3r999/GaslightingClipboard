<div align="center">

# 📋 GaslightingClipboard

### *Your clipboard has been upgraded. Enjoy! :)*

![Platform](https://img.shields.io/badge/platform-Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)
![Language](https://img.shields.io/badge/language-C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Framework](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![License](https://img.shields.io/badge/license-Prank-ff69b4?style=for-the-badge)
![Chaos](https://img.shields.io/badge/chaos_level-maximum-red?style=for-the-badge)

<br/>

> *A completely invisible clipboard vandal that silently corrupts everything your victim copies — replacing words with absurd synonyms, injecting typos, and appending unhinged suffixes. No tray icon. No taskbar entry. No mercy.*

<br/>

```
They copy:   "Send the report to the boss ASAP"
They paste:  "Dispatch teh scroll of findings 2 da grand poobah post-haste (citation needed)"
```

</div>

---

## 🎭 What It Does

The moment it launches, **GaslightingClipboard** vanishes completely from sight. No icon. No window. No trace. Then it waits — and every time your victim copies *anything*, their clipboard gets silently mutilated before they can paste it.

| Corruption Type | Example |
|---|---|
| 🔄 **Synonym Swap** | `boss` → `supreme overlord` |
| ✍️ **Typo Injection** | `the` → `teh` |
| 📝 **Letter Swap** | `send` → `snde` |
| 🌀 **Unhinged Suffix** | `...probably.` / `— source: trust me bro` |
| 📢 **Rare ALL CAPS** | Entire paste becomes a scream |

---

## 🚀 Getting Started

### Prerequisites

- Windows 10 / 11
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

```bash
# 1. Clone the repo
git clone https://github.com/yourusername/GaslightingClipboard.git
cd GaslightingClipboard

# 2. Drop your clipboard.ico into the project folder (optional)

# 3. Build a single standalone .exe
dotnet publish GaslightingClipboard.csproj -c Release -r win-x64 \
  --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -p:DebugType=none \
  -p:DebugSymbols=false
```

Your `.exe` will appear at:
```
bin\Release\net8.0-windows\win-x64\publish\GaslightingClipboard.exe
```

> **One file. No DLLs. No PDBs. Nothing else.**

---

## 🧬 How It Works

```
┌─────────────────────────────────────────────────────┐
│                  GaslightingClipboard                │
│                                                     │
│  Launch → Ghost balloon tip → Tray icon vanishes    │
│                      ↓                              │
│         Background STA thread spins up              │
│                      ↓                              │
│    Every ~300ms: read clipboard contents            │
│                      ↓                              │
│         New content detected? CORRUPT IT.           │
│                      ↓                              │
│    Silently write corrupted text back               │
└─────────────────────────────────────────────────────┘
```

The corruption engine processes text **word by word**:

1. Checks against a **70+ word synonym dictionary**
2. Falls back to subtle letter swaps and typos for unknown words
3. Rolls a dice — 50% chance of appending a chaotic suffix
4. Extremely rare (0.6%) chance of going full **ALL CAPS MODE**

---

## 📖 Corruption Dictionary (Highlights)

| Word | Possible Replacements |
|---|---|
| `meeting` | `powwow`, `séance`, `congregation` |
| `password` | `open sesame`, `secret incantation`, `magic words` |
| `boss` | `supreme overlord`, `big cheese`, `grand poobah` |
| `error` | `gremlin`, `unexpected feature`, `cosmic hiccup` |
| `deadline` | `doom clock`, `the reckoning`, `fate timestamp` |
| `please` | `I beg thee`, `pretty please` |
| `help` | `mayday`, `SOS`, `send reinforcements` |
| `asap` | `post-haste`, `with fury of thousand suns` |

---

## ⚙️ Customization

All the good stuff is right at the top of the source file:

```csharp
// Change the launch message
const string INITIAL_MESSAGE = "Your clipboard has been upgraded. Enjoy! :)";

// Add your own words to the dictionary
{"yourword", new[] {"replacement 1", "replacement 2", "replacement 3"}},

// Add your own unhinged suffixes
" (this is totally fine)",
" — I am not a robot, probably.",
```

---

## 🛑 How to Stop It

Since there's **no tray icon and no taskbar entry**, the only way to kill it is:

```
Ctrl + Shift + Esc  →  Task Manager  →  Find GaslightingClipboard  →  End Task
```

Or restart the computer. That works too.

---

## 📁 Project Structure

```
GaslightingClipboard/
├── GaslightingClipboard.cs       # The entire prank, one file
├── GaslightingClipboard.csproj   # Build config
└── clipboard.ico                 # Custom icon (optional)
```

---

## 🤝 Pairs Well With

> Want to take the chaos to the next level? Run this alongside **Autocorrect From Hell** for the ultimate prank combo — GaslightingClipboard corrupts everything they *copy*, while Autocorrect From Hell destroys everything they *type*. Your victim won't trust a single word on their screen.

<div align="center">

### 🔥 The Ultimate Prank Stack 🔥

| | Project | What it destroys |
|---|---|---|
| 📋 | **GaslightingClipboard** *(you are here)* | Everything they copy & paste |
| ⌨️ | [**Autocorrect From Hell**](https://github.com/gam3r999/Autocorrect-From-Hell) | Everything they type |

[![Autocorrect From Hell](https://img.shields.io/badge/Check%20It%20Out-Autocorrect%20From%20Hell-ff4444?style=for-the-badge&logo=github&logoColor=white)](https://github.com/gam3r999/Autocorrect-From-Hell)

</div>

---

## ⚠️ Disclaimer

This project is a **harmless prank** for use on friends and colleagues who have consented (or at least deserve it). It does **not**:

- ❌ Send any data anywhere
- ❌ Log keystrokes
- ❌ Access any files
- ❌ Persist after process is killed
- ❌ Modify anything outside the clipboard

Use responsibly. Or don't. But don't blame me.

---

<div align="center">

Made with 😈 and way too much free time.

*They'll never know what hit them.*

</div>
