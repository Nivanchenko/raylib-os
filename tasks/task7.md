# Task 7 — rtextures: Texture (GPU), NPatch, утилиты Color

**Модуль:** rtextures (Texture loading/configuration/drawing) + Color/pixel functions
**Приоритет:** средний.
**Статус:** реализованы базовые `LoadTexture`/`UnloadTexture`/`DrawTexture(Ex/Rec/Pro)`. Ни одна `Color*`-утилита, `RenderTexture2D`, фильтры и `NPatch` не реализованы.

## Что реализовать

### Текстуры (GPU)
```c
TextureCubemap LoadTextureCubemap(Image image, int layout);
RenderTexture2D LoadRenderTexture(int width, int height);   // см. также task4.md (BeginTextureMode)
bool IsTextureReady(Texture2D texture); // имя метода raylib-cs 6.0.0
bool IsRenderTextureReady(RenderTexture2D target); // имя метода raylib-cs 6.0.0
void UnloadRenderTexture(RenderTexture2D target);
void UpdateTexture(Texture2D texture, const void *pixels);      // низкий приоритет — работа с raw pixel buffer из OneScript неудобна
void GenTextureMipmaps(Texture2D *texture);
void SetTextureFilter(Texture2D texture, int filter);
void SetTextureWrap(Texture2D texture, int wrap);
```

### Рисование
```c
void DrawTextureV(Texture2D texture, Vector2 position, Color tint);
void DrawTextureNPatch(Texture2D texture, NPatchInfo nPatchInfo, Rectangle dest, Vector2 origin, float rotation, Color tint);
```

### Color-утилиты (высокий приоритет внутри задачи — используются повсеместно)
```c
Color Fade(Color color, float alpha);
int ColorToInt(Color color);
Vector4 ColorNormalize(Color color);
Color ColorFromNormalized(Vector4 normalized);
Vector3 ColorToHSV(Color color);
Color ColorFromHSV(float hue, float saturation, float value);
Color ColorTint(Color color, Color tint);
Color ColorBrightness(Color color, float factor);
Color ColorContrast(Color color, float contrast);
Color ColorAlpha(Color color, float alpha);
Color ColorAlphaBlend(Color dst, Color src, Color tint);
// ColorIsEqual и ColorLerp из более нового C API отсутствуют в raylib-cs 6.0.0
Color GetColor(unsigned int hexValue);
```

## Заметки по реализации
- **Первый шаг:** `Fade` и `ColorAlpha` с уже существующим типом `Color`; утилиты `Vector3`/`Vector4` и рендер в текстуру — отдельные изменения.
- `RenderTexture2D` требует нового `IValueToRenderTexture2D` хелпера — координировать с `task4.md` (`BeginTextureMode`/`EndTextureMode` используют этот же тип), реализовывать вместе. Потребуется отдельный способ получить цветовую текстуру результата и выгрузить render texture ровно один раз.
- `NPatchInfo` — новый marshalable-тип (`NPatchLayout` enum + `Rectangle` source), пригодится для UI (масштабируемые рамки/кнопки без искажений) — но можно отложить, если UI не в приоритете проекта.
- `SetTextureFilter`/`SetTextureWrap` принимают enum (`TextureFilter`, `TextureWrap` в raylib-cs) — принимать как `int` и приводить типом, как уже сделано для `CameraMode`/`CameraProjection`.

## Тестовый скрипт
`src/testColorUtils.os` — прямоугольник с изменяемой прозрачностью через `Fade`/`ColorAlpha`.
`src/testRenderTexture.os` — совместно с `task4.md`.
