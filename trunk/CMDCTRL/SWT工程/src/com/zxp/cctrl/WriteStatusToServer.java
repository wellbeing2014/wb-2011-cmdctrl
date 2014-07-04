package com.zxp.cctrl;

public class WriteStatusToServer extends Thread{
	
	// 单例模式
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
			System.out.println("定时写入失败！");
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
			System.out.println("每2秒写入一次状态");
			MinaConnServer.getInstance().addMessage(MainWNDv2.getAllclientstr());
		}
		}
	}
}
