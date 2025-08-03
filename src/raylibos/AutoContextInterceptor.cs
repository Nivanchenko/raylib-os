using Castle.DynamicProxy;

namespace raylibos;

public class AutoContextInterceptor : IInterceptor
{
    private readonly object _wrappedObject;

    public AutoContextInterceptor(object wrappedObject)
    {
        _wrappedObject = wrappedObject;
    }

    public void Intercept(IInvocation invocation)
    {
        invocation.ReturnValue = invocation.Method.Invoke(
            _wrappedObject,
            invocation.Arguments
        );
    }
}