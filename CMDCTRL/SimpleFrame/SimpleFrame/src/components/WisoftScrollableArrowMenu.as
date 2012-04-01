package components
{
	import com.wisoft.util.IconUtility;
	
	import flash.display.DisplayObjectContainer;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.events.TimerEvent;
	import flash.utils.Timer;
	
	import flexlib.controls.ScrollableMenu;
	
	import mx.controls.LinkButton;
	import mx.controls.Button;
	import mx.controls.Image;
	import mx.controls.TextInput
	import mx.containers.Panel;
	import mx.containers.HBox;
	import mx.controls.Menu;
	import mx.controls.scrollClasses.ScrollBar;
	import mx.core.Application;
	import mx.core.ClassFactory;
	import mx.core.ScrollPolicy;
	import mx.core.mx_internal;
	import mx.events.ScrollEvent;
	
	use namespace mx_internal;
	
	[IconFile("ScrollableArrowMenu.png")]
	
	[Style(name="menuBoardColor", type="uint", format="Color", inherit="no")]
	
	public class WisoftScrollableArrowMenu extends ScrollableMenu
	{
		private var upButton:HBox;
		
		private var downButton:HBox;
		
		private var timer:Timer;
		
		public var scrollSpeed:Number = 80;
		
		public var scrollJump:Number = 1;
	   	
	   	private var _arrowScrollPolicy:String = ScrollPolicy.AUTO;
	   	
	   	
	   	[Embed(source="css/icon/tool/ArrowUp.png")]
		private var upIcon:Class;
		
		[Embed(source="css/icon/tool/ArrowDown.png")]
		private var dowmIcon:Class;


	   	
	   	
	   	public function get arrowScrollPolicy():String {
	   		return _arrowScrollPolicy;
	   	}
	   	
	   	public function set arrowScrollPolicy(value:String):void {
	   		this._arrowScrollPolicy = value;
	   		invalidateDisplayList();
	   	}
		
		public static function createMenu(parent:DisplayObjectContainer, mdp:Object, showRoot:Boolean=true):WisoftScrollableArrowMenu
	    {	
	        var menu:WisoftScrollableArrowMenu = new WisoftScrollableArrowMenu();
	        menu.itemRenderer = new ClassFactory(WisoftMenuItemRenderer);
	        menu.tabEnabled = false;
	        menu.owner = DisplayObjectContainer(Application.application);
	        menu.showRoot = showRoot;
	        popUpMenu(menu, parent, mdp);
	        return menu;
	    }
	    
		public function WisoftScrollableArrowMenu()
		{
			super();
		}
		
		private static function initializeStyles():void
		{}
		
		initializeStyles();
		
		override public function initialize():void {
			super.initialize();
			WisoftScrollableArrowMenu.initializeStyles();
		}
		
		override protected function createChildren():void {
			super.createChildren();
			
			upButton = new HBox();
			downButton = new HBox();
			
			//upButton.source=upIcon;
			//downButton.source=dowmIcon;
			
			
			//upButton.setStyle("horizontalAlign","center");
			//downButton.setStyle("horizontalAlign","center");
			upButton.setStyle("backgroundImage","css/icon/tool/ArrowUp.png");
			downButton.setStyle("backgroundImage","css/icon/tool/ArrowDown.png");
			
			upButton.setStyle("backgroundColor","white");
			downButton.setStyle("backgroundColor","white");
			
			//upButton.setStyle("source", IconUtility.getClass(upButton, "css/icon/tool/MessagePrevious.png"));
			//downButton.setStyle("source", IconUtility.getClass(downButton, "css/icon/tool/MessageNext.png"));
			
			addChild(upButton);
			addChild(downButton);
			
			upButton.addEventListener(MouseEvent.ROLL_OVER, startScrollingUp);
			upButton.addEventListener(MouseEvent.ROLL_OUT, stopScrolling);
			
			downButton.addEventListener(MouseEvent.ROLL_OVER, startScrollingDown);
			downButton.addEventListener(MouseEvent.ROLL_OUT, stopScrolling);
			
			this.addEventListener(ScrollEvent.SCROLL, checkButtons);
		}
		
	    override protected function createSubMenu():Menu {
	    	var menu :WisoftScrollableArrowMenu= new WisoftScrollableArrowMenu();
	    	menu.verticalScrollPolicy = this.verticalScrollPolicy;
	    	menu.arrowScrollPolicy = this.arrowScrollPolicy;
	    	menu.maxHeight = 400;
	    	return  menu;
	    }
		
		override protected  function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void {
			super.updateDisplayList(unscaledWidth, unscaledHeight);
			
			//if (this.width < 180) {
			//	this.width = 180;
			//}
			var w:Number = unscaledWidth;
			
			if(verticalScrollBar && verticalScrollBar.visible) {
				w = unscaledWidth - ScrollBar.THICKNESS;
			}
			
			upButton.setActualSize(w, 22);
			downButton.setActualSize(w, 22);
			
			upButton.move(0, 0);
			downButton.move(0, measuredHeight - downButton.height);
			
			checkButtons(null);
			
			//画边框
			this.graphics.lineStyle(1, getStyle("menuBorderColor"), 0.5);
			
	        this.graphics.moveTo(-1,-1);
	        this.graphics.lineTo(unscaledWidth,-1);
	        
	        this.graphics.moveTo(unscaledWidth,-1);
	        this.graphics.lineTo(unscaledWidth,unscaledHeight);
	        
	        this.graphics.moveTo(unscaledWidth,unscaledHeight);
	        this.graphics.lineTo(-1,unscaledHeight);
	        
	        this.graphics.moveTo(-1,unscaledHeight);
	        this.graphics.lineTo(-1,0);
	        
	        //画阴影
	        this.graphics.lineStyle(1, 0x999999, 0.5);
	        this.graphics.moveTo(-1,unscaledHeight+1);
	        this.graphics.lineTo(unscaledWidth,unscaledHeight+1);
	        
	        this.graphics.lineStyle(1, 0xAAAAAA, 0.5);
	        this.graphics.moveTo(-1,unscaledHeight+2);
	        this.graphics.lineTo(unscaledWidth,unscaledHeight+2);
	        
	        this.graphics.lineStyle(1, 0xCCCCCC, 0.5);
	        this.graphics.moveTo(-1,unscaledHeight+3);
	        this.graphics.lineTo(unscaledWidth,unscaledHeight+3);
	        
	        this.graphics.lineStyle(1, 0xEEEEEE, 0.5);
	        this.graphics.moveTo(-1,unscaledHeight+4);
	        this.graphics.lineTo(unscaledWidth,unscaledHeight+4);
		}
		
		private function checkButtons(event:Event):void {
			if(this.arrowScrollPolicy == ScrollPolicy.AUTO) {
				upButton.visible = upButton.enabled = (this.verticalScrollPosition != 0);
				downButton.visible = downButton.enabled = (this.verticalScrollPosition != this.maxVerticalScrollPosition);
			}
			else if(this.arrowScrollPolicy == ScrollPolicy.ON) {
				upButton.visible = downButton.visible = true;
				upButton.enabled = (this.verticalScrollPosition != 0);
				downButton.enabled = (this.verticalScrollPosition != this.maxVerticalScrollPosition);
			}
			else {
				upButton.visible = upButton.enabled = downButton.visible = downButton.enabled = false;
			}
		}
		
		private function startScrollingUp(event:Event):void {
			
	    	if(timer && timer.running) {
				timer.stop();
			}
			
			timer = new Timer(this.scrollSpeed);
			timer.addEventListener(TimerEvent.TIMER, scrollUp);
			
			timer.start();
	    }
	    
	    private function startScrollingDown(event:Event):void {
	    	
	    	if(timer && timer.running) {
				timer.stop();
			}
			
			timer = new Timer(this.scrollSpeed);
			timer.addEventListener(TimerEvent.TIMER, scrollDown);
			
			timer.start();
	    }
	    
	    private function stopScrolling(event:Event):void {
	    	event.currentTarget.removeEventListener(MouseEvent.MOUSE_UP, stopScrolling);
        	
	    	if(timer && timer.running) {
				timer.stop();
			}
		}
	    
	    private function scrollUp(event:TimerEvent):void {
	    	if(this.verticalScrollPosition - scrollJump > 0) {
	    		this.verticalScrollPosition -= scrollJump;
	    	}
	    	else {
	    		this.verticalScrollPosition = 0;
	    	}
	    	
	    	checkButtons(null);
	    }
	    
	    private function scrollDown(event:TimerEvent):void {
	    	if(this.verticalScrollPosition + scrollJump < this.maxVerticalScrollPosition) {
	    		this.verticalScrollPosition += scrollJump;
	    	}
	    	else {
	    		this.verticalScrollPosition = this.maxVerticalScrollPosition;
	    	}
	    	
	    	checkButtons(null);
	    }
		
	}
}