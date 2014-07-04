package com.zxp.cctrl;

import java.io.Serializable;

public class CmdUnitLog implements Serializable{
	
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 3273279646543250043L;
	private RunModule rm;
	private String log;
	
	public RunModule getRm() {
		return rm;
	}
	public void setRm(RunModule rm) {
		this.rm = rm;
	}
	public String getLog() {
		return log;
	}
	
	public String getLog(RunModule crm) {
		if(rm.equals(crm))
			return log;
		else
			return null;
	}
	public void setLog(String log) {
		this.log = log;
	}
	
	@Override
	public boolean equals(Object obj) {
		boolean rtn = false;
		if(obj instanceof CmdUnitLog)
		{
			rtn = rm.equals(((CmdUnitLog)obj).rm);
		}
		return rtn;
	}
	
}
