using GMYEL8_HSZF_2024251.Model.Exceptions;

using Con = System.Console;

namespace GMYEL8_HSZF_2024251.Console.Middleware;

/// <inheritdoc cref="ICustomMiddleware"/>
public class CustomExceptionHandlingMiddleware : ICustomMiddleware
{
	public async Task InvokeAsync(Func<Task> next)
	{
		try
		{
			Task.WaitAll(next());
		}
		catch (BusinessException ex)
		{
			Con.ForegroundColor = ConsoleColor.Red;
			Con.WriteLine($"Business error: {ex.Message}");
			ReturnToMenu();
		}
		catch (Exception ex)
		{
			Con.ForegroundColor = ConsoleColor.Red;
			Con.WriteLine($"Unhandled error: {ex.Message}");
			ReturnToMenu();
		}
	}

	private static void ReturnToMenu()
	{
		Con.ResetColor();
		Con.WriteLine("Press any key to return to the menu...");
		Con.ReadKey();
	}
}
