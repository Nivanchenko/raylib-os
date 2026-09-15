# Анализ покрытия raylib API

Сравнение `basic_api.md` (полный список C API raylib) и `src/raylibos/Raylibos.cs` (что обёрнуто для OneScript на сегодня).

## Итог

Обёрнуто примерно **100 функций из ~500+** в `basic_api.md` (после `task1.md`). Покрыт "happy path" 2D-рисования, управление окном/мониторами, часть 3D-моделей и минимальный ввод. Не тронуты: аудио (0%), текст/шрифты (кроме `DrawText`/`DrawFPS`), коллизии, работа с изображениями (кроме генераторов градиентов/шума), меши, материалы, анимации, файловая система, VR, жесты, геймпады.

## Что уже реализовано (модуль → функции)

- **rcore / окно**: `InitWindow`, `WindowShouldClose`, `CloseWindow`, `BeginDrawing`, `EndDrawing`, `ClearBackground`, `SetTargetFPS`, `DrawFPS`, плюс полный набор состояния/размеров/мониторов/буфера обмена окна из `task1.md` (`IsWindow*`, `Set/ClearWindowState`, `Toggle*`, `Maximize/Minimize/RestoreWindow`, `SetWindowTitle/Icon(s)/Position/Monitor/Min/Max/Size/Opacity/Focused`, `Get*Screen/Render/Monitor*`, `GetWindowPosition/ScaleDPI`, `Get/SetClipboardText`, `Enable/DisableEventWaiting`)
- **rcore / курсор**: `ShowCursor`, `HideCursor`, `IsCursorHidden`
- **rcore / ввод**: `IsKeyDown`, `IsMouseButtonPressed`, `GetMousePosition`, `GetMouseWheelMove`
- **rcore / камера**: `BeginMode2D`/`EndMode2D`, `BeginMode3D`/`EndMode3D`, `UpdateCamera` (3D), собственные конструкторы `NewCamera2D`/`NewCamera3D` + геттеры полей Camera2D
- **rcore / random**: `GetRandomValue`
- **rshapes**: `DrawCircle`, `DrawCircleGradient`, `DrawCircleLines`, `DrawEllipse`, `DrawEllipseLines`, `DrawRectangle`, `DrawRectangleLines`, `DrawRectangleGradientV/H`, `DrawRectanglePro`, `DrawTriangle`, `DrawTriangleLines`, `DrawLine`, `DrawLineStrip`, `DrawTriangleFan`, `DrawTriangleStrip`, `DrawPoly`, `DrawPolyLines`, `DrawPolyLinesEx`
- **rtextures**: `LoadTexture`, `UnloadTexture`, `DrawTexture`, `DrawTextureEx`, `DrawTextureRec`, `DrawTexturePro`, генераторы `GenImageGradientLinear/Radial/Square`, `GenImageChecked`, `GenImageWhiteNoise`, `GenImagePerlinNoise`, `GenImageCellular`, `LoadTextureFromImage`, `UnloadImage`
- **rtext**: только `DrawText`, `DrawFPS`
- **rmodels**: `LoadModel`, `UnloadModel`, `DrawModel`, `GetModelBoundingBox`, `DrawBoundingBox`, `DrawGrid`, `DrawCube`, `DrawCubeWires`, `SetModelTexture` (кастомный helper, не из raylib)
- **raudio**: ничего

## Задачи на реализацию недостающего

Разбито по модулям/приоритету, каждая — отдельный файл в `tasks/`:

| Файл | Модуль | Приоритет | Что внутри |
|---|---|---|---|
| [task1.md](task1.md) | rcore: окно и мониторы | Высокий | ✅ реализовано — состояние окна, размеры, позиция, буфер обмена |
| [task2.md](task2.md) | rcore: курсор, тайминг, misc | Средний | таймеры, скриншот, флаги конфигурации, seed |
| [task3.md](task3.md) | rcore: ввод (клавиатура/мышь/геймпад/тач/жесты) | Высокий | недостающие проверки клавиш/мыши, геймпады, тач, жесты |
| [task4.md](task4.md) | rcore: экранные координаты, режимы рендера, шейдеры | Средний | Screen↔World, RenderTexture, ShaderMode, BlendMode, ScissorMode, Shader API |
| [task5.md](task5.md) | rshapes: примитивы и коллизии | Высокий | недостающие Draw*, сплайны, коллизии 2D |
| [task6.md](task6.md) | rtextures: Image (CPU) | Средний | загрузка/экспорт/модификация изображений, рисование на Image |
| [task7.md](task7.md) | rtextures: Texture (GPU) и цвет | Средний | cubemap, render texture, фильтры, NPatch, утилиты Color |
| [task8.md](task8.md) | rtext: шрифты и текст | Средний | загрузка шрифтов, DrawTextEx/Pro, измерение текста, строковые утилиты |
| [task9.md](task9.md) | rmodels: 3D примитивы | Средний | линии/сферы/цилиндры/капсулы/биллборды в 3D |
| [task10.md](task10.md) | rmodels: меши, материалы, анимации, 3D-коллизии | Низкий | генерация мешей, материалы, скелетная анимация, raycast |
| [task11.md](task11.md) | raudio: звук целиком | Низкий | устройство, Wave/Sound, Music, AudioStream — модуль не начат |
| [task12.md](task12.md) | rcore: файловая система, сжатие, automation events | Низкий | работа с файлами, base64/CRC/MD5/SHA, запись/воспроизведение событий |

## Общие правила для всех задач

См. `AGENTS.md` → "Adding a new wrapped method":
1. Сигнатура — в `basic_api.md`.
2. Метод — в `Raylibos.cs`, `[ContextMethod("РусскоеИмя", "EnglishName")]`, конвертация через существующие `IValueTo*` хелперы (добавлять новые при необходимости, например `IValueToFont`, `IValueToSound`).
3. Тестовый `.os`-скрипт в `src/`.
4. Обновить README.md в соответствующем разделе.

`Image` оборачивается через `ImageWrapper : AutoContext<ImageWrapper>`, а не `COMWrapperContext.Create()` — держать эту асимметрию в уме при работе с `task6.md`.
