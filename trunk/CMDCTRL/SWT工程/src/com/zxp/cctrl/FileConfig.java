package com.zxp.cctrl;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;


import org.dom4j.Document;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;

public  class FileConfig {
	/**
	 * 读取配置文件
	 * */
	public static List<RunModule> readConfig(String url)
	{
		List<RunModule> runmodules=null;
		try 
		{ 
			File file =new File(url);
			if(!file.exists())
			{
				return runmodules;
			}
			SAXReader reader = new SAXReader(); 
			Document doc = reader.read(file); 
			Element root = doc.getRootElement(); 
			Element foo; 
			runmodules= new ArrayList<RunModule>();
			for (Iterator i = root.elementIterator("RunModule"); i.hasNext();)
			{ 
				//新建一个TAB页对象
				RunModule rm = new RunModule();
				foo = (Element) i.next(); 
				//获取一个TAB页的名称值
				rm.Modulename = foo.attributeValue("name");
				// 获取运行按钮的命令数组
				List<Element> startcmdlist = foo.elements("startcmd").get(0).elements("cmd");
				List<String> cmdlist = new ArrayList<String>();
				for(int j = 0;j<startcmdlist.size();j++)
				{
					String cmdvalue =startcmdlist.get(j).getText();
					cmdlist.add(cmdvalue);
				}
				rm.startcmd= cmdlist.toArray(new String[0]);
				// 获取停止按钮的命令数组
				List<Element> stopcmdlist = foo.elements("stopcmd").get(0).elements("cmd");
				List<String> stoplist = new ArrayList<String>();
				for(int j = 0;j<stopcmdlist.size();j++)
				{
					String cmdvalue =stopcmdlist.get(j).getText();
					stoplist.add(cmdvalue);
				}
				rm.stopcmd= stoplist.toArray(new String[0]);
				//获取自定义按钮MAP
				List<Element> custombuttonlist = foo.elements("custombutton");
				Map<String,String[]> custombuttons = new HashMap<String,String[]>();
				for(int j = 0;j<custombuttonlist.size();j++)
				{
					String buttonname =custombuttonlist.get(j).attributeValue("name");
					List<String> buttoncmd = new ArrayList<String>();
					List<Element> buttoncmdlist = 
						custombuttonlist.get(j).elements("buttoncmd").get(0).elements("cmd");
					for(int k=0;k<buttoncmdlist.size();k++)
					{
						String cmdvalue =buttoncmdlist.get(k).getText();
						buttoncmd.add(cmdvalue);
					}
					custombuttons.put(buttonname, buttoncmd.toArray(new String[0]));
				}
				rm.custombutton=custombuttons;
				List<Element> a =foo.elements("netaddr");
				if(a.size()>0)
				{
					List<Element> netaddr = a.get(0).elements("value");
					rm.netaddr = netaddr.get(0).getText();
				}
				List<Element> b =foo.elements("logpath");
				if(b.size()>0)
				{
					List<Element> logpath = b.get(0).elements("value");
					rm.logpath = logpath.get(0).getText();
				}
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
	/**
	 * 保存配置文件
	 * */
	public static boolean writeConfig(RunModule[] rms)
	{
		StringBuffer xmlcontent =  new StringBuffer();
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
		} 
	}
	/**
	 * 导出日志方法
	 * */
	public static boolean writeLog(String log,String url )
	{
		BufferedWriter bw = null;
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
		} 
	}
}
