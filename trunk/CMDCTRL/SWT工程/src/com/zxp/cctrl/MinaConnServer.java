package com.zxp.cctrl;

import java.util.Queue;
import java.util.Vector;
import java.util.concurrent.ConcurrentLinkedQueue;

import org.apache.mina.core.service.IoHandlerAdapter;
import org.apache.mina.core.session.IoSession;

import wisoft.server.mina.MinaClient;

public class MinaConnServer extends Thread{
	
	// 单例模式
	private static MinaConnServer connserver = null;
	public synchronized static MinaConnServer getInstance()
	{
		if(connserver ==  null)
			return connserver = new MinaConnServer();
		else
			return connserver;
	}
		
	private boolean paused = false;
	private int connectedStatus = 0; //0未连接，1 已连接，2连接失败，9异常
	
	
	//事件模块
	
	private Vector<MinaConnStatusListener> listeners = new Vector<MinaConnStatusListener>();
	
	private synchronized void fireStatusChanged()
	{
		MinaConnStatusEvent event  = new MinaConnStatusEvent(this);
		switch(connectedStatus){
		
			case 0:
				event.setStatus("正在连接");
				break;
			case 1:
				if(paused)
					event.setStatus("已暂停");
				else
					event.setStatus("正常");
				break;
			case 2:
				event.setStatus("连接失败");
				break;
			case 9:
				event.setStatus("异常");
				break;
		}
		
		 Vector<MinaConnStatusListener> l; 
		 synchronized(this) { 
			 l = (Vector<MinaConnStatusListener>)listeners.clone(); 
		 }
		for (int i = 0; i < l.size(); i++) {
			l.get(i).statusChanged(event);
		}
	}
	
	public String getStatus()
	{
		switch(connectedStatus){
		case 0:
			return "正在连接";
		case 1:
			if(paused)
				return "已暂停";
			else
				return "正常";
		case 2:
			return "连接失败";
		case 9:
			return "异常";
		default:
			return "未知";
		}
	}
	
	public synchronized  void removeMinaConnStatusListener(MinaConnStatusListener listener) {
		if(listener!=null)
			listeners.removeElement(listener);
	}

	public synchronized  void addMinaConnStatusListener(MinaConnStatusListener listener) {
		if(listener!=null)
			listeners.addElement(listener);
	}

	MinaClient mc1 = null;
	public MinaConnServer(){
		mc1 = new MinaClient();
		mc1.setHOST("127.0.0.1");
		mc1.setPORT(9527);
		mc1.setHANDLER(new IoHandlerAdapter(){
			
			@Override
			public void messageSent(IoSession session, Object message)
			throws Exception {
			}
			@Override
			public void messageReceived(IoSession session, Object message)
			throws Exception {
				System.out.println("收到信息："+message.toString());
			}
			
			@Override
			public void exceptionCaught(IoSession session, Throwable cause)
					throws Exception {
				System.out.println(cause.getMessage());
			}
		});
		mc1.setTIMEOUT(1000);
		mc1.createConnector();
	}
	
	
	
	/**
	 * 重新连接
	 */
	public void reConnect(){
		pauseSendServer();
		this.connectedStatus = 0;
		fireStatusChanged();
		resumeSendServer();
	}
	
	/**
	 *  停止发送服务信息
	 */
	public void pauseSendServer() {
		this.paused = true;
		fireStatusChanged();
	}
	
	/**
	 *  继续发送服务信息
	 */
	public void resumeSendServer() {
		this.paused = false;
		fireStatusChanged();
	}
	
	private boolean mainStop  = false;
	
	/**
	 *  关闭通讯 此方法已经调用后不可在调用线程
	 */
	public void close()
	{
		pauseSendServer();
		try {
			WriteStatusToServer.getInstance().join();
        } catch (InterruptedException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
		mainStop = true;
		mc1.disposeConnector();
	    connserver = null;
	}
	
	private static Queue<Object> messageList = new ConcurrentLinkedQueue<Object>();
	
	/**
	 * 添加消息到发送队列中
	 * @param message
	 */
	public synchronized void addMessage(Object message){
		if(!paused)
			messageList.offer(message);
		else 
			System.out.println("发送主体已暂停写入失败");
	}
	
	public synchronized Object getMessage(){
		if(!messageList.isEmpty())
			return messageList.poll();
		else 
			return null;
	}

	
	
	@Override
	public void run() {
		while(!mainStop){
			try {
				Thread.sleep(3000);
			}
			catch(InterruptedException e)
			{
				e.printStackTrace();
			}
			System.out.println("我在外面");
			
			//是否链接成功，链接不成功进入空循环
			while(connectedStatus==0)
			{
				if(mc1.getConnector().isDisposed())
					return;
				if(!mc1.connect())
				{	
					System.out.println("与服务器连接失败 ");
					connectedStatus =2;
					fireStatusChanged();
					paused = true;
				}
				else
				{
					System.out.println("服务器链接成功");
					paused = false;
					connectedStatus = 1;
					fireStatusChanged();
					break;
				}
			}
			while(!paused)
			{
				System.out.println("我在发送");
				try {
					Thread.sleep(100);
				}
				catch(InterruptedException e)
				{
					e.printStackTrace();
				}
				Object unsend =getMessage();
				if(unsend != null){
					System.out.println("取到了要发送的对象");
					if(!mc1.wirte(unsend))
					{
						connectedStatus =0;
						fireStatusChanged();
						break;
					}
					//System.out.println("等下次发送");
				}
			}
		}
		
	}
}
