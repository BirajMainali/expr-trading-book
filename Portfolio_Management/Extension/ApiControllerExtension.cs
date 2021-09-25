using Microsoft.AspNetCore.Mvc;

namespace Portfolio_Management.Extension
{
    public static class ApiControllerExtension
    {
        public static IActionResult SendSuccess(this ControllerBase controller, string notify, object data = null)
        {
            return controller.Ok(new
            {
                notify,
                data = data
            });
        }

        public static IActionResult SendError(this ControllerBase controller, string error)
        {
            return controller.BadRequest(new
            {
                error = new
                {
                    message = error
                }
            });
        }
        
    }
}