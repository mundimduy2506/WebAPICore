namespace SPEntityGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label_Parameter = new System.Windows.Forms.Label();
            this.label_ClassName = new System.Windows.Forms.Label();
            this.txt_ClassName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.txt_SaveFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(581, 1038);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(156, 47);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "GENERATE";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 258);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1332, 306);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(12, 37);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(1172, 100);
            this.txtConnectionString.TabIndex = 2;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(1205, 37);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(100, 33);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load SP";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Connection String:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 653);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 28;
            this.dataGridView2.Size = new System.Drawing.Size(1332, 304);
            this.dataGridView2.TabIndex = 5;
            // 
            // label_Parameter
            // 
            this.label_Parameter.AutoSize = true;
            this.label_Parameter.Location = new System.Drawing.Point(8, 630);
            this.label_Parameter.Name = "label_Parameter";
            this.label_Parameter.Size = new System.Drawing.Size(91, 20);
            this.label_Parameter.TabIndex = 6;
            this.label_Parameter.Text = "Parameters";
            // 
            // label_ClassName
            // 
            this.label_ClassName.AutoSize = true;
            this.label_ClassName.Location = new System.Drawing.Point(8, 982);
            this.label_ClassName.Name = "label_ClassName";
            this.label_ClassName.Size = new System.Drawing.Size(98, 20);
            this.label_ClassName.TabIndex = 7;
            this.label_ClassName.Text = "Class Name:";
            // 
            // txt_ClassName
            // 
            this.txt_ClassName.Location = new System.Drawing.Point(112, 982);
            this.txt_ClassName.Name = "txt_ClassName";
            this.txt_ClassName.Size = new System.Drawing.Size(388, 26);
            this.txt_ClassName.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Stored Procedures:";
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(1205, 161);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(100, 29);
            this.btn_Browse.TabIndex = 10;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.Btn_Browse_Click);
            // 
            // txt_SaveFolder
            // 
            this.txt_SaveFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_SaveFolder.Location = new System.Drawing.Point(12, 162);
            this.txt_SaveFolder.Name = "txt_SaveFolder";
            this.txt_SaveFolder.Size = new System.Drawing.Size(1172, 26);
            this.txt_SaveFolder.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Save Directory:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 1109);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_SaveFolder);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_ClassName);
            this.Controls.Add(this.label_ClassName);
            this.Controls.Add(this.label_Parameter);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnGenerate);
            this.Name = "Form1";
            this.Text = "SP Entity Generator";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label_Parameter;
        private System.Windows.Forms.Label label_ClassName;
        private System.Windows.Forms.TextBox txt_ClassName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.TextBox txt_SaveFolder;
        private System.Windows.Forms.Label label3;
    }
}

