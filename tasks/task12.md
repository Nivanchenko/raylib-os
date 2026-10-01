# Task 12 — rcore: файловая система, сжатие/кодирование, automation events

**Модуль:** rcore (File system management / Compression-Encoding / Automation events / Logging / Memory management)
**Приоритет:** низкий — OneScript уже имеет средства работы с файлами; обёртывать стоит только то, чего не хватает или что специфично для raylib-сценариев (например, drag&drop файлов в окно; скриншоты — в `task2.md`).

## Что реализовать (если понадобится)

### Файлы, специфичные для raylib-приложений
```c
bool IsFileDropped(void);
FilePathList LoadDroppedFiles(void);
void UnloadDroppedFiles(FilePathList files);
```
Это единственная по-настоящему полезная часть модуля для графического приложения — drag&drop файлов в окно (например, загрузка модели/текстуры через перетаскивание).

### Общая работа с файлами (низкий приоритет — дублирует возможности OneScript)
```c
bool FileExists(const char *fileName);
bool DirectoryExists(const char *dirPath);
bool IsFileExtension(const char *fileName, const char *ext);
const char *GetFileExtension(const char *fileName);
const char *GetFileName(const char *filePath);
const char *GetFileNameWithoutExt(const char *filePath);
const char *GetDirectoryPath(const char *filePath);
const char *GetWorkingDirectory(void);
const char *GetApplicationDirectory(void);
```

### Логирование (низкий приоритет)
```c
void SetTraceLogLevel(int logLevel);
void TraceLog(int logLevel, const char *text);
```

### Сжатие/кодирование (низкий приоритет, нишевое)
```c
char *EncodeDataBase64(const unsigned char *data, int dataSize, int *outputSize);
unsigned char *DecodeDataBase64(const char *text, int *outputSize);
// ComputeCRC32 из более нового C API отсутствует в raylib-cs 6.0.0
```

### Automation events (очень низкий приоритет — запись/воспроизведение input-событий для авто-тестов/демо-записей)
Следующие методы есть в `basic_api.md`, но отсутствуют в установленном raylib-cs 6.0.0; не планировать обёртку без обновления зависимости:
```c
AutomationEventList LoadAutomationEventList(const char *fileName);
void UnloadAutomationEventList(AutomationEventList list);
void SetAutomationEventList(AutomationEventList *list);
void StartAutomationEventRecording(void);
void StopAutomationEventRecording(void);
void PlayAutomationEvent(AutomationEvent event);
```

## Заметки по реализации
- **Первый шаг:** `IsFileDropped` и метод, который вызывает `LoadDroppedFiles`, копирует имена в `Массив` строк OneScript и гарантированно вызывает `UnloadDroppedFiles`. Проверить порядок и форму результата по raylib-cs 6.0.0.
- `FilePathList` содержит нативный ресурс; нельзя вернуть его напрямую или забыть вызвать `UnloadDroppedFiles` после копирования строк.
- Оставшиеся пункты (File system общего назначения, TraceLog, сжатие, automation events) реализовывать только по явному запросу — не делать "на всякий случай".

## Тестовый скрипт
`src/testDropFiles.os` — окно показывает имя файла, перетащенного из Finder/Explorer.
