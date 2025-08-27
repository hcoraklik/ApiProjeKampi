using ApiProjeKampi.WebUI.Dtos.ChefDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    public class ChefController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChefController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> ChefList()
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Chefs");
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultChefDto>>(JsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateChef()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChef(CreateChefDto createChefDto)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var jsonData = JsonConvert.SerializeObject(createChefDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("Chefs", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ChefList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteChef(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            await client.DeleteAsync("Chefs?id=" + id);
            return RedirectToAction("ChefList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateChef(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Chefs/GetChef?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateChefDto>(jsonData);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateChef(UpdateChefDto updateChefdto)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var jsonData = JsonConvert.SerializeObject(updateChefdto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("Chefs/", stringContent);
            return RedirectToAction("ChefList");
        }
    }
}
