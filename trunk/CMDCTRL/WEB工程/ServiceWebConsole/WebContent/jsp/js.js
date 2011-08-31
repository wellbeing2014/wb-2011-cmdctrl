var input = document.getElementById("reasonInput");
var XMLHttpReq;
function createXMLHttpRequest(){
	if(window.XMLHttpRequest)
	{
		XMLHttpReq = new XMLHttpRequest();
	}
	else if(window.ActiveXObject)
	{
		try
		{
			XMLHttpReq = new ActiveXObject("Msxm12.XMLHTTP");
		}
		catch(e)
		{
			try
			{
				XMLHttpReq = new ActiveXObject("Msxm12.XMLHTTP");
			}
			catch(e)
			{}
		}
	}
}
function sendRequest(){
	var msg = input.value;
	createXMLHttpRequest();
	var url = "BugQuery";
	XMLHttpReq.open("POST",url,true);
	XMLHttpReq.setRequestHeader("Content-Type",
				"application/x-www-form-urlencoded");
	XMLHttpReq.onreadystatechange = processResponse;
	XMLHttpReq.send("aaaa="+msg);
}
function processResponse(){
	if(XMLHttpReq.readyState==4)
	{
		if(XMLHttpReq.status==200){
			document.getElementById("reasonInput").value
			  =XMLHttpReq.responseText;
		}
	}
}
createXMLHttpRequest();
