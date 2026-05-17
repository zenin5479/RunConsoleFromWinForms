using System.Collections.Generic;

namespace SharedLibrary
{
   public class CalculationRequest
   {
      public string Operation { get; set; }
      public List<double> Numbers { get; set; }
      public RequestParameters Parameters { get; set; }
   }
}