package com.zxp.cctrl;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

import org.eclipse.jface.dialogs.IDialogConstants;
import org.eclipse.swt.widgets.Display;
import org.eclipse.swt.widgets.Shell;
import org.eclipse.swt.SWT;
import org.eclipse.swt.events.SelectionEvent;
import org.eclipse.swt.events.SelectionListener;
import org.eclipse.swt.layout.FormData;
import org.eclipse.swt.layout.FormAttachment;
import org.eclipse.swt.layout.GridData;
import org.eclipse.swt.layout.GridLayout;
import org.eclipse.swt.widgets.Event;
import org.eclipse.swt.widgets.Listener;
import org.eclipse.swt.widgets.MessageBox;
import org.eclipse.swt.widgets.TabFolder;
import org.eclipse.swt.widgets.TabItem;
import org.eclipse.swt.widgets.ToolBar;
import org.eclipse.swt.widgets.ToolItem;
import org.eclipse.swt.events.SelectionAdapter;
import org.eclipse.swt.graphics.Image;

public class MainWND {

	private static List<Cmdunit> allcmdunit = new ArrayList<Cmdunit>() ;
	protected Shell shell;
	private Image image_start; 
	private Image image_stop; 
	TabFolder tabFolder ;
	static ServerSocket ss;
	public static boolean STOPLESTEN = false;
//	static OutputStream os;//返回消息
//	static InputStream is;
//	static Thread aaaaaa;
	/**
	 * Launch the application.
	 * @param args
	 */
	public static void main(String[] args) {
		try {
			MainWND window = new MainWND();
			window.open();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/**
	 * Open the window.
	 */
	public void open() {
		Display display = Display.getDefault();
		image_start =this.getImage(display,"com/zxp/icon/CDAudioStartTime.png");
		image_stop =this.getImage(display,"com/zxp/icon/CDAudioStopTime.png");
		createContents();
		serverListen();
		shell.open();
		shell.layout();
		while (!shell.isDisposed()) {
			if (!display.readAndDispatch()) {
				display.sleep();
			}
		}
	}

	/**
	 * Create contents of the window.
	 */
	protected void createContents() {
		shell = new Shell();
		shell.setSize(500, 550);
		shell.setText("WISOFT服务管理平台");
		shell.setLayout(new GridLayout());
		shell.setImage(this.getImage(shell.getDisplay(),"com/zxp/icon/adminpower.png"));
		ToolBar toolBar = new ToolBar(shell, SWT.FLAT | SWT.RIGHT);
		toolBar.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		
		 tabFolder = new TabFolder(shell, SWT.NONE);
		FormData fd_tabFolder = new FormData();
		fd_tabFolder.top = new FormAttachment(0);
		fd_tabFolder.left = new FormAttachment(0);
		fd_tabFolder.bottom = new FormAttachment(0, 262);
		fd_tabFolder.right = new FormAttachment(0, 434);
		tabFolder.setLayoutData(fd_tabFolder);
		final ToolItem textItem = new ToolItem(toolBar, SWT.PUSH);
			textItem.addSelectionListener(new SelectionListener() {
				
				public void widgetSelected(SelectionEvent arg0) {
					// TODO Auto-generated method stub
					ConfigDlg cg = new ConfigDlg(shell);
					int returnnum =cg.open();
					if(returnnum==IDialogConstants.OK_ID)
					{	
						RunModule newrm = cg.rm;
						TabItem tabItem = new TabItem(tabFolder, SWT.NONE);
						tabItem.setText(newrm.Modulename);
						Cmdunit control = new Cmdunit(tabFolder,SWT.NONE,newrm);
						control.addCmdStatusChangeListener(new ICONListener(tabItem));
						tabItem.setControl(control);
						allcmdunit.add(control);
						shell.layout(true);
						saveConfig();
					}
				}
				public void widgetDefaultSelected(SelectionEvent arg0) {
					// TODO Auto-generated method stub
				}
			});
		textItem.setText("新建");
		final ToolItem textItem1 = new ToolItem(toolBar, SWT.PUSH);
		textItem1.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				TabItem[] tabsel = tabFolder.getSelection();
				if(tabsel!=null&&tabsel.length>0)
				{
					TabItem tabItem =tabsel[0];
					Cmdunit cmdcontrol=(Cmdunit)tabItem.getControl();
					if(cmdcontrol.num>0)
					{
						MessageBox mb = new MessageBox(shell);
						mb.setText("提示");
						mb.setMessage("正在运行命令,请结束后修改");
						mb.open();
					}
					else
					{
						ConfigDlg cg = new ConfigDlg(shell);
						cg.rm = cmdcontrol.runmodule;
						int returnnum =cg.open();
						if(returnnum==IDialogConstants.OK_ID)
						{	
							int no=tabFolder.getSelectionIndex();
							allcmdunit.remove(no);
							cmdcontrol.dispose();
							RunModule newrm = cg.rm;
							Cmdunit newcmd = new Cmdunit(tabFolder, SWT.NONE, newrm);
							newcmd.addCmdStatusChangeListener(new ICONListener(tabItem) );
							tabItem.setText(newrm.Modulename);
							tabItem.setControl(newcmd);
							allcmdunit.add(no,newcmd);
							saveConfig();
						}
					}
				}
			}
		});
		textItem1.setText("修改");
		final ToolItem textItem2 = new ToolItem(toolBar, SWT.PUSH);
		textItem2.addSelectionListener(new SelectionListener() {
			public void widgetSelected(SelectionEvent arg0) {
				// TODO Auto-generated method stub
				int style =SWT.APPLICATION_MODAL|SWT.YES|SWT.NO; 
				MessageBox  messageBox = new MessageBox(shell,style); 
				messageBox.setText("提示"); 
				messageBox.setMessage( "确认要删除吗？"); 
				if(messageBox.open()==SWT.YES)
				{
					TabItem[] tabsel = tabFolder.getSelection();
					if(tabsel!=null&&tabsel.length>0)
					{
						TabItem tabItem =tabsel[0];
						Cmdunit cn =(Cmdunit)tabItem.getControl();
						if(cn.num>0)
						{
							MessageBox mb = new MessageBox(shell);
							mb.setText("提示");
							mb.setMessage("正在运行命令,请结束后删除");
							mb.open();
						}
						else
						{
							int no=tabFolder.getSelectionIndex();
							allcmdunit.remove(no);
							tabItem.dispose();
							shell.layout(true);
							saveConfig();
						}
					}
				}
			}
			public void widgetDefaultSelected(SelectionEvent arg0) {
				// TODO Auto-generated method stub
			}
		});
		textItem2.setText("删除");
		List<RunModule> rmlist =null;
		if(new File("config.xml").exists())
		{
			rmlist = FileConfig.readConfig("config.xml");
		}
		
		if(rmlist==null||rmlist.size()==0)
		{
			MessageBox mb1 = new MessageBox(shell);
			mb1.setText("提示");
			mb1.setMessage("读取配置文件失败，设定默认配置");
			mb1.open();
			RunModule defaultrm =new RunModule();
			defaultrm.Modulename ="默认名称";
			boolean f=FileConfig.writeConfig(new RunModule[]{defaultrm});
			if(!f)
			{
				MessageBox mb = new MessageBox(shell);
				mb.setText("提示");
				mb.setMessage("保存默认配置失败，请手动配置后再试。");
				mb.open();
			}
			TabItem tabItem = new TabItem(tabFolder, SWT.NONE);
			tabItem.setText(defaultrm.Modulename);
			Cmdunit control = new Cmdunit(tabFolder,SWT.NONE,defaultrm);
			tabItem.setControl(control);
			allcmdunit.add(control);
		}
		else
		for(int i=0;i<rmlist.size();i++)
		{
			RunModule rm =rmlist.get(i);
			final TabItem tabItem = new TabItem(tabFolder, SWT.NONE);
			tabItem.setImage(image_stop);
			tabItem.setText(rm.Modulename);
			Cmdunit control = new Cmdunit(tabFolder,SWT.NONE,rm);
			control.addCmdStatusChangeListener(new ICONListener(tabItem) );
			tabItem.setControl(control);
			allcmdunit.add(control);
		}
		tabFolder.setLayoutData(new GridData(GridData.FILL_BOTH));
		shell.addListener(SWT.Close,   new Listener(){ 
			public  void  handleEvent(Event   event){ 
			int style =SWT.APPLICATION_MODAL|SWT.YES|SWT.NO; 
			MessageBox  messageBox = new MessageBox(shell,style); 
			messageBox.setText("提示"); 
			messageBox.setMessage( "确认要关闭吗？"); 
			if(messageBox.open()==SWT.YES)
			{
				TabItem[] items= tabFolder.getItems();
				boolean hasrun = false;
				for(int i = 0;i<items.length;i++)
				{
					Cmdunit cn =(Cmdunit)items[i].getControl();
					if(cn.num>0)
					{
						hasrun = true;
					}
				}
				if(hasrun)
				{
					MessageBox  messageBox1 = new MessageBox(shell); 
					messageBox1.setText("提示"); 
					messageBox1.setMessage("有命令正在执行不能退出平台!");
					messageBox1.open();
					event.doit = false;
				}
				else
				{
					STOPLESTEN=true;
					try {
						ss.close();
					} catch (IOException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					event.doit = true; 
				}
			}
			else
			{
				event.doit = false; 
			}
		} 
		}); 
		
	}
	
	public void saveConfig()
	{
		List<RunModule> crrentrm = new ArrayList<RunModule>();
		TabItem[] items= tabFolder.getItems();
		for(int i = 0;i<items.length;i++)
		{
			Cmdunit cn =(Cmdunit)items[i].getControl();
			crrentrm.add(cn.runmodule);
		}
		boolean saveconfig=FileConfig.writeConfig(crrentrm.toArray(new RunModule[0]));
		if(!saveconfig)
		{
			MessageBox  messageBox1 = new MessageBox(shell); 
			messageBox1.setText("提示"); 
			messageBox1.setMessage("保存配置失败");
			messageBox1.open();
		}
	}
	 public Image getImage(Display display,String url) {  
	        //返回读取指定资源的输入流  
		 Image image =null;  
		 InputStream inputstream=this.getClass().getClassLoader().getResourceAsStream(url); 
		 image = new Image(display,inputstream);
	     return image;
	 }
	 class ICONListener extends CmdStatusChangeListener
	 {
		 private TabItem tabItem; 
		public ICONListener(TabItem ti) {
			// TODO Auto-generated constructor stub
			this.tabItem = ti;
		}
		@Override
		public void cmdStatusChanged(CmdStatusChangeEvent e) {
			// TODO Auto-generated method stub
			if((Boolean)e.getSource())
			{
				tabItem.setImage(image_start);
			}
			else
				tabItem.setImage(image_stop);
		}
		 
	 }
	 
	 public static void serverListen() {
		 Runnable r = new Runnable() {
			
			public void run() {
				// TODO Auto-generated method stub
				try {
					ss = new ServerSocket(9527);
					 while (!ss.isClosed()) {
						 Socket  s = ss.accept();
			    		 new Thread(new SendServicer(s)).start();  
			    		 new Thread(new RecServicer(s)).start();   
					 }
				} catch (IOException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
			}
		};
		 new Thread(r).start();
			            
	}
	 
 	public static String getAllclientstr()
 	{
 		String returnstr="";
 		for(int i =0;i<allcmdunit.size();i++)
 		{
 			String name = allcmdunit.get(i).runmodule.Modulename;
 			String state =String.valueOf(allcmdunit.get(i).state);
 			String netaddr= allcmdunit.get(i).runmodule.netaddr;
 			returnstr+=i+"&"+name+"&"+state+"&"+netaddr+"|";
 		}
 		returnstr=returnstr.substring(0, returnstr.length()-1);
 		return 	returnstr;
 	}
	 
 	public static void doclientcmd(final String clientcmd)
	{
		Display.getDefault().asyncExec(new Runnable() {   
			//这个线程是调用UI线程控件
			public void run() {   
				String[] temp =clientcmd.split("&");
				int tabno = Integer.parseInt(temp[0]);
				int type = Integer.parseInt(temp[2]);
				if(type==1)
				{
					allcmdunit.get(tabno).button.notifyListeners(SWT.Selection, new Event());
				}
				else if	(type==2)
				{
					allcmdunit.get(tabno).button_1.notifyListeners(SWT.Selection, new Event());
				}
				
			}  
		});   
	}
}



class SendServicer implements Runnable{
	Socket s;
	public SendServicer(Socket s){
		this.s = s;
	}
	public void run(){
		try{
			OutputStream ops = s.getOutputStream();
			while(true){
				byte[] clientlist = MainWND.getAllclientstr().getBytes("GBK");
				Thread.sleep(2000);
				ops.write(clientlist);
				//dos.writeBytes("bbbb");
				if(MainWND.STOPLESTEN)
				{
					break;
				}
			}
			ops.close();
			s.close();
		}
		catch(Exception e){
			e.printStackTrace();
		}
	}
}
class RecServicer implements Runnable{
	Socket s;
	public RecServicer(Socket s){
		this.s = s;
	}
	public void run(){
		try{
			InputStream ips = s.getInputStream();
			byte[] str1 = new byte[1024];
			byte[] str2 = new byte[1024];
			int len;
			while(true)
			{
				if((len=ips.read(str1))!=-1)
				{
					String returnstr = new String(str1,0,len);
					MainWND.doclientcmd(returnstr);
					str1=str2;
				}
				else
					break;
			}
			ips.close();
			s.close();
		}
		catch(Exception e){
			e.printStackTrace();
		}
	}
}
