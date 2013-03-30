var imgno = 0;
var imgcout = 0;
var checkno = getQueryString("checkno");
function update() {
	var name = dwr.util.getValue("demoName");
	UIcheckBO.getSingleView(name, function(data) {
		dwr.util.setValue("demoReply", data);
	});
}
function init() {
	
	var content="";
	UIcheckBO.getUicheckInfo(checkno, function(data) {
		dwr.util.setValue("checknoid", data.checkno);
		dwr.util.setValue("packageid", data.packagename);
		dwr.util.setValue("moduleid", data.modulename);
		dwr.util.setValue("adminid", data.adminname);
		dwr.util.setValue("projectid", data.projectname);
		dwr.util.setValue("checkedtime", data.checkedtime);
		dwr.util.setValue("checkerid",data.checkername);
		});
	UIcheckBO.getViewCount(checkno,function(data) {
		imgcout=data
		 for(var i=0 ;i<imgcout;i++){
			//alert();
			var a = "../BugQuery?checkno="+checkno+"&imgno=" + i;
			var b ;
		dwr.engine.setAsync(false);
		UIcheckBO.getSingleView(checkno,i,function(data) 
			{b= data.checkmark});
            content += "<div class=\"image\"> <a  href=\""+a+" \" rel=\"lightbox[plants]\" title=\""+b+"\"><img  src=\""+a+"\" align=\"left\" /></a></div>";
        }    
		dwr.engine.setAsync(true);
		document.getElementById("pictures").innerHTML = content;
		});
	
        
	
}


//获取当前的图片
function getview()
{
	UIcheckBO.getSingleView(checkno,imgno,function(data) 
			{dwr.util.setValue("checkmarkid", data.checkmark);});
}

// 获取URL参数
function getQueryString(name) {
	var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
	var r = window.location.search.substr(1).match(reg);
	if (r != null)
		return unescape(r[2]);
	return null;
}