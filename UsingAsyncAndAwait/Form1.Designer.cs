namespace UsingAsyncAndAwait
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
			BtnCalculate = new Button();
			LblResult = new Label();
			SuspendLayout();
			// 
			// BtnCalculate
			// 
			BtnCalculate.Location = new Point(325, 133);
			BtnCalculate.Name = "BtnCalculate";
			BtnCalculate.Size = new Size(94, 29);
			BtnCalculate.TabIndex = 0;
			BtnCalculate.Text = "Calculate";
			BtnCalculate.UseVisualStyleBackColor = true;
			BtnCalculate.Click += BtnCalculate_Click;
			// 
			// LblResult
			// 
			LblResult.AutoSize = true;
			LblResult.Location = new Point(325, 77);
			LblResult.Name = "LblResult";
			LblResult.Size = new Size(50, 20);
			LblResult.TabIndex = 1;
			LblResult.Text = "label1";
			LblResult.Click += label1_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(LblResult);
			Controls.Add(BtnCalculate);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button BtnCalculate;
		private Label LblResult;
	}
}
