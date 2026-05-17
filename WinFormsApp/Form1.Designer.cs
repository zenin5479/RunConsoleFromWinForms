namespace WinFormsApp
{
   partial class Form1
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
         ((System.ComponentModel.ISupportInitialize)nudPrecision).BeginInit();
         SuspendLayout();
         // 
         // txtOperation
         // 
         txtOperation.Location = new System.Drawing.Point(12, 12);
         txtOperation.Name = "txtOperation";
         txtOperation.Size = new System.Drawing.Size(100, 23);
         txtOperation.TabIndex = 0;
         txtOperation.Text = "sum";
         // 
         // txtNumbers
         // 
         txtNumbers.Location = new System.Drawing.Point(12, 41);
         txtNumbers.Name = "txtNumbers";
         txtNumbers.Size = new System.Drawing.Size(100, 23);
         txtNumbers.TabIndex = 1;
         txtNumbers.Text = "12345";
         // 
         // chkRound
         // 
         chkRound.AutoSize = true;
         chkRound.Location = new System.Drawing.Point(118, 14);
         chkRound.Name = "chkRound";
         chkRound.Size = new System.Drawing.Size(140, 19);
         chkRound.TabIndex = 2;
         chkRound.Text = "Округлить результат";
         chkRound.UseVisualStyleBackColor = true;
         // 
         // nudPrecision
         // 
         nudPrecision.Location = new System.Drawing.Point(118, 41);
         nudPrecision.Name = "nudPrecision";
         nudPrecision.Size = new System.Drawing.Size(120, 23);
         nudPrecision.TabIndex = 3;
         // 
         // Form1
         // 
         AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
         AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         ClientSize = new System.Drawing.Size(668, 408);
         Controls.Add(nudPrecision);
         Controls.Add(chkRound);
         Controls.Add(txtNumbers);
         Controls.Add(txtOperation);
         Name = "Form1";
         Text = "Form1";
         ((System.ComponentModel.ISupportInitialize)nudPrecision).EndInit();
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private System.Windows.Forms.TextBox txtOperation;
      private System.Windows.Forms.TextBox txtNumbers;
      private System.Windows.Forms.CheckBox chkRound;
      private System.Windows.Forms.NumericUpDown nudPrecision;
   }
}
