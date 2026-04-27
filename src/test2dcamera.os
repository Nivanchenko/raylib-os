Перем рейлиб;
Перем screenWidth;
Перем screenHeight;
Перем player;
Перем buildings;
Перем buildingWidths;
Перем buildingHeights;
Перем buildColors;
Перем MAX_BUILDINGS;
Перем camera;
Перем playerWidth;
Перем playerHeight;
Перем target;

Процедура Инициализация()
    КаталогКомпоненты = ОбъединитьПути(ТекущийСценарий().Каталог, "raylibos/bin/Debug/net6.0/raylibos.dll");
    ПодключитьВнешнююКомпоненту(КаталогКомпоненты);
    рейлиб = Новый Рейлиб;
    
    screenWidth = 800;
    screenHeight = 450;
    MAX_BUILDINGS = 20;
    рейлиб.ИнициализацияОкна("2D Camera", screenWidth, screenHeight);
    
    player = рейлиб.НовыйВектор(400, 280);
    playerWidth = 40;
    playerHeight = 40;
    
    buildings = Новый Массив();
    buildingWidths = Новый Массив();
    buildingHeights = Новый Массив();
    buildColors = Новый Массив();
    
    spacing = 0;
    Для i = 0 По MAX_BUILDINGS - 1 Цикл
        width = рейлиб.Случайное(50, 200);
        height = рейлиб.Случайное(100, 800);
        
        building = рейлиб.НовыйВектор(-6000 + spacing, screenHeight - 130 - height);
        buildings.Добавить(building);
        
        buildingWidths.Добавить(width);
        buildingHeights.Добавить(height);
        
        r = рейлиб.Случайное(200, 240);
        g = рейлиб.Случайное(200, 240);
        b = рейлиб.Случайное(200, 250);
        color = рейлиб.НовыйЦвет(r, g, b, 255);
        buildColors.Добавить(color);
        
        spacing = spacing + width;
    КонецЦикла;
    
    target = рейлиб.НовыйВектор(рейлиб.ВекторX(player) + 20, рейлиб.ВекторY(player) + 20);
    offset = рейлиб.НовыйВектор(screenWidth / 2, screenHeight / 2);
    camera = рейлиб.НоваяКамера2D(target, offset, 0, 1);
    
    рейлиб.УстановитьЧастотуКадров(60);
КонецПроцедуры

Процедура Обновление()
    // Движение игрока
    Если рейлиб.КлавишаНажата(262) Тогда // RIGHT
        player = рейлиб.НовыйВектор(рейлиб.ВекторX(player) + 2, рейлиб.ВекторY(player));
    КонецЕсли;
    
    Если рейлиб.КлавишаНажата(263) Тогда // LEFT
        player = рейлиб.НовыйВектор(рейлиб.ВекторX(player) - 2, рейлиб.ВекторY(player));
    КонецЕсли;
    
    // Камера следует за игроком
    target = рейлиб.НовыйВектор(рейлиб.ВекторX(player) + 20, рейлиб.ВекторY(player) + 20);
    
    // Вращение камеры
    Если рейлиб.КлавишаНажата(65) Тогда // A
        rotation = рейлиб.Камера2DВращение(camera) - 1;
        camera = рейлиб.UpdateCamera2D(camera, target, , rotation, );
    КонецЕсли;
    
    Если рейлиб.КлавишаНажата(83) Тогда // S
        rotation = рейлиб.Камера2DВращение(camera) + 1;
        camera = рейлиб.UpdateCamera2D(camera, target, , rotation, );
    КонецЕсли;
    
    // Ограничение вращения (-40 до 40)
    Если рейлиб.Камера2DВращение(camera) > 40 Тогда
        camera = рейлиб.UpdateCamera2D(camera, target, , 40, );
    КонецЕсли;
    
    Если рейлиб.Камера2DВращение(camera) < -40 Тогда
        camera = рейлиб.UpdateCamera2D(camera, target, , -40, );
    КонецЕсли;
    
    // Зум колесом мыши
    wheelMove = рейлиб.ПозицияКолесаМыши();
    Если wheelMove <> 0 Тогда
        currentZoom = рейлиб.Камера2DЗум(camera);
        newZoom = currentZoom + (wheelMove * 0.1);
        Если newZoom > 3 Тогда newZoom = 3; КонецЕсли;
        Если newZoom < 0.1 Тогда newZoom = 0.1; КонецЕсли;
        camera = рейлиб.UpdateCamera2D(camera, target, , , newZoom);
    КонецЕсли;
    
    // Сброс камеры (R)
    Если рейлиб.КлавишаНажата(82) Тогда // R
        camera = рейлиб.UpdateCamera2D(camera, target, , 0, 1);
    КонецЕсли;
КонецПроцедуры

Процедура Отрисовка()
    рейлиб.НачатьОтрисовку();
    
    рейлиб.ОчиститьФон(рейлиб.НовыйЦвет(245, 245, 245, 255));
    
    рейлиб.BeginMode2D(camera);
    
        // Фон
        рейлиб.НарисоватьПрямоугольник(-6000, 320, 13000, 8000, рейлиб.НовыйЦвет(64, 64, 64, 255));
        
        // Здания
        Для i = 0 По MAX_BUILDINGS - 1 Цикл
            building = buildings[i];
            width = buildingWidths[i];
            height = buildingHeights[i];
            color = buildColors[i];
            рейлиб.НарисоватьПрямоугольник(рейлиб.ВекторX(building), рейлиб.ВекторY(building), 
                                           width, height, color);
        КонецЦикла;
        
        // Игрок
        рейлиб.НарисоватьПрямоугольник(рейлиб.ВекторX(player), рейлиб.ВекторY(player), 
                                       playerWidth, playerHeight, 
                                       рейлиб.НовыйЦвет(255, 0, 0, 255));
        
        // Линии камеры
        рейлиб.НарисоватьЛинию(рейлиб.ВекторX(target), -screenHeight * 10, 
                              рейлиб.ВекторX(target), screenHeight * 10, 
                              рейлиб.НовыйЦвет(0, 255, 0, 255));
        рейлиб.НарисоватьЛинию(-screenWidth * 10, рейлиб.ВекторY(target), 
                              screenWidth * 10, рейлиб.ВекторY(target), 
                              рейлиб.НовыйЦвет(0, 255, 0, 255));
    
    рейлиб.EndMode2D();
    
    // UI
    рейлиб.НарисоватьТекст("2D Camera Controls:", 20, 20, 10, рейлиб.НовыйЦвет(0, 0, 0, 255));
    рейлиб.НарисоватьТекст("- Right/Left: move player", 40, 40, 10, рейлиб.НовыйЦвет(64, 64, 64, 255));
    рейлиб.НарисоватьТекст("- Mouse Wheel: Zoom in/out", 40, 60, 10, рейлиб.НовыйЦвет(64, 64, 64, 255));
    рейлиб.НарисоватьТекст("- A/S: Rotate camera", 40, 80, 10, рейлиб.НовыйЦвет(64, 64, 64, 255));
    рейлиб.НарисоватьТекст("- R: Reset zoom & rotation", 40, 100, 10, рейлиб.НовыйЦвет(64, 64, 64, 255));
    
    рейлиб.ЗакончитьОтрисовку();
КонецПроцедуры

// Главный цикл
Инициализация();

Пока Не рейлиб.ОкноДолжноЗакрыться() Цикл
    Обновление();
    Отрисовка();
КонецЦикла;

рейлиб.ЗакрытьОкно();
