/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/20
 * 时间: 10:44
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO; 
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.Diagnostics;

namespace WisoftUpdateTool
{
	/// <summary>
	/// Description of UCExecuteSQL.
	/// </summary>
	public partial class UCExecuteSQL : UserControl,INextButton
	{
		public bool OnNextButton()
		{
			return true;		
		}
		public UCExecuteSQL()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);  
            ReadErrOutput += new DelReadErrOutput(ReadErrOutputAction);
			this.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
			
			this.textEditorControl1.LoadFile(GobalParameters.UpdateSqlFilePath);
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		
		//执行SQL按钮
		void Button1Click(object sender, EventArgs e)
		{
			  string sqlarg = UpdateInfo.DBusername+"/"+UpdateInfo.DBpassword+"@"+UpdateInfo.DBSID;
			  Process p = new Process();
			  p.StartInfo.FileName = "sqlplus";
			  p.StartInfo.Arguments = sqlarg+" @"+GobalParameters.UpdateSqlFilePath;
              p.StartInfo.UseShellExecute = false;
              p.StartInfo.RedirectStandardInput = true;
              p.StartInfo.RedirectStandardOutput = true;
              p.StartInfo.RedirectStandardError = true;
              p.StartInfo.CreateNoWindow = true;
              p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);  
        	  p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);  
              
        	  p.EnableRaisingEvents = true;                      // 启用Exited事件   
              p.Exited += new EventHandler(CmdProcess_Exited);   
        	  p.Start();
			  p.BeginOutputReadLine();  
              p.BeginErrorReadLine();  
			  p.StandardInput.WriteLine("exit");              
		}
		
		
		private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)  
        {  
            if (e.Data != null)  
            {  
                // 4. 异步调用，需要invoke   
                this.Invoke(ReadStdOutput, new object[] { e.Data });  
            }  
        }  
  
		private void CmdProcess_Exited(object sender, EventArgs e)  
        {  
			
			string myConnString = "user id="+this.textBox1.Text+";data source="+this.textBox4.Text+";password="+this.textBox3.Text;
	        OracleConnection myConnection = new OracleConnection(myConnString);
	        OracleCommand catCMD = myConnection.CreateCommand();
	        string sql = "insert into system_version_info "+
				"values( '','"+name+"','"+code+"','"+ver+"','"+publishdate+"',to_char(sysdate, 'yyyy-mm-dd hh24:mi:ss'),'"+keyword+"')";;
	        	catCMD.CommandText = string.Format(sql,UpdateInfo.Code);
	        
	        try {
	        	myConnection.Open();
	        	OracleDataReader myReader = catCMD.ExecuteReader();
	        	string cur="";
		        while (myReader.Read())
		        {
		        	cur =myReader.GetString(0);
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
		        if(string.IsNullOrEmpty(cur))
		        {
		        	this.label2.ForeColor = Color.Red;
		        	this.label2.Text ="未能检查到先前的版本。";
		        	MessageBox.Show("你更新的版本从来没有更新过。");
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
			
			
			
			MessageBox.Show("SQL语句执行完毕！","提示");
        } 
		
        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)  
        {  
            if (e.Data != null)  
            {  
                this.Invoke(ReadErrOutput, new object[] { e.Data });  
            }  
        }
		
		private void ReadStdOutputAction(string result)  
        {  
            this.textBox1.AppendText(result + "\r\n");  
        }  
  
        private void ReadErrOutputAction(string result)  
        {  
            this.textBox1.AppendText(result + "\r\n");  
        } 
		
		// 2.定义委托事件   
        public event DelReadStdOutput ReadStdOutput;  
        public event DelReadErrOutput ReadErrOutput;  
	}
	// 1.定义委托 
	public delegate void DelReadStdOutput(string result);  
    public delegate void DelReadErrOutput(string result); 
}
