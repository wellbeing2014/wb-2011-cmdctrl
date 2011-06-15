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
	public int state = 3;//0��ʾ����������1��ʾ������,2��ʾ����ֹͣ��3��ʾ��ֹͣ
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
		button.setText("����");
		
		button_1 = new Button(this, SWT.NONE);
		button_1.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				todoText(runmodule.stopcmd,1);
			}
		});
		button_1.setText("ֹͣ");
				
		final Button button_2 = new Button(this, SWT.NONE);
		button_2.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				todoText(runmodule.startcmd,0);
				todoText(runmodule.stopcmd,1);
			}
		});
		button_2.setText("����");

		final Button button_3 = new Button(this, SWT.NONE);
		button_3.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				//log.append(text.getText());
				//FileConfig.writeLog(text.getText(),runmodule.logpath+runmodule.Modulename+".log" );
				text.setText("---------ϵͳ��־����������ʷ��¼�뵼����鿴��-----------\n");
			}
		});
		button_3.setText("����");
		button_4 = new Button(this, SWT.NONE);
		button_4.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
					//FileConfig.writeLog(text.getText(),runmodule.logpath+runmodule.Modulename+".log" );
					//text.setText("");
					MessageBox mb = new MessageBox(getShell(),SWT.ICON_INFORMATION|SWT.YES|SWT.NO);
					mb.setText("��ʾ");
					mb.setMessage("�Ƿ�򿪲鿴��־��");
					if(mb.open()==SWT.YES)
					{
						todoMessage(new String[]{"notepad.exe "+runmodule.logpath+runmodule.Modulename+".log"},"����");
					}
			}
		});
		button_4.setText("����");
		
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
		label.setText("�����������������"+num);
	}

	/**
	 * ��д��֤��̳з�����ʹʧЧ
	 */
	@Override
	protected void checkSubclass() {
		// Disable the check that prevents subclassing of SWT components
	}
	
	
	
	/**
	 * ִ������ķ���
	 * */
	public void todoText(final String[] cmdstr,final int type)
	{
		
		 Runnable r = new Runnable() {   
	            //�߳����е�����   
	            public void run() {   
	            	if (cmdstr ==null||cmdstr.length <= 0) {
	            		message("����Ϊ�ղ���ִ��"+"\n","��ʾ");
	            		return;
	        	    }
	            	for(int i  = 0;i<cmdstr.length;i++)
	            	{
	            		//��ȡ����ϵͳ����
	            		//String osName = System.getProperty("os.name" );
	            		String cmd = cmdstr[i];
	            		if(cmd==null||"".equals(cmd))
	            			return;
	            		print("����ִ�����"+cmd);
	            		Runtime rt = Runtime.getRuntime();
	            		Process proc = null;
	            		try {
	            			proc = rt.exec("cmd /c "+cmd);
	            			//��ȡ��ӡ������
	            			final BufferedReader pin = new BufferedReader(
	            		              new InputStreamReader(proc.getInputStream()));
	            		   // OutputStream processOut = proc.getOutputStream();
	            			//��ȡ���������Ϣ
	            		    final BufferedReader perr = new BufferedReader(
	            		              new InputStreamReader(proc.getErrorStream()));
	            		    //�½�һ���̣߳�ʹ���������Ϣ�ʹ�ӡ�������ֱ���ʾ��TEXT�ؼ���
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
	            	      //    btnCtrl(false,type);//֪ͨ��ť
	            	          activateEvent(type==0,false);
	            	          num++;
	            	          lablenum(num);
	            	        //��ʾ��ӡ������
	            	        String line =null;
	            			while ((line= pin.readLine()) != null) 
	            			{
	            				print(line);
	            			}
	            			pin.close();
	            			//�ȴ����̽���
	            			proc.waitFor();
	            			int exitValue = proc.exitValue();
//		        	        if (exitValue != 0) {
//		        	        	message("\n�������ʱ��������, ������Ϊ:" + exitValue + "\n",title);
//		        	        }
//		        	        else {
//		        	        	message("����ִ�н���.\n",title);
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
	            				//���ٽ���
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
	            //�߳����е�����   
	            public void run() {   
	            	if (cmdstr ==null||cmdstr.length <= 0) {
	            		message("����Ϊ�ղ���ִ��"+"\n",title);
	        	    }
	            	
	            	for(int i  = 0;i<cmdstr.length;i++)
	            	{
	            		//��ȡ����ϵͳ����
	            		//String osName = System.getProperty("os.name" );
	            		String[] cmd = new String[3];
	            		cmd[0] = "cmd.exe" ;
	            		cmd[1] = "/C" ;
	            		cmd[2] = cmdstr[i];
	            		//print("����ִ�����"+cmd[2]+"\n");
	            		Runtime rt = Runtime.getRuntime();
	            		Process proc = null;
	            		try {
	            			proc = rt.exec(cmd);
	            			//��ȡ��ӡ������
	            			final BufferedReader pin = new BufferedReader(
	            		              new InputStreamReader(proc.getInputStream()));
	            		   // OutputStream processOut = proc.getOutputStream();
	            			//��ȡ���������Ϣ
	            		    final BufferedReader perr = new BufferedReader(
	            		              new InputStreamReader(proc.getErrorStream()));
	            		    
	            		    errormessage = "";//����MESSAGE��Ϣ
	            		    //�½�һ���̣߳�ʹ���������Ϣ�ʹ�ӡ�������ֱ���ʾ��MESSAGEBOX�ؼ���
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
	            	        //��ʾ��ӡ������
	            	        String line =null;
	            			while ((line=pin.readLine()) != null) 
	            			{
	            				errormessage += line;
	            			}
	            			//�ȴ����̽���
	            			proc.waitFor();
	            			pin.close();
	            			int exitValue = proc.exitValue();
//		        	        if (exitValue != 0&&exitValue!=1) {
//		        	        	message("\n�������ʱ��������, ������Ϊ:" + exitValue + "\n"+errormessage,title);
//		        	        }
//		        	        else {
//		        	        	message("����ִ�н���.\n",title);
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
	            				//���ٽ���
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
	 * ��ʾ���ݵ�text�ؼ���Ϊ�˷�ֹ������������������̣߳��첽����ִ���߳�
	 * */
	public void print(final String str)
	{
		if(str.contains("Server startup in")||str.contains("Server started in RUNNING mode"))
		{
			state = 1;// ������ɡ�
		}
		Display.getDefault().asyncExec(new Runnable() {   
			//����߳��ǵ���UI�߳̿ؼ�
			public void run() {   
					text.append(str+"\n");
					FileConfig.writeLog(str+"\r\n",runmodule.logpath+runmodule.Modulename+".log"  );
			}   
		});   
	}
	/**
	 * ��ʾ���ݵ�״̬label��Ϊ�˷�ֹ������������������̣߳��첽����ִ���߳�
	 * */
	public void lablenum(final int num)
	{
		Display.getDefault().asyncExec(new Runnable() {   
			//����߳��ǵ���UI�߳̿ؼ�
			public void run() {   
				label.setText("�����������������"+num);
			}   
		});   
	}
	
	/**
	 * ��ʾ�ַ�����MESSBAGEbox,����ʾ������á�
	 * */
	public void message(final String messagestr,final String textstr)
	{
		Display.getDefault().asyncExec(new Runnable() {   
			//����߳��ǵ���UI�߳̿ؼ�
			public void run() {   
				MessageBox mb = new MessageBox(getShell());
				mb.setMessage(messagestr);
				mb.setText(textstr);
				mb.open();
			}   
		});   
	}
	//*************************************************************
	//�Զ����¼�ѧϰ��
	//********************************************************************
	
	
	//�����¼�Դ����
	private Vector vectorListeners=new Vector();
    
	//����¼�����
    public synchronized void addCmdStatusChangeListener(CmdStatusChangeListener ml)
    {
        vectorListeners.addElement(ml);
    }
    
    //ɾ���¼�����
    public synchronized void removeCmdStatusChangeListener(CmdStatusChangeListener ml)
    {
        vectorListeners.removeElement(ml);
    }
    
    //�����¼��ķ���
    protected void activateEvent(final boolean isStart,final boolean isfinish)
    {
    	
    	Display.getDefault().asyncExec(new Runnable() {   
			//����߳��ǵ���UI�߳̿ؼ�
			public void run() {   
				if(isStart&&!isfinish) //��������
		    	{
					text.setText("");
		    		button.setEnabled(false);
		    		button_1.setEnabled(true);
		    		state = 0;
		    	}
		    	else if (isStart&&isfinish) // ��ֹͣ
		    	{
		    		button.setEnabled(true);
		    		button_1.setEnabled(true);
		    		state = 3;
		    	}
		    	else if (!isStart&&isfinish) // ��ֹͣ
		    	{
		    		button.setEnabled(true);
		    		button_1.setEnabled(true);
		    		state = 3;
		    	}
		    	else if (!isStart&&!isfinish) // ����ֹͣ
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


