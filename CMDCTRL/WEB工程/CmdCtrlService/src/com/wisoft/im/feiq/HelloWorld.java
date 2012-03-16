package com.wisoft.im.feiq;
import java.util.List;

import org.springframework.web.context.ContextLoader;  
import org.springframework.web.context.WebApplicationContext;  

/**
 * @author 朱新培
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
	 *  通过受信任IP直接发送操作命令
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
				return "操作失败，该服务被"+cu.getUsername()+"锁定，" +
						"\n请转到自定义模式下输入密码或联系"+cu.getUsername();
			}
			else return this.tcpclient.send(str);
		}
		else
			return "操作失败，您的IP地址不受信任。\n如果您是管理员，请尝试管理员模式。";
		
	}
	
	/**
	 * 通过管理员密码发送操作命令
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
			return "操作失败，管理员密码不正确。";
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
						return "操作失败，用户密码不正确，请联系用户";
				}
				else
				{
					this.tcpclient.map.remove(cmdno);
					this.tcpclient.map.put(cmdno, user);
					return this.tcpclient.send(str);
				}
			
			}
			else
				return "操作失败，您的IP地址不受信任。\n如果您是管理员，请尝试管理员模式。";
			
		}
		else
			return "你不允许该操作。";
		
	}
	
	/**
	 * 获取各服务用户锁定状态
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
   	   return "获取ip失败" ;
   	  }
   	 }
}  
