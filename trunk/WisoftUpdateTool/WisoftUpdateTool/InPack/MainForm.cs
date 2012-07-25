/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/9
 * 时间: 15:05
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace WisoftUpdateTool.InPack
{
	public interface INextButton 
	{
	   bool OnNextButton();
	}
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private WisoftUpdateTool.InPack.UC01_Config uc01_Config1;
		private WisoftUpdateTool.InPack.UC02_Select uc02_Select1;
//		private WisoftUpdateTool ucFileUpdate1;
//		private WisoftUpdateTool ucExecuteSQL1;
		public MainForm()
		{
			
			this.uc01_Config1 = new WisoftUpdateTool.InPack.UC01_Config();
			this.uc02_Select1 = new WisoftUpdateTool.InPack.UC02_Select();
			//ucCheckVersion1.Visible = false;
			this.uc02_Select1.Visible = false;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			
			InitializeComponent();
			this.Text = "中科更新包工具--"+UpdateInfo.Name+"("+UpdateInfo.Code+")"+UpdateInfo.Ver;
			AddUC();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		private void AddUC()
		{
			// 
			// ucSelectDir1
			// 
			this.uc02_Select1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uc01_Config1.Dock = System.Windows.Forms.DockStyle.Fill;
//			this.ucFileUpdate1.Dock = System.Windows.Forms.DockStyle.Fill;
//			this.ucExecuteSQL1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Controls.Add(this.uc01_Config1);
			this.panel2.Controls.Add(this.uc02_Select1);
//			this.panel2.Controls.Add(ucFileUpdate1);
//			this.panel2.Controls.Add(ucExecuteSQL1);
		}
		
		
		void Button2Click(object sender, EventArgs e)
		{
				System.Windows.Forms.Control.ControlCollection cc = this.panel2.Controls as System.Windows.Forms.Control.ControlCollection;
				
				cc[order].Visible = false;
				cc[order-1].Visible = true;
				order--;
				if(order > 0)
					this.button2.Visible = true;
				else
					this.button2.Visible = false;
				if(order !=cc.Count)
					this.button1.Text = "下一步";

		}
		
		
		//下一步按钮
		int order = 0;
		
		void Button1Click(object sender, EventArgs e)
		{
			
			if(this.button1.Text!="完成")
			{
				System.Windows.Forms.Control.ControlCollection cc = this.panel2.Controls as System.Windows.Forms.Control.ControlCollection;
				if(((INextButton)cc[order]).OnNextButton())
				{
				cc[order].Visible = false;
				cc[order+1].Visible = true;
				order++;
				if(cc.Count==order+1)
				{
					this.button1.Text ="完成";
				}
				}
				if(order > 0)
					this.button2.Visible = true;
				else
					this.button2.Visible = false;
			}
			else
			{
				this.Close();
			}
		}
	}
}
