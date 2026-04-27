# AGENTS.md — Raylib-os

## Project overview
- OneScript wrapper around [raylib-cs](https://github.com/raylib-cs/raylib-cs) (C# bindings for [raylib](https://www.raylib.com))
- Target: expose raylib graphics capabilities (2D/3D, shaders) to OneScript language
- Status: early experimental stage

## Build & run

```bash
# Build the library (requires .NET 6+)
dotnet build src/raylibos/raylibos.csproj

# Run test script (requires OneScript/OScript)
oscript src/test.os
```

## Structure
```
src/
  raylibos/
    Raylibos.cs      # Main context class [ContextClass("Рейлиб", "Raylib")]
    raylibos.csproj  # .NET 6.0, references: OneScript 2.0.0-rc.8, Raylib-cs 6.0.0
  test.os            # Demo script showing all drawing methods
dependency/          # Prebuilt Raylib-cs.dll + native libs (Linux/MacOS/Windows)
```

## Key conventions
- Methods exposed to OneScript use `[ContextMethod("RussianName", "EnglishName")]`
- Constructor uses `[ScriptConstructor]` attribute
- Color/Vector2 marshaling via `COMWrapperContext.Create()` and `MarshalIValue()`
- Russian method names are primary; English names are aliases

## VS Code debugging
- Debug config in `.vscode/launch.json` uses `/Users/nikita.ivanchenko/.local/share/ovm/current/bin/oscript`
- Debug port: 2801, working directory: `${workspaceRoot}/src`

## Important notes
- No tests or CI configured yet
- `dependency/` contains platform-specific native libraries — do not remove
- Build output goes to `src/raylibos/bin/` and `obj/` (gitignored except for Debug DLL used by test.os)
- Test script loads DLL via relative path: `raylibos/bin/Debug/net6.0/raylibos.dll`
