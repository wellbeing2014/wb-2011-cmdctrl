package com.wisoft.im.feiq;

import java.net.InetAddress;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

import org.hyperic.sigar.Mem;
import org.hyperic.sigar.Sigar;
import org.hyperic.sigar.SigarException;

import javax.annotation.Resource;
import javax.jws.WebService;
import javax.servlet.http.HttpServletRequest;
import javax.xml.ws.WebServiceContext;
import javax.xml.ws.handler.MessageContext;

import org.apache.cxf.transport.http.AbstractHTTPDestination;

@WebService
public class ImessageForFeiQWS implements IimessageForFeiQ {
	private TCPClient tcpclient;
	 @Resource
	 private WebServiceContext context; 


	public TCPClient getTcpclient() {
		return tcpclient;
	}

	public void setTcpclient(TCPClient tcpclient) {
		this.tcpclient = tcpclient;
		
	}

	public String broadcast(String str)
	{
		UDPClient a = new UDPClient();
		for(int i=0;i<100;i++)
		{
			a.Init();
		}
		return "�ɹ�";
	}
	
	
	public SendInfo sendinfo()
	{
		SendInfo si = new SendInfo();
		si.setCmdinfo( this.tcpclient.broadcast());
		try {
			si.setMem(this.gather());
		} catch (SigarException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return si;
	}
	/**
	 *  ͨ��������IPֱ�ӷ��Ͳ�������
	 * @param str
	 * @return
	 */
	public  String docmd(String str) {
		List<String> iplist = this.tcpclient.getAllowiplist();
		if (iplist.contains(getIp()))
		{
			int cmdno = Integer.parseInt(str.split("&")[0]);
			CmdUser cu = getcmdUser(cmdno);
			if(cu.getCmdno()==cmdno&&cu.getUsername()!=null)
			{
				return "����ʧ�ܣ��÷���"+cu.getUsername()+"������" +
						"\n��ת���Զ���ģʽ�������������ϵ"+cu.getUsername();
			}
			else return this.tcpclient.send(str);
		}
		else
			return "����ʧ�ܣ�����IP��ַ�������Ρ�\n������ǹ���Ա���볢�Թ���Աģʽ��";
	}
	
	/**
	 * ͨ������Ա���뷢�Ͳ�������
	 * @param str
	 * @param password
	 * @return
	 */
	public  String docmdAdmin(String str,String password) {
		
		String adminpwd = this.tcpclient.getAdminPassWord();
		if(adminpwd.equals(password))
		{
			return this.tcpclient.send(str);
		}
		else
			return "����ʧ�ܣ�����Ա���벻��ȷ��";
	}
	
	public  static String gather() throws SigarException {     
    	Mem mem = new Sigar().getMem(); 
    	long a = mem.getFree()/1024/1024;
        return String.valueOf(a);     
    } 
	
	public  String docmdUser(String str,CmdUser user,boolean isdelete) {
		int cmdno = Integer.parseInt(str.split("&")[0]);
		CmdUser cu = getcmdUser(cmdno);
		//System.out.println();
		if(user!=null)
		{
			List<String> iplist = this.tcpclient.getAllowiplist();
			if (iplist.contains(getIp()))
			{
				if(cu.getCmdno()==cmdno)
				{
					if(user.getPassword().equals(cu.getPassword()))
					{
						if(isdelete)
						{
							this.tcpclient.map.remove(cmdno);
						}
						return this.tcpclient.send(str);
					}
					else
						return "����ʧ�ܣ��û����벻��ȷ������ϵ�û�";
				}
				else
				{
					this.tcpclient.map.remove(cmdno);
					this.tcpclient.map.put(cmdno, user);
					return this.tcpclient.send(str);
				}
			}
			else
				return "����ʧ�ܣ�����IP��ַ�������Ρ�\n������ǹ���Ա���볢�Թ���Աģʽ��";
			
		}
		else
			return "�㲻����ò�����";
		
	}
	
	/**
	 * ��ȡ�������û�����״̬
	 * @param cmdno
	 * @return
	 */
    public CmdUser getcmdUser(int cmdno) { 
    	
    	CmdUser cu = this.tcpclient.map.get(cmdno);
    	if(cu==null)
    	{
    		cu = new CmdUser();
    	}
    	cu.setIp(this.getIp());
    	return cu;
    }  
    
	 public String getIp(){
	 try{
	  MessageContext ctx = context.getMessageContext();
	  HttpServletRequest request = (HttpServletRequest)
	   ctx.get(AbstractHTTPDestination.HTTP_REQUEST);
	  String ip=request.getRemoteAddr();
	     return ip ; 
	  } catch(Exception e){
	   return "��ȡipʧ��" ;
	  }
	 }

}
