package com.wisoft.im.feiq;

import java.net.InetAddress;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

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
		return "成功";
	}
	
	public  List<Cmdstat> client() {
		List<Cmdstat> cmds= new ArrayList<Cmdstat>();
	    String state=this.tcpclient.receive();
	    String[] cmdlist=state.split("\\|");
	    for(int i=0;i<cmdlist.length;i++)
	    {
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
	public  String docmd(String str) {
		//System.out.println(getIp());
		String ip = getIp();
		return this.tcpclient.send(str,ip);
	}
	public  String docmdAdmin(String str) {
		//System.out.println(getIp());
		
		return this.tcpclient.Adminsend(str);
	}
	 public String getIp(){
	 try{
	  MessageContext ctx = context.getMessageContext();
	  HttpServletRequest request = (HttpServletRequest)
	   ctx.get(AbstractHTTPDestination.HTTP_REQUEST);
	  String ip=request.getRemoteAddr();
	     return ip ; 
	  } catch(Exception e){
	   return "获取ip失败" ;
	  }
	 }

}
