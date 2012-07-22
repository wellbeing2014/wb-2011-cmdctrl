/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/19
 * 时间: 16:10
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WisoftUpdateTool
{
	/// <summary>
	/// Description of ContentList.
	/// </summary>
	public partial class ContentList : Form
	{
		public ContentList()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public ContentList(string content)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.textBox1.Text = content;
			this.textBox1.SelectionStart = content.Length;			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
	}
}
