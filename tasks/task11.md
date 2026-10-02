# Task 11 — raudio: звуковые эффекты и потоковая музыка

**Модуль:** raudio
**Приоритет:** низкий/по потребности — базовое воспроизведение коротких звуков уже есть, потоковая музыка и продвинутое управление звуком всё ещё нужны для игровых сценариев.
**Статус:** реализованы `InitAudioDevice`/`IsAudioDeviceReady`/`CloseAudioDevice` и `LoadSound`/`IsSoundReady`/`PlaySound`/`UnloadSound`. Остальные операции с эффектами, потоковая музыка, AudioStream и Wave пока не реализованы.

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
bool IsSoundReady(Sound sound); // имя метода raylib-cs 6.0.0
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
bool IsMusicReady(Music music); // имя метода raylib-cs 6.0.0
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
Также не обёрнуты `UpdateAudioStream`, `IsAudioStreamProcessed`, `PauseAudioStream`, `ResumeAudioStream`, `SetAudioStreamVolume`, `SetAudioStreamPitch`, `SetAudioStreamPan`, `SetAudioStreamBufferSizeDefault` и callback/processor API (`SetAudioStreamCallback`, `AttachAudioStreamProcessor`, `DetachAudioStreamProcessor`, `AttachAudioMixedProcessor`, `DetachAudioMixedProcessor`). Последнее требует аудиопотока, безопасного владения делегатами и буферов; не реализовывать по аналогии с `PlaySound`.

### Wave (низкоуровневая работа с сэмплами) — низкий приоритет, отложить
```c
Wave LoadWave(const char *fileName);
Sound LoadSoundFromWave(Wave wave);
void UnloadWave(Wave wave);
```
Для Wave пока также отсутствуют `LoadWaveFromMemory`, `ExportWave`, `ExportWaveAsCode`, `WaveCopy`, `WaveCrop`, `WaveFormat`, `LoadWaveSamples`, `UnloadWaveSamples`; для Sound — `LoadSoundAlias`, `UnloadSoundAlias` (разделяемые сэмплы) и `UpdateSound`; для Music — `LoadMusicStreamFromMemory`, `SeekMusicStream`, `SetMusicPitch`, `SetMusicPan`. Сначала проверить владение ресурсом, порядок выгрузки алиаса и источника. `IsWaveValid` из более нового `basic_api.md` в биндинге 6.0.0 отсутствует (имя проверки в биндинге — `IsWaveReady`).

## Заметки по реализации
- **Первый шаг выполнен:** `InitAudioDevice`/`IsAudioDeviceReady`/`CloseAudioDevice` и `LoadSound`/`IsSoundReady`/`PlaySound`/`UnloadSound`; пример выгружает Sound до закрытия устройства. На поставляемой нативной raylib 5.5 проверка готовности называется `IsSoundValid`, а в raylib-cs 6.0.0 — `IsSoundReady`; обёртка поддерживает оба имени. `Music`/`AudioStream` остаются отдельными шагами.
- `UpdateMusicStream` обязателен в игровом цикле для потоковой музыки — не забыть добавить в тестовый скрипт внутри `Пока Не ОкноДолжноЗакрыться() Цикл`.
- Новые marshalable-типы: `Sound`, `Music`, `AudioStream`, `Wave` — добавить `IValueToSound`/`IValueToMusic` хелперы по образцу существующих.
- Русские имена: `ИнициализироватьЗвук`, `ЗагрузитьЗвук`, `ВоспроизвестиЗвук`, `ЗагрузитьМузыку`, `ВоспроизвестиМузыку`, `ОбновитьПотокМузыки`.

## Тестовый скрипт
`src/testAudio.os` — загрузка `.wav`, автоматическое проигрывание и повтор по пробелу, выгрузка `Sound` и закрытие устройства; фоновая музыка `.mp3`/`.ogg` с `UpdateMusicStream` — отдельный шаг.

## Предварительное условие
Для демо добавлен `resources/test_tone.wav`: синтезированный в репозитории тон 440 Гц длительностью 0,2 с (моно, PCM 16 bit, 22050 Гц), без сторонних ассетов и их лицензионных ограничений.
