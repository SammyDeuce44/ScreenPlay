using NUnit.Framework;

namespace _05TestUsingContext.Features
{
   public class BaseFeature
   {
      [OneTimeSetUp]
      public void MasterSetup()
      {
      }

      [OneTimeTearDown]
      public void MasterTearDown()
      {
      }
   }
}
