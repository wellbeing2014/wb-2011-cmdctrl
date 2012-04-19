package com.wisoft.im.feiq;

import java.io.IOException;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
   
import java.math.BigDecimal;   
import java.util.Calendar;
import java.util.Date;   
import java.util.List;
  
import flex.messaging.MessageBroker;   
import flex.messaging.messages.AsyncMessage;   
import flex.messaging.util.UUIDUtils;
/**
 * Servlet implementation class SendMsgServlet
 */
public class SendMsgServlet extends HttpServlet {
	
	private static FeedThread1 thread;   
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public SendMsgServlet() {
        super();
        // TODO Auto-generated constructor stub
    }
  
    
  
    @Override  
    protected void doGet(HttpServletRequest req, HttpServletResponse resp)   
            throws ServletException, IOException {   
  
        String cmd = req.getParameter("cmd");   
        if (cmd.equals("start")) {   
            start();   
        }   
        if (cmd.equals("stop")) {   
            stop();   
        }   
    }   
  
    @Override  
    protected void doPost(HttpServletRequest req, HttpServletResponse resp)   
            throws ServletException, IOException {   
        // TODO Auto-generated method stub   
        super.doGet(req, resp);   
    }   
  
    @Override  
    public void destroy() {   
        // TODO Auto-generated method stub   
        super.destroy();   
    }   

	
	public void start() {   
        if (thread == null) {   
            thread = new FeedThread1();   
            thread.start();   
        }   
        System.out.println("start!!");   
    }   
  
    public void stop() {   
        thread.running = false;   
        thread = null;   
    }  

}

class FeedThread1 extends Thread {   
	  
    public boolean running = true;   

	public void run() {
		MessageBroker msgBroker = MessageBroker.getMessageBroker(null);
		while(msgBroker==null)
		{
			msgBroker = MessageBroker.getMessageBroker(null);
		}
		String clientID = UUIDUtils.createUUID();
		int i = 0;
		while (running) {
			System.out.println("1231231231231231"+i);
			List<Cmdstat> cmdlist=TCPClient.broadcast();
			AsyncMessage msg = new AsyncMessage();
			msg.setDestination("CmdCtrlService");
			msg.setHeader("DSSubtopic", "List<Cmdstat>");
			msg.setClientId(clientID);
			msg.setMessageId(UUIDUtils.createUUID());
			msg.setTimestamp(System.currentTimeMillis());
			msg.setBody(cmdlist);
			msgBroker.routeMessageToService(msg, null);
			i++;
			try {
				Thread.sleep(2000);
			} 
			catch (InterruptedException e) {
			}
		}
		System.out.println(Calendar.getInstance().toString()+"我操状态发送blazeds线程退出了");
	}  
}   

