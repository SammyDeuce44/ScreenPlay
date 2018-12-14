using System.Linq;
using System.Net.Http;

namespace _04TestAnnotations.Framework
{
   public class HttpUtils
   {
      public string ReadContentAsString(HttpResponseMessage response)
      {
         return response.Content.ReadAsStringAsync().Result;
      }

      public string ToQueryString(ApiParameter parameter)
      {
         var list = parameter.GetParameters().Select(item => item.Key + "=" + item.Value).ToList();
         return string.Join("&", list);
      }
   }
}