namespace WinFormsApp
{
   partial class MainForm
   {
      /// <summary>
      ///  Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      ///  Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      ///  Required method for Designer support - do not modify
      ///  the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         txtOperation = new System.Windows.Forms.TextBox();
         txtNumbers = new System.Windows.Forms.TextBox();
         chkRound = new System.Windows.Forms.CheckBox();
         nudPrecision = new System.Windows.Forms.NumericUpDown();
         btnCalculate = new System.Windows.Forms.Button();
         lblResult = new System.Windows.Forms.Label();
         labelOperation = new System.Windows.Forms.Label();
         labelNumbers = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)nudPrecision).BeginInit();
         SuspendLayout();
         // 
         // txtOperation
         // 
         txtOperation.Location = new System.Drawing.Point(12, 27);
         txtOperation.Name = "txtOperation";
         txtOperation.Size = new System.Drawing.Size(100, 23);
         txtOperation.TabIndex = 0;
         txtOperation.Text = "сумма";
         // 
         // txtNumbers
         // 
         txtNumbers.Location = new System.Drawing.Point(12, 71);
         txtNumbers.Name = "txtNumbers";
         txtNumbers.Size = new System.Drawing.Size(100, 23);
         txtNumbers.TabIndex = 1;
         txtNumbers.Text = "12345";
         // 
         // chkRound
         // 
         chkRound.AutoSize = true;
         chkRound.Location = new System.Drawing.Point(118, 8);
         chkRound.Name = "chkRound";
         chkRound.Size = new System.Drawing.Size(140, 19);
         chkRound.TabIndex = 2;
         chkRound.Text = "Округлить результат";
         chkRound.UseVisualStyleBackColor = true;
         // 
         // nudPrecision
         // 
         nudPrecision.Location = new System.Drawing.Point(184, 27);
         nudPrecision.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
         nudPrecision.Name = "nudPrecision";
         nudPrecision.Size = new System.Drawing.Size(74, 23);
         nudPrecision.TabIndex = 3;
         nudPrecision.Value = new decimal(new int[] { 2, 0, 0, 0 });
         // 
         // btnCalculate
         // 
         btnCalculate.Location = new System.Drawing.Point(12, 100);
         btnCalculate.Name = "btnCalculate";
         btnCalculate.Size = new System.Drawing.Size(100, 23);
         btnCalculate.TabIndex = 4;
         btnCalculate.Text = "Вычислить";
         btnCalculate.UseVisualStyleBackColor = true;
         btnCalculate.Click += btnCalculate_Click;
         // 
         // lblResult
         // 
         lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         lblResult.Location = new System.Drawing.Point(12, 126);
         lblResult.Name = "lblResult";
         lblResult.Size = new System.Drawing.Size(246, 65);
         lblResult.TabIndex = 5;
         lblResult.Text = "Результат: ";
         // 
         // labelOperation
         // 
         labelOperation.AutoSize = true;
         labelOperation.Location = new System.Drawing.Point(12, 9);
         labelOperation.Name = "labelOperation";
         labelOperation.Size = new System.Drawing.Size(58, 15);
         labelOperation.TabIndex = 6;
         labelOperation.Text = "Действие";
         // 
         // labelNumbers
         // 
         labelNumbers.AutoSize = true;
         labelNumbers.Location = new System.Drawing.Point(12, 53);
         labelNumbers.Name = "labelNumbers";
         labelNumbers.Size = new System.Drawing.Size(70, 15);
         labelNumbers.TabIndex = 7;
         labelNumbers.Text = "Операторы";
         // 
         // MainForm
         // 
         AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
         AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         ClientSize = new System.Drawing.Size(270, 200);
         Controls.Add(labelNumbers);
         Controls.Add(labelOperation);
         Controls.Add(lblResult);
         Controls.Add(btnCalculate);
         Controls.Add(nudPrecision);
         Controls.Add(chkRound);
         Controls.Add(txtNumbers);
         Controls.Add(txtOperation);
         MaximizeBox = false;
         MinimizeBox = false;
         Name = "MainForm";
         StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         Text = "Калькулятор";
         Load += Form1_Load;
         ((System.ComponentModel.ISupportInitialize)nudPrecision).EndInit();
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private System.Windows.Forms.TextBox txtOperation;
      private System.Windows.Forms.TextBox txtNumbers;
      private System.Windows.Forms.CheckBox chkRound;
      private System.Windows.Forms.NumericUpDown nudPrecision;
      private System.Windows.Forms.Button btnCalculate;
      private System.Windows.Forms.Label lblResult;
      private System.Windows.Forms.Label labelOperation;
      private System.Windows.Forms.Label labelNumbers;
   }
}
