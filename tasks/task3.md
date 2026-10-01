# Task 3 — rcore: ввод (клавиатура, мышь, геймпад, тач, жесты)

**Модуль:** rcore (Input Handling) + rgestures
**Приоритет:** высокий для клавиатуры/мыши, низкий для геймпада/тач/жестов (нишевые сценарии).
**Статус:** ✅ реализованы клавиатура, мышь, геймпад, тач и жесты из списка ниже, кроме `GetKeyName` и `SetGamepadVibration` (отсутствуют в используемом raylib-cs 6.0.0). Клавиатура/мышь и опрос доступности геймпада/касаний проверены в оконном демо `src/testInput.os`; поведение с физическим геймпадом и тач-устройством требует проверки на таком устройстве.

## Перечень методов (C-сигнатуры для справки)

### Клавиатура (высокий приоритет)
```c
bool IsKeyPressed(int key);        // один раз при нажатии
bool IsKeyPressedRepeat(int key);
bool IsKeyReleased(int key);       // один раз при отпускании
bool IsKeyUp(int key);
int GetKeyPressed(void);           // очередь кодов клавиш
int GetCharPressed(void);          // очередь unicode-символов
// GetKeyName отсутствует в raylib-cs 6.0.0
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
// SetGamepadVibration отсутствует в raylib-cs 6.0.0
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
- `КлавишаНажата` исторически означает `IsKeyDown` (удержание); `КлавишаНажатаОдин` означает `IsKeyPressed` (один кадр). Для отпускания используется `КлавишаОтпущена`.
- `GetKeyPressed`/`GetCharPressed` извлекают **один** элемент очереди за вызов, возвращают 0 при пустой очереди; для получения всех событий за кадр вызывайте в цикле до 0.
- Имена геймпада запрашивайте только после `IsGamepadAvailable`; индекс касания — только после `GetTouchPointCount`. Перечисления `KeyboardKey`, `MouseButton`, `GamepadButton`, `GamepadAxis`, `MouseCursor`, `Gesture` принимаются как числовые коды в OneScript.
- `GetGestureHoldDuration` возвращает миллисекунды, значения Vector2 доступны через `ВекторX`/`ВекторY`.

## Тестовый скрипт
`src/testKeyDown.os` — удержание стрелок и события нажатия/отпускания RIGHT.
`src/testMouse.os` — видимость курсора, нажатие и состояние левой кнопки мыши.
`src/testInput.os` — ограниченный по времени пример клавиатуры/мыши, опроса геймпада, касаний и жестов.
