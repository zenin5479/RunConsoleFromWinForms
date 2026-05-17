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
      private Process _consoleProcess;
      private StreamWriter _consoleInput;
      private StreamReader _consoleOutput;
      private StreamReader _consoleError;

      public Form1()
      {
         InitializeComponent();
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
            _consoleInput.WriteLine(jsonRequest);
            _consoleInput.Flush(); // обязательно отправляем данные

            // Читаем ответ (строка JSON)
            //string jsonResponse = consoleOutput.ReadLine();
            //if (jsonResponse == null)
            //{
            //   lblResult.Text = "Консольное приложение завершилось неожиданно.";
            //   return;
            //}

            string jsonResponse = _consoleOutput.ReadLine();
            if (jsonResponse == null)
            {
               string errorText = _consoleError.ReadToEnd();
               lblResult.Text = string.Format(@"Консоль упала. Ошибка: {0}", errorText);
               return;
            }

            var response = JsonConvert.DeserializeObject<CalculationResponse>(jsonResponse);

            if (response.Success)
            {
               lblResult.Text = string.Format("Результат: {0}\r\n", response.Result) + string.Join("\r\n", response.Steps);
            }
            else
            {
               lblResult.Text = string.Format(@"Ошибка: {0}", response.ErrorMessage);
            }
         }
         catch (Exception ex)
         {
            lblResult.Text = string.Format(@"Ошибка: {0}", ex.Message);
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
            // Включить если нужно читать ошибки консоли или false, если не нужно
            RedirectStandardError = true,
            // Показывать окно консоли или true, если окно не нужно
            CreateNoWindow = false,
            // Можно не указывать
            WindowStyle = ProcessWindowStyle.Normal,
            // Устанавливаем UTF-8 для обоих потоков
            //StandardInputEncoding = System.Text.Encoding.UTF8,
            StandardOutputEncoding = System.Text.Encoding.UTF8,
            // Можно вообще убрать StandardErrorEncoding
            StandardErrorEncoding = System.Text.Encoding.UTF8
         };

         _consoleProcess = new Process { StartInfo = startInfo };
         _consoleProcess.Start();

         // Создаём StreamWriter БЕЗ BOM не потребуется вызывать Flush() после каждой записи
         _consoleInput = new StreamWriter(_consoleProcess.StandardInput.BaseStream, new System.Text.UTF8Encoding(false))
         {
            AutoFlush = true
         };

         _consoleOutput = _consoleProcess.StandardOutput;
         // Добавили поле в класс
         _consoleError = _consoleProcess.StandardError;
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         // Запускаем консоль при загрузке формы
         StartConsoleApp();
      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         // Закрываем stdin консоли – это сигнал для неё завершиться
         if (_consoleInput != null)
         {
            _consoleInput.Close(); // закрывает поток и даёт консоли прочитать null
         }

         // Ждём завершения консольного процесса (необязательно, но корректно)
         if (_consoleProcess != null && !_consoleProcess.HasExited)
         {
            _consoleProcess.WaitForExit(3000); // максимум 3 секунды
            if (!_consoleProcess.HasExited)
               _consoleProcess.Kill(); // на всякий случай
            _consoleProcess.Close();
         }
      }
   }
}