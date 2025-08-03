using Castle.DynamicProxy;
using OneScript.Contexts;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace raylibos;

public static class ProxyHelper
{
    private static readonly ProxyGenerator _generator = new ProxyGenerator();

    public static T CreateWrapper<T>(params object[] constructorArgs) 
        where T : AutoContext<T>
    {
        // Создаем прокси, передавая аргументы в конструктор
        var proxy = _generator.CreateClassProxy<T>(
            new ProxyGenerationOptions(),
            constructorArgs,
            new AutoContextInterceptor());
        
        return proxy;
    }
}