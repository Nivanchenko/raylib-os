# Task 4 — rcore: экранные координаты, режимы рендера, шейдеры

**Модуль:** rcore (Screen-space / Drawing modes / Shader management)
**Приоритет:** средний — нужно для продвинутых сценариев (кастомный рендер в текстуру, шейдеры, перевод координат экран↔мир для 3D и 2D-камеры).
**Статус:** реализовано только `BeginMode2D`/`EndMode2D`, `BeginMode3D`/`EndMode3D`. Всё остальное — нет.

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
- **Первый шаг:** `GetScreenToWorld2D`/`GetWorldToScreen2D` с существующими `Camera2D` и `Vector2`; показать перевод координат мыши в `src/test2dcamera.os`. Не нужны новые ресурсы или типы.
- `RenderTexture2D` — новый marshalable-тип, потребуется `IValueToRenderTexture2D` и обёртки `LoadRenderTexture`/`UnloadRenderTexture` из `task7.md`. Реализовывать с `BeginTextureMode`/`EndTextureMode` одним отдельным шагом.
- `SetShaderValue`/`SetShaderValueV` принимают `const void *value` — на стороне OneScript придётся ограничиться конкретными перегрузками под типы (float, int, Vector2/3/4, Color) вместо универсального `void*`. Реализовать хотя бы `УстановитьЗначениеШейдераFloat`/`...Int`/`...Vector2` — не пытаться сделать единый метод под все `ShaderUniformDataType`.
- Для 3D-камеры есть `IValueToCamera3D`; сигнатуры `GetScreenToWorldRay` и других функций проверить по пакету, не выводить тип аргумента из C-имени `Camera`.

## Тестовый скрипт
`src/testShaderMode.os` — загрузка и применение шейдера; файла шейдера в `resources/` пока нет, потребуется добавить подходящий пример или использовать `LoadShaderFromMemory`.
`src/testRenderTexture.os` — рендер сцены в `RenderTexture2D` через `BeginTextureMode`, затем отрисовка результата как обычной текстуры.
