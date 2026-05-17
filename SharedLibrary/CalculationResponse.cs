using System.Collections.Generic;

namespace SharedLibrary
{
   public class CalculationResponse
   {
      public double Result { get; set; }
      public List<string> Steps { get; set; } = new List<string>();
      public bool Success { get; set; }
      public string ErrorMessage { get; set; }
   }
}