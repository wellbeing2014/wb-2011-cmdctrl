﻿/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/9
 * 时间: 15:05
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Windows.Forms;

namespace WisoftUpdateTool
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//解包程序
			//Application.Run(new MainForm());
			//打包程序
			Application.Run(new WisoftUpdateTool.InPack.MainForm());
			//Application.Run(new CopyFileFrame());
			//Application.Run(new Test());
			//Application.Run(new MyForm());
		}
		
	}
}
