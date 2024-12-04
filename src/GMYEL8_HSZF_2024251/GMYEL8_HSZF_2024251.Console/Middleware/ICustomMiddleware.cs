namespace GMYEL8_HSZF_2024251.Console.Middleware;

/// <summary>
///     
/// </summary>
public interface ICustomMiddleware
{
    /// <summary>
    ///     
    /// </summary>
    /// <param name="next"></param>
    /// <returns></returns>
    Task InvokeAsync(Func<Task> next);
}
