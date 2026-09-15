# Task 5 — rshapes: недостающие примитивы, сплайны, коллизии 2D

**Модуль:** rshapes
**Приоритет:** высокий — коллизии особенно важны (сейчас `examples/physics` реализует их вручную в OneScript в `ПомошникКолизий.os`, хотя raylib уже предоставляет готовые функции).
**Статус:** базовые фигуры (круг/эллипс/прямоугольник/треугольник/линия/полигон + их варианты Lines/Gradient/Pro) реализованы. Не реализованы: варианты с `Vector2` (`*V`), пиксели, дуги/кольца, скруглённые прямоугольники, сплайны, вся секция коллизий.

## Что реализовать

### Пиксели и линии
```c
void DrawPixel(int posX, int posY, Color color);
void DrawPixelV(Vector2 position, Color color);
void DrawLineV(Vector2 startPos, Vector2 endPos, Color color);
void DrawLineEx(Vector2 startPos, Vector2 endPos, float thick, Color color);
void DrawLineBezier(Vector2 startPos, Vector2 endPos, float thick, Color color);
void DrawLineDashed(Vector2 startPos, Vector2 endPos, int dashSize, int spaceSize, Color color);
```

### Круги, эллипсы, кольца
```c
void DrawCircleV(Vector2 center, float radius, Color color);
void DrawCircleSector(Vector2 center, float radius, float startAngle, float endAngle, int segments, Color color);
void DrawCircleSectorLines(Vector2 center, float radius, float startAngle, float endAngle, int segments, Color color);
void DrawCircleLinesV(Vector2 center, float radius, Color color);
void DrawEllipseV(Vector2 center, float radiusH, float radiusV, Color color);
void DrawEllipseLinesV(Vector2 center, float radiusH, float radiusV, Color color);
void DrawRing(Vector2 center, float innerRadius, float outerRadius, float startAngle, float endAngle, int segments, Color color);
void DrawRingLines(Vector2 center, float innerRadius, float outerRadius, float startAngle, float endAngle, int segments, Color color);
```

### Прямоугольники
```c
void DrawRectangleV(Vector2 position, Vector2 size, Color color);
void DrawRectangleRec(Rectangle rec, Color color);
void DrawRectangleGradientEx(Rectangle rec, Color topLeft, Color bottomLeft, Color bottomRight, Color topRight);
void DrawRectangleLinesEx(Rectangle rec, float lineThick, Color color);
void DrawRectangleRounded(Rectangle rec, float roundness, int segments, Color color);
void DrawRectangleRoundedLines(Rectangle rec, float roundness, int segments, Color color);
void DrawRectangleRoundedLinesEx(Rectangle rec, float roundness, int segments, float lineThick, Color color);
```

### Сплайны (рисование + оценка точки)
```c
void DrawSplineLinear(const Vector2 *points, int pointCount, float thick, Color color);
void DrawSplineBasis(const Vector2 *points, int pointCount, float thick, Color color);
void DrawSplineCatmullRom(const Vector2 *points, int pointCount, float thick, Color color);
void DrawSplineBezierQuadratic(const Vector2 *points, int pointCount, float thick, Color color);
void DrawSplineBezierCubic(const Vector2 *points, int pointCount, float thick, Color color);
// Get*SplinePoint* — низкий приоритет, добавлять только по запросу
```

### Коллизии (важно для игр)
```c
bool CheckCollisionRecs(Rectangle rec1, Rectangle rec2);
bool CheckCollisionCircles(Vector2 center1, float radius1, Vector2 center2, float radius2);
bool CheckCollisionCircleRec(Vector2 center, float radius, Rectangle rec);
bool CheckCollisionCircleLine(Vector2 center, float radius, Vector2 p1, Vector2 p2);
bool CheckCollisionPointRec(Vector2 point, Rectangle rec);
bool CheckCollisionPointCircle(Vector2 point, Vector2 center, float radius);
bool CheckCollisionPointTriangle(Vector2 point, Vector2 p1, Vector2 p2, Vector2 p3);
bool CheckCollisionPointLine(Vector2 point, Vector2 p1, Vector2 p2, int threshold);
bool CheckCollisionPointPoly(Vector2 point, const Vector2 *points, int pointCount);
bool CheckCollisionLines(Vector2 startPos1, Vector2 endPos1, Vector2 startPos2, Vector2 endPos2, Vector2 *collisionPoint);
Rectangle GetCollisionRec(Rectangle rec1, Rectangle rec2);
```

## Заметки по реализации
- Точки для сплайнов/`CheckCollisionPointPoly` передаются как массивы `Vector2` — переиспользуй уже готовый паттерн маршалинга массива из `DrawLineStrip`/`DrawTriangleFan`/`DrawTriangleStrip` (`COMWrapperContext.MarshalIValue` → `dynamic array` → `fixed (Vector2* ptr = ...)`).
- `CheckCollisionLines` — `Vector2 *collisionPoint` возвращается по ссылке; в OneScript сделать это через возврат структуры (например, `IValue` с массивом из `[найдено, точка]` или отдельными методами `ЛинииПересекаются`/`ТочкаПересеченияЛиний`).
- Коллизии стоит сделать одним из первых приоритетов в этой задаче — после их появления `examples/physics/Модули/ПомошникКолизий.os` можно упростить, используя нативные `CheckCollision*` вместо ручной геометрии.

## Тестовый скрипт
`src/testCollisions.os` — два круга/прямоугольника, при пересечении меняют цвет (визуальная проверка `CheckCollisionCircles`/`CheckCollisionRecs`).
`src/testRoundedRect.os` — сетка прямоугольников с разным `roundness`.
