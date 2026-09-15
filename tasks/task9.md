# Task 9 — rmodels: недостающие 3D-примитивы

**Модуль:** rmodels (Basic geometric 3D shapes + Model drawing extended)
**Приоритет:** средний — куб/линия сетки уже есть, но сферы/цилиндры/капсулы/биллборды и расширенные варианты `DrawModel*` отсутствуют.
**Статус:** реализовано `DrawCube`, `DrawCubeWires`, `DrawGrid`, `DrawModel`, `DrawBoundingBox`.

## Что реализовать

### Базовые 3D-примитивы
```c
void DrawLine3D(Vector3 startPos, Vector3 endPos, Color color);
void DrawPoint3D(Vector3 position, Color color);
void DrawCircle3D(Vector3 center, float radius, Vector3 rotationAxis, float rotationAngle, Color color);
void DrawTriangle3D(Vector3 v1, Vector3 v2, Vector3 v3, Color color);
void DrawTriangleStrip3D(const Vector3 *points, int pointCount, Color color);
void DrawCubeV(Vector3 position, Vector3 size, Color color);
void DrawCubeWiresV(Vector3 position, Vector3 size, Color color);
void DrawSphere(Vector3 centerPos, float radius, Color color);
void DrawSphereEx(Vector3 centerPos, float radius, int rings, int slices, Color color);
void DrawSphereWires(Vector3 centerPos, float radius, int rings, int slices, Color color);
void DrawCylinder(Vector3 position, float radiusTop, float radiusBottom, float height, int slices, Color color);
void DrawCylinderEx(Vector3 startPos, Vector3 endPos, float startRadius, float endRadius, int sides, Color color);
void DrawCylinderWires(Vector3 position, float radiusTop, float radiusBottom, float height, int slices, Color color);
void DrawCylinderWiresEx(Vector3 startPos, Vector3 endPos, float startRadius, float endRadius, int sides, Color color);
void DrawCapsule(Vector3 startPos, Vector3 endPos, float radius, int slices, int rings, Color color);
void DrawCapsuleWires(Vector3 startPos, Vector3 endPos, float radius, int slices, int rings, Color color);
void DrawPlane(Vector3 centerPos, Vector2 size, Color color);
void DrawRay(Ray ray, Color color);
```

### Расширенные варианты рисования модели
```c
void DrawModelEx(Model model, Vector3 position, Vector3 rotationAxis, float rotationAngle, Vector3 scale, Color tint);
void DrawModelWires(Model model, Vector3 position, float scale, Color tint);
void DrawModelWiresEx(Model model, Vector3 position, Vector3 rotationAxis, float rotationAngle, Vector3 scale, Color tint);
void DrawBillboard(Camera camera, Texture2D texture, Vector3 position, float scale, Color tint);
void DrawBillboardRec(Camera camera, Texture2D texture, Rectangle source, Vector3 position, Vector2 size, Color tint);
void DrawBillboardPro(Camera camera, Texture2D texture, Rectangle source, Vector3 position, Vector3 up, Vector2 size, Vector2 origin, float rotation, Color tint);
```

## Заметки по реализации
- `DrawModel` сейчас принимает только uniform `scale: float` — `DrawModelEx` даёт полный контроль (позиция/ось поворота/угол/неравномерный масштаб по осям) и логично реализовывать сразу после текущего `DrawModel`.
- `Ray` — новый marshalable-тип (`Vector3 position, Vector3 direction`), нужен `NewRay`/`IValueToRay` — пригодится и для `task4.md` (`GetScreenToWorldRay`) и `task10.md` (raycast-коллизии), стоит сделать один раз и переиспользовать.
- Билборды — часто используются для спрайтов в 3D-сценах (частицы, HP-бары над персонажами) — не самый экзотический кейс, приоритет чуть выше остальных в этой задаче.

## Тестовый скрипт
`src/test3dPrimitives.os` — сфера, цилиндр, капсула и плоскость на одной сцене с орбитальной камерой (расширение существующего `test3d.os`).
