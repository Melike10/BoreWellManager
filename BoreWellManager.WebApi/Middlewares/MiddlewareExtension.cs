namespace BoreWellManager.WebApi.Middlewares
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseMaintenanceMode(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MaintenanceMiddleware>();
        }
    }
}
