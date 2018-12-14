using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using TestStack.BDDfy;

namespace _04TestAnnotations.Framework
{
   /// <summary>
   /// Class <c>StepBuilder</c> is used to build BDDfy steps that produce a PrettyPrint Gherkin output
   /// <para>This should be instantiated in the SetUp() of Test Classes</para>
   /// <para>This allows us to use the same methods Given, When, Then, And.
   /// However, instead calls our code which builds the step text from our annotations.</para>
   /// <para>Should be used in combination with StepText[("")] annotation on step methods</para>
   /// </summary>
   public class StepAnnotation<T> where T : class
   {
      private IFluentStepBuilder<T> _fluentStepBuilder;
      public string CallingMethodName { get; set; }
      public T CallingClass { get; }

      public StepAnnotation(T callingClass)
      {
         CallingClass = callingClass;
      }

      public StepAnnotation<T> Given(Expression<Action<T>> step)
      {
         var callingMethod = new StackTrace().GetFrame(1).GetMethod();
         CallingMethodName = callingMethod.Name;

         return GenericStepText(step, "Given");
      }

      public StepAnnotation<T> When(Expression<Action<T>> step)
      {
         return GenericStepText(step, "When");
      }

      public StepAnnotation<T> Then(Expression<Action<T>> step)
      {
         return GenericStepText(step, "Then");
      }

      public StepAnnotation<T> And(Expression<Action<T>> step)
      {
         return GenericStepText(step, "And");
      }

      private StepAnnotation<T> GenericStepText(Expression<Action<T>> step, string stepPrefix)
      {
         var stepTextAttributeArray = (step.Body as MethodCallExpression)?.Method.GetCustomAttributes(typeof(StepText), true) ?? new string[] { };

         if (!stepTextAttributeArray.Any())
         {
            _fluentStepBuilder = _fluentStepBuilder == null ? CallingClass.Given(step) : _fluentStepBuilder.And(step);
            return this;
         }

         var stepText = stepPrefix + " " + ((StepText)stepTextAttributeArray.First()).Text;
         _fluentStepBuilder = _fluentStepBuilder == null ? CallingClass.Given(step, stepText) : _fluentStepBuilder.And(step, stepText);
         return this;
      }

      public void BDDfy()
      {
         _fluentStepBuilder.BDDfy(caller: CallingMethodName);
      }
   }
}