using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using NUnit.Framework;

namespace _02PageObjectPattern
{
   public class SearchFeature
   {
      private HttpResponseMessage _searchResult;
      private SearchApi _searchApi;

      [SetUp]
      public void Setup()
      {
         _searchApi = new SearchApi();
         const string searchTerm = "dress";
         var parameter = new Dictionary<string, string> { { "q", searchTerm }, { "lang", "en-GB" }, { "currency", "GBP" }, { "store", "1" } };
         _searchResult = _searchApi.SendRequest(parameter, HttpMethod.Get);
      }

      [Test]
      public void Google_Endpoint_Returns_200()
      {
         Assert.That(_searchResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
      }

      [Test]
      public void Google_Endpoint_Returns_Specific_Site()
      {
         var resultAsString = _searchApi.ReadContentAsString(_searchResult);

         Assert.That(_searchResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
         Assert.That(resultAsString.Contains("\"colour\":\"Red\""));
      }
   }
}
