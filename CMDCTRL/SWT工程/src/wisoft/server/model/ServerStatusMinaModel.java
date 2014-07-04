package wisoft.server.model;

import java.io.Serializable;

public class ServerStatusMinaModel implements Serializable  {
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 3706266407446598367L;
	private String no;
	private String name;
	private String url;
	//0 Æô¶¯ 1¹Ø±Õ
	private int operation;
	private String dburl;
	private String path;
	private String sid;
	
	public String getSid() {
		return sid;
	}
	public void setSid(String sid) {
		this.sid = sid;
	}
	public String getDburl() {
		return dburl;
	}
	public void setDburl(String dburl) {
		this.dburl = dburl;
	}
	public String getPath() {
		return path;
	}
	public void setPath(String path) {
		this.path = path;
	}
	public String getNo() {
		return no;
	}
	public void setNo(String no) {
		this.no = no;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String getUrl() {
		return url;
	}
	public void setUrl(String url) {
		this.url = url;
	}
	public int getOperation() {
		return operation;
	}
	public void setOperation(int operation) {
		this.operation = operation;
	}
	
	@Override
	public String toString() {
		// TODO Auto-generated method stub
		return no+"|"+name+"|"+url+"|"+path+"|"+dburl+"&&";
	}
	
}
