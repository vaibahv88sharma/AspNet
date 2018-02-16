using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.ViewComponents
{
    public class LoginLogoutViewComponent: ViewComponent
    {

        // View Component => Not Accessed by URL -- Only Used Internally within Application

        // cshtml => Views => Shared => Components => LoginLogout => .cshtml

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
