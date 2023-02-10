using Microsoft.AspNetCore.Mvc;

namespace InfotecsIntershipMVC.Controllers
{
    // MyBaseController is inherited from Conroller and in order to enable
    // the expansion of the controller system by providing them with attribute [ApiController].
    // Sourse: https://learn.microsoft.com/ru-ru/aspnet/core/web-api/?view=aspnetcore-3.1
    [ApiController]
    public abstract class MyBaseController : Controller
    {
    }
}
