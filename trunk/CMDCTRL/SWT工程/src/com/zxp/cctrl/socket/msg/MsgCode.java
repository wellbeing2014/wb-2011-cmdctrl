package com.zxp.cctrl.socket.msg;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class MsgCode {

	public enum MType{
		CLIST(0),CCONTENT(1),CDOIT(2);
		private int type;
		public int getType(){
			return type;
		}
		private MType(int _type){
			type = _type;
		}
	}
	
	private static int packHead = 287667690;
	
	/**封装 要传输的字符串，使知道类型和大小长度
	 * @param msg
	 */
	public static void packMsg(DataOutputStream out,MType type,String msg)throws IOException
	{
		byte[] bytes= msg.getBytes();    
        int totalLen = 4 + 1 + bytes.length;    
      //  out.writeInt(packHead);
        out.writeInt(totalLen);
        out.writeInt(1000);
        System.out.println("要发送的大小："+totalLen);
        out.write(bytes);
        out.flush();
	}
	
	/**解包要传输的字符串
	 * @throws IOException
	 */
	public static String unpackMsg(DataInputStream dis)throws IOException
	{
		 int totalLen = dis.readInt();
         byte flag = dis.readByte(); 
         System.out.println("接收消息类型"+flag);    
         byte[] data = new byte[totalLen - 4 -1]; 
         dis.readFully(data);
         String msg = new String(data);
         return msg;
	}
	
}
