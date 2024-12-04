namespace GMYEL8_HSZF_2024251.Console.Middleware;

public class MiddlewarePipeline
{
    private readonly List<ICustomMiddleware> _middlewares = [];

    public MiddlewarePipeline Use(ICustomMiddleware middleware)
    {
        _middlewares.Add(middleware);
        return this;
    }

    public async Task ExecuteAsync(Func<Task> finalTask)
    {
        var next = finalTask;

        foreach (var middleware in _middlewares.AsEnumerable().Reverse())
        {
            var currentMiddleware = middleware;
            var nextCopy = next;

            next = () => currentMiddleware.InvokeAsync(nextCopy);
        }

        await next();
    }
}
