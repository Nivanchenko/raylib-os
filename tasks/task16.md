# Task 16 — rtext: Unicode, глифы и низкоуровневый шрифт

**Статус:** перечисленные функции отсутствуют в `Raylibos.cs`. Загрузка обычного Font, `DrawTextEx/Pro`, `MeasureTextEx` и простые строковые функции уже в [task8.md](task8.md).
**Приоритет:** средний для отображения кириллицы/Unicode, низкий для массивов глифов и строкового C API.

## Кодовые точки и метрики

- В биндинге 6.0.0 есть `GetCodepointCount`, `GetCodepoint`, `GetCodepointNext`, `GetCodepointPrevious`, `CodepointToUTF8`, `GetGlyphIndex`, `GetGlyphInfo`, `GetGlyphAtlasRec`, `DrawTextCodepoint`, `DrawTextCodepoints`. Для отображения текста сначала загрузить Font с нужными глифами: стандартный шрифт может не содержать кириллицу. Сопоставить возвращаемые `GlyphInfo` и `Rectangle` с доступными типами OneScript.
- `LoadCodepoints`/`UnloadCodepoints` и `LoadUTF8`/`UnloadUTF8` выделяют память; копировать массивы/строки до освобождения. Предпочитать штатные Unicode-строки OneScript, если достаточно их возможностей.
- `LoadFontFromImage`, `LoadFontFromMemory`, `LoadFontData`, `GenImageFontAtlas`, `UnloadFontData`, `ExportFontAsCode` требуют отдельных правил владения глифами, атласом и GPU-шрифтом. Не выгружать стандартный шрифт через `UnloadFont`.
- `TextIsEqual`, `TextLength`, `TextInsert`, `TextJoin`, `TextAppend`, `TextFindIndex`, `TextToUpper`, `TextToLower`, `TextToPascal` есть в биндинге, но в большинстве случаев уже заменяются OneScript; добавлять по конкретному запросу. Часть более новых `Text*` из `basic_api.md` отсутствует в биндинге.

**Первый небольшой шаг:** `GetCodepointCount` для ASCII и кириллицы с проверкой именно количества символов, а не UTF-8 байтов; затем — Font с проверенной лицензией и демо рендеринга кириллицы.
