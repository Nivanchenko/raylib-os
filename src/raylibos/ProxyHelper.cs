using Castle.DynamicProxy;

namespace raylibos;

public static class ProxyHelper
{
    public static T CreateWrapper<T>(object originalObject) where T : class
    {
        var proxyGenerator = new ProxyGenerator();

        var proxy = proxyGenerator.CreateClassProxy<T>(
            new ProxyGenerationOptions(),
            new AutoContextInterceptor(originalObject)
        );

        return proxy;
    }
}