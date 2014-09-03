using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Whistle.Core.Interfaces;
using Whistle.Core.Services;

namespace Whistle.Core.Helper
{
    public static class ServiceHandler
    {
        static object lockObj;
        private static IHttpClientHelper httpClientHelper;
        static ServiceHandler()
        {
            lockObj = new object();
        }

        private static HttpClient CreateClient()
        {
            if (httpClientHelper == null)
                return new HttpClient();

            return new HttpClient(httpClientHelper.MessageHandler);
        }

        /// <summary>
        /// API method tah will post an object on server using Service API Call
        /// </summary>
        /// <typeparam name="T">A generic object type</typeparam>
        /// <param name="obj">An object will post using API Service</param>
        /// <param name="apiSection">Its related to service or thier action</param>
        /// <param name="value">default value for T</param>
        /// <returns></returns>
        public static async Task<AuthResult> PostAction<T>(T obj, string apiSection, T value = default(T))
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
                    //var temp = new { obj = obj };//email = "sarath@whistle.com", password = "batman" };
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    var content = new StringContent(jsonData);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    string url=API.CreateUrl(apiSection);
                    var result = await client.PostAsync(url, content);
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

    #region WebRequest Extention Method

   
    public static class WebRequestExtensions
    {
        public static Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
        {
            var tcs = new TaskCompletionSource<HttpWebResponse>();

            try
            {
                request.BeginGetResponse(iar =>
                {
                    try
                    {
                        var response = (HttpWebResponse)request.EndGetResponse(iar);
                        tcs.SetResult(response);
                    }
                    catch (Exception exc)
                    {
                        tcs.SetException(exc);
                    }
                }, null);
            }
            catch (Exception exc)
            {
                tcs.SetException(exc);
            }

            return tcs.Task;
        }

        public static Task<Stream> GetRequestStreamAsync(this HttpWebRequest request)
        {
            var tcs = new TaskCompletionSource<Stream>();

            try
            {
                request.BeginGetRequestStream(iar =>
              {
                  try
                  {
                      var response = request.EndGetRequestStream(iar);
                      tcs.SetResult(response);
                  }
                  catch (Exception exc)
                  {
                      tcs.SetException(exc);
                  }
              }, null);
            }
            catch (Exception exc)
            {
                tcs.SetException(exc);
            }

            return tcs.Task;
        }
    }

    #endregion
}
