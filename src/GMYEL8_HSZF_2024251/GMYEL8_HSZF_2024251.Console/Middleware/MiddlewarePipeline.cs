namespace GMYEL8_HSZF_2024251.Console.Middleware;

/// <inheritdoc cref="IMiddlewarePipeline"/>
public class MiddlewarePipeline : IMiddlewarePipeline
{
	private readonly List<ICustomMiddleware> _middlewares = [];

	public IMiddlewarePipeline Use(ICustomMiddleware middleware)
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
