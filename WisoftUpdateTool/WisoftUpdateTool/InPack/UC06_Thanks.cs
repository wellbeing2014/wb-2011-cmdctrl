/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/8/13
 * 时间: 10:16
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WisoftUpdateTool.InPack
{
	/// <summary>
	/// Description of UC06_Thanks.
	/// </summary>
	public partial class UC06_Thanks : UserControl,INextButton
	{
		public bool DownNextButton()
		{
			return true;
		}
		public bool OnNextButton()
		{
			return true;
		}
		public UC06_Thanks()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
	}
}
