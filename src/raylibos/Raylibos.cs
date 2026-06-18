using OneScript.Contexts;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using Raylib_cs;
using System.Numerics;

namespace raylibos;

[ContextClass("Изображение", "Image")]
public class ImageWrapper : AutoContext<ImageWrapper>
{
    public Image Image { get; set; }

    [ScriptConstructor]
    public static ImageWrapper Constructor()
    {
        return new ImageWrapper();
    }

    [ContextMethod("Ширина", "Width")]
    public int GetWidth()
    {
        return Image.Width;
    }

    [ContextMethod("Высота", "Height")]
    public int GetHeight()
    {
        return Image.Height;
    }
}

[ContextClass("Рейлиб", "Raylib")]
public class Raylibos : AutoContext<Raylibos>
{

    [ScriptConstructor]
    public static Raylibos Constructor()
    {
        return new Raylibos();
    }

    [ContextMethod("НовыйЦвет", "NewColor")]
    public IValue NewColor(int r, int g, int b, int a)
    {
        Color color = new Color(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b), Convert.ToByte(a));
        return COMWrapperContext.Create(color);
    }

    [ContextMethod("НовыйВектор", "NewVector2")]
    public IValue NewVector2(IValue x, IValue y)
    {
        Vector2 vector = new Vector2(IValueToFloat(x), IValueToFloat(y));
        return COMWrapperContext.Create(vector);
    }

    [ContextMethod("ВекторX", "Vector2X")]
    public decimal Vector2X(IValue vector)
    {
        Vector2 v = IValueToVector2(vector);
        return (decimal)v.X;
    }

    [ContextMethod("ВекторY", "Vector2Y")]
    public decimal Vector2Y(IValue vector)
    {
        Vector2 v = IValueToVector2(vector);
        return (decimal)v.Y;
    }

    [ContextMethod("ИнициализацияОкна", "InitWindow")]
    public void InitWindow(string title, int width, int height)
    {
        Raylib.InitWindow(width, height, title);
    }

    [ContextMethod("ОкноДолжноЗакрыться", "WindowShouldClose")]
    public bool WindowShouldClose()
    {
        return Raylib.WindowShouldClose();
    }

    [ContextMethod("НачатьОтрисовку", "BeginDrawing")]
    public void BeginDrawing()
    {
        Raylib.BeginDrawing();
    }

    [ContextMethod("ЗакончитьОтрисовку", "EndDrawing")]
    public void EndDrawing()
    {
        Raylib.EndDrawing();
    }

    [ContextMethod("ЗакрытьОкно", "CloseWindow")]
    public void CloseWindow()
    {
        Raylib.CloseWindow();
    }

    [ContextMethod("ОчиститьФон", "ClearBackground")]
    public void ClearBackground(IValue color)
    {
        Raylib.ClearBackground(IValueToColor(color));
    }

    [ContextMethod("НарисоватьТекст", "DrawText")]
    public void DrawText(string text, int posX, int posY, int fontSize, IValue color)
    {
        Raylib.DrawText(text, posX, posY, fontSize, IValueToColor(color));
    }

    [ContextMethod("НарисоватьКруг", "DrawCircle")]
    public void DrawCircle(int centerX, int centerY, IValue radius, IValue color)
    {
        Raylib.DrawCircle(centerX, centerY, IValueToFloat(radius), IValueToColor(color));
    }

    [ContextMethod("НарисоватьКругГрадиент", "DrawCircleGradient")]
    public void DrawCircleGradient(int centerX, int centerY, IValue radius, IValue inner, IValue outer)
    {
        Raylib.DrawCircleGradient(centerX, centerY, IValueToFloat(radius), IValueToColor(inner), IValueToColor(outer));
    }

    [ContextMethod("НарисоватьКругЛиния", "DrawCircleLines")]
    public void DrawCircleLines(int centerX, int centerY, IValue radius, IValue color)
    {
        Raylib.DrawCircleLines(centerX, centerY, IValueToFloat(radius), IValueToColor(color));
    }

    [ContextMethod("НарисоватьЭллипс", "DrawEllipse")]
    public void DrawEllipse(int centerX, int centerY, IValue radiusH, IValue radiusV, IValue color)
    {
        Raylib.DrawEllipse(centerX, centerY, IValueToFloat(radiusH), IValueToFloat(radiusV), IValueToColor(color));
    }

    [ContextMethod("НарисоватьЭллипсЛиния", "DrawEllipseLines")]
    public void DrawEllipseLines(int centerX, int centerY, IValue radiusH, IValue radiusV, IValue color)
    {
        Raylib.DrawEllipseLines(centerX, centerY, IValueToFloat(radiusH), IValueToFloat(radiusV), IValueToColor(color));
    }

    [ContextMethod("НарисоватьПрямоугольник", "DrawRectangle")]
    public void DrawRectangle(int posX, int posY, int width, int height, IValue color)
    {
        Raylib.DrawRectangle(posX, posY, width, height, IValueToColor(color));
    }

    [ContextMethod("НарисоватьПрямоугольникЛиния", "DrawRectangleLines")]
    public void DrawRectangleLines(int posX, int posY, int width, int height, IValue color)
    {
        Raylib.DrawRectangleLines(posX, posY, width, height, IValueToColor(color));
    }

    [ContextMethod("НарисоватьПрямоугольникГрадиентВертикальный", "DrawRectangleGradientV")]
    public void DrawRectangleGradientV(int posX, int posY, int width, int height, IValue top, IValue bottom)
    {
        Raylib.DrawRectangleGradientV(posX, posY, width, height, IValueToColor(top), IValueToColor(bottom));
    }

    [ContextMethod("НарисоватьПрямоугольникГрадиентГоризонтальный", "DrawRectangleGradientH")]
    public void DrawRectangleGradientH(int posX, int posY, int width, int height, IValue left, IValue right)
    {
        Raylib.DrawRectangleGradientH(posX, posY, width, height, IValueToColor(left), IValueToColor(right));
    }

    [ContextMethod("НарисоватьПрямоугольникПро", "DrawRectanglePro")]
    public void DrawRectanglePro(IValue rec, IValue origin, IValue rotation, IValue color)
    {
        Raylib.DrawRectanglePro(IValueToRectangle(rec), IValueToVector2(origin), IValueToFloat(rotation), IValueToColor(color));
    }

    [ContextMethod("НарисоватьТреугольник", "DrawTriangle")]
    public void DrawTriangle(IValue v1, IValue v2, IValue v3, IValue color)
    {
        Raylib.DrawTriangle(IValueToVector2(v1), IValueToVector2(v2), IValueToVector2(v3), IValueToColor(color));
    }

    [ContextMethod("НарисоватьТреугольникЛиния", "DrawTriangleLines")]
    public void DrawTriangleLines(IValue v1, IValue v2, IValue v3, IValue color)
    {
        Raylib.DrawTriangleLines(IValueToVector2(v1), IValueToVector2(v2), IValueToVector2(v3), IValueToColor(color));
    }

    [ContextMethod("НарисоватьЛинию", "DrawLine")]
    public void DrawLine(int startPosX, int startPosY, int endPosX, int endPosY, IValue color)
    {
        Raylib.DrawLine(startPosX, startPosY, endPosX, endPosY, IValueToColor(color));
    }

    [ContextMethod("НарисоватьЛинииПолоса", "DrawLineStrip")]
    public unsafe void DrawLineStrip(IValue points, IValue color)
    {
        object pointsObj = COMWrapperContext.MarshalIValue(points);
        dynamic array = pointsObj;
        int count = (int)array.Count();
        Vector2[] vectors = new Vector2[count];
        
        for (int i = 0; i < count; i++)
        {
            vectors[i] = IValueToVector2(array.Get(i));
        }
        
        fixed (Vector2* ptr = vectors)
        {
            Raylib.DrawLineStrip(ptr, count, IValueToColor(color));
        }
    }

    [ContextMethod("НарисоватьТреугольникВеер", "DrawTriangleFan")]
    public unsafe void DrawTriangleFan(IValue points, IValue color)
    {
        object pointsObj = COMWrapperContext.MarshalIValue(points);
        dynamic array = pointsObj;
        int count = (int)array.Count();
        Vector2[] vectors = new Vector2[count];
        
        for (int i = 0; i < count; i++)
        {
            vectors[i] = IValueToVector2(array.Get(i));
        }
        
        fixed (Vector2* ptr = vectors)
        {
            Raylib.DrawTriangleFan(ptr, count, IValueToColor(color));
        }
    }

    [ContextMethod("НарисоватьТреугольникПолоса", "DrawTriangleStrip")]
    public unsafe void DrawTriangleStrip(IValue points, IValue color)
    {
        object pointsObj = COMWrapperContext.MarshalIValue(points);
        dynamic array = pointsObj;
        int count = (int)array.Count();
        Vector2[] vectors = new Vector2[count];
        
        for (int i = 0; i < count; i++)
        {
            vectors[i] = IValueToVector2(array.Get(i));
        }
        
        fixed (Vector2* ptr = vectors)
        {
            Raylib.DrawTriangleStrip(ptr, count, IValueToColor(color));
        }
    }

    [ContextMethod("НарисоватьПолигон", "DrawPoly")]
    public void DrawPoly(IValue center, int sides, IValue radius, IValue rotation, IValue color)
    {
        Raylib.DrawPoly(IValueToVector2(center), sides, IValueToFloat(radius), IValueToFloat(rotation), IValueToColor(color));
    }

    [ContextMethod("НарисоватьПолигонЛиния", "DrawPolyLines")]
    public void DrawPolyLines(IValue center, int sides, IValue radius, IValue rotation, IValue color)
    {
        Raylib.DrawPolyLines(IValueToVector2(center), sides, IValueToFloat(radius), IValueToFloat(rotation), IValueToColor(color));
    }

    [ContextMethod("НарисоватьПолигонЛинияТолстый", "DrawPolyLinesEx")]
    public void DrawPolyLinesEx(IValue center, int sides, IValue radius, IValue rotation, IValue thickness, IValue color)
    {
        Raylib.DrawPolyLinesEx(IValueToVector2(center), sides, IValueToFloat(radius), IValueToFloat(rotation), IValueToFloat(thickness), IValueToColor(color));
    }

    [ContextMethod("НовыйВектор3", "NewVector3")]
    public IValue NewVector3(IValue x, IValue y, IValue z)
    {
        Vector3 vector = new Vector3(IValueToFloat(x), IValueToFloat(y), IValueToFloat(z));
        return COMWrapperContext.Create(vector);
    }

    [ContextMethod("НоваяКамера3D", "NewCamera3D")]
    public IValue NewCamera3D(IValue position, IValue target, IValue up, IValue fovy, int projection)
    {
        Camera3D camera = new Camera3D
        {
            Position = IValueToVector3(position),
            Target = IValueToVector3(target),
            Up = IValueToVector3(up),
            FovY = IValueToFloat(fovy),
            Projection = (CameraProjection)projection
        };
        return COMWrapperContext.Create(camera);
    }

    [ContextMethod("НачатьРежим3D", "BeginMode3D")]
    public void BeginMode3D(IValue camera)
    {
        Raylib.BeginMode3D(IValueToCamera3D(camera));
    }

    [ContextMethod("ЗакончитьРежим3D", "EndMode3D")]
    public void EndMode3D()
    {
        Raylib.EndMode3D();
    }

    [ContextMethod("НарисоватьКуб", "DrawCube")]
    public void DrawCube(IValue position, IValue width, IValue height, IValue length, IValue color)
    {
        Raylib.DrawCube(IValueToVector3(position), IValueToFloat(width), IValueToFloat(height), IValueToFloat(length), IValueToColor(color));
    }

    [ContextMethod("НарисоватьКубЛиния", "DrawCubeWires")]
    public void DrawCubeWires(IValue position, IValue width, IValue height, IValue length, IValue color)
    {
        Raylib.DrawCubeWires(IValueToVector3(position), IValueToFloat(width), IValueToFloat(height), IValueToFloat(length), IValueToColor(color));
    }

    [ContextMethod("ЗагрузитьМодель", "LoadModel")]
    public IValue LoadModel(string fileName)
    {
        Model model = Raylib.LoadModel(fileName);
        return COMWrapperContext.Create(model);
    }

    [ContextMethod("ВыгрузитьМодель", "UnloadModel")]
    public void UnloadModel(IValue model)
    {
        Raylib.UnloadModel(IValueToModel(model));
    }

    [ContextMethod("УстановитьТекстуруМодели", "SetModelTexture")]
    public unsafe void SetModelTexture(IValue model, IValue texture)
    {
        Model m = IValueToModel(model);
        m.Materials[0].Maps[0].Texture = IValueToTexture2D(texture);
    }

    [ContextMethod("НарисоватьМодель", "DrawModel")]
    public void DrawModel(IValue model, IValue position, IValue scale, IValue color)
    {
        Raylib.DrawModel(IValueToModel(model), IValueToVector3(position), IValueToFloat(scale), IValueToColor(color));
    }

    [ContextMethod("ПолучитьОграничивающийБокс", "GetMeshBoundingBox")]
    public IValue GetMeshBoundingBox(IValue model)
    {
        BoundingBox bounds = Raylib.GetModelBoundingBox(IValueToModel(model));
        return COMWrapperContext.Create(bounds);
    }

    [ContextMethod("НарисоватьОграничивающийБокс", "DrawBoundingBox")]
    public void DrawBoundingBox(IValue box, IValue color)
    {
        Raylib.DrawBoundingBox(IValueToBoundingBox(box), IValueToColor(color));
    }

    [ContextMethod("НарисоватьСетку", "DrawGrid")]
    public void DrawGrid(int slices, IValue spacing)
    {
        Raylib.DrawGrid(slices, IValueToFloat(spacing));
    }

    [ContextMethod("ОбновитьКамеру", "UpdateCamera")]
    public IValue UpdateCamera(IValue camera, int mode)
    {
        Camera3D cam = IValueToCamera3D(camera);
        Raylib.UpdateCamera(ref cam, (CameraMode)mode);
        return COMWrapperContext.Create(cam);
    }

    [ContextMethod("КлавишаНажата", "IsKeyDown")]
    public bool IsKeyDown(int key)
    {
        return Raylib.IsKeyDown((KeyboardKey)key);
    }

    [ContextMethod("ПозицияМыши", "GetMousePosition")]
    public IValue GetMousePosition()
    {
        Vector2 pos = Raylib.GetMousePosition();
        return COMWrapperContext.Create(pos);
    }

    [ContextMethod("КнопкаМышиНажата", "IsMouseButtonPressed")]
    public bool IsMouseButtonPressed(int button)
    {
        return Raylib.IsMouseButtonPressed((MouseButton)button);
    }

    [ContextMethod("КурсорСкрыт", "IsCursorHidden")]
    public bool IsCursorHidden()
    {
        return Raylib.IsCursorHidden();
    }

    [ContextMethod("ПоказатьКурсор", "ShowCursor")]
    public void ShowCursor()
    {
        Raylib.ShowCursor();
    }

    [ContextMethod("Камера2DЦель", "Camera2DTarget")]
    public IValue Camera2DTarget(IValue camera)
    {
        Camera2D cam = (Camera2D)COMWrapperContext.MarshalIValue(camera);
        return COMWrapperContext.Create(cam.Target);
    }

    [ContextMethod("Камера2DСмещение", "Camera2DOffset")]
    public IValue Camera2DOffset(IValue camera)
    {
        Camera2D cam = (Camera2D)COMWrapperContext.MarshalIValue(camera);
        return COMWrapperContext.Create(cam.Offset);
    }

    [ContextMethod("Камера2DВращение", "Camera2DRotation")]
    public decimal Camera2DRotation(IValue camera)
    {
        Camera2D cam = (Camera2D)COMWrapperContext.MarshalIValue(camera);
        return (decimal)cam.Rotation;
    }

    [ContextMethod("Камера2DЗум", "Camera2DZoom")]
    public decimal Camera2DZoom(IValue camera)
    {
        Camera2D cam = (Camera2D)COMWrapperContext.MarshalIValue(camera);
        return (decimal)cam.Zoom;
    }

    [ContextMethod("ОбновитьКамеру2D", "UpdateCamera2D")]
    public IValue UpdateCamera2D(IValue camera, IValue target = null, IValue offset = null, IValue rotation = null, IValue zoom = null)
    {
        Camera2D cam = (Camera2D)COMWrapperContext.MarshalIValue(camera);
        if (target != null) cam.Target = IValueToVector2(target);
        if (offset != null) cam.Offset = IValueToVector2(offset);
        if (rotation != null) cam.Rotation = IValueToFloat(rotation);
        if (zoom != null) cam.Zoom = IValueToFloat(zoom);
        return COMWrapperContext.Create(cam);
    }

    [ContextMethod("НоваяКамера2D", "NewCamera2D")]
    public IValue NewCamera2D(IValue target, IValue offset, IValue rotation, IValue zoom)
    {
        Camera2D camera = new Camera2D
        {
            Target = IValueToVector2(target),
            Offset = IValueToVector2(offset),
            Rotation = IValueToFloat(rotation),
            Zoom = IValueToFloat(zoom)
        };
        return COMWrapperContext.Create(camera);
    }

    [ContextMethod("НачатьРежим2D", "BeginMode2D")]
    public void BeginMode2D(IValue camera)
    {
        Raylib.BeginMode2D(IValueToCamera2D(camera));
    }

    [ContextMethod("ЗакончитьРежим2D", "EndMode2D")]
    public void EndMode2D()
    {
        Raylib.EndMode2D();
    }

    [ContextMethod("ПозицияКолесаМыши", "GetMouseWheelMove")]
    public decimal GetMouseWheelMove()
    {
        return (decimal)Raylib.GetMouseWheelMove();
    }

    [ContextMethod("Случайное", "GetRandomValue")]
    public int GetRandomValue(int min, int max)
    {
        return Raylib.GetRandomValue(min, max);
    }

    [ContextMethod("УстановитьЧастотуКадров", "SetTargetFPS")]
    public void SetTargetFPS(int fps)
    {
        Raylib.SetTargetFPS(fps);
    }

    [ContextMethod("ПоказатьFPS", "DrawFPS")]
    public void DrawFPS(int posX = 10, int posY = 10)
    {
        Raylib.DrawFPS(posX, posY);
    }

    [ContextMethod("СкрытьКурсор", "HideCursor")]
    public void HideCursor()
    {
        Raylib.HideCursor();
    }

    [ContextMethod("ЗагрузитьТекстуру", "LoadTexture")]
    public IValue LoadTexture(string fileName)
    {
        Texture2D texture = Raylib.LoadTexture(fileName);
        return COMWrapperContext.Create(texture);
    }

    [ContextMethod("НарисоватьТекстуру", "DrawTexture")]
    public void DrawTexture(IValue texture, int posX, int posY, IValue color)
    {
        Raylib.DrawTexture(IValueToTexture2D(texture), posX, posY, IValueToColor(color));
    }

    [ContextMethod("НарисоватьТекстуруПозиция", "DrawTextureEx")]
    public void DrawTextureEx(IValue texture, IValue position, float rotation, float scale, IValue color)
    {
        Raylib.DrawTextureEx(IValueToTexture2D(texture), IValueToVector2(position), rotation, scale, IValueToColor(color));
    }

    [ContextMethod("НарисоватьТекстуруПрямоугольник", "DrawTextureRec")]
    public void DrawTextureRec(IValue texture, IValue source, IValue position, IValue color)
    {
        Raylib.DrawTextureRec(IValueToTexture2D(texture), IValueToRectangle(source), IValueToVector2(position), IValueToColor(color));
    }

    [ContextMethod("НарисоватьТекстуруПро", "DrawTexturePro")]
    public void DrawTexturePro(IValue texture, IValue source, IValue dest, IValue origin, IValue rotation, IValue color)
    {
        Raylib.DrawTexturePro(IValueToTexture2D(texture), IValueToRectangle(source), IValueToRectangle(dest), IValueToVector2(origin), IValueToFloat(rotation), IValueToColor(color));
    }

    [ContextMethod("ВыгрузитьТекстуру", "UnloadTexture")]
    public void UnloadTexture(IValue texture)
    {
        Raylib.UnloadTexture(IValueToTexture2D(texture));
    }

    [ContextMethod("ТекстураШирина", "TextureWidth")]
    public int TextureWidth(IValue texture)
    {
        return IValueToTexture2D(texture).Width;
    }

    [ContextMethod("ТекстураВысота", "TextureHeight")]
    public int TextureHeight(IValue texture)
    {
        return IValueToTexture2D(texture).Height;
    }

    [ContextMethod("НовыйПрямоугольник", "NewRectangle")]
    public IValue NewRectangle(IValue x, IValue y, IValue width, IValue height)
    {
        Rectangle rect = new Rectangle(IValueToFloat(x), IValueToFloat(y), IValueToFloat(width), IValueToFloat(height));
        return COMWrapperContext.Create(rect);
    }

    [ContextMethod("ГенерироватьГрадиентЛинейный", "GenImageGradientLinear")]
    public IValue GenImageGradientLinear(int width, int height, int direction, IValue top, IValue bottom)
    {
        Image image = Raylib.GenImageGradientLinear(width, height, direction, IValueToColor(top), IValueToColor(bottom));
        ImageWrapper wrapper = new ImageWrapper { Image = image };
        return wrapper;
    }

    [ContextMethod("ГенерироватьГрадиентРадиальный", "GenImageGradientRadial")]
    public IValue GenImageGradientRadial(int width, int height, IValue spread, IValue inner, IValue outer)
    {
        Image image = Raylib.GenImageGradientRadial(width, height, IValueToFloat(spread), IValueToColor(inner), IValueToColor(outer));
        ImageWrapper wrapper = new ImageWrapper { Image = image };
        return wrapper;
    }

    [ContextMethod("ГенерироватьГрадиентКвадратный", "GenImageGradientSquare")]
    public IValue GenImageGradientSquare(int width, int height, int direction, IValue inner, IValue outer)
    {
        Image image = Raylib.GenImageGradientSquare(width, height, direction, IValueToColor(inner), IValueToColor(outer));
        ImageWrapper wrapper = new ImageWrapper { Image = image };
        return wrapper;
    }

    [ContextMethod("ГенерироватьШахматный", "GenImageChecked")]
    public IValue GenImageChecked(int width, int height, int checksX, int checksY, IValue col1, IValue col2)
    {
        Image image = Raylib.GenImageChecked(width, height, checksX, checksY, IValueToColor(col1), IValueToColor(col2));
        ImageWrapper wrapper = new ImageWrapper { Image = image };
        return wrapper;
    }

    [ContextMethod("ГенерироватьБелыйШум", "GenImageWhiteNoise")]
    public IValue GenImageWhiteNoise(int width, int height, IValue factor)
    {
        Image image = Raylib.GenImageWhiteNoise(width, height, IValueToFloat(factor));
        ImageWrapper wrapper = new ImageWrapper { Image = image };
        return wrapper;
    }

    [ContextMethod("ГенерироватьШумПерлина", "GenImagePerlinNoise")]
    public IValue GenImagePerlinNoise(int width, int height, int offsetX, int offsetY, IValue scale)
    {
        Image image = Raylib.GenImagePerlinNoise(width, height, offsetX, offsetY, IValueToFloat(scale));
        ImageWrapper wrapper = new ImageWrapper { Image = image };
        return wrapper;
    }

    [ContextMethod("ГенерироватьКлеточный", "GenImageCellular")]
    public IValue GenImageCellular(int width, int height, int tileSize)
    {
        Image image = Raylib.GenImageCellular(width, height, tileSize);
        ImageWrapper wrapper = new ImageWrapper { Image = image };
        return wrapper;
    }

    [ContextMethod("ЗагрузитьТекстуруИзИзображения", "LoadTextureFromImage")]
    public IValue LoadTextureFromImage(IValue image)
    {
        ImageWrapper imgWrapper = (ImageWrapper)COMWrapperContext.MarshalIValue(image);
        Texture2D texture = Raylib.LoadTextureFromImage(imgWrapper.Image);
        return COMWrapperContext.Create(texture);
    }

    [ContextMethod("ВыгрузитьИзображение", "UnloadImage")]
    public void UnloadImage(IValue image)
    {
        ImageWrapper imgWrapper = (ImageWrapper)COMWrapperContext.MarshalIValue(image);
        Raylib.UnloadImage(imgWrapper.Image);
    }

    // Вспомогательные функции

    private Color IValueToColor(IValue color)
    {
        return (Color)COMWrapperContext.MarshalIValue(color);
    }

    private float IValueToFloat(IValue floatValue)
    {
        object obj = COMWrapperContext.MarshalIValue(floatValue);
        decimal d = (decimal)obj;
        return (float)d;
    }

    private Vector2 IValueToVector2(IValue vector2)
    {
        return (Vector2)COMWrapperContext.MarshalIValue(vector2);
    }

    private Vector3 IValueToVector3(IValue vector3)
    {
        return (Vector3)COMWrapperContext.MarshalIValue(vector3);
    }

    private Camera3D IValueToCamera3D(IValue camera)
    {
        return (Camera3D)COMWrapperContext.MarshalIValue(camera);
    }

    private Camera2D IValueToCamera2D(IValue camera)
    {
        return (Camera2D)COMWrapperContext.MarshalIValue(camera);
    }

    private Texture2D IValueToTexture2D(IValue texture)
    {
        return (Texture2D)COMWrapperContext.MarshalIValue(texture);
    }

    private Rectangle IValueToRectangle(IValue rect)
    {
        return (Rectangle)COMWrapperContext.MarshalIValue(rect);
    }

    private Model IValueToModel(IValue model)
    {
        return (Model)COMWrapperContext.MarshalIValue(model);
    }

    private BoundingBox IValueToBoundingBox(IValue box)
    {
        return (BoundingBox)COMWrapperContext.MarshalIValue(box);
    }
}
