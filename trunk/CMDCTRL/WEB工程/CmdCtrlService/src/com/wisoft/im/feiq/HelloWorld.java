package com.wisoft.im.feiq;
import org.springframework.web.context.ContextLoader;  
import org.springframework.web.context.WebApplicationContext;  

public class HelloWorld  {   
	 
	public HelloWorld()
	{
		System.out.println("_________________________________");
		WebApplicationContext wac = ContextLoader.getCurrentWebApplicationContext();
		this.tcpclient = (TCPClient)wac.getBean("TCPClient");
	}
	private TCPClient tcpclient;
	
	
	public  String docmd(String str) {
		return this.tcpclient.send(str,getIp());
	}
	public  String docmdAdmin(String str) {
		//System.out.println(getIp());
		
		return this.tcpclient.Adminsend(str);
	}
	
    public String sayHello(String s) {   
        System.out.println("hello,");   
        return getIp()+"   "+s ;   
    }   
    
    public String getIp(){
   	 try{
   		 String ip = flex.messaging.FlexContext.getHttpRequest().getRemoteAddr();
   	     return ip ; 
   	  } catch(Exception e){
   	   return "ªÒ»°ip ß∞‹" ;
   	  }
   	 }
}  
