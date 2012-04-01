package components
{
	import mx.controls.menuClasses.MenuBarItem;
	
	public class WisoftMenuBarItem extends MenuBarItem
	{
		public function WisoftMenuBarItem()
		{
			super();
		}
		/**
		 *  @private
		 */
		override public function set menuBarItemState(value:String):void
		{
			super.menuBarItemState=value;
			var labelColor:Number;
			
			if (value == "itemDownSkin")
				labelColor = getStyle("textSelectedColor");
			else if (value == "itemOverSkin")
				labelColor = getStyle("textRollOverColor");
			else if (value == "itemUpSkin")
				labelColor = getStyle("color");
			label.setColor(labelColor);
		}   
	}
}