package com.zxp.cctrl.socket;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.LinkedList;
import java.util.List;

public class ServerSocketHandle implements Runnable {
	private ServerSocket ss;
	private final static int PORT =9527;
	private  List<ServerBroadcast> socketpool = new LinkedList<ServerBroadcast>();
	
	public boolean isStop(){
		return ss==null||ss.isClosed();
	}
	public void stop(){
		synchronized (ss){
			try {
				ss.close();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			ss = null;
		}
		for(ServerBroadcast sb:socketpool)
		{
			sb.IsStop = true;
		}
		
	}
	
	public synchronized void  setMsgToPool(String msg)
	{
		for(ServerBroadcast sb:socketpool){
			sb.addUnSendList(msg);
		}
	}
	
	public static class ServerSocketInner{
		public final static ServerSocketHandle ssh = new ServerSocketHandle();
		
	}
	/**单例模式获取
	 * @return
	 */
	public static ServerSocketHandle getInstance()
	{
		return ServerSocketInner.ssh;
	}
	
	public void start()
	{
		new Thread(this).start();
	}

	public void run() {
		// TODO Auto-generated method stub
		try {
			ss = new ServerSocket(PORT);
			System.out.println("服务启动");
			 while (!ss.isClosed()) {
				 Socket  s = ss.accept();
				 System.out.println("获取一个客户端链接："+s.getLocalAddress());
				 socketpool.add(new ServerBroadcast(s));
			 }
			 System.out.println("服务关闭");
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}
