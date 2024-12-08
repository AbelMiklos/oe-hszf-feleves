namespace GMYEL8_HSZF_2024251.Console.Middleware;

/// <summary>
///     Represents a custom middleware component.
/// </summary>
public interface ICustomMiddleware
{
    /// <summary>
    ///     Method to invoke the middleware logic.
    /// </summary>
    /// <param name="next"> A function that represents the next middleware in the pipeline. </param>
    Task InvokeAsync(Func<Task> next);
}
