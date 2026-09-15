# Task 12 — rcore: файловая система, сжатие/кодирование, automation events

**Модуль:** rcore (File system management / Compression-Encoding / Automation events / Logging / Memory management)
**Приоритет:** низкий — не специфично для графики, OneScript уже имеет собственные средства работы с файлами (`ФайлSystem`, `НачатьТранзакцию` и т.д.); обёртывать стоит только то, чего не хватает в стандартной библиотеке OneScript или что специфично для raylib-сценариев (скриншоты, drag&drop файлов в окно).

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
unsigned int ComputeCRC32(unsigned char *data, int dataSize);
```

### Automation events (очень низкий приоритет — запись/воспроизведение input-событий для авто-тестов/демо-записей)
```c
AutomationEventList LoadAutomationEventList(const char *fileName);
void UnloadAutomationEventList(AutomationEventList list);
void SetAutomationEventList(AutomationEventList *list);
void StartAutomationEventRecording(void);
void StopAutomationEventRecording(void);
void PlayAutomationEvent(AutomationEvent event);
```

## Заметки по реализации
- Начинать только с `IsFileDropped`/`LoadDroppedFiles`/`UnloadDroppedFiles` — это единственная часть, которая реально расширяет возможности графического приложения (остальное either дублирует OneScript, either слишком нишевое: сжатие, хэши, automation events).
- `FilePathList` — новый marshalable-тип (счётчик + массив строк) — вернуть как обычный OneScript `Массив` строк, без необходимости отдельного враппера.
- Оставшиеся пункты (File system общего назначения, TraceLog, сжатие, automation events) реализовывать только по явному запросу — не делать "на всякий случай".

## Тестовый скрипт
`src/testDropFiles.os` — окно показывает имя файла, перетащенного из Finder/Explorer.
