# Task 4 — rcore: экранные координаты, режимы рендера, шейдеры

**Модуль:** rcore (Screen-space / Drawing modes / Shader management)
**Приоритет:** средний — нужно для продвинутых сценариев (кастомный рендер в текстуру, шейдеры, перевод координат экран↔мир для 3D и 2D-камеры).
**Статус:** реализованы преобразования координат 2D/3D, экранный луч, матрицы камеры, render texture, режимы blend/scissor/texture/shader и основные операции с шейдерами. `GetScreenToWorldRayEx` отсутствует в используемой версии raylib-cs 6.0.0.

## Что реализовать

### Экран ↔ мир
```c
Ray GetScreenToWorldRay(Vector2 position, Camera camera);
Ray GetScreenToWorldRayEx(Vector2 position, Camera camera, int width, int height);
Vector2 GetWorldToScreen(Vector3 position, Camera camera);
Vector2 GetWorldToScreenEx(Vector3 position, Camera camera, int width, int height);
Vector2 GetWorldToScreen2D(Vector2 position, Camera2D camera);
Vector2 GetScreenToWorld2D(Vector2 position, Camera2D camera);
Matrix GetCameraMatrix(Camera camera);
Matrix GetCameraMatrix2D(Camera2D camera);
```

### Режимы рендера
```c
void BeginTextureMode(RenderTexture2D target);
void EndTextureMode(void);
void BeginShaderMode(Shader shader);
void EndShaderMode(void);
void BeginBlendMode(int mode);
void EndBlendMode(void);
void BeginScissorMode(int x, int y, int width, int height);
void EndScissorMode(void);
```

### Шейдеры
```c
Shader LoadShader(const char *vsFileName, const char *fsFileName);
Shader LoadShaderFromMemory(const char *vsCode, const char *fsCode);
bool IsShaderValid(Shader shader);
int GetShaderLocation(Shader shader, const char *uniformName);
int GetShaderLocationAttrib(Shader shader, const char *attribName);
void SetShaderValue(Shader shader, int locIndex, const void *value, int uniformType);
void SetShaderValueV(Shader shader, int locIndex, const void *value, int uniformType, int count);
void SetShaderValueMatrix(Shader shader, int locIndex, Matrix mat);
void SetShaderValueTexture(Shader shader, int locIndex, Texture2D texture);
void UnloadShader(Shader shader);
```

## Заметки по реализации
- `GetScreenToWorld2D`/`GetWorldToScreen2D` используют существующие `Camera2D` и `Vector2`; проверка координат есть в `src/testCameraCoords.os`.
- `RenderTexture2D` передаётся через `IValueToRenderTexture2D`; `LoadRenderTexture`/`UnloadRenderTexture` и `BeginTextureMode`/`EndTextureMode` реализованы совместно (см. также `task7.md`).
- `SetShaderValue`/`SetShaderValueV` принимают `const void *value` в C API; из OneScript доступны типизированные методы для float, int, Vector2, Vector3, массива float, матрицы и текстуры вместо универсального `void*`.
- Для 3D-камеры есть `IValueToCamera3D`; сигнатуры `GetScreenToWorldRay` и других функций проверить по пакету, не выводить тип аргумента из C-имени `Camera`.
- Для экранного луча биндинг использует имя `GetMouseRay`, для проверок готовности — `IsShaderReady`/`IsRenderTextureReady`. Поставляемая нативная raylib 5.5 экспортирует новые имена (`GetScreenToWorldRay`, `IsShaderValid`, `IsRenderTextureValid`); обёртка при отсутствии старого символа вызывает новый. Все методы имеют русское имя и английский alias.

## Тестовый скрипт
`src/testShaderMode.os` — загрузка и применение шейдера; файла шейдера в `resources/` пока нет, потребуется добавить подходящий пример или использовать `LoadShaderFromMemory`.
`src/testRenderTexture.os` — рендер сцены в `RenderTexture2D` через `BeginTextureMode`, затем отрисовка результата как обычной текстуры.
`src/testCameraCoords.os` — преобразования координат и луч через 3D-камеру.
