package com.zxp.cctrl;

public class WriteStatusToServer extends Thread{
	
	// ����ģʽ
	private static WriteStatusToServer connserver = null;
	public synchronized static WriteStatusToServer getInstance()
	{
		if(connserver ==  null)
			return connserver = new WriteStatusToServer();
		else
			return connserver;
	}
	
	public void close(){
		paused = true;
		writeStop = true;
	}
	
	private boolean paused = false;
	private boolean writeStop = false;
	@Override
	public void run() {
		while(!writeStop){
			System.out.println("��ʱд��ʧ�ܣ�");
			try {
				Thread.sleep(2000);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		while(!paused){
			try {
				Thread.sleep(2000);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			System.out.println("ÿ2��д��һ��״̬");
			MinaConnServer.getInstance().addMessage(MainWNDv2.getAllclientstr());
		}
		}
	}
}
