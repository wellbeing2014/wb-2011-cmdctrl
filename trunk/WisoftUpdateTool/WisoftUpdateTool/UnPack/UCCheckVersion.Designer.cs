/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/10
 * 时间: 11:07
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace WisoftUpdateTool
{
	partial class UCCheckVersion
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button2 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(599, 443);
			this.panel1.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.textBox2);
			this.groupBox2.Controls.Add(this.treeView1);
			this.groupBox2.Location = new System.Drawing.Point(3, 193);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(593, 247);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "更新文件备份";
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(539, 18);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(48, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "浏览";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 18);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "备份路径：";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(87, 20);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(446, 21);
			this.textBox2.TabIndex = 4;
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.treeView1.CheckBoxes = true;
			this.treeView1.Location = new System.Drawing.Point(6, 47);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(581, 194);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Controls.Add(this.textBox5);
			this.groupBox1.Controls.Add(this.textBox4);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textBox3);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(593, 184);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "数据库选项";
			// 
			// radioButton2
			// 
			this.radioButton2.Location = new System.Drawing.Point(87, 18);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(94, 24);
			this.radioButton2.TabIndex = 8;
			this.radioButton2.Text = "sqlserver";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2CheckedChanged);
			// 
			// radioButton1
			// 
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(20, 18);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(94, 24);
			this.radioButton1.TabIndex = 8;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "oracle";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.RadioButton2CheckedChanged);
			// 
			// textBox5
			// 
			this.textBox5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.textBox5.Location = new System.Drawing.Point(92, 124);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(154, 21);
			this.textBox5.TabIndex = 3;
			this.textBox5.Visible = false;
			// 
			// textBox4
			// 
			this.textBox4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.textBox4.Location = new System.Drawing.Point(92, 100);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(154, 21);
			this.textBox4.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label6.Location = new System.Drawing.Point(6, 122);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(92, 23);
			this.label6.TabIndex = 5;
			this.label6.Text = "数据源：";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label6.Visible = false;
			// 
			// textBox3
			// 
			this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.textBox3.Location = new System.Drawing.Point(92, 76);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(154, 21);
			this.textBox3.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label5.Location = new System.Drawing.Point(6, 98);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(92, 23);
			this.label5.TabIndex = 5;
			this.label5.Text = "实  例：";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label4.Location = new System.Drawing.Point(6, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(92, 23);
			this.label4.TabIndex = 4;
			this.label4.Text = "密  码：";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Location = new System.Drawing.Point(87, 158);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(500, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "当前版本为：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.button1.Location = new System.Drawing.Point(6, 158);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "检测";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.textBox1.Location = new System.Drawing.Point(92, 52);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(154, 21);
			this.textBox1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.Location = new System.Drawing.Point(6, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "用户名：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UCCheckVersion
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "UCCheckVersion";
			this.Size = new System.Drawing.Size(599, 443);
			this.panel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel1;
	}
}
