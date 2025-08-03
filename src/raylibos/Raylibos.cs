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
}
