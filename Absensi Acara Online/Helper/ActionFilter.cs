using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Absensi.Helper
{
    //public class ActionFilter : IAsyncActionFilter
    //{
    //    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //    {

    //        var Controller = context.Controller as Controller;

    //        var PermsKey = await Cache.CachePerms(context.HttpContext);
    //        Controller.ViewBag.Perms = PermsKey;

    //        Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor actiondesc = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor);
    //        string actionName = actiondesc.ActionName.ToLower();
    //        string controllerName = actiondesc.ControllerName.ToLower();
    //        string requiredPermission = String.Format("{0}-{1}", controllerName, actionName);


    //        var notIncludePermission = new List<string>() {
    //            "home-index", "account-index", "brand-getfile"
    //        };

    //        var notIncludeController = new List<string>() {
    //            "account", "home",
    //        };

    //        if (!notIncludeController.Contains(controllerName) &&
    //            !notIncludePermission.Contains(requiredPermission) &&
    //            CheckPerms.CheckPermis(context.HttpContext.User.GetTheRole(), PermsKey, requiredPermission) == false)
    //        {
    //            context.Result = new RedirectToActionResult("Notfound", "Account", null);
    //        }
    //        else
    //        {
    //            await next();
    //        }
    //    }
























































    //    public void OnActionExecutedAsync(ActionExecutedContext context)
    //    {
    //        // do something after the action executes
    //    }
    //}
}
