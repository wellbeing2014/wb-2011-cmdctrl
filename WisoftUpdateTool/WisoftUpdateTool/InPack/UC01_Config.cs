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
using System.IO;
using System.Text.RegularExpressions;

namespace WisoftUpdateTool.InPack
{
	/// <summary>
	/// Description of UC01_Config.
	/// </summary>
	public partial class UC01_Config : UserControl,INextButton
	{
		public bool OnNextButton()
		{
			if(string.IsNullOrEmpty(this.textBox1.Text)||
			   string.IsNullOrEmpty(this.textBox2.Text)||
			   string.IsNullOrEmpty(this.textBox3.Text))
			{
				MessageBox.Show("笑话，你都不知道打什么包，你点我干啥。");
				return false;
			}
			//删除原来的目录
			try {
				 DirectoryInfo di = new DirectoryInfo(GobalParameters.UpdateFolder);
				 if(di.Exists)
				    di.Delete(true);
			} catch (Exception e) {
				
				MessageBox.Show("我日，没能删除原来打包的文件。先将就过了。"+e.ToString());
			}
			Directory.CreateDirectory(GobalParameters.UpdateFolder);
			XmlHelper.CreateXML();//创建XMl文件
			XmlHelper.Insert("root","name","",this.textBox1.Text);
			XmlHelper.Insert("root","code","",this.textBox2.Text);
			XmlHelper.Insert("root","version","",this.textBox3.Text);
			XmlHelper.InsertCData("root","updatenote",this.textBox4.Text);
			Regex regex = new Regex("<>\"", RegexOptions.IgnoreCase);
			string keyword =regex.Replace(this.textBox5.Text,"");
			XmlHelper.Insert("root","keyword","",this.textBox5.Text);
			XmlHelper.Insert("root","publish_date","",System.DateTime.Now.ToString("yyyy-MM-dd"));
			return true;
		}
		public bool DownNextButton()
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
