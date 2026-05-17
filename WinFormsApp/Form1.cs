using Newtonsoft.Json;
using SharedLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
   public partial class Form1 : Form
   {
      // Процесс консольного приложения
      private Process consoleProcess;
      private StreamWriter consoleInput;
      private StreamReader consoleOutput;

      public Form1()
      {
         InitializeComponent();
         Load += Form1_Load;
         FormClosing += Form1_FormClosing;
      }

      // Обработчик нажатия кнопки (синхронный, без многопоточности!)
      private void btnCalculate_Click(object sender, EventArgs e)
      {
         try
         {
            // Собираем запрос
            var numbers = txtNumbers.Text
               .Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
               .Select(s => double.Parse(s.Trim()))
               .ToList();

            var request = new CalculationRequest
            {
               Operation = txtOperation.Text.Trim(),
               Numbers = numbers,
               Parameters = new RequestParameters
               {
                  RoundResult = chkRound.Checked,
                  Precision = (int)nudPrecision.Value
               }
            };

            string jsonRequest = JsonConvert.SerializeObject(request);

            // Отправляем в консоль
            consoleInput.WriteLine(jsonRequest);
            consoleInput.Flush(); // обязательно отправляем данные

            // Читаем ответ (строка JSON)
            string jsonResponse = consoleOutput.ReadLine();
            if (jsonResponse == null)
            {
               lblResult.Text = "Консольное приложение завершилось неожиданно.";
               return;
            }

            var response = JsonConvert.DeserializeObject<CalculationResponse>(jsonResponse);

            if (response.Success)
            {
               lblResult.Text = $"Результат: {response.Result}\r\n" +
                                string.Join("\r\n", response.Steps);
            }
            else
            {
               lblResult.Text = $"Ошибка: {response.ErrorMessage}";
            }
         }
         catch (Exception ex)
         {
            lblResult.Text = $"Ошибка: {ex.Message}";
         }
      }

      private void StartConsoleApp()
      {
         var startInfo = new ProcessStartInfo
         {
            FileName = "CalculationConsole.exe",
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = false,
            CreateNoWindow = false,            // показывать окно консоли
            //WindowStyle = ProcessWindowStyle.Normal // можно не указывать
         };

         consoleProcess = new Process { StartInfo = startInfo };
         consoleProcess.Start();

         consoleInput = consoleProcess.StandardInput;
         consoleOutput = consoleProcess.StandardOutput;
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         // Запускаем консоль при загрузке формы
         StartConsoleApp();
      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         // Закрываем stdin консоли – это сигнал для неё завершиться
         if (consoleInput != null)
         {
            consoleInput.Close(); // закрывает поток и даёт консоли прочитать null
         }

         // Ждём завершения консольного процесса (необязательно, но корректно)
         if (consoleProcess != null && !consoleProcess.HasExited)
         {
            consoleProcess.WaitForExit(3000); // максимум 3 секунды
            if (!consoleProcess.HasExited)
               consoleProcess.Kill(); // на всякий случай
            consoleProcess.Close();
         }
      }
   }
}