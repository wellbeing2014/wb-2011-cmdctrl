/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/10
 * 时间: 14:20
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System.Reflection;
namespace WisoftUpdateTool
{
	partial class UCFileUpdate
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.exListView1 = new EXControls.EXListView();
			this.columnHeader1 = new EXControls.EXColumnHeader();
			this.boolcol = new EXControls.EXBoolColumnHeader();
			this.columnHeader2 = new EXControls.EXColumnHeader();
			this.columnHeader3 = new EXControls.EXColumnHeader();
			this.columnHeader4 = new EXControls.EXColumnHeader();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.exListView1);
			this.groupBox1.Location = new System.Drawing.Point(0, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(478, 344);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "手动修改文件列表";
			// 
			// exListView1
			// 
			this.exListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.boolcol,
									this.columnHeader2,
									this.columnHeader3,
									this.columnHeader4});
			this.exListView1.ControlPadding = 4;
			this.exListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.exListView1.FullRowSelect = true;
			this.exListView1.GridLines = true;
			this.exListView1.Location = new System.Drawing.Point(3, 17);
			this.exListView1.Name = "exListView1";
			this.exListView1.OwnerDraw = true;
			this.exListView1.Size = new System.Drawing.Size(472, 324);
			this.exListView1.TabIndex = 0;
			this.exListView1.UseCompatibleStateImageBehavior = false;
			this.exListView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "  ";
			this.columnHeader1.Width = 20;
			// 
			// boolcol
			// 
			this.boolcol.Editable = false;
			this.boolcol.FalseImage = null;
			this.boolcol.Text = "通过";
			this.boolcol.TrueImage = null;
			this.boolcol.Width = 72;
			boolcol.TrueImage = System.Drawing.Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("WisoftUpdateTool.Resources.true.png"));
			boolcol.FalseImage = System.Drawing.Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("WisoftUpdateTool.Resources.false.png"));
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "文件";
			this.columnHeader2.Width = 150;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "文件修改说明";
			this.columnHeader3.Width = 250;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "操作";
			this.columnHeader4.Width = 70;
			// 
			// UCFileUpdate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "UCFileUpdate";
			this.Size = new System.Drawing.Size(481, 347);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private EXControls.EXColumnHeader columnHeader3;
		private EXControls.EXColumnHeader columnHeader2;
		private EXControls.EXColumnHeader columnHeader1;
		private EXControls.EXColumnHeader columnHeader4;
		private EXControls.EXBoolColumnHeader boolcol;
		private EXControls.EXListView exListView1;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
