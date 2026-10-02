# Task 14 — rshapes: текстура фигур и вычисление сплайнов

**Статус:** перечисленные методы отсутствуют в `Raylibos.cs`. Рисование сплайнов и базовые коллизии уже учтены в [task5.md](task5.md).
**Приоритет:** низкий/по потребности.

## Текстура фигур

- `SetShapesTexture(Texture2D, Rectangle)` есть в raylib-cs 6.0.0. Текстура должна жить до прекращения её использования фигурами; не выгружать её во время рендера. Продумать восстановление исходной текстуры при завершении демо.
- `GetShapesTexture` и `GetShapesTextureRectangle` указаны в `basic_api.md`, но в биндинге 6.0.0 не найдены: не планировать обёртку без проверки новой версии.

## Вычисление точек и рисование отдельных сегментов

- `GetSplinePointLinear`, `GetSplinePointBasis`, `GetSplinePointCatmullRom`, `GetSplinePointBezierQuad`, `GetSplinePointBezierCubic` возвращают `Vector2`; принимать существующие обёртки точек и параметр `t` от 0 до 1.
- `DrawSplineSegmentLinear`, `DrawSplineSegmentBasis`, `DrawSplineSegmentCatmullRom`, `DrawSplineSegmentBezierQuadratic`, `DrawSplineSegmentBezierCubic` работают с `Vector2`, толщиной и `Color`. Многоточечные `DrawSpline*` уже запланированы в `task5.md`.
- Для вычисления точки проверить концы и середину на простом отрезке; для рисования — оконное демо с контрольными точками. Не делать вывод о точности по одному лишь успешному `dotnet build`.

**Первый небольшой шаг:** `GetSplinePointLinear` с проверкой значений при `t=0`, `0.5`, `1`, затем отдельный шаг — `DrawSplineSegmentLinear`.
