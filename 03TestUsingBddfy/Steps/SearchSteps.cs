using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using _03TestUsingBddfy.Framework;

namespace _03TestUsingBddfy.Steps
{
   public class SearchSteps
   {
      private HttpResponseMessage _searchResult;
      private string _searchTerm;
      private string _resultAsString;

      public void TheUserSearchesFor(string searchTerm)
      {
         _searchTerm = searchTerm;
      }

      public void SearchApiIsCalled()
      {
         var parameter = new Dictionary<string, string> { { "q", _searchTerm }, { "lang", "en-GB" }, { "currency", "GBP" }, { "store", "1" } };
         _searchResult = SearchApi.SendRequest(parameter, HttpMethod.Get);
         _resultAsString = SearchApi.ReadContentAsString(_searchResult);
      }

      public void TheStatusCodeIs(HttpStatusCode statusCode)
      {
         Assert.That(statusCode, Is.EqualTo(_searchResult.StatusCode));
      }

      public void TheResultMessageContains(string message)
      {
         var json = $"\"colour\":\"{message}\"";
         Assert.That(_resultAsString.Contains(json));
      }
   }
}
