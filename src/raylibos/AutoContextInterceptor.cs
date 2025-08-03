using Castle.DynamicProxy;

namespace raylibos;

public class AutoContextInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        // Перенаправляем вызовы к оригинальному объекту
        invocation.Proceed();
    }
}