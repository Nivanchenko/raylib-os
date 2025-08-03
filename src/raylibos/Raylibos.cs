using OneScript.Contexts;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using Raylib_cs;
using System.Numerics;
using Castle.DynamicProxy;

namespace raylibos;

[ContextClass("Рейлиб", "Raylib")]
public class Raylibos : AutoContext<Raylibos>
{

    [ScriptConstructor]
    public static Raylibos Constructor()
    {
        return new Raylibos();
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
    public void ClearBackground(Color_OS color)
    {
        Raylib.ClearBackground(color);
    }

    [ContextMethod("НарисоватьТекст", "DrawText")]
    public void DrawText(string text, int posX, int posY, int fontSize, Color_OS color)
    {
        Raylib.DrawText(text, posX, posY, fontSize, color);
    }

    [ContextMethod("НарисоватьКруг", "DrawCircle")]
    public void DrawCircle(int centerX, int centerY, IValue radius, Color_OS color)
    {
        Raylib.DrawCircle(centerX, centerY, IValueToFloat(radius), color);
    }

    [ContextMethod("НарисоватьКругГрадиент", "DrawCircleGradient")]
    public void DrawCircleGradient(int centerX, int centerY, IValue radius, Color_OS inner, Color_OS outer)
    {
        Raylib.DrawCircleGradient(centerX, centerY, IValueToFloat(radius), inner, outer);
    }

    [ContextMethod("НарисоватьКругЛиния", "DrawCircleLines")]
    public void DrawCircleLines(int centerX, int centerY, IValue radius, Color_OS color)
    {
        Raylib.DrawCircleLines(centerX, centerY, IValueToFloat(radius), color);
    }

    [ContextMethod("НарисоватьЭллипс", "DrawEllipse")]
    public void DrawEllipse(int centerX, int centerY, IValue radiusH, IValue radiusV, Color_OS color)
    {
        Raylib.DrawEllipse(centerX, centerY, IValueToFloat(radiusH), IValueToFloat(radiusV), color);
    }

    [ContextMethod("НарисоватьЭллипсЛиния", "DrawEllipseLines")]
    public void DrawEllipseLines(int centerX, int centerY, IValue radiusH, IValue radiusV, Color_OS color)
    {
        Raylib.DrawEllipseLines(centerX, centerY, IValueToFloat(radiusH), IValueToFloat(radiusV), color);
    }

    [ContextMethod("НарисоватьПрямоугольник", "DrawRectangle")]
    public void DrawRectangle(int posX, int posY, int width, int height, Color_OS color)
    {
        Raylib.DrawRectangle(posX, posY, width, height, color);
    }

    [ContextMethod("НарисоватьПрямоугольникЛиния", "DrawRectangleLines")]
    public void DrawRectangleLines(int posX, int posY, int width, int height, Color_OS color)
    {
        Raylib.DrawRectangleLines(posX, posY, width, height, color);
    }

    [ContextMethod("НарисоватьПрямоугольникГрадиентВертикальный", "DrawRectangleGradientV")]
    public void DrawRectangleGradientV(int posX, int posY, int width, int height, Color_OS top, Color_OS bottom)
    {
        Raylib.DrawRectangleGradientV(posX, posY, width, height, top, bottom);
    }

    [ContextMethod("НарисоватьПрямоугольникГрадиентГоризонтальный", "DrawRectangleGradientH")]
    public void DrawRectangleGradientH(int posX, int posY, int width, int height, Color_OS left, Color_OS right)
    {
        Raylib.DrawRectangleGradientH(posX, posY, width, height, left, right);
    }

    [ContextMethod("НарисоватьТреугольник", "DrawTriangle")]
    public void DrawTriangle(IValue v1, IValue v2, IValue v3, Color_OS color)
    {
        Raylib.DrawTriangle(IValueToVector2(v1), IValueToVector2(v2), IValueToVector2(v3), color);
    }

    [ContextMethod("НарисоватьТреугольникЛиния", "DrawTriangleLines")]
    public void DrawTriangleLines(IValue v1, IValue v2, IValue v3, Color_OS color)
    {
        Raylib.DrawTriangleLines(IValueToVector2(v1), IValueToVector2(v2), IValueToVector2(v3), color);
    }

    [ContextMethod("НарисоватьЛинию", "DrawLine")]
    public void DrawLine(int startPosX, int startPosY, int endPosX, int endPosY, Color_OS color)
    {
        Raylib.DrawLine(startPosX, startPosY, endPosX, endPosY, color);
    }

    // Вспомогательные функции

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
