namespace GMYEL8_HSZF_2024251.Console.Middleware;

/// <summary>
///     Represents a pipeline for middleware components.
/// </summary>
public interface IMiddlewarePipeline
{
    /// <summary>
    ///     Adds a middleware component to the pipeline.
    /// </summary>
    /// <param name="middleware"> The middleware component to add. </param>
    /// <returns> The updated middleware pipeline. </returns>
    IMiddlewarePipeline Use(ICustomMiddleware middleware);

    /// <summary>
    ///     Executes the middleware pipeline.
    /// </summary>
    /// <param name="finalTask"> The final task to execute after all middleware components. </param>
    Task ExecuteAsync(Func<Task> finalTask);
}
