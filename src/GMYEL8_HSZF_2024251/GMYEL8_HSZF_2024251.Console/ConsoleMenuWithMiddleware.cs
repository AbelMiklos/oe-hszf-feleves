using ConsoleTools;

using GMYEL8_HSZF_2024251.Console.Middleware;

namespace GMYEL8_HSZF_2024251.Console;

public class ConsoleMenuWithMiddleware(IMiddlewarePipeline middlewarePipeline) : ConsoleMenu
{
	private readonly IMiddlewarePipeline _middlewarePipeline = middlewarePipeline;

	public ConsoleMenuWithMiddleware Add(string title, Func<Task> action)
	{
		base.Add(title, async () => await _middlewarePipeline.ExecuteAsync(action));
		return this;
	}
}
