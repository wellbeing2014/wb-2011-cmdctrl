package com.flex
{
	[RemoteClass(alias="com.wisoft.im.feiq.CmdUser")] 
	[Bindable]
	public class CmdUser
	{
		private var _cmdno:int;
		
		private var _username:String;
		
		private var _password:String;
		
		private var _content:String;
		
		public function CmdUser()
		{
		}
		
		public function get cmdno():int
		{
			return this._cmdno;
		}
		public function set cmdno(value:int):void
		{
			this._cmdno = value;
		}
		
		
		public function get username():String
		{
			return this._username;
		}
		public function set username(value:String):void
		{
			this._username = value;
		}
		
		public function get password():String
		{
			return this._password;
		}
		public function set password(value:String):void
		{
			this._password = value;
		}
		
		
		public function get content():String
		{
			return this._content;
		}
		public function set content(value:String):void
		{
			this._content = value;
		}
	}
}