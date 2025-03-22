namespace ZX.Platform.Windows
{
    partial class ScreenForm
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
            pictureScreen = new PictureBox();
            buttonLoad = new Button();
            buttonReset = new Button();
            buttonScreenshot = new Button();
            pictureDebug = new PictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel1 = new Panel();
            comboFiles = new ComboBox();
            textBoxFps = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureScreen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureDebug).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureScreen
            // 
            pictureScreen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureScreen.Location = new Point(3, 3);
            pictureScreen.Name = "pictureScreen";
            pictureScreen.Size = new Size(256, 192);
            pictureScreen.SizeMode = PictureBoxSizeMode.Zoom;
            pictureScreen.TabIndex = 0;
            pictureScreen.TabStop = false;
            pictureScreen.Click += PictureScreen_Click;
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(8, 49);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(75, 23);
            buttonLoad.TabIndex = 1;
            buttonLoad.Text = "Load";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += ButtonLoad_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(8, 8);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(75, 23);
            buttonReset.TabIndex = 2;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += ButtonReset_Click;
            // 
            // buttonScreenshot
            // 
            buttonScreenshot.Location = new Point(8, 148);
            buttonScreenshot.Name = "buttonScreenshot";
            buttonScreenshot.Size = new Size(75, 23);
            buttonScreenshot.TabIndex = 5;
            buttonScreenshot.Text = "Screenshot";
            buttonScreenshot.UseVisualStyleBackColor = true;
            buttonScreenshot.Click += ButtonScreenshot_Click;
            // 
            // pictureDebug
            // 
            pictureDebug.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureDebug.Location = new Point(3, 201);
            pictureDebug.Name = "pictureDebug";
            pictureDebug.Size = new Size(256, 192);
            pictureDebug.SizeMode = PictureBoxSizeMode.Zoom;
            pictureDebug.TabIndex = 6;
            pictureDebug.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Location = new Point(1, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(368, 402);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(pictureScreen, 0, 0);
            tableLayoutPanel2.Controls.Add(pictureDebug, 0, 1);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(262, 396);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(textBoxFps);
            panel1.Controls.Add(comboFiles);
            panel1.Controls.Add(buttonReset);
            panel1.Controls.Add(buttonScreenshot);
            panel1.Controls.Add(buttonLoad);
            panel1.Location = new Point(271, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(94, 396);
            panel1.TabIndex = 1;
            // 
            // comboFiles
            // 
            comboFiles.DropDownWidth = 300;
            comboFiles.FormattingEnabled = true;
            comboFiles.Location = new Point(8, 88);
            comboFiles.Name = "comboFiles";
            comboFiles.Size = new Size(75, 23);
            comboFiles.TabIndex = 6;
            comboFiles.DropDown += ComboFiles_DropDown;
            // 
            // textBoxFps
            // 
            textBoxFps.Location = new Point(8, 201);
            textBoxFps.Name = "textBoxFps";
            textBoxFps.ReadOnly = true;
            textBoxFps.Size = new Size(75, 23);
            textBoxFps.TabIndex = 7;
            // 
            // ScreenForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(367, 403);
            Controls.Add(tableLayoutPanel1);
            KeyPreview = true;
            Name = "ScreenForm";
            Text = "ZX";
            FormClosing += ScreenForm_FormClosing;
            Load += ScreenForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureScreen).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureDebug).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureScreen;
        private Button buttonLoad;
        private Button buttonReset;
        private Button buttonScreenshot;
        private PictureBox pictureDebug;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel1;
        private ComboBox comboFiles;
        private TextBox textBoxFps;
    }
}