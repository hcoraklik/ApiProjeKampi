namespace ApiProjeKampi.WebUI.Models
{
    public class GeminiRespons
    {// Gemini'den gelen JSON yanıtını bu C# sınıflarıyla karşılayacağız.
        // Bu sınıfları Controller sınıfınızın içine veya dışına (namespace içine) koyabilirsiniz.
        public class GeminiResponse
        {
            public List<Candidate> Candidates { get; set; }
        }
        public class Candidate
        {
            public Content Content { get; set; }
        }
        public class Content
        {
            public List<Part> Parts { get; set; }
        }
        public class Part
        {
            public string Text { get; set; }
        }
    }
}
