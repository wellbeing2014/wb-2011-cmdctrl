package com.flex
{
	//取得model中的值
	[RemoteClass(alias="com.wisoft.im.feiq.Cmdstat")] 
	
	[Bindable] 
	public class Cmdstat
	{
		private var _name:String;
		private var _netaddr:String;
		private var _stat:int;
		private var _no:int;
		private var _type:int;
	
		public function Cmdstat()
		{
		}
		
		public function get name():String
		{
			return _name;
		}
		
		public function set name(value:String):void
		{
			_name = value;
		}
	
		public function get netaddr():String
		{
			return _netaddr;
		}
		
		public function set netaddr(value:String):void
		{
			_netaddr = value;
		}
		
		public function get stat():int
		{
			return _stat;
		}
		
		public function set stat(value:int):void
		{
			_stat = value;
		}
		
		
		public function get no():int
		{
			return _no;
		}
		
		public function set no(value:int):void
		{
			_no = value;
		}
		
		public function get type():int
		{
			return _type;
		}
		
		public function set type(value:int):void
		{
			_type = value;
		}
	
	}
}