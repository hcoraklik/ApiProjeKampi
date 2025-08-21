﻿using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using ApiProjeKampi.WebUI.Dtos.ImageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GalleryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> ImageList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5155/api/Images");
            if (responseMessage.IsSuccessStatusCode)
            {
                var JsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultImageDto>>(JsonData);
                return View(values);
            }
            return View();
        }
    }

}

