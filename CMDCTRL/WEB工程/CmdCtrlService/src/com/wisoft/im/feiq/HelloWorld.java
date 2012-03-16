package com.wisoft.im.feiq;
import java.util.List;

import org.springframework.web.context.ContextLoader;  
import org.springframework.web.context.WebApplicationContext;  

/**
 * @author ������
 *
 */
public class HelloWorld  {   
	 
	public HelloWorld()
	{
		WebApplicationContext wac = ContextLoader.getCurrentWebApplicationContext();
		this.tcpclient = (TCPClient)wac.getBean("TCPClient");
	}
	private TCPClient tcpclient;
	
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
			if(cu!=null)
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
	
	public  String docmdUser(String str,CmdUser user,boolean isdelete) {
		int cmdno = Integer.parseInt(str.split("&")[0]);
		CmdUser cu = getcmdUser(cmdno);
		//System.out.println();
		if(user!=null)
		{
			List<String> iplist = this.tcpclient.getAllowiplist();
			if (iplist.contains(getIp()))
			{
				if(cu!=null)
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
    	return this.tcpclient.map.get(cmdno);
    }   
    
    public String getIp(){
   	 try{
   		 String ip = flex.messaging.FlexContext.getHttpRequest().getRemoteAddr();
   	     return ip ; 
   	  } catch(Exception e){
   	   return "��ȡipʧ��" ;
   	  }
   	 }
}  
