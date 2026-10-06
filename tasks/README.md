# План расширения raylib API

`basic_api.md` — справочник по C API raylib, а `src/raylibos/Raylibos.cs` — фактически опубликованные методы OneScript. Списки ниже — ориентиры, а не синхронизируемая автоматически спецификация. Перед выбором метода проверяйте его наличие в `Raylibos.cs` и сигнатуру в установленном пакете Raylib-cs из `src/raylibos/raylibos.csproj` (XML-документация при стандартном расположении NuGet-пакетов: `~/.nuget/packages/raylib-cs/<версия>/lib/net6.0/Raylib-cs.xml`; окончательная проверка — сборка).

## Итог

Покрыты базовое 2D-рисование, управление окном/мониторами, ввод (включая опрос геймпада/тача/жестов), основные коллизии, часть 3D, базовый звук, drag-and-drop и стандартный текст. Пользовательские шрифты, обработка Image, большинство мешей/материалов/анимаций, потоковая музыка и низкоуровневый ввод-вывод остаются отдельными направлениями. Точное число методов смотрите в `Raylibos.cs`: здесь намеренно нет процента покрытия, поскольку C API и биндинг разных версий не совпадают один к одному.

## Что уже реализовано (модуль → функции)

- **rcore / окно**: `InitWindow`, `WindowShouldClose`, `CloseWindow`, `BeginDrawing`, `EndDrawing`, `ClearBackground`, `SetTargetFPS`, `DrawFPS`, плюс полный набор состояния/размеров/мониторов/буфера обмена окна из `task1.md` (`IsWindow*`, `Set/ClearWindowState`, `Toggle*`, `Maximize/Minimize/RestoreWindow`, `SetWindowTitle/Icon(s)/Position/Monitor/Min/Max/Size/Opacity/Focused`, `Get*Screen/Render/Monitor*`, `GetWindowPosition/ScaleDPI`, `Get/SetClipboardText`, `Enable/DisableEventWaiting`)
- **rcore / курсор**: `ShowCursor`, `HideCursor`, `IsCursorHidden`
- **rcore / тайминг и misc**: `GetFrameTime`, `GetTime`, `GetFPS`, `WaitTime`, `SetRandomSeed`, `TakeScreenshot`, `SetConfigFlags`, `OpenURL`, `EnableCursor`, `DisableCursor`, `IsCursorOnScreen`
- **rcore / ввод**: клавиатура (`IsKeyPressed/Repeat/Down/Released/Up`, `GetKeyPressed`, `GetCharPressed`, `SetExitKey`), мышь (`IsMouseButton*`, `Get/SetMouse*`), геймпад, касания и жесты из `task3.md` (кроме методов, отсутствующих в raylib-cs 6.0.0)
- **rcore / файлы**: `IsFileDropped` и `GetDroppedFiles` с копированием путей в массив OneScript
- **rcore / камера**: `BeginMode2D`/`EndMode2D`, `BeginMode3D`/`EndMode3D`, `UpdateCamera` (3D), собственные конструкторы `NewCamera2D`/`NewCamera3D` + геттеры полей Camera2D, преобразования координат 2D/3D, экранный луч и матрицы камеры
- **rcore / режимы и шейдеры**: render texture, `Begin/EndTextureMode`, `Begin/EndShaderMode`, `Begin/EndBlendMode`, `Begin/EndScissorMode`, загрузка шейдеров, uniform float/int/Vector2/Vector3/float[]/матрица/текстура
- **rcore / random**: `GetRandomValue`
- **rshapes**: `DrawCircle`, `DrawCircleGradient`, `DrawCircleLines`, `DrawEllipse`, `DrawEllipseLines`, `DrawRectangle`, `DrawRectangleLines`, `DrawRectangleGradientV/H`, `DrawRectanglePro`, `DrawTriangle`, `DrawTriangleLines`, `DrawLine`, `DrawLineStrip`, `DrawTriangleFan`, `DrawTriangleStrip`, `DrawPoly`, `DrawPolyLines`, `DrawPolyLinesEx`, `CheckCollisionRecs`, `CheckCollisionCircles`
- **rtextures**: `LoadTexture`, `UnloadTexture`, `DrawTexture`, `DrawTextureEx`, `DrawTextureRec`, `DrawTexturePro`, render texture, `LoadImage`, `IsImageReady`, `Fade`, `ColorAlpha`, генераторы `GenImageGradientLinear/Radial/Square`, `GenImageChecked`, `GenImageWhiteNoise`, `GenImagePerlinNoise`, `GenImageCellular`, `LoadTextureFromImage`, `UnloadImage`
- **rtext**: `DrawText`, `DrawFPS`, `MeasureText` для стандартного шрифта
- **rmodels**: `LoadModel`, `UnloadModel`, `DrawModel`, `GetModelBoundingBox`, `DrawBoundingBox`, `DrawGrid`, `DrawCube`, `DrawCubeWires`, `DrawLine3D`, `DrawSphere`, `CheckCollisionSpheres`, `CheckCollisionBoxes`, `NewBoundingBox`, `SetModelTexture` (кастомный helper, не из raylib)
- **raudio**: инициализация и закрытие устройства, проверка готовности, загрузка/проверка/воспроизведение/выгрузка `Sound`

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
| [task7.md](task7.md) | Текстуры и цвет | Средний, начато | `IsTextureReady`, `SetTextureFilter`/`SetTextureWrap` |
| [task8.md](task8.md) | Шрифты и текст | Средний, начато | `GetFontDefault` и `MeasureTextEx` |
| [task9.md](task9.md) | 3D-примитивы | Средний, начато | `DrawSphereWires`, `DrawCylinder` |
| [task10.md](task10.md) | Меши, материалы, анимации | Низкий, начато | `CheckCollisionBoxSphere` или генерация мешей при необходимости |
| [task11.md](task11.md) | Аудио | По потребности, начато | Настройки `Sound` и потоковая музыка — отдельно |
| [task12.md](task12.md) | Файлы, automation | Основной шаг сделан | Проверить на реальном drag&drop; прочее — только по запросу |
| [task13.md](task13.md) | Кадр, камера Pro, VR | Низкий | `UpdateCameraPro` без VR и ручного кадра |
| [task14.md](task14.md) | Текстура фигур, точки/сегменты сплайнов | Низкий | `GetSplinePointLinear` с проверкой крайних точек |
| [task15.md](task15.md) | Расширенные Image и пиксельные буферы | Средний/низкий | `ImageAlphaCrop` на копии Image |
| [task16.md](task16.md) | Unicode, глифы, атласы шрифтов | Средний/низкий | `GetCodepointCount` на ASCII и кириллице |
| [task17.md](task17.md) | Бинарные файлы, каталоги, callbacks | Только по запросу | `LoadFileData` с копированием и выгрузкой |

Новые карточки 13–17 покрывают направления `basic_api.md`, не выделенные в задачах 1–12; пропуски внутри существующих направлений дополнены в соответствующих карточках (продвинутые меши — task10, аудио — task11). Это **не** гарантия, что каждая C-функция есть в raylib-cs 6.0.0: некоторые сигнатуры относятся к более новой версии C API. Перед реализацией сверяйте наличие и параметры с установленным NuGet-пакетом и нативной библиотекой. Для проверок, требующих человека/устройств, см. [чеклист ручной проверки](manual-checklist.md).

## Отдельное технодемо на текущем API

[Minecraft-подобная voxel-песочница](minecraft/README.md) — ТЗ, поэтапный план, инструкции следующему агенту и ручная приёмка. Это отдельный пример, не задача расширения API; этапы 1–6 реализованы (сетка, renderer, игрок, DDA, редактирование, материалы и профили скорости). Полная ручная приёмка ожидает выполнения.

## Общие правила для задач расширения API

См. [`AGENTS.md`](../AGENTS.md) → «Добавление метода». Готовность одного шага:

1. Проверены наличие метода в `Raylibos.cs` и сигнатура в используемой версии Raylib-cs; записи C API в карточках могут относиться к более новой версии и не обязаны компилироваться дословно.
2. Добавлены русское имя и английский alias, преобразование аргументов/результата и тематический пример в `src/test*.os` с освобождением выделенных ресурсов.
3. Обновлены соответствующие разделы `README.md` и статус этой карточки; выполнены `dotnet build` и `oscript -check` по [`AGENTS.md`](../AGENTS.md). Для оконных методов по возможности проверено поведение в GUI, иначе это явно отмечено.

`Image` оборачивается через `ImageWrapper : AutoContext<ImageWrapper>`, а не `COMWrapperContext.Create()` — держать эту асимметрию в уме при работе с `task6.md`.
