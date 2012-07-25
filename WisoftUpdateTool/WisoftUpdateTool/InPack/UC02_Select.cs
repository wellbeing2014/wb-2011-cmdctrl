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


namespace WisoftUpdateTool.InPack
{
	/// <summary>
	/// Description of UC02_Select.
	/// </summary>
	public partial class UC02_Select : UserControl,INextButton
	{
		public bool OnNextButton()
		{
			return true;
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
			TreeNode node = new TreeNode("Images");
            treeView1.Nodes.Add(node);

            GetTreeViewData(@"D:\TOOLS\",node);
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if(this.checkBox1.Checked)
			{
				this.textBox2.Enabled = true;
				this.button2.Enabled = true;
			}
			else
			{
				this.textBox2.Enabled = false;
				this.button2.Enabled = false;
			}
		}
		
		// 递归调用,加载Images下的所有文件夹。


		private void GetTreeViewData(string path, TreeNode node)
		{
			if(Directory.Exists(path))
			{
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
		 }

 

 

		/////////////////////////////////////////该方法就是读取指定文件夹下的所有文件/////////////////////////////////////////////

 		/// <summary>
        /// 通过递归调用,获取指定文件夹下的所有文件夹以及文件,存储到ArrayList。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filter"></param>
        private void ParseDirectory(string path, string filter)
        {
//            string[] dirs = Directory.GetDirectories(path);  //得到子目录
//            IEnumerator iter = dirs.GetEnumerator();
//            while (iter.MoveNext())
//            {
//                string str = (string)(iter.Current);
//                ParseDirectory(str, filter);  //递归调用。
//            }
//            string[] files = Directory.GetFiles(path, filter);   //获取文件
//            
//            if (files.Length > 0)
//            {
//                m_numFiles += files.Length;
//                m_pathList.Add(files);   //存储文件目录。
//            }
        }
	}
}
