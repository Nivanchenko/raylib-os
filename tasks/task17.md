# Task 17 — rcore: низкоуровневый ввод-вывод и колбэки

**Статус:** описанные функции не обёрнуты в `Raylibos.cs`. Drag-and-drop и обычные файловые утилиты рассматриваются в [task12.md](task12.md); не дублировать OneScript без потребности.
**Приоритет:** низкий/только по явному сценарию.

## Файлы и сжатие

- В биндинге 6.0.0 есть `LoadFileData`, `UnloadFileData`, `SaveFileData`, `ExportDataAsCode`, `LoadDirectoryFiles`, `LoadDirectoryFilesEx`, `UnloadDirectoryFiles`, `GetFileLength`, `GetFileModTime`, `GetPrevDirectoryPath`, `ChangeDirectory`, `IsPathFile`, `CompressData`, `DecompressData`. Определить способ передачи бинарных данных OneScript, скопировать данные и пути перед освобождением указателей, задокументировать пределы размера.
- `LoadFileText`, `SaveFileText`, `FileRename/Remove/Copy/Move` и некоторые новые hash/FS-функции из `basic_api.md` не найдены в биндинге 6.0.0; это не список готовых сигнатур для реализации.

## Обратные вызовы и память

- `SetLoadFileDataCallback`, `SetSaveFileDataCallback`, `SetLoadFileTextCallback`, `SetSaveFileTextCallback`, `SetTraceLogCallback` и `MemAlloc`, `MemRealloc`, `MemFree` относятся к нативной памяти и callback ABI. Требуется определить поток исполнения, GC-root делегата и время жизни буфера; не раскрывать указатели как обычные значения OneScript.
- Для логов сначала проверить возможность использовать `SetTraceLogLevel`/`TraceLog` из `task12.md` без callback.

**Первый небольшой шаг:** при реальном запросе бинарного API сделать пару `LoadFileData` + безопасное копирование в двоичные данные OneScript + `UnloadFileData` в `finally`, с тестом на небольшом файле и отсутствии файла.
