package com.wisoft.im.feiq;

public class Cmdstat {
	private String name;
	private int stat;
	private int no;
	private int type;
	private String netaddr;
	public String getNetaddr() {
		return netaddr;
	}
	public void setNetaddr(String netaddr) {
		this.netaddr = netaddr;
	}
	public int getType() {
		return type;
	}
	public void setType(int type) {
		this.type = type;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public int getStat() {
		return stat;
	}
	public void setStat(int stat) {
		this.stat = stat;
	}
	public int getNo() {
		return no;
	}
	public void setNo(int no) {
		this.no = no;
	}
}
