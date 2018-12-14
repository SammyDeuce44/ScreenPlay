using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace _02PageObjectPattern
{
   public class SearchApi
   {
      private const string BaseAddress = "http://searchapi.asos.com";
      private const string UriPath = "/product/search/V1";

      public HttpResponseMessage SendRequest(IDictionary<string, string> parameter, HttpMethod method)
      {
         var handler1 = new HttpClientHandler
         {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
         };

         var client = new HttpClient(handler1);

         var buildUrl = new UriBuilder(BaseAddress) { Path = UriPath, Query = ToQueryString(parameter) };

         var request = new HttpRequestMessage()
         {
            RequestUri = buildUrl.Uri,
            Method = method,
            Headers =
            {
               { HttpRequestHeader.Accept.ToString(), "application/json"},
               { "User-Agent", "api"}
            }
         };

         var response = client.SendAsync(request).Result;

         if (response.IsSuccessStatusCode)
         {
            return response;
         }
         throw new ArgumentException("Error: Did not get a successful response");
      }

      public string ReadContentAsString(HttpResponseMessage response)
      {
         return response.Content.ReadAsStringAsync().Result;
      }

      private string ToQueryString(IDictionary<string, string> dict)
      {
         var list = dict.Select(item => item.Key + "=" + item.Value).ToList();
         return string.Join("&", list);
      }
   }
}
