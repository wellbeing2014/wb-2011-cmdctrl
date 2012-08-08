/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/8/8
 * 时间: 14:21
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
	/// Description of UC05_OverView.
	/// </summary>
	public partial class UC05_OverView : UserControl,INextButton
	{
		public bool OnNextButton()
		{
			return true;
		}
		public UC05_OverView()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
		{
			
		}
	}
}
