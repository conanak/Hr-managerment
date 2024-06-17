using HRM.Domain.Enums;
using System;
using System.Text;

namespace HRM.Domain
{
    public static class CommonUIService
    {
        public static HttpResponseMessage GetDataAPI(string url, MethodAPI method, string? jsonData = null)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage respone = new HttpResponseMessage();
            if (method == MethodAPI.GET)
            {
                respone = client.GetAsync(url).GetAwaiter().GetResult();
            }
            else if (method == MethodAPI.DELETE)
            {
                respone = client.DeleteAsync(url).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(jsonData) && method == MethodAPI.POST)
            {
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                respone = client.PostAsync(url, content).GetAwaiter().GetResult();
            }
            else if (string.IsNullOrEmpty(jsonData) && method == MethodAPI.PUT)
            {
                respone = client.PutAsync(url, null).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(jsonData) && method == MethodAPI.PUT)
            {
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                respone = client.PutAsync(url, content).GetAwaiter().GetResult();
            }
            return respone;
        }
    }
}

