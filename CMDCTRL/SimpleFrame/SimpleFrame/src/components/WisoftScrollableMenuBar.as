package components
{
	import flash.events.Event;
	
	import mx.controls.Menu;
	import mx.controls.MenuBar;
	import mx.controls.menuClasses.IMenuBarItemRenderer;
	import mx.core.ClassFactory;
	import mx.core.mx_internal;
	import mx.events.MenuEvent;
	import mx.styles.CSSStyleDeclaration;
	import mx.styles.StyleManager;
	
	use namespace mx_internal;
	
	public class WisoftScrollableMenuBar extends MenuBar
	{
		public function WisoftScrollableMenuBar()
		{
			super();
		}
		
		private var _verticalScrollPolicy:String;
		
		public function get verticalScrollPolicy():String {
			return this._verticalScrollPolicy;
		}
		
		public function set verticalScrollPolicy(value:String):void {
			var newPolicy:String = value.toLowerCase();

	        if (_verticalScrollPolicy != newPolicy)
	        {
	            _verticalScrollPolicy = newPolicy;
	        }
        	invalidateDisplayList();
		}
		
		private var _arrowScrollPolicy:String;
    	
    	public function get arrowScrollPolicy():String {
			return this._arrowScrollPolicy;
		}
		
	    public function set arrowScrollPolicy(value:String):void {
			var newPolicy:String = value.toLowerCase();
	
		    if (_arrowScrollPolicy != newPolicy)
		    {
		    	_arrowScrollPolicy = newPolicy;
		    }
	        invalidateDisplayList();
		}
		
		override public function getMenuAt(index:int):Menu
	    {
	        var item:IMenuBarItemRenderer = menuBarItems[index];
	        var mdp:Object = item.data;
	        var menu:WisoftScrollableArrowMenu = menus[index];
	
	        if (menu == null)
	        {
	            menu = new WisoftScrollableArrowMenu();
	            menu.verticalScrollPolicy = this.verticalScrollPolicy;
	            menu.arrowScrollPolicy = this.arrowScrollPolicy;
	            
	            menu.maxHeight = this.maxHeight;
	            
	            menu.showRoot = false;
	            menu.styleName = this;
	            
	            var menuStyleName:Object = getStyle("menuStyleName");
	            if (menuStyleName)
	            {
	                var styleDecl:CSSStyleDeclaration =
	                    StyleManager.getStyleDeclaration("." + menuStyleName);
	                if (styleDecl)
	                    menu.styleDeclaration = styleDecl;
	            }
	            
	            menu.sourceMenuBar = this;
	            menu.owner = this;
	            menu.addEventListener("menuHide", eventHandler);
	            menu.addEventListener("itemRollOver", eventHandler);
	            menu.addEventListener("itemRollOut", eventHandler);
	            menu.addEventListener("menuShow", eventHandler);
	            menu.addEventListener("itemClick", eventHandler);
	            menu.addEventListener("change", eventHandler);
	            
	            menu.iconField = this.iconField;
	            menu.labelField = this.labelField;
	            menu.labelFunction = labelFunction;
	            menu.dataDescriptor = _dataDescriptor;
	            menu.itemRenderer = new ClassFactory(WisoftMenuItemRenderer);
	            menu.invalidateSize();
	
	            menus[index] = menu;
	            menu.sourceMenuBarItem = item;
	            Menu.popUpMenu(menu, null, mdp);
	        }
	        
	        if(menu.maxHeight != this.maxHeight) {
	        	menu.maxHeight = this.maxHeight;
	        }
	        
	        if(menu.verticalScrollPolicy != this.verticalScrollPolicy) {
	        	menu.verticalScrollPolicy = this.verticalScrollPolicy;
	        }
	        
	        if(menu.arrowScrollPolicy != this.arrowScrollPolicy) {
	        	menu.arrowScrollPolicy = this.arrowScrollPolicy;
	        }
	
	        return super.getMenuAt(index);
	    
	   	}
	    
	    private function eventHandler(event:Event):void
	    {
	        if (event is MenuEvent) 
	        {
	            var t:String = event.type;
	    
	    		var openMenuIndex:Number = this.selectedIndex;
	    		
	            if (event.type == MenuEvent.MENU_HIDE && 
	                MenuEvent(event).menu == menus[openMenuIndex])
	            {
	                menuBarItems[openMenuIndex].menuBarItemState = "itemUpSkin";
	                dispatchEvent(event as MenuEvent);
	                this.selectedIndex = -1;
	            }
	            else
	                dispatchEvent(event);
	        }
	    }
	}
}