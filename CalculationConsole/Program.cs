using Newtonsoft.Json;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculationConsole
{
   class Program
   {
      static void Main()
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
         // Main заканчивается, консольное приложение останавливается
      }

      static void ProcessLine(string json)
      {
         CalculationResponse response = new CalculationResponse();
         try
         {
            CalculationRequest request = JsonConvert.DeserializeObject<CalculationRequest>(json);
            if (request == null || request.Numbers == null || !request.Numbers.Any())
            {
               response.Success = false;
               response.ErrorMessage = "Некорректные входные данные";
            }
            else
            {
               double result = 0;
               List<string> steps = new List<string>();

               if (request.Operation?.ToLower() == "сумма")
               {
                  result = request.Numbers.Sum();
                  steps.Add($"Суммировали {request.Numbers.Count} чисел");
               }
               else if (request.Operation?.ToLower() == "product")
               {
                  result = request.Numbers.Aggregate(1.0, (acc, x) => acc * x);
                  steps.Add("Вычислили произведение");
               }
               else if (request.Operation?.ToLower() == "average")
               {
                  result = request.Numbers.Average();
                  steps.Add("Вычислили среднее арифметическое");
               }
               else
               {
                  response.Success = false;
                  response.ErrorMessage = $"Операция '{request.Operation}' не поддерживается";
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
            response.ErrorMessage = string.Format("Ошибка обработки: {0}", ex.Message);
         }

         // Отправляем результат обратно
         Console.WriteLine(JsonConvert.SerializeObject(response));
      }
   }
}