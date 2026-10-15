# WinWid

A lightweight Windows desktop widget that renders a clock directly on the
desktop, behind your icons. It stays out of the Alt-Tab switcher and sits on
the wallpaper layer, so it survives **Win+D** and never gets in the way of your
windows.

The clock shows the current time (12 or 24 hour, optional seconds) and the
current date, sized and positioned relative to your primary screen. Everything
is driven by a small JSON config file.

## Requirements

- Windows
- [.NET 10 SDK](https://dotnet.microsoft.com/download) (the project targets
  `net10.0-windows`)

## Build and run

```powershell
dotnet build
dotnet run
```

The app starts hidden and creates the clock window if it is enabled in the
config. Use `dotnet publish` to produce a standalone build:

```powershell
dotnet publish -c Release
```

## Configuration

On first launch the app writes a config directory to:

```
%LOCALAPPDATA%\WinWid\config\
```

It contains `clock.json`. Edit the values and restart the app to apply them.

### `clock.json`

| Key              | Type    | Default | Description                                                                 |
| ---------------- | ------- | ------- | --------------------------------------------------------------------------- |
| `Enable`         | bool    | `true`  | Whether the clock window is shown.                                          |
| `HeightRatio`    | int     | `3`     | Clock height is `screen height / HeightRatio`.                              |
| `TopOffsetRatio` | int     | `20`    | Clock top offset is `screen height / TopOffsetRatio`.                       |
| `Use24H`         | bool    | `true`  | Use 24-hour time. When `false`, uses 12-hour time with an `AM`/`PM` suffix. |
| `ShowSeconds`    | bool    | `true`  | Show seconds in the time.                                                   |
| `ShowDate`       | bool    | `true`  | Show the date line under the time.                                          |
| `Opacity`        | double  | `1`     | Opacity of the clock (`0`–`1`).                                             |
| `WidthRatio`     | int     | `3`     | Reserved; not currently applied.                                            |

Example:

```json
{
  "Enable": true,
  "WidthRatio": 3,
  "HeightRatio": 3,
  "TopOffsetRatio": 20,
  "Use24H": true,
  "ShowSeconds": true,
  "ShowDate": true,
  "Opacity": 1
}
```

The clock is always horizontally centered on the primary screen.

## Project layout

| Path                       | Purpose                                             |
| -------------------------- | --------------------------------------------------- |
| `App.xaml` / `App.xaml.cs` | Application entry point and shared font resources.  |
| `MainWindow.xaml`          | Hidden host window; creates the enabled widgets.    |
| `Clock.xaml` / `Clock.xaml.cs` | The clock desktop widget.                       |
| `Config.cs`                | Loads and saves the config.                         |
| `ConfigObjects/`           | Config models, one per widget.                      |
| `Assetts/Fonts/`           | Bundled fonts (Lexend Deca, Montserrat, Roboto).    |

## Fonts

The bundled fonts in `Assetts/Fonts/` are licensed under the SIL Open Font
License. See the `OFL.txt` files in each font directory.
