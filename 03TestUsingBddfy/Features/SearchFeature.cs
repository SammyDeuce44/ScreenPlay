using System.Net;
using NUnit.Framework;
using TestStack.BDDfy;
using _03TestUsingBddfy.Steps;

namespace _03TestUsingBddfy.Features
{
   [Story(AsA = "User", IWant = "Search", SoThat = "I get specific results")]
   public class SearchFeature
   {
      private SearchSteps _step;

      [SetUp]
      public void Setup()
      {
         _step = new SearchSteps();
      }

      [Test]
      public void Endpoint_Returns_200()
      {
         this.Given(_ => _step.TheUserSearchesFor("dress"))
            .When(_ => _step.SearchApiIsCalled())
            .Then(_ => _step.TheStatusCodeIs(HttpStatusCode.OK))
            .BDDfy();
      }

      [Test]
      public void Result_Message_Contains_Colour_Red()
      {
         this.Given(_ => _step.TheUserSearchesFor("dress"))
            .When(_ => _step.SearchApiIsCalled())
            .Then(_ => _step.TheStatusCodeIs(HttpStatusCode.OK))
            .And(_ => _step.TheResultMessageContains("Red"))
            .BDDfy();
      }
   }
}