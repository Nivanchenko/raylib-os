# План расширения raylib API

`basic_api.md` — справочник по C API raylib, а `src/raylibos/Raylibos.cs` — фактически опубликованные методы OneScript. Списки ниже — ориентиры, а не синхронизируемая автоматически спецификация. Перед выбором метода проверяйте его наличие в `Raylibos.cs` и сигнатуру в установленном пакете Raylib-cs из `src/raylibos/raylibos.csproj` (XML-документация при стандартном расположении NuGet-пакетов: `~/.nuget/packages/raylib-cs/<версия>/lib/net6.0/Raylib-cs.xml`; окончательная проверка — сборка).

## Итог

Покрыты базовое 2D-рисование, управление окном/мониторами, часть 3D-моделей и минимальный ввод. Аудио, шрифты, коллизии, меши, материалы, анимации, файловая система, жесты и геймпады ещё не обёрнуты. Точное число методов смотрите в `Raylibos.cs`: здесь намеренно нет процента покрытия, поскольку C API и биндинг разных версий не совпадают один к одному.

## Что уже реализовано (модуль → функции)

- **rcore / окно**: `InitWindow`, `WindowShouldClose`, `CloseWindow`, `BeginDrawing`, `EndDrawing`, `ClearBackground`, `SetTargetFPS`, `DrawFPS`, плюс полный набор состояния/размеров/мониторов/буфера обмена окна из `task1.md` (`IsWindow*`, `Set/ClearWindowState`, `Toggle*`, `Maximize/Minimize/RestoreWindow`, `SetWindowTitle/Icon(s)/Position/Monitor/Min/Max/Size/Opacity/Focused`, `Get*Screen/Render/Monitor*`, `GetWindowPosition/ScaleDPI`, `Get/SetClipboardText`, `Enable/DisableEventWaiting`)
- **rcore / курсор**: `ShowCursor`, `HideCursor`, `IsCursorHidden`
- **rcore / тайминг и misc**: `GetFrameTime`, `GetTime`, `GetFPS`, `WaitTime`, `SetRandomSeed`, `TakeScreenshot`, `SetConfigFlags`, `OpenURL`, `EnableCursor`, `DisableCursor`, `IsCursorOnScreen`
- **rcore / ввод**: клавиатура (`IsKeyPressed/Repeat/Down/Released/Up`, `GetKeyPressed`, `GetCharPressed`, `SetExitKey`), мышь (`IsMouseButton*`, `Get/SetMouse*`), геймпад, касания и жесты из `task3.md` (кроме методов, отсутствующих в raylib-cs 6.0.0)
- **rcore / камера**: `BeginMode2D`/`EndMode2D`, `BeginMode3D`/`EndMode3D`, `UpdateCamera` (3D), собственные конструкторы `NewCamera2D`/`NewCamera3D` + геттеры полей Camera2D, преобразования координат 2D/3D, экранный луч и матрицы камеры
- **rcore / режимы и шейдеры**: render texture, `Begin/EndTextureMode`, `Begin/EndShaderMode`, `Begin/EndBlendMode`, `Begin/EndScissorMode`, загрузка шейдеров, uniform float/int/Vector2/Vector3/float[]/матрица/текстура
- **rcore / random**: `GetRandomValue`
- **rshapes**: `DrawCircle`, `DrawCircleGradient`, `DrawCircleLines`, `DrawEllipse`, `DrawEllipseLines`, `DrawRectangle`, `DrawRectangleLines`, `DrawRectangleGradientV/H`, `DrawRectanglePro`, `DrawTriangle`, `DrawTriangleLines`, `DrawLine`, `DrawLineStrip`, `DrawTriangleFan`, `DrawTriangleStrip`, `DrawPoly`, `DrawPolyLines`, `DrawPolyLinesEx`, `CheckCollisionRecs`, `CheckCollisionCircles`
- **rtextures**: `LoadTexture`, `UnloadTexture`, `DrawTexture`, `DrawTextureEx`, `DrawTextureRec`, `DrawTexturePro`, render texture, `LoadImage`, `IsImageReady`, генераторы `GenImageGradientLinear/Radial/Square`, `GenImageChecked`, `GenImageWhiteNoise`, `GenImagePerlinNoise`, `GenImageCellular`, `LoadTextureFromImage`, `UnloadImage`
- **rtext**: только `DrawText`, `DrawFPS`
- **rmodels**: `LoadModel`, `UnloadModel`, `DrawModel`, `GetModelBoundingBox`, `DrawBoundingBox`, `DrawGrid`, `DrawCube`, `DrawCubeWires`, `SetModelTexture` (кастомный helper, не из raylib)
- **raudio**: ничего

## Задачи на реализацию недостающего

Каждый файл описывает направление, **не одну задачу для одного PR**. Для первого небольшого изменения выбирайте шаг из последнего столбца; остальные методы реализуйте отдельными порциями по потребности.

| Файл | Направление | Приоритет | Первый небольшой шаг |
|---|---|---|---|
| [task1.md](task1.md) | Окно и мониторы | Завершено | Архив реализованного; `GetClipboardImage` отсутствует в биндинге |
| [task2.md](task2.md) | Тайминг, курсор | Основное завершено | `LoadRandomSequence`/`UnloadRandomSequence` — по потребности |
| [task3.md](task3.md) | Ввод | Основное завершено | `GetKeyName`/`SetGamepadVibration` отсутствуют в биндинге; проверить на физических устройствах |
| [task4.md](task4.md) | Экранные координаты, рендер, шейдеры | Основное завершено | `GetScreenToWorldRayEx` отсутствует в биндинге |
| [task5.md](task5.md) | Примитивы и коллизии 2D | Высокий, начато | Следующие коллизии: `CheckCollisionCircleRec` и `CheckCollisionPointRec` |
| [task6.md](task6.md) | Изображения (CPU) | Средний, начато | `ExportImage` и `GenImageColor` |
| [task7.md](task7.md) | Текстуры и цвет | Средний | `Fade` и `ColorAlpha` |
| [task8.md](task8.md) | Шрифты и текст | Средний | `MeasureText` для стандартного шрифта |
| [task9.md](task9.md) | 3D-примитивы | Средний | `DrawLine3D`, `DrawSphere` |
| [task10.md](task10.md) | Меши, материалы, анимации | Низкий | Пара простых 3D-коллизий без новых типов |
| [task11.md](task11.md) | Аудио | По потребности | Устройство + короткий `Sound` |
| [task12.md](task12.md) | Файлы, automation | По потребности | Перетаскивание файлов в окно |

## Общие правила для всех задач

См. [`AGENTS.md`](../AGENTS.md) → «Добавление метода». Готовность одного шага:

1. Проверены наличие метода в `Raylibos.cs` и сигнатура в используемой версии Raylib-cs; записи C API в карточках могут относиться к более новой версии и не обязаны компилироваться дословно.
2. Добавлены русское имя и английский alias, преобразование аргументов/результата и тематический пример в `src/test*.os` с освобождением выделенных ресурсов.
3. Обновлены соответствующие разделы `README.md` и статус этой карточки; выполнены `dotnet build` и `oscript -check` по [`AGENTS.md`](../AGENTS.md). Для оконных методов по возможности проверено поведение в GUI, иначе это явно отмечено.

`Image` оборачивается через `ImageWrapper : AutoContext<ImageWrapper>`, а не `COMWrapperContext.Create()` — держать эту асимметрию в уме при работе с `task6.md`.
