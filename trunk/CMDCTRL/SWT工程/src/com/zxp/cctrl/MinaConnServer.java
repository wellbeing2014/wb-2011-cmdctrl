package com.zxp.cctrl;

import java.util.Queue;
import java.util.Vector;
import java.util.concurrent.ConcurrentLinkedQueue;

import org.apache.mina.core.service.IoHandlerAdapter;
import org.apache.mina.core.session.IoSession;

import wisoft.server.mina.MinaClient;

public class MinaConnServer extends Thread{
	
	// ����ģʽ
	private static MinaConnServer connserver = null;
	public synchronized static MinaConnServer getInstance()
	{
		if(connserver ==  null)
			return connserver = new MinaConnServer();
		else
			return connserver;
	}
		
	private boolean paused = false;
	private int connectedStatus = 0; //0δ���ӣ�1 �����ӣ�2����ʧ�ܣ�9�쳣
	
	
	//�¼�ģ��
	
	private Vector<MinaConnStatusListener> listeners = new Vector<MinaConnStatusListener>();
	
	private synchronized void fireStatusChanged()
	{
		MinaConnStatusEvent event  = new MinaConnStatusEvent(this);
		switch(connectedStatus){
		
			case 0:
				event.setStatus("��������");
				break;
			case 1:
				if(paused)
					event.setStatus("����ͣ");
				else
					event.setStatus("����");
				break;
			case 2:
				event.setStatus("����ʧ��");
				break;
			case 9:
				event.setStatus("�쳣");
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
			return "��������";
		case 1:
			if(paused)
				return "����ͣ";
			else
				return "����";
		case 2:
			return "����ʧ��";
		case 9:
			return "�쳣";
		default:
			return "δ֪";
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
				System.out.println("�յ���Ϣ��"+message.toString());
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
	 * ��������
	 */
	public void reConnect(){
		pauseSendServer();
		this.connectedStatus = 0;
		fireStatusChanged();
		resumeSendServer();
	}
	
	/**
	 *  ֹͣ���ͷ�����Ϣ
	 */
	public void pauseSendServer() {
		this.paused = true;
		fireStatusChanged();
	}
	
	/**
	 *  �������ͷ�����Ϣ
	 */
	public void resumeSendServer() {
		this.paused = false;
		fireStatusChanged();
	}
	
	private boolean mainStop  = false;
	
	/**
	 *  �ر�ͨѶ �˷����Ѿ����ú󲻿��ڵ����߳�
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
	 * �����Ϣ�����Ͷ�����
	 * @param message
	 */
	public synchronized void addMessage(Object message){
		if(!paused)
			messageList.offer(message);
		else 
			System.out.println("������������ͣд��ʧ��");
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
			System.out.println("��������");
			
			//�Ƿ����ӳɹ������Ӳ��ɹ������ѭ��
			while(connectedStatus==0)
			{
				if(mc1.getConnector().isDisposed())
					return;
				if(!mc1.connect())
				{	
					System.out.println("�����������ʧ�� ");
					connectedStatus =2;
					fireStatusChanged();
					paused = true;
				}
				else
				{
					System.out.println("���������ӳɹ�");
					paused = false;
					connectedStatus = 1;
					fireStatusChanged();
					break;
				}
			}
			while(!paused)
			{
				System.out.println("���ڷ���");
				try {
					Thread.sleep(100);
				}
				catch(InterruptedException e)
				{
					e.printStackTrace();
				}
				Object unsend =getMessage();
				if(unsend != null){
					System.out.println("ȡ����Ҫ���͵Ķ���");
					if(!mc1.wirte(unsend))
					{
						connectedStatus =0;
						fireStatusChanged();
						break;
					}
					//System.out.println("���´η���");
				}
			}
		}
		
	}
}
