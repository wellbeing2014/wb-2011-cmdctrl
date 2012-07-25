/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/7/10
 * 时间: 10:24
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Xml;

namespace WisoftUpdateTool
{
	/// <summary>
	/// Description of UpdateInfo.
	/// </summary>
	public class UpdateInfo
	{
		//private static string ;
		
		public static string Name {
			get { return XmlHelper.Read("/root/name",""); }
			set { XmlHelper.Update("/root/name","", value); }
		}
		
		public static string Code {
			get { return XmlHelper.Read("/root/code",""); }
			
		}
		
		public static string Ver {
			get { return XmlHelper.Read("/root/version",""); }
			
		}
		
		public static string PublishDate {
			get { return XmlHelper.Read("/root/publish_date",""); }
			
		}
		
		public static string UpdateNote {
			get { return XmlHelper.Read("/root/updatenote",""); }
			
		}
		public static Update_File[] UpdateFiles {
			get { 
				
				XmlNodeList xl = XmlHelper.ReadChild("/root/update_files");
				Update_File[] uf = new Update_File[xl.Count];
				for (int i = 0; i < xl.Count; i++) {
					XmlAttributeCollection jc = (xl.Item(i)).Attributes;
					uf[i] = new Update_File();
					uf[i].Name = jc["name"].Value;
					uf[i].Action = jc["action"].Value;
					uf[i].Fileurl = jc["fileurl"].Value;
				}
				return uf;
			}
			
		}
		
		public static Manual_File[] Manual_Files {
			get {
				XmlNodeList xl = XmlHelper.ReadChild("/root/before_configs");
				Manual_File[] uf = new Manual_File[xl.Count];
				for (int i = 0; i < xl.Count; i++) {
					XmlAttributeCollection jc = (xl.Item(i)).Attributes;
					uf[i] = new Manual_File();
					uf[i].Name = jc["name"].Value;
					uf[i].Fileurl = jc["fileurl"].Value;
					uf[i].Content =  (xl.Item(i)).InnerText;
				}
				return uf;
			}
		}
		
		public static string DBpassword {
		get { return XmlHelper.Read("/root/DBConfig/password",""); }
		
		}
		public static string DBusername {
		get { return XmlHelper.Read("/root/DBConfig/username",""); }
		
		}
		public static string DBSID {
		get { return XmlHelper.Read("/root/DBConfig/SID",""); }
		
		}
				
		
		public UpdateInfo()
		{
		}
	}
	
	public class Manual_File
	{
		private string _name;
		
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		private string _fileurl;
		
		public string Fileurl {
			get { return _fileurl; }
			set { _fileurl = value; }
		}
		private string _content;
		
		public string Content {
			get { return _content; }
			set { _content = value; }
		}
		private string _oderno;
		
		public string Oderno {
			get { return _oderno; }
			set { _oderno = value; }
		}
	}
	
	public class Update_File
	{
		private string _name;
		
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		private string _action;
		
		public string Action {
			get { return _action; }
			set { _action = value; }
		}
		private string _fileurl;
		
		public string Fileurl {
			get { return _fileurl; }
			set { _fileurl = value; }
		}
	}
}
