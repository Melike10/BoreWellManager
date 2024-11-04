using BoreWellManager.Business.Operations.Setting;

namespace BoreWellManager.WebApi.Middlewares
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ISettingService _settingService;
        public MaintenanceMiddleware(RequestDelegate next/*,ISettingService settingService*/)
        {
            _next = next;
            //_settingService = settingService;
        }
        public async Task Invoke(HttpContext context) {
            var _settingService = context.RequestServices.GetRequiredService<ISettingService>();
            bool maintenenceMode = _settingService.GetMaintenenceStat();

            if(context.Request.Path.StartsWithSegments("/api/Auth/login") || context.Request.Path.StartsWithSegments("/api/Settings") )
            {
                await _next(context);
                return;
            }

            if (maintenenceMode) {
                await context.Response.WriteAsync("Şu anda hizmet veremiyoruz.");
            }
            else {
            await _next(context);
            }
        }

    }
}
