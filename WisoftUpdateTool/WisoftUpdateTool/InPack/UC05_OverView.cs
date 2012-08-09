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
using System.IO;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

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
			
			ImageList il = new ImageList();
            il.Images.Add("folder",SystemIcon.GetDirectoryIcon(true));
            il.Images.Add("other",SystemIcon.GetIcon(".other",false));
            il.Images.Add("swf",SystemIcon.GetIcon(".swf",false));
			il.Images.Add("xml",SystemIcon.GetIcon(".xml",false));
			il.Images.Add("jar",SystemIcon.GetIcon(".jar",false));
			this.treeView1.ImageList = il;
			inittree();
		}
		
		void inittree()
		{
			treeView1.Nodes.Clear();
//			TreeNode node = new TreeNode("根目录");
//			node.ImageKey = "folder";
//            treeView1.Nodes.Add(node);
            
            GetTreeViewData(System.IO.Directory.GetCurrentDirectory(),null);
            this.treeView1.ExpandAll();
		}
		
		private void GetTreeViewData(string path, TreeNode node)
		{
			if(Directory.Exists(path))
			{
//				node.ImageKey="folder";
//				node.SelectedImageKey="folder";
	            //获取指定路径下的所有文件夹集合,GetFiles()则会包含文件,如:a.jpg。
	            string[] dirs = Directory.GetFileSystemEntries(path);
	            
	            string imagePath = "";
	            for (int i = 0; i < dirs.Length; i++)
	            {
	                //得到指定文件夹的子级文件夹,子级文件夹通过自调自获取其子级文件夹,
	
	               //这样一层一层得到指定文件下的所有文件夹。
	                string childpath = dirs[i];  
	                //截取字符串,把路径去掉,只显示文件夹名称。
	                imagePath = childpath.Substring(childpath.LastIndexOf(@"\") + 1, childpath.Length - childpath.LastIndexOf(@"\") - 1);
	                TreeNode child = new TreeNode(imagePath,0,0); // 把文件夹增加到TreeNode。
	                child.Name = childpath;
	                if(node==null)
	                	this.treeView1.Nodes.Add(child);
	                else 
	                	node.Nodes.Add(child);
	                GetTreeViewData(childpath, child);  //递归调用.
	            }
			}
			else
			{
				string[] spiltstr = path.Split('.');
				int a =this.treeView1.ImageList.Images.IndexOfKey(spiltstr[spiltstr.Length-1]);
				if(a>0)
				{
					node.ImageKey=spiltstr[spiltstr.Length-1];
					node.SelectedImageKey = spiltstr[spiltstr.Length-1];
				}
					
				else
				{
					node.ImageKey = "other";
					node.SelectedImageKey = "other";
				}
			}
		 }
		void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Action != TreeViewAction.Unknown)
            {
				TreeNode tn = e.Node;
				DirectoryInfo ar = new DirectoryInfo(tn.FullPath);
				if(ar.Exists)
				{
					this.label6.Text = ar.Name;
					this.label7.Text = ar.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
					this.label8.Text = "不可读取大小";
					this.label9.Text = ar.FullName;
					this.textEditorControl1.Visible = false;
					this.label10.Visible = true;
				}
				else
				{
					FileInfo fi = new FileInfo(tn.FullPath);
					if(fi.Exists)
					{
						float a = fi.Length;
						string temp = a.ToString();
						if((a-1024)>0)
						{
							if(a-1024*1024>0)
							{
								temp = (a/1024.0/1024.0).ToString("0.00")+"M";
							}
							else
								temp = (a/1024.0).ToString("0.00")+"K";
						}
						else
							temp  = a.ToString("0.00")+"Bytes";
						this.label6.Text = fi.Name;
						this.label7.Text = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
						this.label8.Text = temp;
						this.label9.Text = fi.FullName;
						
						string[] spiltstr = fi.Name.Split('.');
						string filetype = spiltstr[spiltstr.Length-1];
						if("xml".Equals(filetype))
						{
							this.textEditorControl1.Visible = true;
							this.label10.Visible = false;
							this.textEditorControl1.LoadFile(tn.FullPath);
							this.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
						}
						if("sql".Equals(filetype))
						{
							this.textEditorControl1.Visible = true;
							this.label10.Visible = false;
							this.textEditorControl1.LoadFile(tn.FullPath);
							this.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
						}
						if("txt".Equals(filetype)||"log".Equals(filetype)||"properties".Equals(filetype))
						{
							this.textEditorControl1.Visible = true;
							this.label10.Visible = false;
							this.textEditorControl1.LoadFile(tn.FullPath);
							//this.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("");
						}
					}
					else
						MessageBox.Show("歇菜，可能是打包过程中出现问题这个文件不存在了。");
				}

            }
		}
	}
}
