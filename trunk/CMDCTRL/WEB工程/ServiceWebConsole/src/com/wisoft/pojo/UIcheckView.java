package com.wisoft.pojo;

import java.io.Serializable;

public class UIcheckView implements Serializable  {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private int id;
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
	public byte[] getSrcimage() {
		return srcimage;
	}
	public void setSrcimage(byte[] srcimage) {
		this.srcimage = srcimage;
	}
	public String getCheckmark() {
		return checkmark;
	}
	public void setCheckmark(String checkmark) {
		this.checkmark = checkmark;
	}
	public byte[] getCheckedimage() {
		return checkedimage;
	}
	public void setCheckedimage(byte[] checkedimage) {
		this.checkedimage = checkedimage;
	}
	public String getImageno() {
		return imageno;
	}
	public void setImageno(String imageno) {
		this.imageno = imageno;
	}
	private String checkno;
	private byte[] srcimage;
	private String checkmark;
	private byte[] checkedimage;
	private String imageno;

}
