# Task 15 — rtextures: расширенные Image, пиксельные буферы и GPU-обновление

**Статус:** перечисленные операции отсутствуют в `Raylibos.cs`. Базовая загрузка/модификация Image — [task6.md](task6.md), базовые текстуры и Color — [task7.md](task7.md).
**Приоритет:** средний для работы с альфой, низкий для raw-буферов.

## Расширенная обработка Image

- В биндинге 6.0.0 есть `LoadImageRaw`, `LoadImageAnim`, `ImageText`, `ImageTextEx`, `ExportImageAsCode`, `ImageFormat`, `ImageToPOT`, `ImageAlphaCrop`, `ImageAlphaClear`, `ImageAlphaMask`, `ImageAlphaPremultiply`, `ImageBlurGaussian`, `ImageResizeCanvas`, `ImageMipmaps`, `ImageDither`, `GetImageAlphaBorder`. Дополнительные методы рисования: `ImageClearBackground`, `ImageDrawPixelV`, `ImageDrawLineV`, `ImageDrawCircleV`, `ImageDrawCircleLines`, `ImageDrawCircleLinesV`, `ImageDrawRectangleV`, `ImageDrawRectangleRec`, `ImageDrawRectangleLines`, `ImageDrawTextEx`. Их нет в списке `task6.md`; разбивать на небольшие шаги по необходимости.
- Методы, изменяющие `Image*`, должны обновлять `ImageWrapper.Image` после вызова `ref`; при возврате нового Image создавать отдельный `ImageWrapper`. Освобождать каждый реально выделенный буфер ровно один раз.
- `ImageFromChannel`, `ImageKernelConvolution`, некоторые `ImageDrawTriangle*` из `basic_api.md` не найдены в биндинге 6.0.0 — сначала сверить версию зависимости.

## Пиксельные данные и текстура

- `LoadImageColors`, `LoadImagePalette`, `UnloadImageColors`, `UnloadImagePalette`, `ExportImageToMemory`, `GetPixelColor`, `SetPixelColor`, `GetPixelDataSize`, `UpdateTextureRec` доступны в биндинге. Для указателей/буферов определить длину, формат пикселя и способ копирования в `Массив`/двоичные данные OneScript до освобождения нативной памяти; не возвращать сырые указатели.
- GPU-методы вызываются только после создания окна. `UpdateTexture` и фильтры уже запланированы в `task7.md`; здесь — получение/изменение пикселей и обновление прямоугольной области.

**Первый небольшой шаг:** `ImageAlphaCrop` на копии тестового Image с прозрачной рамкой и проверкой размеров до/после; отдельно проверить отсутствие двойной выгрузки.
