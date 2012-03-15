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
		
		public get cmdno():int
		{
			return this.cmdno;
		}
		public set cmdno(value:int):void
		{
			this.cmdno = value;
		}
		
		
		public get username():String
		{
			return this.username;
		}
		public set username(value:String):void
		{
			this.username = value;
		}
		
		public get password():String
		{
			return this.password;
		}
		public set password(value:String):void
		{
			this.password = value;
		}
		
		
		public get content():String
		{
			return this.content;
		}
		public set content(value:String):void
		{
			this.content = value;
		}
	}
}