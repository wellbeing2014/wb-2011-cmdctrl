package com.zxp.cctrl.socket;

import java.io.DataInputStream;
import java.io.IOException;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.Vector;

import com.zxp.cctrl.socket.msg.MsgCode;

public class CctrlClient implements Runnable {
	private Socket s ;
	public CctrlClient() {
		try {
			s = new Socket(InetAddress.getByName("127.0.0.1"), 9527);
		} catch (UnknownHostException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	private Vector<ReceiveEventListener> ReceiveEventCol = new Vector<ReceiveEventListener>();
	
	public void addReceiveEventListener(ReceiveEventListener listener)
	{
		ReceiveEventCol.add(listener);
	}
	
	public void removeReceiveEventListener(ReceiveEventListener listener)
	{
		ReceiveEventCol.remove(listener);
	}
	
	public void notifyEvents(ReceiveEventObject reo)
	{
		synchronized (ReceiveEventCol) {
			for(ReceiveEventListener rel:ReceiveEventCol)
			{
				rel.handleMsg(reo);
			}
		}
	}
	
	public void run() {
		// TODO Auto-generated method stub
		DataInputStream ips = null;
		 
		try {
			ips =new DataInputStream(s.getInputStream());
			while(!Thread.interrupted())
		    {
				Thread.sleep(50);
				System.out.println(MsgCode.unpackMsg(ips));
				//notifyEvents(new ReceiveEventObject(this, new String(str1,0,len)));
		    }
			ips.close();	
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InterruptedException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
	}

}
