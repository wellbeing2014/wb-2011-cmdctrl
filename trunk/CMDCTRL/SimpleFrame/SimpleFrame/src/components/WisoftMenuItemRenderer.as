package components
{
	import flash.display.DisplayObject;
	import flash.events.IOErrorEvent;
	
	import mx.controls.Image;
	import mx.controls.menuClasses.MenuItemRenderer;
	
	[Style(name="menuBackgroundColor", type="uint", format="Color", inherit="no")]
	
	public class WisoftMenuItemRenderer extends MenuItemRenderer
	{
		public function WisoftMenuItemRenderer()
		{
			super();
		}
		
		private var image:Image;

		override protected function commitProperties():void
		{
		     super.commitProperties();
				
		     if(image && icon)
		     {
		         if( this.contains(DisplayObject(icon)) )
		         {
		             this.removeChild(DisplayObject(icon))
		         }    
		
		         icon = null;
		         image = null;
		     }
		
			 // attempt to add the icon
			 if (data)
			 {
			     try
			     {
			         // retrieve the icon reference
			         var iconString:String = data.minicon;
			
			         if(iconString!=null && iconString.length)
			         {
			              image = new Image();
			              image.source = iconString;
						  image.addEventListener(IOErrorEvent.IO_ERROR, function(event:IOErrorEvent):void{
						  	  icon = null;
						  });
			              
			              icon = image;
			              this.addChild(DisplayObject(icon));
			          }
			
			      }catch (error:Error){}
			 }
		}
		
		override protected function updateDisplayList(unscaledWidth:Number,
												  unscaledHeight:Number):void
		{
			super.updateDisplayList(unscaledWidth, unscaledHeight);
			this.label.x = this.label.x + 14;
		}
	}
}