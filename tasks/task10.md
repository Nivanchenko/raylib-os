# Task 10 — rmodels: меши, материалы, анимации, 3D-коллизии

**Модуль:** rmodels (Mesh management / Mesh generation / Materials / Animations / Collision detection)
**Приоритет:** низкий — продвинутые сценарии (процедурная генерация геометрии, скелетная анимация), нужны только если проект пойдёт в сторону полноценных 3D-игр, а не только 2D + простые 3D-сцены.
**Статус:** не реализовано ничего из этого раздела.

## Что реализовать

### Генерация мешей (можно делать независимо от остального, полезно уже сейчас)
```c
Mesh GenMeshPoly(int sides, float radius);
Mesh GenMeshPlane(float width, float length, int resX, int resZ);
Mesh GenMeshCube(float width, float height, float length);
Mesh GenMeshSphere(float radius, int rings, int slices);
Mesh GenMeshCylinder(float radius, float height, int slices);
Mesh GenMeshCone(float radius, float height, int slices);
Mesh GenMeshTorus(float radius, float size, int radSeg, int sides);
Mesh GenMeshHeightmap(Image heightmap, Vector3 size);
```

### Меши и модель из меша
```c
Model LoadModelFromMesh(Mesh mesh);
bool IsModelValid(Model model);
void UnloadMesh(Mesh mesh);
void DrawMesh(Mesh mesh, Material material, Matrix transform);
BoundingBox GetMeshBoundingBox(Mesh mesh);
```

### Материалы
```c
Material LoadMaterialDefault(void);
bool IsMaterialValid(Material material);
void UnloadMaterial(Material material);
void SetMaterialTexture(Material *material, int mapType, Texture2D texture);
void SetModelMeshMaterial(Model *model, int meshId, int materialId);
```

### Анимации
```c
ModelAnimation *LoadModelAnimations(const char *fileName, int *animCount);
void UpdateModelAnimation(Model model, ModelAnimation anim, float frame);
void UnloadModelAnimations(ModelAnimation *animations, int animCount);
bool IsModelAnimationValid(Model model, ModelAnimation anim);
```

### 3D-коллизии
```c
bool CheckCollisionSpheres(Vector3 center1, float radius1, Vector3 center2, float radius2);
bool CheckCollisionBoxes(BoundingBox box1, BoundingBox box2);
bool CheckCollisionBoxSphere(BoundingBox box, Vector3 center, float radius);
RayCollision GetRayCollisionSphere(Ray ray, Vector3 center, float radius);
RayCollision GetRayCollisionBox(Ray ray, BoundingBox box);
RayCollision GetRayCollisionMesh(Ray ray, Mesh mesh, Matrix transform);
RayCollision GetRayCollisionTriangle(Ray ray, Vector3 p1, Vector3 p2, Vector3 p3);
```

## Заметки по реализации
- `LoadModelAnimations` возвращает массив (`ModelAnimation *` + count через out-параметр) — на стороне OneScript вернуть `Массив` из враппер-объектов, аналогично тому, как `Физика.Объекты` — массив объектов в `examples/physics`.
- 3D-коллизии переиспользуют `Ray`/`RayCollision` типы из `task9.md` — делать после него.
- Это самая нишевая и трудоёмкая часть API (полноценная поддержка skeletal animation) — приступать в последнюю очередь, только по явному запросу.

## Тестовый скрипт
`src/testMeshGen.os` — процедурная генерация сферы/тора через `GenMeshSphere`/`GenMeshTorus`, отрисовка через `LoadModelFromMesh`+`DrawModel`.
