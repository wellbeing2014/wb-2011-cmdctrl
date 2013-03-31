package com.wisoft.pojo;

import java.io.Serializable;

public class ModuleInfo implements Serializable{

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	private int id;
	private String fullname;
	private String code;
	private String createtimes;
	private String lastversion;
	private int managerid;
	private String managername;
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public String getFullname() {
		return fullname;
	}
	public void setFullname(String fullname) {
		this.fullname = fullname;
	}
	public String getCode() {
		return code;
	}
	public void setCode(String code) {
		this.code = code;
	}
	public String getCreatetimes() {
		return createtimes;
	}
	public void setCreatetimes(String createtimes) {
		this.createtimes = createtimes;
	}
	public String getLastversion() {
		return lastversion;
	}
	public void setLastversion(String lastversion) {
		this.lastversion = lastversion;
	}
	public int getManagerid() {
		return managerid;
	}
	public void setManagerid(int managerid) {
		this.managerid = managerid;
	}
	public String getManagername() {
		return managername;
	}
	public void setManagername(String managername) {
		this.managername = managername;
	}
	

}
