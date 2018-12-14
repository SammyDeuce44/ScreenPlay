using System.Net;
using System.Net.Http;
using NUnit.Framework;
using _05TestUsingContext.Framework;

namespace _05TestUsingContext.Steps
{
   public class SearchSteps : BaseSteps
   {
      private readonly StepContext _context;
      private readonly SearchApi _searchApi;

      public SearchSteps(StepContext context)
      {
         _context = context;
         _searchApi = new SearchApi();
      }

      [StepText("a user searches the catalogue for a {0}")]
      public void TheUserSearchesFor(string searchTerm)
      {
         var param = new ApiParameter();
         param.AddDefaultByQuery(searchTerm);
         _context.Set(StepTitle.SearchTerm, param);
      }

      [StepText("a user searches the catalogue for a product")]
      public void TheUserSearchesFor(ApiParameter param)
      {
         _context.Set(StepTitle.SearchTerm, param);
      }

      [StepText("the search API endpoint is called")]
      public void SearchApiIsCalled()
      {
         var response = _searchApi.Execute(_context.Get<ApiParameter>(StepTitle.SearchTerm), HttpMethod.Get);
         var httpUtils = new HttpUtils();
         _context.Set(StepTitle.Response, response);

         if (response.IsSuccessStatusCode)
         {
            _context.Set(StepTitle.SuccessResponse, httpUtils.ReadContentAsString(response));
         }
         else
         {
            _context.Set(StepTitle.FailureResponse, httpUtils.ReadContentAsString(response));
         }
      }

      [StepText("The status code {0} is returned")]
      public void TheStatusCodeIs(HttpStatusCode statusCode)
      {
         Assert.That(statusCode, Is.EqualTo(_context.Get<HttpResponseMessage>(StepTitle.Response).StatusCode));
      }

      [StepText("the message result contains the colour {0}")]
      public void TheResultMessageContainsColour(string query)
      {
         var json = $"\"colour\":\"{query}\"";
         Assert.That(_context.Get<string>(StepTitle.SuccessResponse).Contains(json));
      }

      [StepText("the message result contains the name {0}")]
      public void TheResultMessageContainsName(string query)
      {
         var json = $"\"name\":\"{query}";
         Assert.That(_context.Get<string>(StepTitle.SuccessResponse).Contains(json));
      }

      [StepText("the message result contains the text {0}")]
      public void TheResultMessageContainsText(string query)
      {
         Assert.That(_context.Get<string>(StepTitle.SuccessResponse).Contains(query));
      }

      [StepText("the error contains the text {0}")]
      public void TheErrorContains(string query)
      {
         Assert.That(_context.Get<string>(StepTitle.FailureResponse).Contains(query));
      }

      [StepText("the message result contains the product array")]
      public void TheResultMessageContainsProductArray()
      {
         var json = $"\"products\":[{{\"id\":";
         Assert.That(_context.Get<string>(StepTitle.SuccessResponse).Contains(json));
      }
   }
}