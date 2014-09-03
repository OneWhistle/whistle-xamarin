using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Whistle.Core.Interfaces;

namespace Whistle.Core.Services
{
    /// <summary>
    /// Caution: This class will be moved soon, to a better place..
    /// </summary>
    public class AuthResult
    {
        public bool Success { get; set; }
        // need some property there..
    }

    public interface IAuthenticationService
    {
        Task<AuthResult> Authenticate(string email, string password);
        Task<AuthResult> Authenticate(string socialNetwork);
    }
    /// <summary>
    /// Simple authentication utility calling rest services...
    /// http://stackoverflow.com/questions/21029416/mvvm-cross-rest-service-post-and-get
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        internal const string AUTH_FB = "{3D4E2F26-F06F-462C-A6E5-E66B0494BDEE}"; // random..
        internal const string AUTH_GPLUS = "{23C69149-2576-46FB-9BF8-F03B67F0B615}"; // same...

        private IHttpClientHelper httpClientHelper;
        public AuthenticationService(IHttpClientHelper httpClientHelper = null)
        {
            this.httpClientHelper = httpClientHelper;
        }

        private HttpClient CreateClient()
        {
            if (httpClientHelper == null)
                return new HttpClient();

            return new HttpClient(httpClientHelper.MessageHandler);
        }

        /// <summary>
        /// Classic authentication
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <returns></returns>
        public async Task<AuthResult> Authenticate(string email, string password)
        {
            /// If you want to see the streams on fiddler
            /// http://stackoverflow.com/questions/21554235/how-to-setup-fiddler-and-genymotion
            using (var client = CreateClient())
            {
                try
                {
                    if (client.DefaultRequestHeaders.CacheControl == null)
                        client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

                    client.DefaultRequestHeaders.CacheControl.NoCache = true;
                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
                    client.DefaultRequestHeaders.CacheControl.NoStore = true;
                    client.Timeout = new TimeSpan(0, 0, 30);

                    var dynamicContent = new { email = email, password = password };
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicContent);
                    var content = new StringContent(jsonData);
                    System.Diagnostics.Debug.WriteLine(jsonData);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = await client.PostAsync(API.CreateUrl(ApiSection.User + "/", ApiAction.LOGIN), content);
                    if (!result.IsSuccessStatusCode)
                    {
                        return new AuthResult();
                    }
                    var response = await result.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                  
                }

            }

            return new AuthResult { Success = true };
        }
        /// <summary>
        /// Lazy authentication..
        /// </summary>
        /// <param name="socialNetwork"></param>
        /// <returns></returns>
        public async Task<AuthResult> Authenticate(string socialNetwork)
        {
            switch (socialNetwork)
            {
                case AUTH_FB:
                    break;
                case AUTH_GPLUS:
                    break;
                default:
                    throw new InvalidOperationException();
            }
            await Task.Delay(50);
            return new AuthResult();
        }
    }
}
