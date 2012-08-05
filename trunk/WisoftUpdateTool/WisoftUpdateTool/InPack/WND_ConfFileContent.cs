/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/8/2
 * 时间: 13:45
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Collections;

namespace WisoftUpdateTool.InPack
{
	/// <summary>
	/// Description of WND_ConfFileContent.
	/// </summary>
	public partial class WND_ConfFileContent : Form
	{
		public WND_ConfFileContent()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.Closing += new CancelEventHandler(WND_CLosing);
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
		}
		//关闭时将选择的文件赋值到list中。
		public void WND_CLosing(object sender,CancelEventArgs e)
		{
			getCheckedTreeNodes(this.treeView1.Nodes);
			e.Cancel = false;
		}
		void Button1Click(object sender, EventArgs e)
		{
			DialogResult dr =folderBrowserDialog1.ShowDialog();
			if(dr==DialogResult.OK)
			{
				this.textBox1.Text = folderBrowserDialog1.SelectedPath;
				Button2Click(sender,e);
			}
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			treeView1.Nodes.Clear();
			TreeNode node = new TreeNode("根目录");
			node.ImageKey = "folder";
            treeView1.Nodes.Add(node);
            
            GetTreeViewData(this.textBox1.Text,node);
            this.treeView1.ExpandAll();
		}
		
		// 递归调用,加载Images下的所有文件夹。


		private void GetTreeViewData(string path, TreeNode node)
		{
			if(Directory.Exists(path))
			{
				node.ImageKey="folder";
				node.SelectedImageKey="folder";
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
		private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                CheckAllChildNodes(e.Node, e.Node.Checked);
                //选中父节点 
                bool bol = true;
                if (e.Node.Parent != null)
                {
                    for (int i = 0; i < e.Node.Parent.Nodes.Count; i++)
                    {
                        if (!e.Node.Parent.Nodes[i].Checked)
                            bol = false;
                    }
                    e.Node.Parent.Checked = bol;
                }
            }
        }
        #region  选中子节点
        public void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
        #endregion
        
        public ArrayList packlist = new ArrayList();
		private void getCheckedTreeNodes(TreeNodeCollection tnc)
		{
			for (int i = 0; i < tnc.Count; i++) {
				if(tnc[i].Checked&&tnc[i].Nodes.Count==0)
				{
					packlist.Add(tnc[i].FullPath.Replace("根目录",""));
				}
				else
					getCheckedTreeNodes(tnc[i].Nodes);
			}
		}
	}
}
