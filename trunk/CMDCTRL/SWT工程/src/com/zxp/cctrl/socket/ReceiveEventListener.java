package com.zxp.cctrl.socket;

import java.util.EventListener;

/** 
 * �������ӿ�
 * @author wellbeing
 *
 */
public interface ReceiveEventListener extends EventListener {
	
	public void handleMsg(ReceiveEventObject obj);

}
