using System.Collections.Generic;

namespace SharedLibrary
{
   public class CalculationRequest
   {
      public string Operation { get; set; }
      public List<double> Numbers { get; set; }
      public RequestParameters Parameters { get; set; }
   }

   public class RequestParameters
   {
      public bool RoundResult { get; set; }
      public int Precision { get; set; } = 2;
   }

   public class CalculationResponse
   {
      public double Result { get; set; }
      public List<string> Steps { get; set; } = new List<string>();
      public bool Success { get; set; }
      public string ErrorMessage { get; set; }
   }
}