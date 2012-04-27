package com.wisoft.im.feiq;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import flex.messaging.MessageBroker;
import flex.messaging.messages.AsyncMessage;
import flex.messaging.util.UUIDUtils;

public class TCPClient {

	public Socket s ;
	
	//控制台服务器IP默认端口9527
	private String ip;
	//用户信息列表
	public static Map<Integer,CmdUser> map = new HashMap<Integer,CmdUser>(); 
	
	private List<String> allowiplist ;
	public List<String> getAllowiplist() {
		return allowiplist;
	}
	
	public void setAllowiplist(List<String> allowiplist) {
		this.allowiplist = allowiplist;
	}
	
	private String adminPassWord;
	public String getAdminPassWord() {
		return adminPassWord;
	}
	public void setAdminPassWord(String adminPassWord) {
		this.adminPassWord = adminPassWord;
	}
	
	public static String returnstr=null;
	SendThread sendthread;
	RecThread recthread;
	public static boolean stoped= false;
	public TCPClient()
	{
	}
	public TCPClient(String ip)
	{
		this.ip = ip;
		try {
			s = new Socket(InetAddress.getByName(ip), 9527);
		} catch (UnknownHostException e) {
			// TODO Auto-generated catch block
			System.out.println("连接服务器失败，请确认服务器是否启动！");
		} catch (IOException e) {
			// TODO Auto-generated catch block
			System.out.println("连接服务器失败，请确认服务器是否启动！");
		}
		recthread = new RecThread("recthread",s);
		recthread.start();
		sendthread=new  SendThread("sendthread",s);
		sendthread.start();
//		FeedThread ft = new FeedThread();
//		ft.start();
	}
	
	/**
	 *  发布控制台各服务状态信息
	 * @return
	 */
	public static  List<Cmdstat> broadcast() {
		List<Cmdstat> cmds= new ArrayList<Cmdstat>();
	    String state="0&0&0&0";
	    if(returnstr!=null)
	    	state= returnstr;
	    //String state="0&无锡QLYG&1&AAA|1&无锡1QLYG&2&AAA|2&无锡2QLYG&3&AAA";
	    String[] cmdlist=state.split("\\|");
	    for(int i=0;i<cmdlist.length;i++)
	    {
	    	if(!map.containsKey(i))
	    	{
	    		map.put(i, null);
	    	}
	    	Cmdstat temp = new Cmdstat();
	    	String[] temp1 = cmdlist[i].split("&");
	    	int no = Integer.parseInt(temp1[0]);
	    	String name = temp1[1];
	    	int stat = Integer.parseInt(temp1[2]);
	    	String netaddr= temp1[3];
	    	temp.setNetaddr(netaddr);
	    	temp.setName(name);
	    	temp.setNo(no);
	    	temp.setStat(stat);
	    	cmds.add(temp);
	    }
	    return cmds;
	}
	
	/**
	 * 通过IP认证的用户进行操作
	 * @param str
	 * @param ip
	 * @return
	 */
	public String send(String str)
	{
		
		sendthread.str = str;
		return "操作成功";
	}
	
	public String receive()
	{
		return this.returnstr;
	}
}

/**
 * 与控制台通讯的发送命令线程
 * @author 朱新培
 *
 */
class SendThread extends Thread {
	public String str =null;
	private Socket s;
    public SendThread(String threadName,Socket s) {
        super(threadName);
        this.s = s;
    }
    public SendThread()
    {
    }
    public void run() {
        while(!Thread.interrupted())
        {
        	try {
				Thread.sleep(500);
			} catch (InterruptedException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}
        	if(str!=null)
        	{
        		try {
        			OutputStream ops = s.getOutputStream();
        			ops.write(str.getBytes("GBK"));
        			ops.flush();
        			str = null;
        		} catch (UnknownHostException e) {
        			// TODO Auto-generated catch block
        			e.printStackTrace();
        		} catch (IOException e) {
        			// TODO Auto-generated catch block
        			e.printStackTrace();
        		}  
        	}
        	if(TCPClient.stoped)
			{
				break;
			}
        }
    }
}
/**
 * 与控制台通讯的接收各服务状态的线程
 * @author 朱新培
 *
 */
class RecThread extends Thread {
	private Socket s;
    public RecThread(String threadName,Socket s) {
        super(threadName);
        this.s = s;
    }
    public RecThread()
    {
    	
    }
    public void run() {

		// TODO Auto-generated method stub
		InputStream ips;
		try {
			ips = s.getInputStream();
			byte[] str1 = new byte[25600];
			int len;
			while(!Thread.interrupted())
			{
				if((len=ips.read(str1))!=-1)
				{
					TCPClient.returnstr = new String(str1,0,len);
				}
				if(TCPClient.stoped)
				{
					break;
				}
			}
			ips.close();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    }
}

