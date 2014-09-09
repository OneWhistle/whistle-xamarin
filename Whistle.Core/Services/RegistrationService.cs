
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Whistle.Core.Interfaces;
using Whistle.Core.Modal;

namespace Whistle.Core.Services
{
    public interface IRegistrationService
    {
        void PartialRegister(/*missing params*/);
        void FinishRegister(/*missing params*/);
        Task<AuthResult> Registration(User user);
        void Done();
    }

    public class RegistrationService: IRegistrationService
    {
        private IHttpClientHelper httpClientHelper;
        public void PartialRegister()
        {
            /*here we'll save partial registration data*/
        }
        public void FinishRegister()
        {
            /*Here we'll complete the partial registration
            after what we'll call done.
            */
            Done();
        }

        public void Done()
        {
            // there we'll call the backend api..
        }

        private HttpClient CreateClient()
        {
            if (httpClientHelper == null)
                return new HttpClient();

            return new HttpClient(httpClientHelper.MessageHandler);
        }

        public async Task<AuthResult> Registration(User user)
        {
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

                    // temporary:
                    var temp = new {user= user};
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(temp);
                    System.Diagnostics.Debug.WriteLine(jsonData);
                    var content = new StringContent(jsonData);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = await client.PostAsync(API.CreateUrl(ApiSection.User), content);
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
    }
}
