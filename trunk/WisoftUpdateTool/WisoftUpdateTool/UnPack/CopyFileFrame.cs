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

namespace WisoftUpdateTool
{
	/// <summary>
	/// Description of CopyFileFrame.
	/// </summary>
	public partial class CopyFileFrame : Form
	{
		public ArrayList copylist = null;
		private ArrayList baklist = new ArrayList();
		public string servicespath = @"C:\Users\ZhuXinpei\Desktop\wisoftintegrateframe";
		public string bakToPath = @"C:\Users\ZhuXinpei\Desktop\bak";
		public CopyFileFrame()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			copylist = new ArrayList(UpdateInfo.UpdateFiles);
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
		 	
		 	letdo.BeginInvoke("正在分析需要备份的文件***",null,null);
		 	for (int i = 0; i < copylist.Count; i++) {
		 		Update_File uf = copylist[i] as Update_File;
		 		string url = servicespath+uf.Fileurl;
		 		FileInfo fi =null;
		 		fi = new FileInfo(url);
		 		if(fi.Exists)
		 		{
		 			TotalSize+=(int)fi.Length;
		 			letdo.BeginInvoke("INFO:文件正常！将被覆盖。"+url ,null,null);
		 			baklist.Add(url);
		 		}
		 		else
		 		{
	 			   	letdo.BeginInvoke("WARNING:文件不存在！将以更新包内文件插入。"+url ,null,null);
		 		}
		 	} 
		 	letdo.BeginInvoke("INFO:检查完毕，总共需要备份"+baklist.Count+"个文件，总共大小"+TotalSize ,null,null);
		 	letdo.BeginInvoke("INFO:开始备份" ,null,null);
		 	this.progressBar1.Minimum = 0;
			this.progressBar1.Maximum = TotalSize;
		 	copyCircle();
		 	
		 }
		 
		 /// <summary>
		 /// 更新正式开始
		 /// </summary>
		 private void UpdateFiles()
		 {
		 	//提示倒数3秒
		 	Thread.Sleep(1000);
		 	letdo.BeginInvoke("INFO: 开始更新***倒数 3",null,null);
		 	Thread.Sleep(1000);
		 	letdo.BeginInvoke("INFO: 开始更新***倒数 2",null,null);
		 	Thread.Sleep(1000);
		 	letdo.BeginInvoke("INFO: 开始更新***倒数 1",null,null);
		 	
		 	//清空原来用作备份的数组，初始化各参数
		 	this.baklist.Clear();
		 	this.TotalSize = 0;
		 	this.TotalPostion =0;
		 	this.position = 0;
		 	this.curCopyNo = 0;
		 	
		 	//从更新包里预读文件，以便确定更新和统计更新大小
		 	foreach (Update_File element in copylist) {
		 		if(File.Exists(GobalParameters.UpdateFolder+element.Fileurl))
		 		{
		 			FileInfo fi = new FileInfo(GobalParameters.UpdateFolder+element.Fileurl);
		 			TotalSize +=(int)fi.Length;
		 			baklist.Add(element.Fileurl);
		 		}
		 		else
		 			letdo.BeginInvoke("ERROR: 需要更新的文件："+element.Fileurl+"不在更新包中。",null,null);
		 	}
		 	letdo.BeginInvoke("INFO: 需要更新文件"+baklist.Count+"共"+TotalSize,null,null);
		 	
		 	copyCircle();
		 }
		 
		 //由于备份和更新使用同一个方法。用一个字符串来区分。
		 private string infostr = "备份" ;
		 //设置一个标志，以便循环复制
		 private int curCopyNo = 0;
		 
		 /// <summary>
		 /// 正式复制方法。
		 /// </summary>
		 private void copyCircle()
		 {
	 		if(curCopyNo>=baklist.Count)
	 		{
	 			letdo.BeginInvoke("INFO: "+infostr+"完成！",null,null);
	 			if("更新".Equals(infostr))
	 			{
	 				AutoClose ac = AutoColseFunc;
	 				this.BeginInvoke(ac,null);
	 				return;
	 			}
	 			else 
	 			{
	 				infostr = "更新";
	 				UpdateFiles();
	 				return;
	 			}
	 			
	 		}
	 		string bakto ="";
	 		if(!"更新".Equals(infostr))
	 		{
	 			bakto= ((string)baklist[curCopyNo]).Replace(servicespath,bakToPath);
	 			bakto = AutoCreateFolder(bakto);
	 		}
	 		else
	 		{
	 			bakto= servicespath+@"\"+baklist[curCopyNo];
	 			bakto = AutoCreateFolder(bakto);
	 		}
	 		FileInfo fi = new FileInfo(bakto);
	 		if(File.Exists(bakto))
	 			File.Delete(bakto);
	 		letdo.BeginInvoke("INFO: 正在"+infostr+"文件:"+baklist[curCopyNo],null,null);
	 		//开始备份
	 		try
	        {
	 			string fromfile = (string)baklist[curCopyNo];
	 			if("更新".Equals(infostr))
	 				fromfile = GobalParameters.UpdateFolder+fromfile;
	 			FileStream fs = new FileStream(fromfile, FileMode.Open, FileAccess.Read);
	            Size = (int)fs.Length;
	            position = 0;
	            stream = fs;
	            //这里是设置缓存的大小，可以根据需要修改逻辑
	            buffer = new byte[BUFFER_SIZE];
	            stream.BeginRead(buffer,0,BUFFER_SIZE,new AsyncCallback( AsyncCopyFile ),bakto);  
	        }
	        catch (Exception Ex)
	        {
	        	MessageBox.Show("ERROR:"+infostr+"文件:"+baklist[curCopyNo]+"出错(" + Ex.ToString());
	        	letdo.BeginInvoke("ERROR:"+infostr+"文件:"+baklist[curCopyNo]+"出错(" + Ex.ToString()+")",null,null);
	        	curCopyNo++;
	        	TotalPostion += Size;
	        	copyCircle();
	        	
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
            	MessageBox.Show(infostr+"文件出错！"+e.ToString());
            	this.BeginInvoke(s,new Object[]{"ERROR:"+infostr+"文件出错"+e.ToString()});
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
		                //tempFolderPath = root + "\\" + tempFolderPath;  
		                //  MessageBox.Show("需要创建的目录地址：" + folderRoot+"\\"+tempFolderPath);  
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
