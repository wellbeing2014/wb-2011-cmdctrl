package com.zxp.cctrl.socket;

import java.util.EventListener;

/** 
 * ¼àÌıÆ÷½Ó¿Ú
 * @author wellbeing
 *
 */
public interface ReceiveEventListener extends EventListener {
	
	public void handleMsg(ReceiveEventObject obj);

}
