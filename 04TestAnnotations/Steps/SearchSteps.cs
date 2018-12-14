using System.Net;
using System.Net.Http;
using NUnit.Framework;
using _04TestAnnotations.Framework;

namespace _04TestAnnotations.Steps
{
   public class SearchSteps
   {

      private HttpResponseMessage _searchResult;
      private ApiParameter _parameter;
      private string _resultAsString;
      private string _errorAsString;

      [StepText("a user searches the catalogue for a {0}")]
      public void TheUserSearchesFor(string searchTerm)
      {
         _parameter = new ApiParameter();
         _parameter.AddDefaultByQuery(searchTerm);
      }

      [StepText("a user searches the catalogue for a product")]
      public void TheUserSearchesFor(ApiParameter param)
      {
         _parameter = param;
      }

      [StepText("the search API endpoint is called")]
      public void SearchApiIsCalled()
      {
         var searchApi = new SearchApi();
         var httpUtils = new HttpUtils();
         _searchResult = searchApi.Execute(_parameter, HttpMethod.Get);

         if (_searchResult.IsSuccessStatusCode)
         {
            _resultAsString = httpUtils.ReadContentAsString(_searchResult);
         }
         else
         {
            _errorAsString = httpUtils.ReadContentAsString(_searchResult);
         }
      }

      [StepText("The status code {0} is returned")]
      public void TheStatusCodeIs(HttpStatusCode statusCode)
      {
         Assert.That(statusCode, Is.EqualTo(_searchResult.StatusCode));
      }

      [StepText("the message result contains the colour {0}")]
      public void TheResultMessageContainsColour(string query)
      {
         var json = $"\"colour\":\"{query}\"";
         Assert.That(_resultAsString.Contains(json));
      }

      [StepText("the message result contains the name {0}")]
      public void TheResultMessageContainsName(string query)
      {
         var json = $"\"name\":\"{query}";
         Assert.That(_resultAsString.Contains(json));
      }

      [StepText("the message result contains the text {0}")]
      public void TheResultMessageContainsText(string query)
      {
         Assert.That(_resultAsString.Contains(query));
      }

      public void TheErrorContains(string query)
      {
         Assert.That(_errorAsString.Contains(query));
      }
   }
}
