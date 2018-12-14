using System.Collections.Generic;

namespace _04TestAnnotations.Framework
{
   public class ApiParameter
   {
      private readonly Dictionary<string, string> _parameter;

      public ApiParameter()
      {
         _parameter = new Dictionary<string, string>();
      }

      public ApiParameter AddQuery(string param)
      {
         _parameter.Add("q", param);
         return this;
      }

      public ApiParameter AddLanguage(string param)
      {
         _parameter.Add("lang", param);
         return this;
      }

      public ApiParameter AddCurrency(string param)
      {
         _parameter.Add("currency", param);
         return this;
      }

      public ApiParameter AddStore(string param)
      {
         _parameter.Add("store", "1");
         return this;
      }

      public void AddDefaultByQuery(string param)
      {
         _parameter.Clear();
         _parameter.Add("q", param);
         _parameter.Add("lang", "en-GB");
         _parameter.Add("currency", "GBP");
         _parameter.Add("store", "1");
      }

      public Dictionary<string, string> GetParameters()
      {
         return _parameter;
      }
   }
}