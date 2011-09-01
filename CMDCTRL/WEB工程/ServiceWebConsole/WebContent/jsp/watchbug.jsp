<%@page import="java.util.Date"%>
<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<script type="text/javascript" src="jquery-1.6.2.js"></script>
<script type="text/javascript">
	$(document).
</script>
<style>
<!--
BODY{FONT-FAMILY: 宋体; FONT-SIZE: 12.6px;
SCROLLBAR-HIGHLIGHT-COLOR: buttonface;
SCROLLBAR-SHADOW-COLOR: buttonface;
SCROLLBAR-3DLIGHT-COLOR: buttonhighlight;
SCROLLBAR-TRACK-COLOR: #eeeeee;
SCROLLBAR-DARKSHADOW-COLOR: buttonshadow}
td,form,select {FONT-SIZE: 12px; LINE-HEIGHT: 20px}
input {FONT-SIZE: 12px; BORDER: #000000 1px solid;height: 18px; BACKGROUND-color: #f7f7ff; COLOR: #000000;}
.small {FONT-SIZE: 1px; LINE-HEIGHT: 1px}
.mid {FONT-SIZE: 12px;LINE-HEIGHT: 20px}
A:active {COLOR: #ff0000; TEXT-DECORATION: none}
A:hover {COLOR: #ff0000; TEXT-DECORATION: underline}
A:link {COLOR: #000000; TEXT-DECORATION: none}
A:visited {COLOR: #000000; TEXT-DECORATION: none}
-->
</style>


 
 
<html>
<head>
<title>中科惠软测试管理系统</title>

<style type="text/css">
<!--
.zf {  border: #999999; border-style: solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px}
-->
</style>

</head>
<body bgcolor=#FFFFFF>

<style>
<!--
.drag{position:relative;cursor:hand
}
#scontentmain{
position:absolute;
width:150px;
}
#scontentbar{
cursor:hand;
position:absolute;
background-color:green;
height:15;
width:100%;
top:0;
font: 9pt;
}
#scontentsub{
position:absolute;
width:100%;
top:15;
background-color:lightyellow;
border:2px solid green;
padding:1.5px;
}
-->
</style>

<script language="JavaScript1.2">
<!--

var dragapproved=false
var zcor,xcor,ycor
function movescontentmain(){
if (event.button==1&&dragapproved){
zcor.style.pixelLeft=tempvar1+event.clientX-xcor
zcor.style.pixelTop=tempvar2+event.clientY-ycor
leftpos=document.all.scontentmain.style.pixelLeft-document.body.scrollLeft
toppos=document.all.scontentmain.style.pixelTop-document.body.scrollTop
return false
}
}
function dragscontentmain(){
if (!document.all)
return
if (event.srcElement.id=="scontentbar"){
dragapproved=true
zcor=scontentmain
tempvar1=zcor.style.pixelLeft
tempvar2=zcor.style.pixelTop
xcor=event.clientX
ycor=event.clientY
document.onmousemove=movescontentmain
}
}
document.onmousedown=dragscontentmain
document.onmouseup=new Function("dragapproved=false")
//-->
</script>
<div id="scontentmain">
<div id="scontentbar" onClick="onoffdisplay()"  align="right">
<span size=1>显示/隐藏</span>
</div>
<div id="scontentsub">

<table width="200" height="200" border="0" cellpadding="0" cellspacing="0" id="topNav">
  <tr>
  		<td width="60"><em><strong>BUG单号:</strong></em></td>
        <td>BUG2011080801</td>
  </tr>
  <tr>  
    <td  class="class1"><em><strong>BUG状态:</strong></em></td>
    <td id="bugstat" ><select size="1" >
      <option selected="selected">未确认</option>
      <option>已确认 </option>
  <option> 已废止 </option>
</select></td>
  </tr>
   <tr>
   		<td colspan="2" align="right"><textarea name="textarea1" cols="28" rows="3">请解释原由</textarea><input name="Submit4" type="button"  value="提交" />  </td>
   </tr>	
   <tr>
   <td colspan="2" align="right"><input type="text" name="textfield" value="请输入BUG单号" />
      <input name="Submit3" type="button"  value="查询" /></td>
   </tr>
  <tr>
   		<td align="center"><input name="Submit1" type="button"  value="上一个" /></td>
   		<td align="center"><input name="Submit2" type="button"  value="下一个" /></td>
        
  </tr>
</table>
</div> 
</div>
</div>
<script language="JavaScript1.2">

var w=document.body.clientWidth-195
var h=50


////Do not edit pass this line///////////
w+=document.body.scrollLeft
h+=document.body.scrollTop

var leftpos=w
var toppos=h
scontentmain.style.left=w
scontentmain.style.top=h

function onoffdisplay(){
if (scontentsub.style.display=='') 
scontentsub.style.display='none'
else
scontentsub.style.display=''
}

function staticize(){
w2=document.body.scrollLeft+leftpos
h2=document.body.scrollTop+toppos
scontentmain.style.left=w2
scontentmain.style.top=h2
}
window.onscroll=staticize

</script>
<iframe width="100%" height="800" src="BUG2011082301.html"></iframe>

</body>
</html>
