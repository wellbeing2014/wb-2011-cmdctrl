package com.zxp.cctrl.socket;

import java.io.DataOutputStream;
import java.net.Socket;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;

import com.zxp.cctrl.socket.msg.MsgCode;

public class ServerBroadcast implements Runnable {
	private static Socket s;
	public boolean IsStop = false;
	private Thread myThread = null;
	private List<String> UnSendList = new LinkedList<String>();
	
	public ServerBroadcast(Socket client) {
		s = client;
		myThread = new Thread(this);
		myThread.start();
	}
	
	public void  addUnSendList(String unSend) {
		synchronized (UnSendList) {
			UnSendList.add(unSend);
		}
	}

	public void run() {
		try{
			DataOutputStream ops = new DataOutputStream(s.getOutputStream());
			List<String> dealed =new ArrayList<String>();
			while(true){
				Thread.sleep(50);
				synchronized (UnSendList) {
 					for(String unSend:UnSendList)
					{
 						MsgCode.packMsg(ops, MsgCode.MType.CLIST, unSend);
 						dealed.add(unSend);
					}
 					UnSendList.removeAll(dealed);
 					dealed.clear();
				}
				if(IsStop)
				{
					break;
				}
			}
			ops.close();
			s.close();
		}
		catch(Exception e){
			e.printStackTrace();
		}
	}

}
