/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/17
 * 时间: 14:15
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WisoftUpdateTool
{
	/// <summary>
	/// Description of Test.
	/// </summary>
	public partial class Test : Form
	{
		private WisoftUpdateTool.UCExecuteSQL ucFileUpdate1;
		public Test()
		{
			this.ucFileUpdate1= new WisoftUpdateTool.UCExecuteSQL();
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.ucFileUpdate1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Controls.Add(this.ucFileUpdate1);
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
	}
}
