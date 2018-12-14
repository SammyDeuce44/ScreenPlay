using System;

namespace _05TestUsingContext.Framework
{
   /// <summary>
   /// Attribute <c>StepText</c> is used to customise the Gherkin Step Text output that BDDfy produces when running a scenario,
   /// as the default constructed text doesn't always read well and is often cluttered with junk.
   /// <para>Usage:</para>
   /// <para>[StepText("This is must custom step text. {0} is the first parameter, {1} is the second. etc.")]</para>
   /// <para>public void This_Is_My_Step_Method(string param1, string param2) { ... }</para>
   /// </summary>
   public class StepText : Attribute
   {
      public string Text { get; }

      public StepText(string stepText)
      {
         Text = stepText;
      }
   }
}