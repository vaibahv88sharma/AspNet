using Microsoft.AspNetCore.Mvc;
using OdeToFood.Services;

namespace OdeToFood.ViewComponents
{

    // View Component => Not Accessed by URL -- Only Used Internally within Application

    // cshtml => Views => Shared => Components => Greeter => .cshtml

    public class GreeterViewComponent : ViewComponent
    {
        #region Dependency Injection
        private IGreeter _greeter;
        public GreeterViewComponent(IGreeter greeter)
        {
            _greeter = greeter;
        }
        #endregion

        public IViewComponentResult Invoke()
        {
            var model = _greeter.GetMessageOfTheDay();
            return View("Default", model);
        }
    }
}
