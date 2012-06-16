package com.wisoft.pojo;

import java.io.Serializable;

public class UIcheckInfo implements Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	private int id;
	private String checkno;
	private int packageid;
	private String packagename;
	private int adminid;
	private String adminname;
	private int moduleid;
	private String modulename;
	private int projectid;
	private String projectname;
	private String createtime;
	private String checkedtime;
	private int checkerid;
	private String checkername;
	private String state;
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public String getCheckno() {
		return checkno;
	}
	public void setCheckno(String checkno) {
		this.checkno = checkno;
	}
	public int getPackageid() {
		return packageid;
	}
	public void setPackageid(int packageid) {
		this.packageid = packageid;
	}
	public String getPackagename() {
		return packagename;
	}
	public void setPackagename(String packagename) {
		this.packagename = packagename;
	}
	public int getAdminid() {
		return adminid;
	}
	public void setAdminid(int adminid) {
		this.adminid = adminid;
	}
	public String getAdminname() {
		return adminname;
	}
	public void setAdminname(String adminname) {
		this.adminname = adminname;
	}
	public int getModuleid() {
		return moduleid;
	}
	public void setModuleid(int moduleid) {
		this.moduleid = moduleid;
	}
	public String getModulename() {
		return modulename;
	}
	public void setModulename(String modulename) {
		this.modulename = modulename;
	}
	public int getProjectid() {
		return projectid;
	}
	public void setProjectid(int projectid) {
		this.projectid = projectid;
	}
	public String getProjectname() {
		return projectname;
	}
	public void setProjectname(String projectname) {
		this.projectname = projectname;
	}
	public String getCreatetime() {
		return createtime;
	}
	public void setCreatetime(String createtime) {
		this.createtime = createtime;
	}
	public String getCheckedtime() {
		return checkedtime;
	}
	public void setCheckedtime(String checkedtime) {
		this.checkedtime = checkedtime;
	}
	public int getCheckerid() {
		return checkerid;
	}
	public void setCheckerid(int checkerid) {
		this.checkerid = checkerid;
	}
	public String getCheckername() {
		return checkername;
	}
	public void setCheckername(String checkername) {
		this.checkername = checkername;
	}
	public String getState() {
		return state;
	}
	public void setState(String state) {
		this.state = state;
	}
	public static long getSerialVersionUID() {
		return serialVersionUID;
	}
	
	
	
	
}
