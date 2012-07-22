/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/10
 * 时间: 14:20
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EXControls;
namespace WisoftUpdateTool
{
	/// <summary>
	/// Description of UCFileUpdate.
	/// </summary>
	public partial class UCFileUpdate : UserControl,INextButton
	{
		public UCFileUpdate()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			initRows();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public bool OnNextButton()
		{
			bool ischeck = true;
			foreach (EXListViewItem element in this.exListView1.Items) {
				EXBoolListViewSubItem exb = element.SubItems[1] as EXBoolListViewSubItem;
				if (!exb.BoolValue)
				{
					ischeck=false;
					break;
				}
				
			}
			if (!ischeck)
			{
				MessageBox.Show("还有没有完成修改的文件请验证","警告");
				return false;
			}
			else
				return true;
		}
		
		public void bt_Click(object sender, LinkLabelLinkClickedEventArgs e)
		{
			LinkLabel ll = sender as LinkLabel;
			EXListViewItem ev = ll.Tag as EXListViewItem;
			//string fileurl = this.Parent.Tag as string+@"\" + ev.SubItems[4].ToString();
			string fileurl =@"C:\Users\wellbeing.wellbeing-PC\Desktop\wisoftintegrateframe\" + ev.SubItems[5].Text;
			XmlPad.XmlEditor xe = new XmlPad.XmlEditor();
			xe._content =  ev.SubItems[3].Text;
			if(!xe.LoadFile(fileurl))
			{
				xe.Close();
				xe.Dispose();
			}
			else 
			{
				xe.ShowDialog();
				EXBoolListViewSubItem booes = ev.SubItems[1] as EXBoolListViewSubItem;
				booes.BoolValue = true;
				this.exListView1.Refresh();
			}
			
		}
		
		public void initRows()
		{
			Manual_File[] uf =UpdateInfo.Manual_Files;
			//this.exListView1.ItemCheck
			 ImageList il = new ImageList();
            il.ImageSize = new Size(1, 20);
            exListView1.SmallImageList = il;
			this.exListView1.BeginUpdate();
			for (int i = 0; i < uf.Length; i++) {
				EXListViewItem item = new EXListViewItem(i.ToString());
				item.SubItems.Add(new EXBoolListViewSubItem(false));
				item.SubItems.Add(uf[i].Name);
				item.SubItems.Add(uf[i].Content);
				
				
				EXControlListViewSubItem es = new EXControlListViewSubItem();
				LinkLabel bt = new LinkLabel();
				bt.Text = "编辑";
				bt.Tag = item;
				bt.LinkClicked += new LinkLabelLinkClickedEventHandler(bt_Click);
				item.SubItems.Add(es);
				item.SubItems.Add(uf[i].Fileurl);
				this.exListView1.AddControlToSubItem(bt,es);
				this.exListView1.Items.Add(item);
			}
			this.exListView1.EndUpdate();
		}
		
		
	}
}
