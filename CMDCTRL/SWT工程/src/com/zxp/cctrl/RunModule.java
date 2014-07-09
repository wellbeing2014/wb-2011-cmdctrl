package com.zxp.cctrl;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

public class RunModule implements Serializable{
	
	/**
	 * 
	 */
	private static final long serialVersionUID = -8084988700646041715L;
	public String sid = "";
	public String Modulename = "";
	public String[] startcmd = new String[0];
	public String[] stopcmd = new String[0];
	public String netaddr ="";
	public String logpath="";
	public String DBurl = "";
	public String appPath = "";
	
	public RunModule() {
		// TODO Auto-generated constructor stub
		//sid = setSID();
	}
	
	public Map<String,String[]> custombutton= new HashMap<String,String[]>();
	
	public  String setSID() {  
        UUID uuid = UUID.randomUUID();  
        String str = uuid.toString();  
        // È¥µô"-"·ûºÅ  
        String temp = str.substring(0, 8) + str.substring(9, 13) + str.substring(14, 18) + str.substring(19, 23) + str.substring(24);
        sid = temp;
        return temp;  
    }
	
	@Override
	public boolean equals(Object obj) {
		if(obj instanceof RunModule)
		{
			return sid.equals(((RunModule)obj).sid);
		}
		else
			return false;
	}
	
}
