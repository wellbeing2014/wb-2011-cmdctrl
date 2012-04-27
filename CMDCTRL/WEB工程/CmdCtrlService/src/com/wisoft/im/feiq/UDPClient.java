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
	            // ָ���˿ںţ�����������Ӧ�ó�������ͻ

	            dataSocket = new DatagramSocket(PORT+1);
	            sendDataByte = new byte[1024];
	            sendStr = "1_lbt4_09#65664#205������#0#0#0:1289671407:205����1��С����:���°����:288:������С���µ���";
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
