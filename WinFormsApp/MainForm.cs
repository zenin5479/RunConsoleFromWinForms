using Newtonsoft.Json;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
   public partial class MainForm : Form
   {
      // Процесс консольного приложения
      private Process _consoleProcess;
      private StreamWriter _consoleInput;
      private StreamReader _consoleOutput;

      public MainForm()
      {
         InitializeComponent();
      }

      private void MainForm_Load(object sender, EventArgs e)
      {
         ComboBoxOperation.SelectedIndex = 0;
      }

      // Обработчик нажатия кнопки (синхронный, без многопоточности)
      private void btnCalculate_Click(object sender, EventArgs e)
      {
         // Запускаем консоль
         StartConsole();

         try
         {
            // Собираем запрос
            List<double> numbers = txtNumbers.Text
               .Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => double.Parse(s.Trim()))
               .ToList();



            CalculationRequest request = new CalculationRequest
            {


               Operation = ComboBoxOperation.SelectedItem.ToString(),
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
            // Обязательно отправляем данные
            _consoleInput.Flush();

            // Читаем ответ (строка JSON)
            string jsonResponse = _consoleOutput.ReadLine();
            if (jsonResponse == null)
            {
               lblResult.Text = @"Консоль упала";
               return;
            }

            CalculationResponse response = JsonConvert.DeserializeObject<CalculationResponse>(jsonResponse);

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

         // Закрываем консоль
         ClosingConsole();
      }

      private void StartConsole()
      {
         ProcessStartInfo startInfo = new ProcessStartInfo
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
      }

      private void ClosingConsole()
      {
         // Закрываем stdin консоли – это сигнал для неё завершиться
         if (_consoleInput != null)
         {
            // Закрывает поток и даёт консоли прочитать null
            _consoleInput.Close();
         }

         // Ждём завершения консольного процесса (необязательно, но корректно)
         if (_consoleProcess != null && !_consoleProcess.HasExited)
         {
            // Максимум 1 секунда
            _consoleProcess.WaitForExit(1000);
            if (!_consoleProcess.HasExited)
            {
               // На всякий случай
               _consoleProcess.Kill();
            }

            _consoleProcess.Close();
         }
      }


   }
}