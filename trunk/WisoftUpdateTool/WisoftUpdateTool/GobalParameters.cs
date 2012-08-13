/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/8/13
 * 时间: 8:47
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace WisoftUpdateTool
{
	/// <summary>
	/// Description of GobalParameter.
	/// </summary>
	public class GobalParameters
	{
		public GobalParameters()
		{
		}
		
		//配置文件路径
		private static string _UpdateXmlFilePath = "UpdateInfo.xml";
		
		public static string UpdateXmlFilePath {
			get { return _UpdateFolder+_UpdateXmlFilePath; }
		}
		private static string _UpdateFolder = @"Updates\";
		
		public static string UpdateFolder {
			get { return _UpdateFolder; }
		}
		private static string _UpdateSqlFilePath="databaseupdate.sql";
		
		public static string UpdateSqlFilePath {
			get { return _UpdateFolder+_UpdateSqlFilePath; }
		}
	}
}
