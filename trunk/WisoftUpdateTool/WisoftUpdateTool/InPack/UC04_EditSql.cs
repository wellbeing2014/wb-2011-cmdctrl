/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/8/7
 * 时间: 13:12
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.IO;

namespace WisoftUpdateTool.InPack
{
	/// <summary>
	/// Description of UC04_EditSql.
	/// </summary>
	public partial class UC04_EditSql : UserControl,INextButton
	{
		public bool OnNextButton()
		{
			this.textEditorControl1.SaveFile(GobalParameters.UpdateSqlFilePath);
			return true;
		}
		public bool DownNextButton()
		{
			initstr = string.Format(initstr,UpdateInfo.Name+"("+UpdateInfo.Code+")"+UpdateInfo.Ver,UpdateInfo.PublishDate);
			if(File.Exists(GobalParameters.UpdateSqlFilePath))
				this.textEditorControl1.LoadFile(GobalParameters.UpdateSqlFilePath);
			else
			{
				this.textEditorControl1.Text = this.initstr;
			}
			return true;
		}
		
		string initstr = "/*************************************************************/\r\n"+
								 "/**                   注         意                         **/\r\n"+
								 "/**       直接执行本sql文件，保证能够完整执行到底。         **/\r\n"+
								 "/**       特别注意要添加commit和存储过程加隔断符的地方      **/\r\n"+
								 "/*************************************************************/\r\n"+

								 "--※版本信息：{0}\r\n"+
								 "--※发布日期：{1}\r\n"+

								 "/*********************新 建 对 象 开 始***********************/\r\n\n\n"+


								 "/*********************新 建 对 象 结 束***********************/\r\n\n"+

								 "---------------------------------------------------------------\r\n\n"+

								 "/*********************修 改 对 象 开 始***********************/\r\n\n\n"+


								 "/*********************修 改 对 象 结 束***********************/\r\n\n"+

								 "---------------------------------------------------------------\r\n\n"+
								
								 "/*********************数 据 语 句 开 始***********************/\r\n\n\n"+
								
								
								 "/*********************数 据 语 句 结 束***********************/\r\n";
		public UC04_EditSql()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
			this.textEditorControl1.Encoding = System.Text.Encoding.Default;
			this.textEditorControl1.TextChanged+= new EventHandler(UC04_EditSql_TextChanged);
			
		}
		
		
		//恢复模板
		void Button1Click(object sender, System.EventArgs e)
		{
			this.textEditorControl1.Text = this.initstr;
		}
		
		void UC04_EditSql_TextChanged(object sender, System.EventArgs e)
		{
			this.button2.Enabled = true;
		}
		
		//保存按钮
		void Button2Click(object sender, EventArgs e)
		{
			this.textEditorControl1.SaveFile(GobalParameters.UpdateSqlFilePath);
			this.button2.Enabled = false;
		}
	}
}
