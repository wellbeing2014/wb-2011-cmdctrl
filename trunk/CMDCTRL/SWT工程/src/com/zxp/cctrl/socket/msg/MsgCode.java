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
	
	/**��װ Ҫ������ַ�����ʹ֪�����ͺʹ�С����
	 * @param msg
	 */
	public static void packMsg(DataOutputStream out,MType type,String msg)throws IOException
	{
		byte[] bytes= msg.getBytes();    
        int totalLen = 4 + 1 + bytes.length;    
      //  out.writeInt(packHead);
        out.writeInt(totalLen);
        out.writeInt(1000);
        System.out.println("Ҫ���͵Ĵ�С��"+totalLen);
        out.write(bytes);
        out.flush();
	}
	
	/**���Ҫ������ַ���
	 * @throws IOException
	 */
	public static String unpackMsg(DataInputStream dis)throws IOException
	{
		 int totalLen = dis.readInt();
         byte flag = dis.readByte(); 
         System.out.println("������Ϣ����"+flag);    
         byte[] data = new byte[totalLen - 4 -1]; 
         dis.readFully(data);
         String msg = new String(data);
         return msg;
	}
	
}
