using ApiProjeKampi.WebUI.Dtos.ServiceDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;

namespace ApiProjeKampi.WebUI.ViewComponents
{
    public class ServiceDefaultComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client= _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5155/api/Services/");
            if(responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsondata);
                return View(values);
            }
            return View();
        }

    }
}
