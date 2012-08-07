/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/8/7
 * 时间: 13:12
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
	/// Description of UC04_EditSql.
	/// </summary>
	public partial class UC04_EditSql : UserControl,INextButton
	{
		public bool OnNextButton()
		{
			return true;
		}
		public UC04_EditSql()
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
