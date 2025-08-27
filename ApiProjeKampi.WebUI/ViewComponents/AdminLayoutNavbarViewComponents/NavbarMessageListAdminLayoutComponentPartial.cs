using ApiProjeKampi.WebUI.Dtos.ChefDtos;
using ApiProjeKampi.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.AdminLayoutNavbarViewComponents
{
    public class NavbarMessageListAdminLayoutComponentPartial : ViewComponent

    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NavbarMessageListAdminLayoutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Messages/GetMessagesByListTrueFalse");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageByIsReadFalseDto>>(jsondata);
                return View(values);
            }
            return View();
        }
    }
}
