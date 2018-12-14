using System;
using System.Collections.Generic;

namespace _05TestUsingContext.Framework
{
   public class StepContext
   {
      private readonly IDictionary<StepTitle, object> _dict;

      public StepContext()
      {
         _dict = new Dictionary<StepTitle, object>();
      }

      public void Set(StepTitle key, object value)
      {
         _dict[key] = value;
      }

      public T Get<T>(StepTitle key)
      {
         if (_dict.ContainsKey(key))
         {
            return (T)_dict[key];
         }

         throw new ArgumentException("Error: Could not ", key.ToString());
      }

      public void ClearContext()
      {
         _dict.Clear();
      }

      public StepContext AddCustomer()
      {
         return this;
      }

      public StepContext AddEnvironment()
      {
         return this;
      }

      public StepContext AddSavedItem()
      {
         return this;
      }

      public StepContext AddProducts()
      {
         return this;
      }
   }
}