/*
 * 由SharpDevelop创建。
 * 用户： wellbeing
 * 日期: 2012/7/22
 * 时间: 10:59
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WisoftUpdateTool
{
	/// <summary>
	/// Description of UCFinishState.
	/// </summary>
	public partial class UCFinishState : UserControl,INextButton
	{
		public bool OnNextButton()
		{
			return true;
		}
		public UCFinishState()
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
