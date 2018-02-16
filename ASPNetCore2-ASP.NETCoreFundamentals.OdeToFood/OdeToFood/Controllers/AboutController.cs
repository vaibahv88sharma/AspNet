using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    // Attribute based Routing at CONTROLLER level => HIERARCHICAL level
    // [Route("about")]
        //  OR
    // [Route("[controller]")]

    // Attribute based Routing at CONTROLLER level
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        public string Phone()
        {
            return "1+555+555+5555";
        }

        // Attribute based Routing at ACTION level => HIERARCHICAL level
        // [Route("address")]
        //  OR
        // [Route("[action]")]
        public string Address()
        {
            return "USA";
        }
    }
}
