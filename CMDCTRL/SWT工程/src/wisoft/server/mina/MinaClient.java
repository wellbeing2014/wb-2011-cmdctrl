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
		System.out.println("新建通讯客户端");
		oscf = new ObjectSerializationCodecFactory();
		oscf.setDecoderMaxObjectSize(1024*1024);
		oscf.setEncoderMaxObjectSize(1024*1024);
		//createConnector();
	}	
	
	public void disposeConnector()
	{
		System.out.println("销毁链接");
		if(session!=null)
		{
			session.close(true);
			session.getCloseFuture().awaitUninterruptibly(); 
		}
		connector.dispose();
	}
	 
	public void createConnector()
	{
		// 创建一个非阻塞的客户端程序
		connector = new NioSocketConnector();
		
		// 设置链接超时时间
		connector.setConnectTimeoutMillis(TIMEOUT);
		// 添加过滤器
		connector.getFilterChain().addLast(   //添加消息过滤器
				"codec",
				new ProtocolCodecFilter(oscf));
		connector.setDefaultRemoteAddress(new InetSocketAddress(
				HOST, PORT));
		connector.setHandler(HANDLER);// 添加业务处理
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
		
		future =getConnector().connect();// 创建连接
		future.awaitUninterruptibly();// 等待连接创建完成
		if( future.isConnected())
			setSession(future.getSession());
		return future.isConnected();
		
	}
	
	
	public boolean wirte(Object obj){
		WriteFuture writeFuture = 
			session.write(obj);
		 writeFuture.awaitUninterruptibly();  
		 if(writeFuture.isWritten())
			 System.out.println("发送完成");
		 return writeFuture.isWritten(); 
	}
	
	public void waitClose(){
		session.getCloseFuture().awaitUninterruptibly();// 等待连接断开
		getConnector().dispose();
	}
	public void setConnector(IoConnector connector) {
		this.connector = connector;
	}
	public IoConnector getConnector() {
		return connector;
	}
	
}
