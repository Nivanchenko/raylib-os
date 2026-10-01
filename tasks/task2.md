# Task 2 — rcore: курсор, тайминг, случайные значения, misc

**Модуль:** rcore (Cursor / Timing / Misc / Random)
**Приоритет:** средний — небольшие, но часто нужные функции (delta time, FPS, seed для случайных чисел).
**Статус:** ✅ основные методы реализованы в `Raylibos.cs`; пример `src/testTiming.os` проверяет время, FPS, seed (включая верхнюю границу `uint`) и вызовы управления курсором. Низкоприоритетные `LoadRandomSequence`/`UnloadRandomSequence` остаются отдельным шагом по потребности.

## Реализованные методы (C-сигнатуры для справки)

### Курсор
```c
void EnableCursor(void);   // показать курсор и разблокировать ввод
void DisableCursor(void);  // скрыть курсор и захватить ввод
bool IsCursorOnScreen(void);
```

### Тайминг
```c
float GetFrameTime(void);   // delta time
double GetTime(void);       // время с InitWindow()
int GetFPS(void);
void WaitTime(double seconds);
```

### Случайные значения
```c
void SetRandomSeed(unsigned int seed);
```
`LoadRandomSequence`/`UnloadRandomSequence` возвращают/принимают `int*` — отдельная задача, если понадобится: вернуть значения в `Массив` OneScript и освободить нативную последовательность.

### Misc
```c
void TakeScreenshot(const char *fileName);
void SetConfigFlags(unsigned int flags);
void OpenURL(const char *url);
```

## Заметки по реализации
- `GetFrameTime`/`GetTime` пригодятся для физики/анимации; `examples/physics` пока использует фиксированный шаг.
- `SetConfigFlags` вызывается **до** `InitWindow`. `SetRandomSeed` принимает в OneScript целое число от 0 до 4294967295, затем передаёт `uint` в raylib-cs.
- `EnableCursor`/`DisableCursor` отличаются от существующих `ПоказатьКурсор`/`СкрытьКурсор`: разблокируют/захватывают ввод, а не только меняют видимость.
- В raylib 5.5 `TakeScreenshot` сохраняет снимок в текущем рабочем каталоге даже при передаче полного пути; проверено отдельным оконным запуском из временного каталога.

## Тестовый скрипт
`src/testTiming.os` — вывод `GetFrameTime`/`GetFPS`/`GetTime` каждый кадр, проверка seed и времени после ожидания. Окно закрывается через 5 секунд; `OpenURL` не вызывается автоматически, чтобы не открывать браузер.
