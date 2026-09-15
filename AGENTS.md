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
  test*.os           # 13 demo scripts, one per feature area (see README "Тестовые файлы")
dependency/          # Prebuilt Raylib-cs.dll + native libs (Linux/MacOS/Windows)
resources/           # Sample textures/models used by test scripts
examples/physics/    # Standalone 2D physics engine written in OneScript; uses raylibos only for rendering (see its README-equivalent: no docs yet, read app.os)
basic_api.md         # Reference dump of raylib's C API signatures, grouped by module (rcore, rshapes, rtextures, rmodels, ...) — use it to check what's still unwrapped in Raylibos.cs
raylibos.sln         # VS solution wrapping src/raylibos
```

## Key conventions
- Methods exposed to OneScript use `[ContextMethod("RussianName", "EnglishName")]`
- Constructor uses `[ScriptConstructor]` attribute
- Color/Vector2/Vector3/Rectangle/Camera2D/Camera3D/Model/BoundingBox/Texture2D marshaling via `COMWrapperContext.Create()` and `MarshalIValue()`
- Exception: `Image` is wrapped in its own `AutoContext` class (`ImageWrapper`) instead of going through `COMWrapperContext.Create()` — relevant when touching `GenImage*`, `LoadTextureFromImage`, or `UnloadImage`
- Russian method names are primary; English names are aliases

## Adding a new wrapped method
1. Look up the C signature in `basic_api.md` (grouped by raylib module)
2. Add a `[ContextMethod("RussianName", "EnglishName")]` to `Raylibos.cs`, converting params/return via the `IValueTo*` helpers at the bottom of the class
3. Add or extend a `.os` test script under `src/` demonstrating the new method
4. Document the method in README.md under the matching section

## VS Code debugging
- Debug config in `.vscode/launch.json` uses `/Users/nikita.ivanchenko/.local/share/ovm/current/bin/oscript`
- Debug port: 2801, working directory: `${workspaceRoot}/src`

## Important notes
- No tests or CI configured yet
- `dependency/` contains platform-specific native libraries — do not remove
- Build output goes to `src/raylibos/bin/` and `obj/` (gitignored except for Debug DLL used by test.os)
- Test script loads DLL via relative path: `raylibos/bin/Debug/net6.0/raylibos.dll` (from `src/`) or `../../src/raylibos/bin/Debug/net6.0/raylibos.dll` (from `examples/physics/`)
