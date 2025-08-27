using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using ApiProjeKampi.WebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ProductList()
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(JsonData);
                return View(values);
            }
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Categories");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            //List<SelectListItem> CategoryValues = (from category in categories
            //                                       select new SelectListItem
            //                                       {
            //                                           Text = category.CategoryName,
            //                                           Value = category.CategoryId.ToString()

            //                                       }).ToList();

            List<SelectListItem> categoryValues = new List<SelectListItem>();

            foreach (var category in categories)
            {
                categoryValues.Add(
                    new SelectListItem() { Text = category.CategoryName,Value=category.CategoryId.ToString() } );
            }



            ViewBag.v = categoryValues;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("Products/CreateProductWithCategory", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            await client.DeleteAsync("Products?id=" + id);
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var responseMessage = await client.GetAsync("Products/GetProducts?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductdto)
        {
            var client = _httpClientFactory.CreateClient("ApiHttpClient");
            var jsonData = JsonConvert.SerializeObject(updateProductdto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("Products/", stringContent);
            return RedirectToAction("ProductList");
        }
    }
}
