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
using System.Collections.Generic;
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
			if(this.listviewitems.Count==0)
			{
				DialogResult dr =MessageBox.Show("您给实施造福了，这次更新不要手动修改配置文件？","提示",MessageBoxButtons.OKCancel);
				if(DialogResult.Cancel==dr)
					return false;
			}
			XmlHelper.Delete("root/before_configs","");
			XmlHelper.Insert("root","before_configs","","");
			XmlHelper.InsertConfFiles("root/before_configs",this.listviewitems);
			return true;
		}
		
		private List<Update_File> listviewitems = new List<Update_File>();
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
				
				getAddConfFiles(wc.packlist);
				//this.listBox1.Refresh();
			}
		}
		
		void getAddConfFiles(ArrayList al)
		{
			for (int i = 0; i < al.Count; i++) {
				string filepath = al[i] as string;
				bool ishave = false;
				for (int j = 0; j < listviewitems.Count; j++) {
					Update_File uf= listviewitems[j] as Update_File;
					if (filepath.Equals(uf.Fileurl))
					{	
						ishave = true;
						break;
					}
				}
				if(!ishave)
				{
					string[] temp = filepath.Split('\\');
					string filename = temp[temp.Length-1];
					listviewitems.Add(new Update_File(){Name = filename,Fileurl = filepath});
				}
				
			}
			this.listBox1.DataSource =null ;
			this.listBox1.DataSource =listviewitems ;
			this.listBox1.DisplayMember = "Name";
			this.listBox1.ValueMember = "Fileurl";
			this.listBox1.SelectedItem =null;
			this.listBox1.Refresh();
		}
		
		
		
		void Button2Click(object sender, EventArgs e)
		{
			if(this.listBox1.SelectedItem!=null)
			{
				listviewitems.Remove(this.listBox1.SelectedItem as Update_File);
				this.listBox1.DataSource =null ;
				this.listBox1.DataSource =listviewitems ;
				this.listBox1.DisplayMember = "Name";
				this.listBox1.ValueMember = "Fileurl";
				this.listBox1.SelectedItem =null;
				this.listBox1.Refresh();
				this.textBox1.Text = null;
				this.textBox2.Text = null;
			}
			else
				MessageBox.Show("请选择一个要删除的文件","提示");
		}
		
		private Update_File lastFile = new Update_File();
		void ListBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			if(lastFile!=null&&this.textBox2.Text!=null)
			{
				int a=this.listviewitems.IndexOf(lastFile);
				if(a>=0)
				{
					listviewitems[a].ConfContent = this.textBox2.Text;
				}
			}
			if(this.listBox1.SelectedItem!=null)
			{
				Update_File uf = this.listBox1.SelectedItem as Update_File;
				
				this.textBox1.Text = uf.Fileurl;
				this.textBox2.Text = uf.ConfContent;
				lastFile = uf;
			}
		}
	}
}
