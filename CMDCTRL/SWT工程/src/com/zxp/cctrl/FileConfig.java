package com.zxp.cctrl;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.io.UnsupportedEncodingException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.io.OutputFormat;
import org.dom4j.io.SAXReader;
import org.dom4j.io.XMLWriter;

public  class FileConfig {
	
	private static String configName = "Config.xml";
	private static String RunModule= "RunModule";
	private static String RunModuleName="name" ;
	private static String RunModuleCmdStart="startcmd" ;
	private static String RunModuleCmdStop="stopcmd" ;
	private static String RunModuleCmdCustom= "CustomButton";
	private static String RunModuleCmdCustomName= "name";
	private static String RunModuleNetUrl= "netaddr";
	private static String RunModuleDBUrl= "dburl";
	private static String RunModuleAppPath = "appPath";
	private static String RunModuleLogUrl ="logpath";
	private static String RunModuleSid ="sid";
	
	private static String GobalConf = "GobalConfig";
	private static String GobalConfWebIp = "webip";
	private static String GobalConfWebPort = "webport";
	
	
	private static String RunModulecmd= "cmd";
	
	
	/**
	 * 读取配置文件
	 * */
	public static List<RunModule> readConfig(String url)
	{
		List<RunModule> runmodules=null;
		try 
		{ 
			File file =new File(configName);
			if(!file.exists())
			{
				return runmodules;
			}
			SAXReader reader = new SAXReader(); 
			Document doc = reader.read(file); 
			Element root = doc.getRootElement(); 
			Element foo; 
			runmodules= new ArrayList<RunModule>();
			for (Iterator i = root.elementIterator(RunModule); i.hasNext();)
			{ 
				//新建一个TAB页对象
				RunModule rm = new RunModule();
				foo = (Element) i.next(); 
				//获取一个TAB页的名称值
				rm.Modulename = foo.elementText(RunModuleName);
				// 获取运行按钮的命令数组
				List<Element> startcmdlist = foo.element(RunModuleCmdStart).elements(RunModulecmd);
				List<String> cmdlist = new ArrayList<String>();
				for(int j = 0;j<startcmdlist.size();j++)
				{
					String cmdvalue =startcmdlist.get(j).getText();
					cmdlist.add(cmdvalue);
				}
				rm.startcmd= cmdlist.toArray(new String[0]);
				// 获取停止按钮的命令数组
				List<Element> stopcmdlist = foo.element(RunModuleCmdStop).elements("cmd");
				List<String> stoplist = new ArrayList<String>();
				for(int j = 0;j<stopcmdlist.size();j++)
				{
					String cmdvalue =stopcmdlist.get(j).getText();
					stoplist.add(cmdvalue);
				}
				rm.stopcmd= stoplist.toArray(new String[0]);
				//获取自定义按钮MAP
				List<Element> custombuttonlist = foo.elements(RunModuleCmdCustom);
				Map<String,String[]> custombuttons = new HashMap<String,String[]>();
				for(int j = 0;j<custombuttonlist.size();j++)
				{
					String buttonname =custombuttonlist.get(j).attributeValue(RunModuleCmdCustomName);
					List<String> buttoncmd = new ArrayList<String>();
					List<Element> buttoncmdlist = 
						custombuttonlist.get(j).elements(RunModulecmd);
					for(int k=0;k<buttoncmdlist.size();k++)
					{
						String cmdvalue =buttoncmdlist.get(k).getText();
						buttoncmd.add(cmdvalue);
					}
					custombuttons.put(buttonname, buttoncmd.toArray(new String[0]));
				}
				rm.custombutton=custombuttons;
				rm.netaddr = foo.elementText(RunModuleNetUrl);
				rm.logpath = foo.elementText(RunModuleLogUrl);
				
				rm.DBurl = foo.elementText(RunModuleDBUrl);
				rm.appPath = foo.elementText(RunModuleAppPath);
				rm.sid = foo.attributeValue(RunModuleSid);
				if(rm.sid.isEmpty())
					rm.setSID();
				runmodules.add(rm);
			} 
			return runmodules;
		} 
		catch (Exception e) { 
			e.printStackTrace();
			return null;
		} 
	}
	/**
	 * 读取文件的修改时间，以便确定在程序运行中，判断有没被修改
	 * */
	public static String readfile(String url)
	{
		File file = new File(url);
		long   time=file.lastModified();
		Calendar   cal=Calendar.getInstance(); 
		cal.setTimeInMillis(time); 
		DateFormat format= new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		return format.format(cal.getTime()); 
	}
	
	
	public static void writeGobalConfig(GobalConfigParameter cp)
	{
		Element root = null;
		try {
			root = getRootElement();
		} catch (DocumentException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		Element gobal = getOnlyElement(root,GobalConf);
		Element gobal_webip = getOnlyElement(gobal,GobalConfWebIp);
		gobal_webip.setText(cp.webip);
		Element gobal_webport = getOnlyElement(gobal,GobalConfWebPort);
		gobal_webport.setText(cp.webport);
		saveConfig(root.getDocument());
	}
	
	public static GobalConfigParameter readGobalConfig(){
		GobalConfigParameter cp = new GobalConfigParameter();
		Element root = null;
		try {
			root = getRootElement();
		} catch (DocumentException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		String port = getOnlyElement(getOnlyElement(root,GobalConf),GobalConfWebPort).getText();
		String ip = getOnlyElement(getOnlyElement(root,GobalConf),GobalConfWebIp).getText();
		if(!ip.isEmpty())
			cp.webip = ip;
		if(!port.isEmpty())
			cp.webport = port;
		return cp;
	}
	
	private static Element getOnlyElement(Element parent,String elementName)
	{
		List<Element> children = parent.elements(elementName);
		if(children.size()==0)
			return parent.addElement(elementName);
		else if (children.size()>1)
		{	
			children.remove(0);
			for(Element child:children)
			{
				parent.remove(child);
			}
		}
		return children.get(0);
		
	}
	
	public static Element getRootElement() throws DocumentException
	{
		SAXReader reader = new SAXReader();
		Document doc = null;
		doc = reader.read(new File(configName));
		Element root = doc.getRootElement();
		return root;
	}
	
	public static boolean saveConfig(Document doc)
	{
		OutputFormat format = OutputFormat.createPrettyPrint();
		format.setEncoding("UTF-8"); 
		XMLWriter writer;
		try {
			writer = new XMLWriter( new FileOutputStream(configName), format );
			writer.write(doc);
			writer.close();
			return true;
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return false;
	}
	
	/**
	 * 保存配置文件
	 * */
	public static boolean writeConfig(RunModule[] rms)
	{
		Element root  = null;
		try {
			root = getRootElement();
			for(Element a:root.elements(RunModule)){
				a.detach();
			}
			for(RunModule rm:rms)
			{
				Element e_rm = root.addElement(RunModule);
				e_rm.addAttribute(RunModuleSid, rm.sid);
				e_rm.addElement(RunModuleName).addText(rm.Modulename);
				Element e_rm_cmdstart = e_rm.addElement(RunModuleCmdStart);
				for(String cmd:rm.startcmd)
				{
					e_rm_cmdstart.addElement(RunModulecmd).addText(cmd); 
				}
				Element e_rm_cmdstop = e_rm.addElement(RunModuleCmdStop);
				for(String cmd:rm.stopcmd)
				{
					e_rm_cmdstop.addElement(RunModulecmd).addText(cmd); 
				}
				for (Map.Entry<String, String[]> entry : rm.custombutton.entrySet()) {  
		            
					Element e_rm_cmdCustom = e_rm.addElement(RunModuleCmdCustom);
					String name = entry.getKey();  
					e_rm_cmdCustom.addAttribute(RunModuleCmdCustomName, name);
					
		            String[] value = entry.getValue();  
		            for(String cmd:value){
		            	e_rm_cmdCustom.addElement(RunModulecmd).addText(cmd);
		            }
		        }  
				
				e_rm.addElement(RunModuleNetUrl).addText(rm.netaddr);
				e_rm.addElement(RunModuleLogUrl).addText(rm.logpath);
				
				e_rm.addElement(RunModuleDBUrl).addText(rm.DBurl);
				e_rm.addElement(RunModuleAppPath).addText(rm.appPath);

			}
			
			saveConfig(root.getDocument());
			return true;
		} catch (DocumentException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
		
		
		return false;
/*		StringBuffer xmlcontent =  new StringBuffer();
		BufferedWriter bw = null;
		try {
			xmlcontent.delete(0, xmlcontent.length());
			xmlcontent.append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
			xmlcontent.append("<root>\n");
			OutputStream os1= new FileOutputStream(new File("config.xml"));
			OutputStreamWriter osw1 = new OutputStreamWriter(os1,"UTF-8");
			 bw = new BufferedWriter(osw1);//包装一下
			 for(int i = 0;i<rms.length;i++)
			 {
				 xmlcontent.append("<RunModule name = \""+rms[i].Modulename+"\" >\n");
				 xmlcontent.append("<startcmd>\n");
				String[] startstr = rms[i].startcmd;
				for(int a =0;a<startstr.length;a++ )
				{
					xmlcontent.append("<cmd>"+startstr[a]+"</cmd>\n");
				}
				xmlcontent.append("</startcmd>\n");
				xmlcontent.append("<stopcmd>\n");
				String[] stopstr = rms[i].stopcmd;
				for(int b =0;b<stopstr.length;b++ )
				{
					xmlcontent.append("<cmd>"+stopstr[b]+"</cmd>\n");
				}
				xmlcontent.append("</stopcmd>\n");
				Map<String,String[]> csbtn=rms[i].custombutton;
				String[] namelist = csbtn.keySet().toArray(new String[0]);
				for(int c = 0;c<namelist.length;c++)
				{
					xmlcontent.append("<custombutton name = \""+namelist[c]+"\">\n");
					String[] cmdvalues = csbtn.get(namelist[c]);
					xmlcontent.append("	<buttoncmd>\n");
					for(int c1 = 0 ;c1<cmdvalues.length;c1++)
					{
						xmlcontent.append("<cmd>"+namelist[c]+"</cmd>\n");
					}
					xmlcontent.append("	</buttoncmd>\n");
					xmlcontent.append("</custombutton>\n");
				}
				xmlcontent.append("<netaddr>\n");
				xmlcontent.append("<value>"+rms[i].netaddr+"</value>\n");
				xmlcontent.append("</netaddr>\n");
				
				xmlcontent.append("<logpath>\n");
				xmlcontent.append("<value>"+rms[i].logpath+"</value>\n");
				xmlcontent.append("</logpath>\n");
				xmlcontent.append("</RunModule>\n");
			 }
			 xmlcontent.append("</root>\n");
			bw.write(new String(xmlcontent.toString().getBytes("UTF-8"),"UTF-8"));//写出到文件
			bw.flush(); //刷新输出流
			bw.close();
			return true;
		} catch (IOException e) {
			e.printStackTrace();
			return false;
		} */
	}
	/**
	 * 导出日志方法
	 * */
	public static boolean writeLog(String log,String url )
	{
		/*BufferedWriter bw = null;
		try {
			OutputStream os1= new FileOutputStream(new File(url),true);//追加写入。
			OutputStreamWriter osw1 = new OutputStreamWriter(os1,"UTF-8");
			 bw = new BufferedWriter(osw1);//包装一下
			bw.write(new String(log.toString().getBytes("UTF-8"),"UTF-8"));//写出到文件
			bw.flush(); //刷新输出流
			bw.close();
			return true;
		} catch (IOException e) {
			e.printStackTrace();
			return false;
		}*/ 
		try {
			File file = new File(url);
			if(!file.exists())
				file.createNewFile();
			RandomAccessFile raf = null;
			raf = new RandomAccessFile(file, "rw");
			long  length = raf.length();
			raf.seek(length);
			raf.writeBytes(log);
			return true;
			
		}  catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return false;
	}
}
