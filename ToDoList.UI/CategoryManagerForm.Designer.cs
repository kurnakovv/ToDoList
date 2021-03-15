
namespace ToDoList.UI
{
    partial class CategoryManagerForm
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
            this.CancelBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.AddBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(13, 13);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(30, 23);
            this.CancelBtn.TabIndex = 0;
            this.CancelBtn.Text = "<---";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title});
            this.dataGridView1.Location = new System.Drawing.Point(13, 74);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(143, 364);
            this.dataGridView1.TabIndex = 1;
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.DeleteBtn);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.UpdateBtn);
            this.panel1.Location = new System.Drawing.Point(162, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 216);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tasks";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(4, 70);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(250, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Title";
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DeleteBtn.Location = new System.Drawing.Point(163, 190);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(91, 23);
            this.DeleteBtn.TabIndex = 1;
            this.DeleteBtn.Text = "Delete category";
            this.DeleteBtn.UseVisualStyleBackColor = false;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(250, 20);
            this.textBox1.TabIndex = 2;
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.UpdateBtn.Location = new System.Drawing.Point(4, 190);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(85, 23);
            this.UpdateBtn.TabIndex = 0;
            this.UpdateBtn.Text = "Save category";
            this.UpdateBtn.UseVisualStyleBackColor = false;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // AddBtn
            // 
            this.AddBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.AddBtn.Location = new System.Drawing.Point(13, 45);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(81, 23);
            this.AddBtn.TabIndex = 3;
            this.AddBtn.Text = "Add category";
            this.AddBtn.UseVisualStyleBackColor = false;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // CategoryManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(429, 450);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.CancelBtn);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(445, 489);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(445, 489);
            this.Name = "CategoryManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CategoryManagerForm";
            this.Load += new System.EventHandler(this.CategoryManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
    }
}