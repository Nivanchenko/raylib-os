# Task 11 — raudio: звук целиком (модуль не начат)

**Модуль:** raudio
**Приоритет:** низкий/по потребности — но если библиотека претендует на "полноценные графические приложения (в т.ч. игры)" (см. README), звук — единственный полностью отсутствующий крупный модуль.
**Статус:** 0% — ни одна функция `raudio` не обёрнута.

## Что реализовать (минимальный жизнеспособный набор)

### Устройство
```c
void InitAudioDevice(void);
void CloseAudioDevice(void);
bool IsAudioDeviceReady(void);
void SetMasterVolume(float volume);
float GetMasterVolume(void);
```

### Sound (короткие эффекты) — высокий приоритет внутри задачи
```c
Sound LoadSound(const char *fileName);
bool IsSoundValid(Sound sound);
void UnloadSound(Sound sound);
void PlaySound(Sound sound);
void StopSound(Sound sound);
void PauseSound(Sound sound);
void ResumeSound(Sound sound);
bool IsSoundPlaying(Sound sound);
void SetSoundVolume(Sound sound, float volume);
void SetSoundPitch(Sound sound, float pitch);
void SetSoundPan(Sound sound, float pan);
```

### Music (потоковая музыка) — средний приоритет
```c
Music LoadMusicStream(const char *fileName);
bool IsMusicValid(Music music);
void UnloadMusicStream(Music music);
void PlayMusicStream(Music music);
bool IsMusicStreamPlaying(Music music);
void UpdateMusicStream(Music music);  // важно: нужно звать каждый кадр в игровом цикле
void StopMusicStream(Music music);
void PauseMusicStream(Music music);
void ResumeMusicStream(Music music);
void SetMusicVolume(Music music, float volume);
float GetMusicTimeLength(Music music);
float GetMusicTimePlayed(Music music);
```

### AudioStream — низкий приоритет (продвинутый сценарий, raw PCM)
```c
AudioStream LoadAudioStream(unsigned int sampleRate, unsigned int sampleSize, unsigned int channels);
void UnloadAudioStream(AudioStream stream);
void PlayAudioStream(AudioStream stream);
bool IsAudioStreamPlaying(AudioStream stream);
void StopAudioStream(AudioStream stream);
```

### Wave (низкоуровневая работа с сэмплами) — низкий приоритет, отложить
```c
Wave LoadWave(const char *fileName);
Sound LoadSoundFromWave(Wave wave);
void UnloadWave(Wave wave);
```

## Заметки по реализации
- `InitAudioDevice()` нужно звать один раз, аналогично `InitWindow` — задокументировать порядок в README ("звук нужно инициализировать после/до окна" — уточнить по документации raylib, обычно можно в любом порядке, но традиционно после `InitWindow`).
- `UpdateMusicStream` обязателен в игровом цикле для потоковой музыки — не забыть добавить в тестовый скрипт внутри `Пока Не ОкноДолжноЗакрыться() Цикл`.
- Новые marshalable-типы: `Sound`, `Music`, `AudioStream`, `Wave` — добавить `IValueToSound`/`IValueToMusic` хелперы по образцу существующих.
- Русские имена: `ИнициализироватьЗвук`, `ЗагрузитьЗвук`, `ВоспроизвестиЗвук`, `ЗагрузитьМузыку`, `ВоспроизвестиМузыку`, `ОбновитьПотокМузыки`.

## Тестовый скрипт
`src/testAudio.os` — загрузка `.wav` из `resources/`, проигрывание по нажатию клавиши; отдельно — фоновая музыка `.mp3`/`.ogg` с `UpdateMusicStream` в цикле.

## Предварительное условие
Понадобится добавить аудио-файл(ы) в `resources/` (сейчас там только текстуры и `.obj`-модель) — уточнить у пользователя формат/лицензию перед добавлением бинарных файлов в репозиторий.
