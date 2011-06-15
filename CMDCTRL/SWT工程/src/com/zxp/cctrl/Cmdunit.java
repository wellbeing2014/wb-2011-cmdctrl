package com.zxp.cctrl;

import org.eclipse.swt.widgets.Composite;
import org.eclipse.swt.widgets.Button;
import org.eclipse.swt.widgets.Display;
import org.eclipse.swt.widgets.FileDialog;
import org.eclipse.swt.widgets.MessageBox;
import org.eclipse.swt.SWT;
import org.eclipse.swt.events.SelectionAdapter;
import org.eclipse.swt.events.SelectionEvent;
import org.eclipse.swt.layout.GridData;
import org.eclipse.swt.layout.GridLayout;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.Iterator;
import java.util.Vector;

import org.eclipse.swt.widgets.Text;
import org.eclipse.swt.widgets.Label;

/**
 * @author wellbeing
 *
 */
public class Cmdunit extends Composite {

	public RunModule runmodule = new RunModule();
	private String errormessage = "";
	public int num = 0;
	public int state = 3;//0表示正在启动，1表示已启动,2表示正在停止，3表示已停止
	//private StringBuffer log = new StringBuffer();
	//private int logcount = 0;
	
	
	private Text text;
	private Label label ;
	public  Button button;
	public  Button button_1;
	private Button button_4;
	//private  Composite shell;
	/**
	 * Create the composite.
	 * @param parent
	 * @param style
	 */
	public Cmdunit(final Composite parent, int style,RunModule rm) {
		super(parent, style);
		runmodule = rm;
		setLayout(new GridLayout(5, false));
		button = new Button(this, SWT.NONE);
		button.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				todoText(runmodule.startcmd,0);
			}
		});
		button.setText("运行");
		
		button_1 = new Button(this, SWT.NONE);
		button_1.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				todoText(runmodule.stopcmd,1);
			}
		});
		button_1.setText("停止");
				
		final Button button_2 = new Button(this, SWT.NONE);
		button_2.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				todoText(runmodule.startcmd,0);
				todoText(runmodule.stopcmd,1);
			}
		});
		button_2.setText("重启");

		final Button button_3 = new Button(this, SWT.NONE);
		button_3.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				//log.append(text.getText());
				//FileConfig.writeLog(text.getText(),runmodule.logpath+runmodule.Modulename+".log" );
				text.setText("---------系统日志已清屏，历史记录请导出后查看。-----------\n");
			}
		});
		button_3.setText("清屏");
		button_4 = new Button(this, SWT.NONE);
		button_4.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
					//FileConfig.writeLog(text.getText(),runmodule.logpath+runmodule.Modulename+".log" );
					//text.setText("");
					MessageBox mb = new MessageBox(getShell(),SWT.ICON_INFORMATION|SWT.YES|SWT.NO);
					mb.setText("提示");
					mb.setMessage("是否打开查看日志！");
					if(mb.open()==SWT.YES)
					{
						todoMessage(new String[]{"notepad.exe "+runmodule.logpath+runmodule.Modulename+".log"},"导出");
					}
			}
		});
		button_4.setText("导出");
		
		if(this.runmodule.custombutton.size()>0)
		{
			 Iterator iterator = runmodule.custombutton.keySet().iterator();            
	            while (iterator.hasNext()) {
	             final Object key = iterator.next();
	             final Button custombutton = new Button(this, SWT.NONE);
					custombutton.addSelectionListener(new SelectionAdapter() {
						@Override
						public void widgetSelected(SelectionEvent e) {
							todoMessage(runmodule.custombutton.get(key.toString()),runmodule.Modulename+custombutton.getText());
						}
					});
				custombutton.setText(key.toString());
	         }    
		}
		
		new Label(this, SWT.NONE);
		new Label(this, SWT.NONE);
		new Label(this, SWT.NONE);
		new Label(this, SWT.NONE);
		new Label(this, SWT.NONE);
		text = new Text(this, SWT.BORDER|SWT.WRAP|SWT.V_SCROLL|SWT.MULTI);
		GridData gd_styledText = new GridData(GridData.FILL_BOTH);
		gd_styledText.horizontalSpan = 5;
		text.setLayoutData(gd_styledText);
		
		label = new Label(this, SWT.NONE);
		label.setLayoutData(new GridData(SWT.LEFT, SWT.CENTER, false, false, 5, 1));
		label.setText("正在运行命令个数："+num);
	}

	/**
	 * 重写验证类继承方法，使失效
	 */
	@Override
	protected void checkSubclass() {
		// Disable the check that prevents subclassing of SWT components
	}
	
	
	
	/**
	 * 执行命令的方法
	 * */
	public void todoText(final String[] cmdstr,final int type)
	{
		
		 Runnable r = new Runnable() {   
	            //线程运行的主体   
	            public void run() {   
	            	if (cmdstr ==null||cmdstr.length <= 0) {
	            		message("命令为空不能执行"+"\n","提示");
	            		return;
	        	    }
	            	for(int i  = 0;i<cmdstr.length;i++)
	            	{
	            		//获取操作系统名称
	            		//String osName = System.getProperty("os.name" );
	            		String cmd = cmdstr[i];
	            		if(cmd==null||"".equals(cmd))
	            			return;
	            		print("正在执行命令："+cmd);
	            		Runtime rt = Runtime.getRuntime();
	            		Process proc = null;
	            		try {
	            			proc = rt.exec("cmd /c "+cmd);
	            			//读取打印数据流
	            			final BufferedReader pin = new BufferedReader(
	            		              new InputStreamReader(proc.getInputStream()));
	            		   // OutputStream processOut = proc.getOutputStream();
	            			//读取进程输出信息
	            		    final BufferedReader perr = new BufferedReader(
	            		              new InputStreamReader(proc.getErrorStream()));
	            		    //新建一个线程，使进程输出信息和打印数据流分别显示在TEXT控件上
	            		    Thread errReadThread = new Thread() {
	            	            public void run() {
	            	              try {
	            	                String line;
	            	                while ( (line = perr.readLine()) != null) {
	            	                	print(line);
	            	                }
	            	                perr.close();
	            	              }
	            	              catch (Exception ex) {
	            	                ex.printStackTrace();
	            	              }
	            	            }
	            	          };
	            	          errReadThread.start();
	            	      //    btnCtrl(false,type);//通知按钮
	            	          activateEvent(type==0,false);
	            	          num++;
	            	          lablenum(num);
	            	        //显示打印数据流
	            	        String line =null;
	            			while ((line= pin.readLine()) != null) 
	            			{
	            				print(line);
	            			}
	            			pin.close();
	            			//等待进程结束
	            			proc.waitFor();
	            			int exitValue = proc.exitValue();
//		        	        if (exitValue != 0) {
//		        	        	message("\n命令结束时发生错误, 错误码为:" + exitValue + "\n",title);
//		        	        }
//		        	        else {
//		        	        	message("命令执行结束.\n",title);
//		        	        }
		        	     //   btnCtrl(true,type);
		        	        if(type==0)
		        	        {
		        	        	activateEvent(false,true);
		        	        }
		        	        num--;
		        	        lablenum(num);
	            		} catch (Throwable t) {
	            			t.printStackTrace();
	            		}
	            		finally
	            		{
	            			if(proc!=null)
	            			{
	            				//销毁进程
	            				proc.destroy();
	            				//proc = null;
	            			}
	            		}
	            	}
	            }   
	           
	        };   
	        new Thread(r).start(); 
	}
	public void todoMessage(final String[] cmdstr,final String title)
	{
		 Runnable r = new Runnable() {   
	            //线程运行的主体   
	            public void run() {   
	            	if (cmdstr ==null||cmdstr.length <= 0) {
	            		message("命令为空不能执行"+"\n",title);
	        	    }
	            	
	            	for(int i  = 0;i<cmdstr.length;i++)
	            	{
	            		//获取操作系统名称
	            		//String osName = System.getProperty("os.name" );
	            		String[] cmd = new String[3];
	            		cmd[0] = "cmd.exe" ;
	            		cmd[1] = "/C" ;
	            		cmd[2] = cmdstr[i];
	            		//print("正在执行命令："+cmd[2]+"\n");
	            		Runtime rt = Runtime.getRuntime();
	            		Process proc = null;
	            		try {
	            			proc = rt.exec(cmd);
	            			//读取打印数据流
	            			final BufferedReader pin = new BufferedReader(
	            		              new InputStreamReader(proc.getInputStream()));
	            		   // OutputStream processOut = proc.getOutputStream();
	            			//读取进程输出信息
	            		    final BufferedReader perr = new BufferedReader(
	            		              new InputStreamReader(proc.getErrorStream()));
	            		    
	            		    errormessage = "";//重置MESSAGE信息
	            		    //新建一个线程，使进程输出信息和打印数据流分别显示在MESSAGEBOX控件上
	            		    Thread errReadThread = new Thread() {
	            	            public void run() {
	            	              try {
	            	                String line = null;
	            	                while ( (line=perr.readLine()) != null) {
	            	                	errormessage+=line;
	            	                }
	            	                perr.close();
	            	              }
	            	              catch (Exception ex) {
	            	                ex.printStackTrace();
	            	              }
	            	            }
	            	          };
	            	          errReadThread.start();
	            	          num++;
			        	      lablenum(num);
	            	        //显示打印数据流
	            	        String line =null;
	            			while ((line=pin.readLine()) != null) 
	            			{
	            				errormessage += line;
	            			}
	            			//等待进程结束
	            			proc.waitFor();
	            			pin.close();
	            			int exitValue = proc.exitValue();
//		        	        if (exitValue != 0&&exitValue!=1) {
//		        	        	message("\n命令结束时发生错误, 错误码为:" + exitValue + "\n"+errormessage,title);
//		        	        }
//		        	        else {
//		        	        	message("命令执行结束.\n",title);
//		        	        }
		        	        num--;
		        	        lablenum(num);
	            			
	            		} catch (Throwable t) {
	            			t.printStackTrace();
	            		}
	            		finally
	            		{
	            			if(proc!=null)
	            			{
	            				//销毁进程
	            				proc.destroy();
	            				//proc = null;
	            			}
	            		}
	            	}
	            }   
	           
	        };   
	        new Thread(r).start(); 
	}
	/**
	 * 显示数据到text控件，为了防止界面假死，必须另起线程，异步调度执行线程
	 * */
	public void print(final String str)
	{
		if(str.contains("Server startup in")||str.contains("Server started in RUNNING mode"))
		{
			state = 1;// 启动完成。
		}
		Display.getDefault().asyncExec(new Runnable() {   
			//这个线程是调用UI线程控件
			public void run() {   
					text.append(str+"\n");
					FileConfig.writeLog(str+"\r\n",runmodule.logpath+runmodule.Modulename+".log"  );
			}   
		});   
	}
	/**
	 * 显示数据到状态label，为了防止界面假死，必须另起线程，异步调度执行线程
	 * */
	public void lablenum(final int num)
	{
		Display.getDefault().asyncExec(new Runnable() {   
			//这个线程是调用UI线程控件
			public void run() {   
				label.setText("正在运行命令个数："+num);
			}   
		});   
	}
	
	/**
	 * 显示字符串到MESSBAGEbox,起到提示框的作用。
	 * */
	public void message(final String messagestr,final String textstr)
	{
		Display.getDefault().asyncExec(new Runnable() {   
			//这个线程是调用UI线程控件
			public void run() {   
				MessageBox mb = new MessageBox(getShell());
				mb.setMessage(messagestr);
				mb.setText(textstr);
				mb.open();
			}   
		});   
	}
	//*************************************************************
	//自定义事件学习块
	//********************************************************************
	
	
	//定义事件源容器
	private Vector vectorListeners=new Vector();
    
	//添加事件方法
    public synchronized void addCmdStatusChangeListener(CmdStatusChangeListener ml)
    {
        vectorListeners.addElement(ml);
    }
    
    //删除事件方法
    public synchronized void removeCmdStatusChangeListener(CmdStatusChangeListener ml)
    {
        vectorListeners.removeElement(ml);
    }
    
    //触发事件的方法
    protected void activateEvent(final boolean isStart,final boolean isfinish)
    {
    	
    	Display.getDefault().asyncExec(new Runnable() {   
			//这个线程是调用UI线程控件
			public void run() {   
				if(isStart&&!isfinish) //正在启动
		    	{
					text.setText("");
		    		button.setEnabled(false);
		    		button_1.setEnabled(true);
		    		state = 0;
		    	}
		    	else if (isStart&&isfinish) // 已停止
		    	{
		    		button.setEnabled(true);
		    		button_1.setEnabled(true);
		    		state = 3;
		    	}
		    	else if (!isStart&&isfinish) // 已停止
		    	{
		    		button.setEnabled(true);
		    		button_1.setEnabled(true);
		    		state = 3;
		    	}
		    	else if (!isStart&&!isfinish) // 正在停止
		    	{
		    		button.setEnabled(true);
		    		button_1.setEnabled(true);
		    		state = 2;
		    	}
				Vector tempVector=null;
				
				CmdStatusChangeEvent e=new CmdStatusChangeEvent(isStart);
				
				synchronized(this)
				{
					tempVector=(Vector)vectorListeners.clone();
					
					for(int i=0;i<tempVector.size();i++)
					{
						CmdStatusChangeListener ml=(CmdStatusChangeListener)tempVector.elementAt(i);
						ml.cmdStatusChanged(e);
					}
				}
				
			}   
		});   
        
    }

	
}


