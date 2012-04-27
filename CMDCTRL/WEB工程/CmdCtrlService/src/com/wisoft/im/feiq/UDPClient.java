package com.wisoft.im.feiq;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;

public class UDPClient {
	 private static final int PORT = 2425;
	    private DatagramSocket dataSocket;
	    private DatagramPacket dataPacket;
	    private byte sendDataByte[];
	    private String sendStr;

	    public UDPClient() {
	    	try {
	            // 指定端口号，避免与其他应用程序发生冲突

	            dataSocket = new DatagramSocket(PORT+1);
	            sendDataByte = new byte[1024];
	            sendStr = "1_lbt4_09#65664#205服务器#0#0#0:1289671407:205飞秋1号小月月:更新包监控:288:来人吗？小月月等你";
	            sendDataByte = sendStr.getBytes();
	            dataPacket = new DatagramPacket(sendDataByte, sendDataByte.length,
	                    InetAddress.getByName("127.0.0.1"), PORT);
	            dataSocket.send(dataPacket);
	            dataSocket.close();
	        } catch (SocketException se) {
	            se.printStackTrace();
	        } catch (IOException ie) {
	            ie.printStackTrace();
	        }
	    }

	    public void Init() {
	        
	    }
}
