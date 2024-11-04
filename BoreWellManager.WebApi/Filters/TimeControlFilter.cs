using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BoreWellManager.WebApi.Filters
{
    public class TimeControlFilter:ActionFilterAttribute
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var now = DateTime.Now.TimeOfDay;
            StartTime = "09:00";
            EndTime = "18:00";

            if (now >= TimeSpan.Parse(StartTime) && now <= TimeSpan.Parse(EndTime))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "iş saatleri dışında bu işlem yapılamaz!",
                    StatusCode=403
                };
            }
        }
    }
}
