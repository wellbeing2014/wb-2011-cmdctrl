/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/24
 * 时间: 16:01
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;


namespace WisoftUpdateTool.InPack
{
	/// <summary>
	/// Description of UC02_Select.
	/// </summary>
	public partial class UC02_Select : UserControl,INextButton
	{
		public bool OnNextButton()
		{
			this.packlist.Clear();
			getCheckedTreeNodes(this.treeView1.Nodes);
			
			InPack.CopyFileFrame cf = new InPack.CopyFileFrame();
			cf.copylist = packlist;
			cf.frompath = this.textBox2.Text;
			cf.Closing+= new CancelEventHandler(cf_Closing);
			cf.ShowDialog();
			return true;
		}
		
		private ArrayList packlist = new ArrayList();
		private void getCheckedTreeNodes(TreeNodeCollection tnc)
		{
			for (int i = 0; i < tnc.Count; i++) {
				if(tnc[i].Checked&&tnc[i].Nodes.Count==0)
				{
					packlist.Add(tnc[i].FullPath.Replace("我的打包目录",""));
				}
				else
					getCheckedTreeNodes(tnc[i].Nodes);
			}
		}
		
		public void cf_Closing(object sender,CancelEventArgs e)
		{
			DialogResult dr=MessageBox.Show("COPY完成了，你想详细看看日志？那就稍后按CTRL+F4","提示",MessageBoxButtons.OKCancel);
			if(dr== DialogResult.OK)
			{
				e.Cancel = true;
			}
			else
				e.Cancel = false;
		}
		
		public UC02_Select()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.textBox1.Enabled = false;
			this.button1.Enabled = false;
			this.button3.Enabled = false;
			ImageList il = new ImageList();
            il.Images.Add("folder",SystemIcon.GetDirectoryIcon(true));
            il.Images.Add("other",SystemIcon.GetIcon(".other",false));
            il.Images.Add("swf",SystemIcon.GetIcon(".swf",false));
			il.Images.Add("xml",SystemIcon.GetIcon(".xml",false));
			il.Images.Add("jar",SystemIcon.GetIcon(".jar",false));
			this.treeView1.ImageList = il;
			
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if(this.checkBox1.Checked)
			{
				this.textBox1.Enabled = true;
				this.button1.Enabled = true;
				this.button3.Enabled = true;
			}
			else
			{
				this.textBox1.Enabled = false;
				this.button1.Enabled = false;
				this.button3.Enabled = false;
			}
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

		
		void Button2Click(object sender, EventArgs e)
		{
			DialogResult dr =folderBrowserDialog1.ShowDialog();
			if(dr==DialogResult.OK)
			{
				this.textBox2.Text = folderBrowserDialog1.SelectedPath;
				Button4Click(sender,e);
			}
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			treeView1.Nodes.Clear();
			TreeNode node = new TreeNode("我的打包目录");
			node.ImageKey = "folder";
            treeView1.Nodes.Add(node);
            
            GetTreeViewData(this.textBox2.Text,node);
            this.treeView1.ExpandAll();
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			this.openFileDialog1.Title = "选择您的构建脚本";
			this.openFileDialog1.Filter ="脚本(*.bat)|*.bat|可执行(*.exe)|*.exe";
			DialogResult dr = this.openFileDialog1.ShowDialog();
			if(dr==DialogResult.OK)
			{
				this.textBox1.Text = this.openFileDialog1.FileName;
			}
				
		}
	}
}
