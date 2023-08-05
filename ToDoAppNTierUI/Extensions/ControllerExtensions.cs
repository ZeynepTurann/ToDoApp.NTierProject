using Microsoft.AspNetCore.Mvc;
using ToDoAppNTierCommon.ResponseObjects;

namespace ToDoAppNTierUI.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ResponseRedirectToAction<T>(this Controller controller,IResponse<T> response,string actionName)
        {
            if (response.ResponseType==ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            if (response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    controller.ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    
                }
                return controller.View(response.Data);
            }
            controller.TempData["message"] = response.Message;
            return controller.RedirectToAction(actionName);
        }
        public static IActionResult ResponseView<T>(this Controller controller,IResponse<T> response)
        {
            if (response.ResponseType == ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            return controller.View(response.Data);
        }
        public static IActionResult ResponseRedirectToAction(this Controller controller, IResponse response, string actionName)
        {
            if (response.ResponseType == ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            return controller.RedirectToAction(actionName);
        }
    }
}
