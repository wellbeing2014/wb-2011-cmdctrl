package com.zxp.cctrl.socket;

import java.util.EventObject;

/**
 *  �¼������࣬�̳����¼�����
 * @author wellbeing
 *
 */
public class ReceiveEventObject extends EventObject {
	
	private static final long serialVersionUID = -6326882337029689627L;
	
	private String msg;

	public String getMsg() {
		return msg;
	}

	public ReceiveEventObject(Object source,String arg0) {
		super(source);
		msg = arg0;
	}
	
	
	
}
