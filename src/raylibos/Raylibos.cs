using OneScript.Contexts;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using Raylib_cs;
using System.Numerics;

namespace raylibos;

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

    [ContextMethod("СкрытьКурсор", "HideCursor")]
    public void HideCursor()
    {
        Raylib.HideCursor();
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
}
