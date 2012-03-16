package com.flex
{
	public class CmdUser
	{
		private var cmdno:int;
		
		private var username:String;
		
		private var password:String;
		
		private var content:String;
		
		public function CmdUser()
		{
		}
		
		public function get getCmdno():int
		{
			return this.cmdno;
		}
		public function set setCmdno(value:int):void
		{
			this.cmdno = value;
		}
		
		
		public function get getusername():String
		{
			return this.username;
		}
		public function set setusername(value:String):void
		{
			this.username = value;
		}
		
		public function get getpassword():String
		{
			return this.password;
		}
		public function set setpassword(value:String):void
		{
			this.password = value;
		}
		
		
		public function get getcontent():String
		{
			return this.content;
		}
		public function set setcontent(value:String):void
		{
			this.content = value;
		}
	}
}