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
