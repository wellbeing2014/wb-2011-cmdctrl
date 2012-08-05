/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/31
 * 时间: 15:23
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using EXControls;

namespace WisoftUpdateTool.InPack
{
	/// <summary>
	/// Description of UC03_ConfFiles.
	/// </summary>
	public partial class UC03_ConfFiles : UserControl,INextButton
	{
		public bool OnNextButton()
		{
			return true;
		}
		
		private ArrayList listviewitems = new ArrayList();
		public UC03_ConfFiles()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			
			
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			WND_ConfFileContent wc = new WND_ConfFileContent();
			DialogResult dr = wc.ShowDialog();
			if(dr==DialogResult.OK)
			{
				wc.packlist = listviewitems;
				this.listBox1.DataSource = listviewitems;
				//this.listBox1.Refresh();
			}
		
		}
		
		
	}
}
