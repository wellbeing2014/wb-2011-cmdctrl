<%@ page contentType="text/html; charset=utf-8" language="java"  errorPage="" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <script type='text/javascript' src='../dwr/engine.js'> </script>
  <script type='text/javascript' src='../dwr/util.js'> </script>
  <script charset="utf-8" language="javascript" type="text/javascript" type='text/javascript' src='../dwr/interface/UIcheckBO.js'> </script>
  <script charset="gb2312" language="javascript" type="text/javascript" src='Uicheck.js'> </script>
  <script src="js/jquery-1.7.2.min.js"></script>
  <script src="js/lightbox.js"></script>
	<link rel="stylesheet" href="style.css" />
	<link href="css/lightbox.css" rel="stylesheet" />
<title>中科惠软软件界面检查表</title>
<style type="text/css">

</style>
</head>

<body onload="init();">

<div class="Section1" style='layout-grid:15.6pt'>
  <h1 align="center" style='margin-top:24.0pt;margin-right:0cm;margin-bottom:15.6pt;
margin-left:0cm;mso-add-space:auto;text-align:center;text-indent:0cm;
mso-list:none'><span style='font-size:22.0pt;line-height:150%;font-family:华文新魏;
mso-fareast-language:ZH-CN'>中科惠软软件界面检查表<span lang="EN-US" xml:lang="EN-US">
    <o:p></o:p>
  </span></span></h1>
  <div align="center">
    <table class="MsoNormalTable" border="1" cellspacing="0" cellpadding="0" width="568"
style='width:426.1pt;border:solid #1F497D 1.5pt;mso-border-alt:solid #1F497D 1.5pt;
mso-yfti-tbllook:1184;mso-padding-alt:0cm 5.4pt 0cm 5.4pt;mso-border-insideh:
1.0pt solid #1F497D;mso-border-insidev:1.0pt solid #1F497D'>

<!--<tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes'>
	<td width="568" colspan="6" align="left" style='width:213.0pt;border:none;padding:0cm 5.4pt 0cm 5.4pt'>
		<span id="checknoid" style='mso-fareast-language:ZH-CN' lang="EN-US" xml:lang="EN-US">NO:BUG2012060101</span>
	</td>
	
</tr>-->
<tr style='mso-yfti-irow:1;height:31.6pt'>
	<td width="568" valign="bottom" class="fonttitle" >
		<span>检查编号：</span>
	</td>
	<td  class="fonttitle1" >
    	<span id="checknoid"></span>
    </td>
	
    <td width="568" class="fonttitle">
		<span>检查人：</span>
	</td>
	<td  class="fonttitle1">
    	<span id="checkerid"></span>
    </td>
    <td width="568" class="fonttitle">
		<span>检查时间：</span>
	</td>
	<td  class="fonttitle1">
    	<span id="checkedtime"></span>
    </td>
</tr>
<tr style='mso-yfti-irow:2'>
  	<td class="fonttitle">
    	<span>检查对象：</span>
    </td>
  	<td colspan="5" class="fonttitle1">
    	<span id="packageid"></span>
    </td>
</tr>
<tr style='mso-yfti-irow:3'>
    <td width="568" class="fonttitle">
   		<span>责任主管：</span>
    </td>
    <td width="568" class="fonttitle1">
    	<span id="adminid"></span>
    </td>
    <td width="568" class="fonttitle">
        <span>所属模块：</span>
    </td>
    <td width="568" class="fonttitle1">
        <span id="moduleid"></span>
    </td>
	<td width="568" class="fonttitle">
    	<span>所属项目：</span>
    </td>
    <td width="568" class="fonttitle1">
        <span id="projectid"></span>
    </td>
</tr>
<tr style='mso-yfti-irow:4'>
    <td width="568" class="fonttitle">
    <span>说明概要：</span>
    </td>
    <td width="568" colspan="5" class="fonttitle1">
        <span id="checkmarkid"></span>
    </td>
</tr>
<tr style='mso-yfti-irow:5'>
    <td width="568" colspan="6" class="fonttitle">
    <span>图片说明：</span>
    </td>
</tr>
<tr style='mso-yfti-irow:6;mso-yfti-lastrow:yes;height:424.3pt'>
<td colspan="6" valign="top" width="426.1pt" >		
    <div id="pictures" class="picbox">
       
    </div>
</td>
</tr>
</table>
  </div>
  <p class="MsoNormal1"><span lang="EN-US" xml:lang="EN-US">
    <o:p>&nbsp;</o:p>
  </span></p>
</div>
</body>
</html>