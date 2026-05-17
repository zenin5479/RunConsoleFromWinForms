using Newtonsoft.Json;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculationConsole
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.InputEncoding = System.Text.Encoding.UTF8;
         Console.OutputEncoding = System.Text.Encoding.UTF8;

         // Работаем до тех пор, пока входной поток не будет закрыт родительским процессом
         string line;
         while ((line = Console.ReadLine()) != null)
         {
            ProcessLine(line);
         }
         // При закрытии stdin (WinForms закрывает свой writer) цикл завершается,
         // Main заканчивается, консольное приложение останавливается.
      }

      static void ProcessLine(string json)
      {
         var response = new CalculationResponse();
         try
         {
            var request = JsonConvert.DeserializeObject<CalculationRequest>(json);
            if (request == null || request.Numbers == null || !request.Numbers.Any())
            {
               response.Success = false;
               response.ErrorMessage = "Некорректные входные данные";
            }
            else
            {
               double result = 0;
               var steps = new List<string>();

               switch (request.Operation?.ToLower())
               {
                  case "sum":
                     result = request.Numbers.Sum();
                     steps.Add($"Суммировали {request.Numbers.Count} чисел");
                     break;
                  case "product":
                     result = request.Numbers.Aggregate(1.0, (acc, x) => acc * x);
                     steps.Add("Вычислили произведение");
                     break;
                  case "average":
                     result = request.Numbers.Average();
                     steps.Add("Вычислили среднее арифметическое");
                     break;
                  default:
                     response.Success = false;
                     response.ErrorMessage = $"Операция '{request.Operation}' не поддерживается";
                     break;
               }

               if (response.Success || response.ErrorMessage == null)
               {
                  if (request.Parameters?.RoundResult == true)
                  {
                     result = Math.Round(result, request.Parameters.Precision);
                     steps.Add($"Округлили до {request.Parameters.Precision} знаков");
                  }

                  response.Result = result;
                  response.Steps = steps;
                  response.Success = true;
               }
            }
         }
         catch (Exception ex)
         {
            response.Success = false;
            response.ErrorMessage = $"Ошибка обработки: {ex.Message}";
         }

         // Отправляем результат обратно
         Console.WriteLine(JsonConvert.SerializeObject(response));
      }
   }
}