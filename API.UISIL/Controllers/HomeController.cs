using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace API.UISIL.Controllers
{
    public class HomeController : Controller
    {

        private readonly IApiDescriptionGroupCollectionProvider _ApiExpler;


        public HomeController(IApiDescriptionGroupCollectionProvider apiexplores)
        {
            _ApiExpler = apiexplores;

        }

        [HttpGet("/")]
        public IActionResult Inicio()
        {

            var endpoints = _ApiExpler.ApiDescriptionGroups.Items.SelectMany(grupo => grupo.Items)

                .Where(endpoint =>
                endpoint.ActionDescriptor is ControllerActionDescriptor
                )

                .Where(endpoint => ((ControllerActionDescriptor)
                endpoint.ActionDescriptor)
                .ControllerName != "Home"
                )

                .OrderBy(endpoint =>
                ((ControllerActionDescriptor)
                endpoint.ActionDescriptor)
                .ControllerName)

                .ThenBy(endpoint => 
                endpoint.RelativePath)

                .ToList();





            return View(endpoints);
        }
    }
}
