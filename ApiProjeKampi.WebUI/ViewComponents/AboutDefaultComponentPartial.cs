using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents
{
    public class AboutDefaultComponentPartial:ViewComponent

    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
