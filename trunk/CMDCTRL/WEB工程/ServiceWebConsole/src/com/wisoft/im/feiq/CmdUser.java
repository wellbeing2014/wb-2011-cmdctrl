package com.wisoft.im.feiq;

public class CmdUser {
	private int cmdno;
	private String username;
	private String password;
	private String content;
	private String ip;
	public String getIp() {
		return ip;
	}
	public void setIp(String ip) {
		this.ip = ip;
	}
	public int getCmdno() {
		return cmdno;
	}
	public void setCmdno(int cmdno) {
		this.cmdno = cmdno;
	}
	public String getUsername() {
		return username;
	}
	public void setUsername(String username) {
		this.username = username;
	}
	public String getPassword() {
		return password;
	}
	public void setPassword(String password) {
		this.password = password;
	}
	public String getContent() {
		return content;
	}
	public void setContent(String content) {
		this.content = content;
	}
	
}
