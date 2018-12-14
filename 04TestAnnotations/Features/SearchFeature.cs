using System.Net;
using NUnit.Framework;
using TestStack.BDDfy;
using _04TestAnnotations.Framework;
using _04TestAnnotations.Steps;

namespace _04TestAnnotations.Features
{
   [Story(AsA = "User", IWant = "Search", SoThat = "I get specific results")]
   public class SearchFeature
   {
      private SearchSteps _step;
      private StepAnnotation<SearchFeature> _annotate;

      [SetUp]
      public void Setup()
      {
         _annotate = new StepAnnotation<SearchFeature>(this);
         _step = new SearchSteps();
      }

      [TearDown]
      public void TearDown()
      {
         _annotate = null;
         _step = null;
      }

      [Test]
      public void Endpoint_Returns_200()
      {
         _annotate.Given(_ => _step.TheUserSearchesFor("dress"))
            .When(_ => _step.SearchApiIsCalled())
            .Then(_ => _step.TheStatusCodeIs(HttpStatusCode.OK))
            .BDDfy();
      }

      [Test]
      public void Result_Message_Is_Returned()
      {
         _annotate.Given(_ => _step.TheUserSearchesFor("dress"))
            .When(_ => _step.SearchApiIsCalled())
            .Then(_ => _step.TheStatusCodeIs(HttpStatusCode.OK))
            .And(_ => _step.TheResultMessageContainsColour("Red"))
            .BDDfy();
      }

      [Test]
      public void Result_Message_Contains_Name_Watch()
      {
         _annotate.Given(_ => _step.TheUserSearchesFor("Watch"))
            .When(_ => _step.SearchApiIsCalled())
            .Then(_ => _step.TheStatusCodeIs(HttpStatusCode.OK))
            .And(_ => _step.TheResultMessageContainsName("Casio"))
            .BDDfy();
      }

      [Test]
      public void Result_Message_Contains_Colour_Pink()
      {
         _annotate.Given(_ => _step.TheUserSearchesFor("Pink dress"))
            .When(_ => _step.SearchApiIsCalled())
            .Then(_ => _step.TheStatusCodeIs(HttpStatusCode.OK))
            .And(_ => _step.TheResultMessageContainsColour("Pink"))
            .BDDfy();
      }

      [Test]
      public void Result_Message_Contains_Brand_FCUK()
      {
         _annotate.Given(_ => _step.TheUserSearchesFor("French Connection"))
            .When(_ => _step.SearchApiIsCalled())
            .Then(_ => _step.TheStatusCodeIs(HttpStatusCode.OK))
            .And(_ => _step.TheResultMessageContainsText("-fcuk-"))
            .BDDfy();
      }

      [Test]
      public void Unsuccessful_When_No_SearchTerm_Is_Sent()
      {
         var param = new ApiParameter();
         param.AddCurrency("GBP").AddLanguage("en-GB").AddStore("1");

         _annotate.Given(_ => _step.TheUserSearchesFor(param))
            .When(_ => _step.SearchApiIsCalled())
            .Then(_ => _step.TheStatusCodeIs(HttpStatusCode.BadRequest))
            .And(_ => _step.TheErrorContains("The QueryTerm field is required"))
            .BDDfy();
      }
   }
}
