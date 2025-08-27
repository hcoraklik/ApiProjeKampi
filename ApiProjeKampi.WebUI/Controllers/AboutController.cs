using ApiProjeKampi.WebUI.Dtos.AboutDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> AboutList()
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Abouts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(JsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var jsonData = JsonConvert.SerializeObject(createAboutDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("Abouts", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AboutList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteAbout(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            await client.DeleteAsync("Abouts?id=" + id);
            return RedirectToAction("AboutList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Abouts/GetAbout?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateAboutDto>(jsonData);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutdto)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var jsonData = JsonConvert.SerializeObject(updateAboutdto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("Abouts/", stringContent);
            return RedirectToAction("AboutList");
        }
     
    }
}
