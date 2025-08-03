using OneScript.Contexts;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using Raylib_cs;

namespace raylibos;

[ContextClass("Рейлиб", "Raylib")]
public class Raylibos : AutoContext<Raylibos>
{

    [ScriptConstructor]
    public static Raylibos Constructor()
    {
        return new Raylibos();
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

    [ContextMethod("НовыйЦвет", "NewColor")]
    public IValue NewColor(int r, int g, int b, int a)
    {
        Color color = new Color(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b), Convert.ToByte(a));
        return COMWrapperContext.Create(color);
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
}
