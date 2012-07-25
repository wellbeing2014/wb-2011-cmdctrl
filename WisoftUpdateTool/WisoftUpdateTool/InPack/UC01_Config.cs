/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/24
 * 时间: 15:54
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
	/// Description of UC01_Config.
	/// </summary>
	public partial class UC01_Config : UserControl,INextButton
	{
		public bool OnNextButton()
		{
			return true;
		}
		public UC01_Config()
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
