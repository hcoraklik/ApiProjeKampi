using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    [Route("Admin/[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> CategoryList()
        {
            var client= _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(JsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var jsonData=JsonConvert.SerializeObject(createCategoryDto);
            //var accessToken = Request.Cookies["AuthToken"];
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var responseMessage = await client.PostAsync("Categories", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteCategory (int id)
        {
            var client =_httpClientFactory.CreateClient("ApiHttpClient");
            await client.DeleteAsync("Categories?id=" + id);
            return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Categories/GetCategory?id=" + id);
            var jsonData=await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateCategorydto>(jsonData);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategorydto updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("http://localhost:5155/api/Categories/", stringContent);
            return RedirectToAction("CategoryList");
        }





    }
}
