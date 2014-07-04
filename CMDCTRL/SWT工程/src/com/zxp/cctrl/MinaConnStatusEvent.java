package com.zxp.cctrl;

import java.util.EventObject;

public class MinaConnStatusEvent extends EventObject {

	private String status = null;
	
	public String getStatus() {
		return status;
	}
	public void setStatus(String status) {
		this.status = status;
	}
		/**
	 * 
	 */
	private static final long serialVersionUID = -3551644915379975875L;
	public MinaConnStatusEvent(Object source) {
		super(source);
		// TODO Auto-generated constructor stub
	}
	
	

}
