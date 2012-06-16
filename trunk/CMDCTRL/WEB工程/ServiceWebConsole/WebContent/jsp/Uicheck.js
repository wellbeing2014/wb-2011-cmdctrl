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
	//alert(getQueryString("checkno"));
	UIcheckBO.getViewCount(checkno,function(data) {imgcout=data});
	UIcheckBO.getUicheckInfo(checkno, function(data) {
		dwr.util.setValue("checknoid", data.checkno);
		dwr.util.setValue("packageid", data.packagename);
		dwr.util.setValue("moduleid", data.modulename);
		dwr.util.setValue("adminid", data.adminname);
		dwr.util.setValue("projectid", data.projectname);
		dwr.util.setValue("checkedtime", data.checkedtime);
		dwr.util.setValue("checkerid",data.checkername);
		var a = document.getElementById("srcimgid");
		a.src = "../BugQuery?checkno="+checkno+"&imgno=" + imgno;
		});
	getview();
}

function sideonclick(i) {
	var a = document.getElementById("srcimgid");
	if (i < 0) {
		imgno = imgno - 1;
		if (imgno < 0) {
			alert("已经达到第一张！");
			return;
		}
	} else {
		
		if(imgno>=imgcout-1)
		{
			alert("已经达到最后一张！");
			return;
		}
		else
			imgno = imgno + 1;
		
	}
	getview();
	a.src = "../BugQuery?checkno="+checkno+"&imgno=" + imgno;
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