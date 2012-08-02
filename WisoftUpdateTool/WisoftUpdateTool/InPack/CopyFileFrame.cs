/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/12
 * 时间: 13:37
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Threading;

namespace WisoftUpdateTool.InPack
{
	/// <summary>
	/// Description of CopyFileFrame.
	/// </summary>
	public partial class CopyFileFrame : Form
	{
		public ArrayList copylist = null;
		//private ArrayList baklist = new ArrayList();
		public string servicespath = @"C:\Users\ZhuXinpei\Desktop\wisoftintegrateframe";
		//public string bakToPath = @"C:\Users\ZhuXinpei\Desktop\bak";
		public string frompath = "";
		public CopyFileFrame()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//frompath = this.Parent.Tag as string;
			//copylist = new ArrayList(UpdateInfo.UpdateFiles);
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		private int Size =0;
		private byte[] buffer;
        private int position = 0; //已复制大小
        private Stream stream;
       	private int BUFFER_SIZE = 4096;
        private int TotalSize = 0;
        private int TotalPostion = 0;
        private delegate void SetStatus(string a);
        private delegate void AutoClose();
        private void AutoColseFunc()
        {
        	this.Close();
       // 	this.Dispose();
        }
        private void appendTextbox(string a)
        {
        	if(!string.IsNullOrEmpty(a))
        	{
				richTextBox1.Select(this.richTextBox1.TextLength,0);
				if(a.Contains("ERROR:"))
				{
					richTextBox1.SelectionColor = Color.Red;
				}
				else if(a.Contains("WARNING:"))
				{
					richTextBox1.SelectionColor = Color.Gold;
				}
				else 
					richTextBox1.SelectionColor = Color.Green;				
				richTextBox1.AppendText(a + "\r\n");
				richTextBox1.ScrollToCaret();
        	}
        	this.progressBar1.Minimum = 0;
        	this.progressBar1.Maximum = TotalSize;
        	this.progressBar1.Value =TotalPostion+position;
        }
        
        private delegate void dl_async_status(string b);
       
        private void async_status(string b)
        {
        	 SetStatus s = new SetStatus(appendTextbox);
    		//Thread.Sleep(1000);
    		this.BeginInvoke(s,new Object[]{b});

        }
        
        private  dl_async_status letdo = null;
		 private void Form1_Load(object sender, EventArgs e)
		 {
		 	if (copylist.Count <= 0)
            {
                this.Close();
                return;
            }
			letdo = async_status;
		 	
		 	letdo.BeginInvoke("正在检查需要打包的文件***",null,null);
		 	for (int i = 0; i < copylist.Count; i++) {
		 		FileInfo fi =null;
		 		fi = new FileInfo(this.frompath+copylist[i] as string);
		 		if(fi.Exists)
		 		{
		 			TotalSize+=(int)fi.Length;
		 		}
		 		else
		 		{
		 			letdo.BeginInvoke("ERROR:文件"+copylist[i] as string+"不存在",null,null);
		 		}
		 	}
		 	letdo.BeginInvoke("INFO:检查完毕，总共需要打包"+copylist.Count+"个文件，总共大小"+TotalSize ,null,null);
		 	letdo.BeginInvoke("INFO:开始打包" ,null,null);
		 	this.progressBar1.Minimum = 0;
			this.progressBar1.Maximum = TotalSize;
			//删除原来的目录
			try {
				 DirectoryInfo di = new DirectoryInfo("Updates");
				 if(di.Exists)
				    di.Delete(true);
				
			} catch (Exception) {
				
				MessageBox.Show("我日，没能删除原来打包的文件。先将就过了。");
			}
		 	copyCircle();
		 	
		 }
		 
		 
		 
		 private int curCopyNo = 0;
		 
		 private void copyCircle()
		 {
	 		if(curCopyNo>=copylist.Count)
	 		{
	 			letdo.BeginInvoke("INFO: 打包完成！",null,null);
	 			AutoClose ac = AutoColseFunc;
	 			this.BeginInvoke(ac,null);
	 			return;
	 		}
	 		string bakto =AutoCreateFolder(System.Environment.CurrentDirectory+@"\Updates\"+copylist[curCopyNo]);

	 		//FileInfo fi = new FileInfo(bakto);
	 		try {
	 			if(File.Exists(bakto))
	 			File.Delete(bakto);
	 		} catch (Exception e1) {
	 			
	 			MessageBox.Show(e1.ToString());
	 		}
	 		
	 		letdo.BeginInvoke("INFO: 正在复制文件:"+frompath+copylist[curCopyNo],null,null);
	 		//开始备份
	 		try
	        {
	 			FileStream fs = new FileStream(frompath+(string)copylist[curCopyNo], FileMode.Open, FileAccess.Read);
	            Size = (int)fs.Length;
	            position = 0;
	            stream = fs;
	            //这里是设置缓存的大小，可以根据需要修改逻辑
	            buffer = new byte[BUFFER_SIZE];
	            stream.BeginRead(buffer,0,BUFFER_SIZE,new AsyncCallback( AsyncCopyFile ),bakto);  
	        }
	        catch (Exception Ex)
	        {
	        	curCopyNo++;
	        	TotalPostion += Size;
	        	copyCircle();
	            
	        	MessageBox.Show("ERROR:复制文件:"+frompath+copylist[curCopyNo]+"出错(" + Ex.ToString());
	        	letdo.BeginInvoke("ERROR:复制文件文件:"+frompath+copylist[curCopyNo]+"出错(" + Ex.ToString()+")",null,null);
	        }
		 }
		 
		
	 	/// <summary>
        ///异步复制文件
        /// </summary>
        /// <param name="ar"></param>
        private void AsyncCopyFile(IAsyncResult ar)
        {
            int readedLength;
             SetStatus s = new SetStatus(appendTextbox);
    		
            try {
            	  //判断stream是否可读（是否已被关掉）
	            if (stream.CanRead)
	            { 
	                // 锁定 FileStream
	                lock (stream)
	                {
	                    readedLength = stream.EndRead(ar); // 等到挂起的异步读取完成
	                }
	            }
	            else
	            {
	            	//this.BeginInvoke(s,new Object[]{""});
	                return;
	            }
				string url = ar.AsyncState as string ;
	            // 写入磁盘
	            var fsWriter = new FileStream(url, FileMode.Append, FileAccess.Write);
	            fsWriter.Write(buffer, 0, buffer.Length);
	            fsWriter.Close();
	
	            // 当前位置 
	            position += readedLength;
	            
				this.BeginInvoke(s,new Object[]{""});
	            if (position >= Size) // 读取完毕
	            {
	                stream.Close(); //关闭
	                curCopyNo++;
	                TotalPostion +=Size;
	                Size = 0;
	                position = 0;
             		copyCircle();
	                return;
	            }
	            if (stream.CanRead)
	            {
	                lock (stream)
	                {
	                    int leftSize = Size - position;
	                    if (leftSize < BUFFER_SIZE)
	                        buffer = new byte[leftSize];
	                    stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(AsyncCopyFile), url);
	                }
	            }
	            else
	            {
	                return;
	            }
            } catch (Exception e) {
             	 
             	TotalPostion +=Size;
            	MessageBox.Show("复制文件出错！"+e.ToString());
            	this.BeginInvoke(s,new Object[]{"ERROR:复制文件出错"+e.ToString()});
            	curCopyNo++;
             	 copyCircle();
            }     
        }	
	
         //自动创建不存在路径
       	public  String AutoCreateFolder(string filePath)   
		{   
	      	try
	    	{
	    		string[] fArr = filePath.Split('\\'); 
	    		String root = null;
	    		int beginInt ;
	    		if(fArr.Length>=3&&fArr[0]==""&&fArr[1]=="")
				{
					root = @"\\"+@fArr[2]+@"\";
					beginInt = 3;
				}
				else
				{
					root = fArr[0]+@"\";
					beginInt =1;
				}
				
                string tempFolderPath =null;
		        for (int fArrItem = beginInt; fArrItem < fArr.Length; fArrItem++)  
		        {  
		            //如果此项不为空且不包含“.”符号（即非文件名称）则判断文件目录是否存在  
		            if (fArr[fArrItem] != "" && fArr[fArrItem].IndexOf('.') == -1)   
		            {   
		                //截取文件路径中至此项的字符串+文件夹根目录，判断此目录是否存在   
		               tempFolderPath = filePath.Substring(0, filePath.IndexOf(fArr[fArrItem]) + fArr[fArrItem].Length);   
		                //检测当前目录是否存在，如果不存在则创建  
		                DirectoryInfo tempCreateFolder = new DirectoryInfo(@tempFolderPath);  
		                if (!tempCreateFolder.Exists)  
		                {  
		                    tempCreateFolder.Create();  
		                } 
		            } 
					else
						tempFolderPath+=@"\"+	fArr[fArrItem];					
		        }  
		        return tempFolderPath;
	    	}
	    	catch(Exception)
	    	{
	    		return null;
	    	}
	    
		}
	}
}
