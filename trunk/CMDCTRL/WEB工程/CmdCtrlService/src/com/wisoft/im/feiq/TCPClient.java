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
	
	//����̨������IPĬ�϶˿�9527
	private String ip;
	//�û���Ϣ�б�
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
			System.out.println("���ӷ�����ʧ�ܣ���ȷ�Ϸ������Ƿ�������");
		} catch (IOException e) {
			// TODO Auto-generated catch block
			System.out.println("���ӷ�����ʧ�ܣ���ȷ�Ϸ������Ƿ�������");
		}
		recthread = new RecThread("recthread",s);
		recthread.start();
		sendthread=new  SendThread("sendthread",s);
		sendthread.start();
//		FeedThread ft = new FeedThread();
//		ft.start();
	}
	
	/**
	 *  ��������̨������״̬��Ϣ
	 * @return
	 */
	public static  List<Cmdstat> broadcast() {
		List<Cmdstat> cmds= new ArrayList<Cmdstat>();
	    String state="0&0&0&0";
	    if(returnstr!=null)
	    	state= returnstr;
	    //String state="0&����QLYG&1&AAA|1&����1QLYG&2&AAA|2&����2QLYG&3&AAA";
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
	 * ͨ��IP��֤���û����в���
	 * @param str
	 * @param ip
	 * @return
	 */
	public String send(String str)
	{
		
		sendthread.str = str;
		return "�����ɹ�";
	}
	
	public String receive()
	{
		return this.returnstr;
	}
}

/**
 * �����̨ͨѶ�ķ��������߳�
 * @author ������
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
 * �����̨ͨѶ�Ľ��ո�����״̬���߳�
 * @author ������
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

