package com.wisoft.im.feiq;


import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

public class SocketListener implements ServletContextListener {
	public void contextInitialized(ServletContextEvent event) { 
	}
	public void contextDestroyed(ServletContextEvent arg0) {
		// TODO Auto-generated method stub
		TCPClient.stoped =true;
	}
	
}