# Task 6 — rtextures: работа с Image (CPU)

**Модуль:** rtextures (Image loading / generation / manipulation / drawing-on-image)
**Приоритет:** средний — генераторы шума/градиентов уже есть, но загрузка изображений из файла и любое редактирование пикселей отсутствует.
**Статус:** реализованы только генераторы (`GenImageGradient*`, `GenImageChecked`, `GenImageWhiteNoise`, `GenImagePerlinNoise`, `GenImageCellular`), `LoadTextureFromImage`, `UnloadImage`.

## Что реализовать

### Загрузка/экспорт (высокий приоритет внутри этой задачи)
```c
Image LoadImage(const char *fileName);
Image LoadImageFromMemory(const char *fileType, const unsigned char *fileData, int dataSize); // низкий приоритет — нужен доступ к байтам из OneScript
Image LoadImageFromTexture(Texture2D texture);
Image LoadImageFromScreen(void);
bool IsImageValid(Image image);
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
- Все `Image *image` — функции-мутаторы. `ImageWrapper.Image` — публичное поле, так что можно писать `imgWrapper.Image = Raylib.ImageCopy(imgWrapper.Image)` либо мутировать через `ref`, если raylib-cs поддерживает — проверить сигнатуру в Raylib-cs.dll (там методы принимают `ref Image`).
- `LoadImage`/`GenImageColor`/`GenImageText`/`ImageCopy`/`ImageFromImage` возвращают новый `Image` — оборачивай в `ImageWrapper`, как уже сделано в `GenImageGradientLinear` и т.д.
- `IsImageValid` полезен сразу после `LoadImage`, чтобы явно проверять неудачную загрузку файла (сейчас никакой метод не проверяет успешность загрузки — стоит добавить проверку и в существующий `LoadTexture`, если найдётся аналог `IsTextureValid`, см. `task7.md`).

## Тестовый скрипт
`src/testImageManipulation.os` — `LoadImage` → `ImageResize`/`ImageRotate`/`ImageColorGrayscale` → `LoadTextureFromImage` → отрисовка результата.
