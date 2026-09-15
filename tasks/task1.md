# Task 1 — rcore: окно и мониторы

**Модуль:** rcore (Window-related functions)
**Приоритет:** высокий — это базовые вещи, которые нужны почти в любом приложении (изменение размера окна, получение текущих размеров экрана, полноэкранный режим).
**Статус:** реализовано только `InitWindow`, `CloseWindow`, `WindowShouldClose`. Остальное окно — нет.

## Что реализовать

### Состояние окна
```c
bool IsWindowReady(void);
bool IsWindowFullscreen(void);
bool IsWindowHidden(void);
bool IsWindowMinimized(void);
bool IsWindowMaximized(void);
bool IsWindowFocused(void);
bool IsWindowResized(void);
bool IsWindowState(unsigned int flag);
void SetWindowState(unsigned int flags);
void ClearWindowState(unsigned int flags);
void ToggleFullscreen(void);
void ToggleBorderlessWindowed(void);
void MaximizeWindow(void);
void MinimizeWindow(void);
void RestoreWindow(void);
void SetWindowFocused(void);
```

### Заголовок, иконка, позиция, размеры
```c
void SetWindowTitle(const char *title);
void SetWindowIcon(Image image);
void SetWindowIcons(Image *images, int count);
void SetWindowPosition(int x, int y);
void SetWindowMonitor(int monitor);
void SetWindowMinSize(int width, int height);
void SetWindowMaxSize(int width, int height);
void SetWindowSize(int width, int height);
void SetWindowOpacity(float opacity);
Vector2 GetWindowPosition(void);
Vector2 GetWindowScaleDPI(void);
```

### Размеры экрана и мониторы
```c
int GetScreenWidth(void);
int GetScreenHeight(void);
int GetRenderWidth(void);
int GetRenderHeight(void);
int GetMonitorCount(void);
int GetCurrentMonitor(void);
Vector2 GetMonitorPosition(int monitor);
int GetMonitorWidth(int monitor);
int GetMonitorHeight(int monitor);
int GetMonitorPhysicalWidth(int monitor);
int GetMonitorPhysicalHeight(int monitor);
int GetMonitorRefreshRate(int monitor);
const char *GetMonitorName(int monitor);
```

### Буфер обмена и события
```c
void SetClipboardText(const char *text);
const char *GetClipboardText(void);
Image GetClipboardImage(void);
void EnableEventWaiting(void);
void DisableEventWaiting(void);
```

## Заметки по реализации
- `SetWindowIcon`/`SetWindowIcons`/`GetClipboardImage` возвращают/принимают `Image` — используй существующий `ImageWrapper`, не `COMWrapperContext.Create()`.
- Флаги окна (`FLAG_WINDOW_RESIZABLE` и т.д. из `ConfigFlags`) в raylib-cs — enum `Raylib_cs.ConfigFlags`; принимай их как `int`/`uint` в OneScript-методе и приводи через `(ConfigFlags)flags`, аналогично тому, как в `Raylibos.cs` уже приводится `(CameraMode)mode`.
- `IsWindowResized` полезно проверить в тестовом скрипте с `FLAG_WINDOW_RESIZABLE`.
- Русские имена по аналогии с существующими: `ОкноПолноэкранное`, `ОкноСвёрнуто`, `ОкноРазвёрнуто`, `ОкноВФокусе`, `УстановитьЗаголовокОкна`, `УстановитьПозициюОкна`, `ШиринаЭкрана`, `ВысотаЭкрана` и т.д.

## Тестовый скрипт
`src/testWindowState.os` — окно с `FLAG_WINDOW_RESIZABLE`, вывод текущих `GetScreenWidth/Height` и переключение полноэкранного режима по клавише.
