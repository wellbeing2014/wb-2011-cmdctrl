/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/10
 * 时间: 11:07
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.OracleClient;
namespace WisoftUpdateTool
{
	/// <summary>
	/// Description of UCCheckVersion.
	/// </summary>
	public partial class UCCheckVersion : UserControl,INextButton
	{
		private string checkverstr ="更新版本：{0}，当前版本:{1}";
		public bool Checked = false;
			CopyFileFrame cf =null;
		public bool OnNextButton()
		{
			if(string.IsNullOrEmpty(this.textBox2.Text))
			{
				MessageBox.Show("请选择备份目录","提示");
				return false;
			}
			if(string.IsNullOrEmpty(this.textBox1.Text)||string.IsNullOrEmpty(this.textBox4.Text))
			{
				MessageBox.Show("Sorry,你必须得连接数据库，否则，最后执行SQL就悲催了。");
				return false;
			}
			else
			{
				XmlHelper.Update("root/DBConfig/username","",this.textBox1.Text);
				XmlHelper.Update("root/DBConfig/password","",this.textBox3.Text);
				XmlHelper.Update("root/DBConfig/SID","",this.textBox4.Text);
				string myConnString = "user id="+this.textBox1.Text+";data source="+this.textBox4.Text+";password="+this.textBox3.Text;
		        OracleConnection myConnection = new OracleConnection(myConnString);
		        try {
		        	myConnection.Open();
		        	myConnection.Close();
		        } catch (OracleException e1) {
		        	if (e1.Code ==1017)
		        		MessageBox.Show("伤不起啊，你居然把用户名密码输错了，认真点行不行？");
		        	else if(e1.Code == 12541)
		        		MessageBox.Show("尼玛，你给的数据库我根本连不上。");
		        	else 
		        		MessageBox.Show("歇菜，不知道数据库怎么了，反正是有问题。");
		        	return false;
		        }
			}
			if(!Checked)
			{
				DialogResult dr =MessageBox.Show("更新包版本检查不通过，是否继续","提示",MessageBoxButtons.OKCancel);
				if(dr !=DialogResult.OK)
				{
					return false;
				}
			}
			
			
			cf = new CopyFileFrame();
			cf.servicespath = this.Parent.Tag as string;
			cf.bakToPath = this.textBox2.Text;
			cf.Closing+= new CancelEventHandler(cf_Closing);
			cf.ShowDialog();
			
			
			return true;
		}
		public void cf_Closing(object sender,CancelEventArgs e)
		{
			DialogResult dr=MessageBox.Show("更新完成，是否关闭进度条，或者稍后按CTRL+F4","提示",MessageBoxButtons.OKCancel);
			if(dr== DialogResult.OK)
			{
				e.Cancel = false;
			}
			else
				e.Cancel = true;
			
		}
		public UCCheckVersion()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			CreateFileTree();
			this.treeView1.ExpandAll();
			this.label2.Text = string.Format(checkverstr,UpdateInfo.Ver,"未检测") ;
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public void CreateFileTree()
		{
			Update_File[] ufs = UpdateInfo.UpdateFiles;
			for (int i = 0; i <ufs.Length; i++) {
				CreateFileTree(this.treeView1.Nodes,ufs[i].Action,ufs[i].Fileurl);
			}
			
		}
		
		public void CreateFileTree(TreeNodeCollection tr,string action,string tnname)
		{
			string firstname ="";
			string othername ="";
			if(tnname.Split('\\').Length>1)
			{
			 firstname = tnname.Split('\\')[0];
			 othername = tnname.Substring(firstname.Length+1);
			}
			else
			{
				firstname = tnname+"("+action+")";
			}
			bool ishave = false;
			foreach (TreeNode element in tr) {
				if (element.Text.Equals(firstname))
				{
					ishave = true;
					if(!string.IsNullOrEmpty(othername))
						CreateFileTree(element.Nodes,action,othername);
					break;
				}
			}
			if(!ishave)
			{
				TreeNode newtn = new TreeNode(firstname);
				tr.Add(newtn);
				if(!string.IsNullOrEmpty(othername))
					CreateFileTree(newtn.Nodes,action,othername);
			}
			
		}
			
		
		void Button1Click(object sender, EventArgs e)
		{
			XmlHelper.Update("root/DBConfig/username","",this.textBox1.Text);
			XmlHelper.Update("root/DBConfig/password","",this.textBox3.Text);
			XmlHelper.Update("root/DBConfig/SID","",this.textBox4.Text);
			string myConnString = "user id="+this.textBox1.Text+";data source="+this.textBox4.Text+";password="+this.textBox3.Text;
	        OracleConnection myConnection = new OracleConnection(myConnString);
	        OracleCommand catCMD = myConnection.CreateCommand();
	        catCMD.CommandText = "select * from (select version  from system_version_info where modulecode='maea'  order by version desc) where  rownum=1 ";
	        
	        try {
	        	myConnection.Open();
	        	OracleDataReader myReader = catCMD.ExecuteReader();
		        while (myReader.Read())
		        {
		        	string cur =myReader.GetString(0);
		        	int should_w = Int32.Parse(cur.Substring(cur.Length-3,1))+1;
		        	string should = cur.Substring(0,cur.Length-3)+should_w;
		        	this.label2.Text = string.Format(checkverstr,UpdateInfo.Ver,cur);
		        	string should1 =UpdateInfo.Ver.Substring(0,UpdateInfo.Ver.Length-2);
		        	if(should.Equals(should1))
		        	{
		        		this.label2.ForeColor = Color.Green;
		        		Checked = true;
		        	}
		        	else
		        	{
		        		this.label2.ForeColor = Color.Red;
		        		MessageBox.Show("你所更新的版本不符合要求，先更新“"+should+"”版本！","警告");
		        	}
		        	
		        }
				myReader.Close();
	        	myConnection.Close();
	        } catch (OracleException e1) {
	        	if (e1.Code ==1017)
	        		MessageBox.Show("伤不起啊，你居然把用户名密码输错了，认真点行不行？");
	        	else if(e1.Code == 12541)
	        		MessageBox.Show("尼玛，你给的数据库我根本连不上。");
	        	else 
	        		MessageBox.Show("歇菜，不知道数据库怎么了，反正是有问题。不信？用PLSQL连连看");
	        }

		}
		
		void Button2Click(object sender, EventArgs e)
		{
			DialogResult dr =this.folderBrowserDialog1.ShowDialog();
			if(dr==DialogResult.OK)
			{
				string servicespath = this.Parent.Tag as string ;
				if(servicespath.Equals(this.folderBrowserDialog1.SelectedPath))
					MessageBox.Show("您的备份路径和服务所在路径一致!请另选它处吧!","警告");
				else 
					this.textBox2.Text = this.folderBrowserDialog1.SelectedPath;
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
	}
}
