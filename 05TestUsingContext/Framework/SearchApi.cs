using System;
using System.Net;
using System.Net.Http;

namespace _05TestUsingContext.Framework
{
   internal class SearchApi
   {
      private const string BaseAddress = "http://searchapi.asos.com";
      private const string UriPath = "/product/search/V1";

      public HttpResponseMessage Execute(ApiParameter parameter, HttpMethod method)
      {
         var httpUtils = new HttpUtils();
         var handler1 = new HttpClientHandler
         {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
         };

         var client = new HttpClient(handler1);

         var buildUrl = new UriBuilder(BaseAddress) { Path = UriPath, Query = httpUtils.ToQueryString(parameter) };

         var request = new HttpRequestMessage()
         {
            RequestUri = buildUrl.Uri,
            Method = method,
            Headers =
            {
               {HttpRequestHeader.Accept.ToString(), "application/json"},
               {"User-Agent", "api"}
            }
         };

         return client.SendAsync(request).Result;
      }
   }
}