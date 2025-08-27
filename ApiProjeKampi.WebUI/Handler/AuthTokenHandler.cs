using System.Net.Http.Headers;
namespace ApiProjeKampi.WebUI.Handler
{
    public class AuthTokenHandler : DelegatingHandler

    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancelletionToken)
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            return await base.SendAsync(request, cancelletionToken);
        }
    }
}
