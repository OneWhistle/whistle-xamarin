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
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using ModernHttpClient;
using Whistle.Core.Helpers;
using Whistle.Core.Modal;

namespace Whistle.Core.Helper
{
    public static class ServiceHandler
    {
        static object lockObj;
        //private static IHttpClientHelper httpClientHelper;

        //private static IHttpClientHelper HttpClientHelper
        //{
        //    get { return httpClientHelper ?? (httpClientHelper = Mvx.GetSingleton<IHttpClientHelper>()); }
        //}
        static ServiceHandler()
        {
            lockObj = new object();
        }

        private static HttpClient CreateClient()
        {
            return new HttpClient(new NativeMessageHandler());
        }

        /// <summary>
        /// API method tah will post an object on server using Service API Call
        /// </summary>
        /// <typeparam name="T">A generic object type</typeparam>
        /// <param name="obj">An object will post using API Service</param>
        /// <param name="apiSection">Its related to service or thier action</param>
        /// <param name="value">default value for T</param>
        /// <returns></returns>
        public static async Task<ServiceResult<TResponse>> PostAction<TRequest, TResponse>(TRequest obj, string apiSection) where TResponse : class
        {
            TResponse output = null;
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

                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                    var content = new StringContent(jsonData);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    Uri url = new Uri(API.CreateUrl(apiSection));

                    var result = await client.PostAsync(url, content);
                    var response = await result.Content.ReadAsStringAsync();

                    Mvx.Trace(MvxTraceLevel.Diagnostic, "Result: {0} / {1}", result.StatusCode.ToString(), response);
                    if (!result.IsSuccessStatusCode)
                    {
                        var err = JsonConvert.DeserializeObject<ErrorResponse>(response);
                        return new ServiceResult<TResponse>(err);
                    }
                    output = JsonConvert.DeserializeObject<TResponse>(response);
                }
                catch (Exception ex)
                {
                    Mvx.Trace(Cirrious.CrossCore.Platform.MvxTraceLevel.Error, ex.Message);
                    return new ServiceResult<TResponse>(new ErrorResponse { Msg = ex.Message });
                }
            }

            return new ServiceResult<TResponse>(output);
        }
    }
}
