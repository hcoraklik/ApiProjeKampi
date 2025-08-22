using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using static ApiProjeKampi.WebUI.Models.GeminiRespons;

namespace ApiProjeKampi.WebUI.Controllers
{

    public class AIController : Controller
    {

        private readonly IConfiguration _configuration;

        public AIController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult CreateRecipeWithGemini()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipeWithGemini(string prompt)
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                // Hata durumunda Error view'ına doğru modeli gönderiyoruz.
                // Bu kodun çalışması için ApiProjeKampi.WebUI.Models.Errors.ErrorViewModel modelinizin olması gerekir.
                // Eğer yoksa, bu satırı ViewBag kullanarak hata mesajını aynı view'da göstermekle değiştirebilirsiniz.
                // Şimdilik varsayalım ki ErrorViewModel mevcut.
                var errorViewModel = new ApiProjeKampi.WebUI.Models.ErrorViewModel
                {
                    Message = "Gemini API anahtarı yapılandırılmamış."
                };
                return View("Error", errorViewModel);
            }

            var model = "gemini-1.5-flash-latest";
            var apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

            // 1. GÜNCELLEME: Sistem mesajını prompt'un başına ekledik.
            var fullPrompt = "Sen bir restoran için yemek önerileri yapan bir yapay zeka aracısın. Amacımız kullanıcı tarafından girilen malzemelere göre yemek tarifi önerisinde bulunmak. Kullanıcının malzemeleri şunlar: " + prompt;

            var requestData = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = fullPrompt } } }
                },
                // 2. GÜNCELLEME: Temperature ayarını ekledik.
                generationConfig = new
                {
                    temperature = 0.5
                }
            };

            using var client = new HttpClient();
            var jsonContent = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                // 3. GÜNCELLEME: Cevabı artık kendi model sınıflarımızla, daha temiz karşılıyoruz.
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // JSON'daki "candidates" ile C#'daki "Candidates" eşleşsin
                };
                var result = await response.Content.ReadFromJsonAsync<GeminiResponse>(options);

                // Cevabın içinden metni güvenli bir şekilde al
                var recipeText = result?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

                ViewBag.Recipe = recipeText ?? "Yapay zekadan geçerli bir tarif alınamadı.";
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"API Hatası: {response.StatusCode} - {errorBody}";
            }

            // DÜZELTME YAPILAN YER
            // return View("CreateRecipe"); yerine, doğru view adını veya boş View() metodunu kullanın.
            return View(); // Metod adı ile view adı aynı olduğu için bu yeterlidir.
            // veya alternatif olarak:
            // return View("CreateRecipeWithGemini");
        }
    }
}