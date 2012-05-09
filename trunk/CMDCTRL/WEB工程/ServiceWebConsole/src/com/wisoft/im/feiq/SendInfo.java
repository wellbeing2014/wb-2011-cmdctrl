package com.wisoft.im.feiq;

import java.util.List;

public class SendInfo {
	private List<Cmdstat> cmdinfo;
	private String mem;
	public List<Cmdstat> getCmdinfo() {
		return cmdinfo;
	}
	public void setCmdinfo(List<Cmdstat> cmdinfo) {
		this.cmdinfo = cmdinfo;
	}
	public String getMem() {
		return mem;
	}
	public void setMem(String mem) {
		this.mem = mem;
	}

}
