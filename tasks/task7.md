# Task 7 — rtextures: Texture (GPU), NPatch, утилиты Color

**Модуль:** rtextures (Texture loading/configuration/drawing) + Color/pixel functions
**Приоритет:** средний.
**Статус:** реализованы базовые `LoadTexture`/`UnloadTexture`/`DrawTexture(Ex/Rec/Pro)`. Ни одна `Color*`-утилита, `RenderTexture2D`, фильтры и `NPatch` не реализованы.

## Что реализовать

### Текстуры (GPU)
```c
TextureCubemap LoadTextureCubemap(Image image, int layout);
RenderTexture2D LoadRenderTexture(int width, int height);   // см. также task4.md (BeginTextureMode)
bool IsTextureValid(Texture2D texture);
bool IsRenderTextureValid(RenderTexture2D target);
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
bool ColorIsEqual(Color col1, Color col2);
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
Color ColorLerp(Color color1, Color color2, float factor);
Color GetColor(unsigned int hexValue);
```

## Заметки по реализации
- `Fade`/`ColorAlpha`/`ColorLerp` — самые частые в реальных играх (мигание, затухание, интерполяция цвета при уроне и т.д.), сделать их первыми.
- `RenderTexture2D` требует нового `IValueToRenderTexture2D` хелпера — координировать с `task4.md` (`BeginTextureMode`/`EndTextureMode` используют этот же тип), реализовывать вместе одним PR.
- `NPatchInfo` — новый marshalable-тип (`NPatchLayout` enum + `Rectangle` source), пригодится для UI (масштабируемые рамки/кнопки без искажений) — но можно отложить, если UI не в приоритете проекта.
- `SetTextureFilter`/`SetTextureWrap` принимают enum (`TextureFilter`, `TextureWrap` в raylib-cs) — принимать как `int` и приводить типом, как уже сделано для `CameraMode`/`CameraProjection`.

## Тестовый скрипт
`src/testColorUtils.os` — прямоугольник, цвет которого плавно интерполируется между двумя цветами через `ColorLerp` по кадрам.
`src/testRenderTexture.os` — совместно с `task4.md`.
