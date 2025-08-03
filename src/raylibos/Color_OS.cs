using OneScript.Contexts;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using Raylib_cs;
using Castle.DynamicProxy;

namespace raylibos;

[ContextClass("ЦветРЛ", "ColorRL")]
public class Color_OS : AutoContext<Color_OS>
{
    private Color _originalObject;

    public Color_OS(Color originalObject)
    {
        _originalObject = originalObject;
    }

    public Color_OS()
    {

    }

    [ScriptConstructor]
    public static Color_OS Constructor(int r, int g, int b, int a)
    {
        Color color = new Color(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b), Convert.ToByte(a));
        Color_OS color_OS = ProxyHelper.CreateWrapper<Color_OS>(color);

        return color_OS;
    }

    public static implicit operator Color(Color_OS wrapper)
        => wrapper._originalObject;
}