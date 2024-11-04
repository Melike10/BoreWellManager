namespace BoreWellManager.WebApi.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Security.Claims;

    public class IsResponsibleFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            // Kullanıcının giriş yapıp yapmadığını kontrol ediyoruz
            if (user?.Identity?.IsAuthenticated == true)
            {
                // Kullanıcının IsResponsible özelliğini alıyoruz
                var isResponsibleClaim = user.FindFirst("IsResponsible")?.Value;

                if (isResponsibleClaim != null && bool.TryParse(isResponsibleClaim, out bool isResponsible) && isResponsible)
                {
                    // Kullanıcı IsResponsible özelliğine sahip ve true ise, işleme devam et
                    base.OnActionExecuting(context);
                    return;
                }
            }

            // Kullanıcı yetkili değilse 403 Forbidden döndür
            context.Result = new ForbidResult();
        }
    }

}
