# Task 6 — rtextures: работа с Image (CPU)

**Модуль:** rtextures (Image loading / generation / manipulation / drawing-on-image)
**Приоритет:** средний — генераторы шума/градиентов уже есть, но загрузка изображений из файла и любое редактирование пикселей отсутствует.
**Статус:** реализованы `LoadImage`, `IsImageReady`, генераторы (`GenImageGradient*`, `GenImageChecked`, `GenImageWhiteNoise`, `GenImagePerlinNoise`, `GenImageCellular`), `LoadTextureFromImage`, `UnloadImage`. Экспорт, модификация и рисование на изображении пока не реализованы.

## Что реализовать

### Загрузка/экспорт (высокий приоритет внутри этой задачи)
```c
Image LoadImage(const char *fileName);
Image LoadImageFromMemory(const char *fileType, const unsigned char *fileData, int dataSize); // низкий приоритет — нужен доступ к байтам из OneScript
Image LoadImageFromTexture(Texture2D texture);
Image LoadImageFromScreen(void);
bool IsImageReady(Image image); // имя метода raylib-cs 6.0.0
bool ExportImage(Image image, const char *fileName);
Image GenImageColor(int width, int height, Color color);
Image GenImageText(int width, int height, const char *text);
```

### Модификация (среднее)
```c
Image ImageCopy(Image image);
Image ImageFromImage(Image image, Rectangle rec);
void ImageCrop(Image *image, Rectangle crop);
void ImageResize(Image *image, int newWidth, int newHeight);
void ImageResizeNN(Image *image, int newWidth, int newHeight);
void ImageFlipVertical(Image *image);
void ImageFlipHorizontal(Image *image);
void ImageRotate(Image *image, int degrees);
void ImageRotateCW(Image *image);
void ImageRotateCCW(Image *image);
void ImageColorTint(Image *image, Color color);
void ImageColorInvert(Image *image);
void ImageColorGrayscale(Image *image);
void ImageColorContrast(Image *image, float contrast);
void ImageColorBrightness(Image *image, int brightness);
void ImageColorReplace(Image *image, Color color, Color replace);
Color GetImageColor(Image image, int x, int y);
```

### Рисование на изображении (низкое, по запросу)
```c
void ImageDrawPixel(Image *dst, int posX, int posY, Color color);
void ImageDrawLine(Image *dst, int startPosX, int startPosY, int endPosX, int endPosY, Color color);
void ImageDrawCircle(Image *dst, int centerX, int centerY, int radius, Color color);
void ImageDrawRectangle(Image *dst, int posX, int posY, int width, int height, Color color);
void ImageDraw(Image *dst, Image src, Rectangle srcRec, Rectangle dstRec, Color tint);
void ImageDrawText(Image *dst, const char *text, int posX, int posY, int fontSize, Color color);
```
(остальные `ImageDraw*` — по аналогии, добавлять при конкретной необходимости)

## Заметки по реализации
- **Первый шаг выполнен:** `LoadImage` и `IsImageReady`; `src/testImageLoad.os` проверяет загрузку `resources/raylib_logo.png`, размер 256×256, отсутствие файла и выгрузку через `UnloadImage`. В комплекте с raylib-cs 6.0.0 нативная raylib 5.5 экспортирует `IsImageValid` вместо `IsImageReady` — обёртка поддерживает оба имени. Изменения пикселей и GPU-загрузка — отдельные шаги.
- Все `Image *image` — функции-мутаторы. `ImageWrapper.Image` — свойство; для мутации передать локальную копию `Image` в `ref`-перегрузку raylib-cs и записать результат обратно в свойство. `ImageCopy` создаёт **новое** изображение, его нельзя использовать вместо изменения исходного без освобождения старого ресурса.
- `LoadImage`/`GenImageColor`/`GenImageText`/`ImageCopy`/`ImageFromImage` возвращают новый `Image` — оборачивай в `ImageWrapper`, как уже сделано в `GenImageGradientLinear` и т.д.
- `IsImageReady` проверяет результат загрузки; для текстур аналогично `IsTextureReady` (см. `task7.md`). Перед выгрузкой изображения убедиться, что оно было успешно загружено.

## Тестовый скрипт
Первый шаг: `src/testImageLoad.os` — загрузка, проверка и выгрузка существующего PNG. Отдельный шаг: `src/testImageManipulation.os` — `LoadImage` → `ImageResize`/`ImageRotate`/`ImageColorGrayscale` → `LoadTextureFromImage` → отрисовка и выгрузка изображения/текстуры.
