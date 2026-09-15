# Task 2 — rcore: курсор, тайминг, случайные значения, misc

**Модуль:** rcore (Cursor / Timing / Misc / Random)
**Приоритет:** средний — небольшие, но часто нужные функции (delta time, FPS, seed для случайных чисел).
**Статус:** реализовано `ShowCursor`, `HideCursor`, `IsCursorHidden`, `SetTargetFPS`, `GetRandomValue`. Остальное — нет.

## Что реализовать

### Курсор
```c
void EnableCursor(void);   // lock cursor
void DisableCursor(void);  // unlock cursor
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
(`LoadRandomSequence`/`UnloadRandomSequence` — низкий приоритет, возвращают `int*`; можно пропустить или сделать через `OneScript`-массив, если понадобится.)

### Misc
```c
void TakeScreenshot(const char *fileName);
void SetConfigFlags(unsigned int flags);
void OpenURL(const char *url);
```

## Заметки по реализации
- `GetFrameTime`/`GetTime` — самое частое, что нужно для физики/анимации (уже используется вручную через `Шаг = ФПС / 400` в `examples/physics`, но правильнее было бы через `GetFrameTime`).
- `SetConfigFlags` нужно вызывать **до** `InitWindow` — стоит явно упомянуть это в README при документировании (аналогично примечанию "текстуры грузятся после InitWindow").
- `unsigned int flags` для `SetConfigFlags`/`SetRandomSeed` — принимай как `int` на стороне OneScript, приводи к `uint` в C#.
- Русские имена: `ВремяКадра`, `ВремяРаботы` (`GetTime`), `ТекущийFPS`, `Подождать`, `СделатьСкриншот`, `УстановитьФлагиКонфигурации`, `ОткрытьURL`, `ВключитьКурсор`/`ОтключитьКурсор` (Enable/DisableCursor — избегать путаницы с уже занятыми `ПоказатьКурсор`/`СкрытьКурсор`, которые про видимость, а не про lock).

## Тестовый скрипт
`src/testTiming.os` — вывод `GetFrameTime`/`GetFPS`/`GetTime` текстом на экране каждый кадр.
