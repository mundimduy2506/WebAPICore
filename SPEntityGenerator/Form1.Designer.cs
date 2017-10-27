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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.txt_Namespace = new System.Windows.Forms.TextBox();
            this.label_Namespace = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStoreProc = new System.Windows.Forms.TextBox();
            this.btnPull = new System.Windows.Forms.Button();
            this.radButtonSP = new System.Windows.Forms.RadioButton();
            this.radButtonView = new System.Windows.Forms.RadioButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(387, 685);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(104, 31);
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
            this.dataGridView1.Location = new System.Drawing.Point(8, 158);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(888, 152);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(8, 24);
            this.txtConnectionString.Margin = new System.Windows.Forms.Padding(2);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(783, 66);
            this.txtConnectionString.TabIndex = 2;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(803, 67);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(67, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load SP";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Connection String:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(8, 487);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 28;
            this.dataGridView2.Size = new System.Drawing.Size(888, 183);
            this.dataGridView2.TabIndex = 5;
            // 
            // label_Parameter
            // 
            this.label_Parameter.AutoSize = true;
            this.label_Parameter.Location = new System.Drawing.Point(5, 472);
            this.label_Parameter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Parameter.Name = "label_Parameter";
            this.label_Parameter.Size = new System.Drawing.Size(60, 13);
            this.label_Parameter.TabIndex = 6;
            this.label_Parameter.Text = "Parameters";
            // 
            // label_ClassName
            // 
            this.label_ClassName.AutoSize = true;
            this.label_ClassName.Location = new System.Drawing.Point(292, 421);
            this.label_ClassName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ClassName.Name = "label_ClassName";
            this.label_ClassName.Size = new System.Drawing.Size(66, 13);
            this.label_ClassName.TabIndex = 7;
            this.label_ClassName.Text = "Class Name:";
            // 
            // txt_ClassName
            // 
            this.txt_ClassName.Location = new System.Drawing.Point(295, 436);
            this.txt_ClassName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_ClassName.Name = "txt_ClassName";
            this.txt_ClassName.Size = new System.Drawing.Size(245, 20);
            this.txt_ClassName.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 143);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Stored Procedures:";
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(803, 103);
            this.btn_Browse.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(67, 22);
            this.btn_Browse.TabIndex = 10;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.Btn_Browse_Click);
            // 
            // txt_SaveFolder
            // 
            this.txt_SaveFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_SaveFolder.Location = new System.Drawing.Point(8, 105);
            this.txt_SaveFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txt_SaveFolder.Name = "txt_SaveFolder";
            this.txt_SaveFolder.Size = new System.Drawing.Size(783, 20);
            this.txt_SaveFolder.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Save Directory:";
            // 
            // txt_Namespace
            // 
            this.txt_Namespace.Location = new System.Drawing.Point(8, 436);
            this.txt_Namespace.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Namespace.Name = "txt_Namespace";
            this.txt_Namespace.Size = new System.Drawing.Size(245, 20);
            this.txt_Namespace.TabIndex = 14;
            // 
            // label_Namespace
            // 
            this.label_Namespace.AutoSize = true;
            this.label_Namespace.Location = new System.Drawing.Point(5, 420);
            this.label_Namespace.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Namespace.Name = "label_Namespace";
            this.label_Namespace.Size = new System.Drawing.Size(67, 13);
            this.label_Namespace.TabIndex = 13;
            this.label_Namespace.Text = "Namespace:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 335);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Particular Retrieval:";
            // 
            // txtStoreProc
            // 
            this.txtStoreProc.Location = new System.Drawing.Point(168, 358);
            this.txtStoreProc.Name = "txtStoreProc";
            this.txtStoreProc.Size = new System.Drawing.Size(286, 20);
            this.txtStoreProc.TabIndex = 16;
            // 
            // btnPull
            // 
            this.btnPull.Location = new System.Drawing.Point(168, 384);
            this.btnPull.Name = "btnPull";
            this.btnPull.Size = new System.Drawing.Size(62, 23);
            this.btnPull.TabIndex = 17;
            this.btnPull.Text = "Pull";
            this.btnPull.UseVisualStyleBackColor = true;
            this.btnPull.Click += new System.EventHandler(this.btnPull_Click);
            // 
            // radButtonSP
            // 
            this.radButtonSP.AutoSize = true;
            this.radButtonSP.Location = new System.Drawing.Point(9, 19);
            this.radButtonSP.Name = "radButtonSP";
            this.radButtonSP.Size = new System.Drawing.Size(108, 17);
            this.radButtonSP.TabIndex = 19;
            this.radButtonSP.TabStop = true;
            this.radButtonSP.Text = "Stored Procedure";
            this.radButtonSP.UseVisualStyleBackColor = true;
            // 
            // radButtonView
            // 
            this.radButtonView.AutoSize = true;
            this.radButtonView.Location = new System.Drawing.Point(9, 42);
            this.radButtonView.Name = "radButtonView";
            this.radButtonView.Size = new System.Drawing.Size(48, 17);
            this.radButtonView.TabIndex = 20;
            this.radButtonView.TabStop = true;
            this.radButtonView.Text = "View";
            this.radButtonView.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 727);
            this.splitter1.TabIndex = 21;
            this.splitter1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(302, 322);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(299, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "========================*========================";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radButtonSP);
            this.groupBox1.Controls.Add(this.radButtonView);
            this.groupBox1.Location = new System.Drawing.Point(18, 351);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 64);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SQL Type";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 727);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.btnPull);
            this.Controls.Add(this.txtStoreProc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Namespace);
            this.Controls.Add(this.label_Namespace);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "SP Entity Generator";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox txt_Namespace;
        private System.Windows.Forms.Label label_Namespace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStoreProc;
        private System.Windows.Forms.Button btnPull;
        private System.Windows.Forms.RadioButton radButtonSP;
        private System.Windows.Forms.RadioButton radButtonView;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

