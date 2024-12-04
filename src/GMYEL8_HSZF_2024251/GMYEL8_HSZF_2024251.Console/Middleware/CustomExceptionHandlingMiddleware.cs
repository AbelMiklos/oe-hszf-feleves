using GMYEL8_HSZF_2024251.Model.Exceptions;

using Con = System.Console;

namespace GMYEL8_HSZF_2024251.Console.Middleware;

public class CustomExceptionHandlingMiddleware : ICustomMiddleware
{
    public async Task InvokeAsync(Func<Task> next)
    {
        try
        {
            await next();
        }
        catch (BusinessException ex)
        {
            Con.ForegroundColor = ConsoleColor.Red;
            Con.WriteLine($"Business error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Con.ForegroundColor = ConsoleColor.Red;
            Con.WriteLine($"Unhandled error: {ex.Message}");
        }
        finally
        {
            Con.ResetColor();
        }
    }
}
