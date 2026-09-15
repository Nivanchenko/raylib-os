# Task 3 — rcore: ввод (клавиатура, мышь, геймпад, тач, жесты)

**Модуль:** rcore (Input Handling) + rgestures
**Приоритет:** высокий для клавиатуры/мыши, низкий для геймпада/тач/жестов (нишевые сценарии).
**Статус:** реализовано только `IsKeyDown`, `IsMouseButtonPressed`, `GetMousePosition`, `GetMouseWheelMove`. Всё остальное отсутствует, хотя для игр критично различать "нажата один раз" / "зажата" / "отпущена".

## Что реализовать

### Клавиатура (высокий приоритет)
```c
bool IsKeyPressed(int key);        // один раз при нажатии
bool IsKeyPressedRepeat(int key);
bool IsKeyReleased(int key);       // один раз при отпускании
bool IsKeyUp(int key);
int GetKeyPressed(void);           // очередь кодов клавиш
int GetCharPressed(void);          // очередь unicode-символов
const char *GetKeyName(int key);
void SetExitKey(int key);
```

### Мышь (высокий приоритет)
```c
bool IsMouseButtonDown(int button);
bool IsMouseButtonReleased(int button);
bool IsMouseButtonUp(int button);
int GetMouseX(void);
int GetMouseY(void);
Vector2 GetMouseDelta(void);
void SetMousePosition(int x, int y);
void SetMouseOffset(int offsetX, int offsetY);
void SetMouseScale(float scaleX, float scaleY);
Vector2 GetMouseWheelMoveV(void);
void SetMouseCursor(int cursor);
```

### Геймпад (низкий приоритет)
```c
bool IsGamepadAvailable(int gamepad);
const char *GetGamepadName(int gamepad);
bool IsGamepadButtonPressed(int gamepad, int button);
bool IsGamepadButtonDown(int gamepad, int button);
bool IsGamepadButtonReleased(int gamepad, int button);
bool IsGamepadButtonUp(int gamepad, int button);
int GetGamepadButtonPressed(void);
int GetGamepadAxisCount(int gamepad);
float GetGamepadAxisMovement(int gamepad, int axis);
int SetGamepadMappings(const char *mappings);
void SetGamepadVibration(int gamepad, float leftMotor, float rightMotor, float duration);
```

### Тач (низкий приоритет)
```c
int GetTouchX(void);
int GetTouchY(void);
Vector2 GetTouchPosition(int index);
int GetTouchPointId(int index);
int GetTouchPointCount(void);
```

### Жесты (низкий приоритет, module rgestures)
```c
void SetGesturesEnabled(unsigned int flags);
bool IsGestureDetected(unsigned int gesture);
int GetGestureDetected(void);
float GetGestureHoldDuration(void);
Vector2 GetGestureDragVector(void);
float GetGestureDragAngle(void);
Vector2 GetGesturePinchVector(void);
float GetGesturePinchAngle(void);
```

## Заметки по реализации
- `IsKeyPressed` vs `IsKeyDown` — сейчас в тестах (`testKeyDown.os`) используется только `IsKeyDown`, а для UI/меню обычно нужен именно `IsKeyPressed` (одно срабатывание). Стоит явно показать разницу в тестовом скрипте.
- Именование пересекается с уже занятым `КлавишаНажата` = `IsKeyDown`. Для новых нужно аккуратно развести смысл, например: `КлавишаНажатаОдин` (`IsKeyPressed`), `КлавишаОтпущена` (`IsKeyReleased`), `КлавишаНеНажата` (`IsKeyUp`). Аналогично для мыши: `КнопкаМышиЗажата` (`IsMouseButtonDown`), `КнопкаМышиОтпущена` (`IsMouseButtonReleased`).
- Геймпад/тач/жесты — реализовывать в последнюю очередь, только если появится конкретный сценарий использования.

## Тестовый скрипт
`src/testKeyPressReleased.os` — печатает на экран, какая клавиша была нажата/отпущена в этом кадре (используя `GetKeyPressed`/`IsKeyReleased`).
`src/testMouseButtons.os` — расширение `testMouse.os`: down/released/up для всех трёх кнопок.
