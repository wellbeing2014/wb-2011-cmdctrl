/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/10
 * 时间: 8:42
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
	/// Description of UCSelectDir.
	/// </summary>
	public partial class UCSelectDir : UserControl,INextButton
	{
		
		
		public bool OnNextButton()
		{
			if(string.IsNullOrEmpty(this.textBox2.Text))
			{
				MessageBox.Show("请选择服务路径","提示");
				return false;
			}
			else 
			{
				this.Parent.Tag = this.textBox2.Text;
				
				return true;
			}
		}
		public UCSelectDir()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.label1.Text=UpdateInfo.Name+"("+UpdateInfo.Code+")"+UpdateInfo.Ver+"  发布日期：" + UpdateInfo.PublishDate;			//this.textBox1.ReadOnly = true;
			this.textBox1.Text = UpdateInfo.UpdateNote;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, System.EventArgs e)
		{
			DialogResult dr =folderBrowserDialog1.ShowDialog();
			if(dr==DialogResult.OK)
			{
				this.textBox2.Text = folderBrowserDialog1.SelectedPath;
			}
		}
	}
}
