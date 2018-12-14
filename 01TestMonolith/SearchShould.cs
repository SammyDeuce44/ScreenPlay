using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using NUnit.Framework;

namespace _01TestMonolith
{
   public class SearchShould
   {
      private const string BaseAddress = "http://searchapi.asos.com";
      private const string Path = "/product/search/V1";

      [Test]
      public void Google_Endpoint_Returns_200()
      {
         const string searchTerm = "dress";
         var parameter = new Dictionary<string, string> { { "q", searchTerm }, { "lang", "en-GB" }, { "currency", "GBP" }, { "store", "1" } };

         var searchResult = SearchApi(Path, parameter, HttpMethod.Get);

         Assert.That(searchResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
      }

      [Test]
      public void Google_Endpoint_Returns_Specific_Site()
      {
         const string searchTerm = "red dress";
         var parameter = new Dictionary<string, string> { { "q", searchTerm }, { "lang", "en-GB" }, { "currency", "GBP" }, { "store", "1" } };

         var searchResult = SearchApi(Path, parameter, HttpMethod.Get);
         var resultAsString = ReadContentAsString(searchResult);

         Assert.That(searchResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
         Assert.That(resultAsString.Contains("\"colour\":\"Red\""));
      }

      public HttpResponseMessage SearchApi(string path, IDictionary<string, string> parameter, HttpMethod method)
      {
         var handler1 = new HttpClientHandler
         {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
         };

         var client = new HttpClient(handler1);

         var buildUrl = new UriBuilder(BaseAddress) { Path = path, Query = ToQueryString(parameter) };

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

      private string ReadContentAsString(HttpResponseMessage response)
      {
         return response.Content.ReadAsStringAsync().Result;
      }

      private static string ToQueryString(IDictionary<string, string> dict)
      {
         var list = dict.Select(item => item.Key + "=" + item.Value).ToList();
         return string.Join("&", list);
      }
   }
}
