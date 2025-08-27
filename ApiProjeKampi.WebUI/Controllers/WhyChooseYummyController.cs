using ApiProjeKampi.WebUI.Dtos.ServiceDtos;
using ApiProjeKampi.WebUI.Dtos.WhyChooseYummyDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    public class WhyChooseYummyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WhyChooseYummyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> WhyChooseYummyList()
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Services");
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultWhyChooseYummyDto>>(JsonData);
                return View(values);
            }
            return View();
        } 

        [HttpGet]
        public IActionResult CreateWhyChooseYummy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWhyChooseYummy(CreateWhyChooseYummyDto createWhyChooseYummyDto)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var jsonData = JsonConvert.SerializeObject(createWhyChooseYummyDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("Services", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("WhyChooseYummyList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteWhyChooseYummy(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            await client.DeleteAsync("Services?id=" + id);
            return RedirectToAction("WhyChooseYummyList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateWhyChooseYummy(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Services/GetService?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetWhyChooseYummyByIdDto>(jsonData);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateWhyChooseYummy(UpdateWhyChooseYummyDto updateWhyChooseYummyDto)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var jsonData = JsonConvert.SerializeObject(updateWhyChooseYummyDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("Services/", stringContent);
            return RedirectToAction("WhyChooseYummyList");
        }
    }
}
