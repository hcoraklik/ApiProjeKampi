using ApiProjeKampi.WebUI.Dtos.AuthDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API'ye isteği atıyoruz
            var response = await client.PostAsync("http://localhost:5155/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoginResponseDto>(json);

                if (data == null)
                {
                    return BadRequest("DataBoşOlamaz");
                }
                string token = data.AccessToken;

                // Cookie'de token sakla
                Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddMinutes(60) // süresini API'den de alabilirsin
                });

                // Admin paneline yönlendir
                return RedirectToAction("WhyChooseYummyList", "WhyChooseYummy");
            }
            else
            {
                ViewBag.Error = "Kullanıcı adı veya şifre hatalı";
                return View();
            }
        }
    }
}
