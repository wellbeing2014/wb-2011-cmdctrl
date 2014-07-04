package wisoft.server.mina;

import java.net.InetSocketAddress;

import org.apache.mina.core.future.ConnectFuture;
import org.apache.mina.core.future.WriteFuture;
import org.apache.mina.core.service.IoConnector;
import org.apache.mina.core.service.IoHandler;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolCodecFilter;
import org.apache.mina.filter.codec.serialization.ObjectSerializationCodecFactory;
import org.apache.mina.transport.socket.nio.NioSocketConnector;

public class MinaClient {

	private String HOST = "127.0.0.1";
	private int PORT = 9527;
	private int TIMEOUT = 3000;
	private IoHandler  HANDLER = null;
	
	public String getHOST() {
		return HOST;
	}
	public void setHOST(String hOST) {
		HOST = hOST;
	}
	public int getPORT() {
		return PORT;
	}
	public void setPORT(int pORT) {
		PORT = pORT;
	}
	public int getTIMEOUT() {
		return TIMEOUT;
	}
	public void setTIMEOUT(int tIMEOUT) {
		TIMEOUT = tIMEOUT;
	}
	public IoHandler getHANDLER() {
		return HANDLER;
	}
	public void setHANDLER(IoHandler hANDLER) {
		HANDLER = hANDLER;
	}
	
	private IoConnector connector = null;
	ObjectSerializationCodecFactory oscf = null;
	public MinaClient(){
		System.out.println("�½�ͨѶ�ͻ���");
		oscf = new ObjectSerializationCodecFactory();
		oscf.setDecoderMaxObjectSize(1024*1024);
		oscf.setEncoderMaxObjectSize(1024*1024);
		//createConnector();
	}	
	
	public void disposeConnector()
	{
		System.out.println("��������");
		if(session!=null)
		{
			session.close(true);
			session.getCloseFuture().awaitUninterruptibly(); 
		}
		connector.dispose();
	}
	 
	public void createConnector()
	{
		// ����һ���������Ŀͻ��˳���
		connector = new NioSocketConnector();
		
		// �������ӳ�ʱʱ��
		connector.setConnectTimeoutMillis(TIMEOUT);
		// ��ӹ�����
		connector.getFilterChain().addLast(   //�����Ϣ������
				"codec",
				new ProtocolCodecFilter(oscf));
		connector.setDefaultRemoteAddress(new InetSocketAddress(
				HOST, PORT));
		connector.setHandler(HANDLER);// ���ҵ����
	}
	
	
	private IoSession session = null;
	public IoSession getSession() {
		return session;
	}
	public void setSession(IoSession session) {
		this.session = session;
	}

	ConnectFuture future = null;
	public ConnectFuture getFuture() {
		return future;
	}
	public boolean connect(){
		
		future =getConnector().connect();// ��������
		future.awaitUninterruptibly();// �ȴ����Ӵ������
		if( future.isConnected())
			setSession(future.getSession());
		return future.isConnected();
		
	}
	
	
	public boolean wirte(Object obj){
		WriteFuture writeFuture = 
			session.write(obj);
		 writeFuture.awaitUninterruptibly();  
		 if(writeFuture.isWritten())
			 System.out.println("�������");
		 return writeFuture.isWritten(); 
	}
	
	public void waitClose(){
		session.getCloseFuture().awaitUninterruptibly();// �ȴ����ӶϿ�
		getConnector().dispose();
	}
	public void setConnector(IoConnector connector) {
		this.connector = connector;
	}
	public IoConnector getConnector() {
		return connector;
	}
	
}
