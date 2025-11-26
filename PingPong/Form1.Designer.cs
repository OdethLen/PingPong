namespace PingPong
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
            btnStart = new Button();
            btnEnd = new Button();
            panel = new Panel();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStart.Location = new Point(313, 37);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(149, 33);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start Game";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnEnd
            // 
            btnEnd.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEnd.Location = new Point(313, 87);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(149, 33);
            btnEnd.TabIndex = 2;
            btnEnd.Text = "Exit";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // panel
            // 
            panel.Location = new Point(90, 157);
            panel.Name = "panel";
            panel.Size = new Size(601, 100);
            panel.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 303);
            Controls.Add(panel);
            Controls.Add(btnEnd);
            Controls.Add(btnStart);
            Name = "Form1";
            Text = "Refresh";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button btnStart;
        private Button btnEnd;
        private Panel panel;
    }
}
