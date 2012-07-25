/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/24
 * 时间: 16:01
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace WisoftUpdateTool.InPack
{
	partial class UC02_Select
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
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.treeView1);
			this.panel1.Controls.Add(this.checkBox1);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(422, 363);
			this.panel1.TabIndex = 0;
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.treeView1.Location = new System.Drawing.Point(3, 105);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(416, 255);
			this.treeView1.TabIndex = 4;
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(3, 29);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(104, 24);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "启用构建脚本";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(380, 54);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(39, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "浏览";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(365, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(54, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "浏览";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Enabled = false;
			this.textBox2.Location = new System.Drawing.Point(86, 56);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(288, 21);
			this.textBox2.TabIndex = 1;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(71, 2);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(288, 21);
			this.textBox1.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3, 79);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(119, 23);
			this.label3.TabIndex = 0;
			this.label3.Text = "勾选打包文件路径：";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "脚本输出路径：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "工程路径：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UC02_Select
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "UC02_Select";
			this.Size = new System.Drawing.Size(422, 363);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
	}
}
