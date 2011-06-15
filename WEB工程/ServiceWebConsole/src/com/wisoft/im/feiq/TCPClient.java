package com.wisoft.im.feiq;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.List;

public class TCPClient {

	public Socket s ;
	
	private String ip;
	private List<String> allowiplist ;
	public List<String> getAllowiplist() {
		return allowiplist;
	}
	public void setAllowiplist(List<String> allowiplist) {
		this.allowiplist = allowiplist;
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
			System.out.print("连接服务器失败，请确认服务器是否启动！");
		} catch (IOException e) {
			// TODO Auto-generated catch block
			System.out.print("连接服务器失败，请确认服务器是否启动！");
		}
		recthread = new RecThread("recthread",s);
		recthread.start();
		sendthread=new  SendThread("sendthread",s);
		sendthread.start();
	}
	public String send(String str,String ip)
	{
		if (this.allowiplist.contains(ip))
		{
			sendthread.str = str;
			return "操作成功";
		}
		else
			return "操作失败，IP地址不受信任。";
	}
	
	public String receive()
	{
		return returnstr; 
	}
}
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
