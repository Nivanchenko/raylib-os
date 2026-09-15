# Task 8 — rtext: шрифты, текст, строковые утилиты

**Модуль:** rtext
**Приоритет:** средний — `DrawText` с дефолтным шрифтом уже работает, но нет ни загрузки кастомных шрифтов, ни измерения текста (важно для центрирования/UI), ни строковых хелперов.
**Статус:** реализовано только `DrawText`, `DrawFPS`.

## Что реализовать

### Шрифты (среднее)
```c
Font GetFontDefault(void);
Font LoadFont(const char *fileName);
Font LoadFontEx(const char *fileName, int fontSize, const int *codepoints, int codepointCount); // codepoints — передавать NULL/0 из OneScript для дефолтного набора
bool IsFontValid(Font font);
void UnloadFont(Font font);
```

### Рисование текста (высокое внутри задачи — сразу нужно после LoadFont)
```c
void DrawTextEx(Font font, const char *text, Vector2 position, float fontSize, float spacing, Color tint);
void DrawTextPro(Font font, const char *text, Vector2 position, Vector2 origin, float rotation, float fontSize, float spacing, Color tint);
```

### Измерение текста (высокое — нужно для центрирования текста в UI)
```c
int MeasureText(const char *text, int fontSize);
Vector2 MeasureTextEx(Font font, const char *text, float fontSize, float spacing);
void SetTextLineSpacing(int spacing);
```

### Строковые утилиты (низкое — в OneScript уже есть свои строковые функции, обёртка нужна только если не хватает функционала raylib, например `TextFormat`)
```c
const char *TextFormat(const char *text, ...);
const char *TextSubtext(const char *text, int position, int length);
char *TextReplace(const char *text, const char *search, const char *replacement);
char **TextSplit(const char *text, char delimiter, int *count);
int TextToInteger(const char *text);
float TextToFloat(const char *text);
```

## Заметки по реализации
- `Font` — новый marshalable-тип, добавить `IValueToFont` хелпер по аналогии с `IValueToTexture2D`.
- `LoadFontEx` с `codepoints=NULL, codepointCount=0` — в OneScript экспонировать перегрузку без параметра кодпоинтов: `ЗагрузитьШрифтРасширенный(fileName, fontSize)` вызывает `Raylib.LoadFontEx(fileName, fontSize, null, 0)`.
- Строковые утилиты — низкий приоритет, OneScript и так предоставляет `СтрНайти`, `СтрЗаменить`, `СтрРазделить` и т.п.; обёртывать смысл имеет только там, где нужна 1:1 совместимость с raylib-примерами или где нужен именно `TextFormat`-стиль (`%d`, `%.2f`) для быстрой сборки строк с числами.

## Тестовый скрипт
`src/testFonts.os` — загрузка кастомного `.ttf` из `resources/`, `DrawTextEx` с разным `fontSize`/`spacing`, текст отцентрирован через `MeasureTextEx`.
