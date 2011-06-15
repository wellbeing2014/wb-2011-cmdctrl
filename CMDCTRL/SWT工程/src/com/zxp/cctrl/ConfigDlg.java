package com.zxp.cctrl;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import org.eclipse.jface.dialogs.Dialog;
import org.eclipse.jface.dialogs.IDialogConstants;
import org.eclipse.swt.widgets.Button;
import org.eclipse.swt.widgets.Composite;
import org.eclipse.swt.widgets.Control;
import org.eclipse.swt.widgets.MessageBox;
import org.eclipse.swt.widgets.Shell;
import org.eclipse.swt.widgets.Table;
import org.eclipse.swt.widgets.TableColumn;
import org.eclipse.swt.widgets.TableItem;
import org.eclipse.swt.layout.GridLayout;
import org.eclipse.swt.widgets.Label;
import org.eclipse.swt.SWT;
import org.eclipse.swt.widgets.Text;
import org.eclipse.swt.layout.GridData;
import org.eclipse.swt.events.MouseEvent;
import org.eclipse.swt.events.MouseListener;
import org.eclipse.swt.events.SelectionAdapter;
import org.eclipse.swt.events.SelectionEvent;
import com.swtdesigner.SWTResourceManager;

public class ConfigDlg extends Dialog {
	RunModule rm = new RunModule();
	private Text text;
	private Text text_1;
	private Table table;
	private Text text_2;
	private Text text_3;
	private Text text_4;

	protected ConfigDlg(Shell parentShell) {
		super(parentShell);
		//parentShell.setText("fasdfasd");
		// TODO Auto-generated constructor stub
	}
	 protected void configureShell(Shell shell) {
	      super.configureShell(shell);
	      shell.setText("配置");
	      shell.setSize(450, 500);
	 }
	protected Control createDialogArea(Composite parent) {
        final Composite shell = (Composite) super.createDialogArea(parent);
        shell.setLayout(new GridLayout(6, false));
		
		Label label_3 = new Label(shell, SWT.NONE);
		label_3.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		label_3.setText("模块名称");
		
		text_2 = new Text(shell, SWT.BORDER);
		text_2.setLayoutData(new GridData(SWT.FILL, SWT.CENTER, true, false, 4, 1));
		new Label(shell, SWT.NONE);
		
		Label label = new Label(shell, SWT.NONE);
		label.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		label.setText("运行按钮命令");
		
		text = new Text(shell, SWT.BORDER);
		text.setLayoutData(new GridData(SWT.FILL, SWT.CENTER, true, false, 4, 1));
		new Label(shell, SWT.NONE);
		
		Label label_1 = new Label(shell, SWT.NONE);
		label_1.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		label_1.setText("停止按钮命令");
		
		text_1 = new Text(shell, SWT.BORDER);
		text_1.setLayoutData(new GridData(SWT.FILL, SWT.CENTER, true, false, 4, 1));
		new Label(shell, SWT.NONE);
		
		Label label_4 = new Label(shell, SWT.NONE);
		label_4.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		label_4.setText("\u7F51\u5740\uFF1A");
		
		text_3 = new Text(shell, SWT.BORDER);
		text_3.setLayoutData(new GridData(SWT.FILL, SWT.CENTER, true, false, 4, 1));
		new Label(shell, SWT.NONE);
		
		Label label_5 = new Label(shell, SWT.NONE);
		label_5.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		label_5.setAlignment(SWT.RIGHT);
		label_5.setText("\u65E5\u5FD7\u8DEF\u5F84\uFF1A");
		
		text_4 = new Text(shell, SWT.BORDER);
		text_4.setLayoutData(new GridData(SWT.FILL, SWT.CENTER, true, false, 4, 1));
		new Label(shell, SWT.NONE);
		
		Button button = new Button(shell, SWT.NONE);
		button.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				ConfigBtn newbtn = new ConfigBtn(shell.getShell());
				
				if(newbtn.open()==IDialogConstants.OK_ID)
				{
					TableItem ti = new TableItem(table, SWT.NONE);
					String key = newbtn.csbutton[0];
					String value = newbtn.csbutton[1];
					ti.setText(new String[]{key,value});
				}
			}
		});
		button.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		button.setText("新建按钮");
		
		Button button_1 = new Button(shell, SWT.NONE);
		button_1.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				TableItem[] editbtns = table.getSelection();
				if(editbtns==null||editbtns.length==0)
				{
					MessageBox mb = new MessageBox(shell.getShell());
					mb.setText("提示");
					mb.setMessage("请选择一行按钮");
					mb.open();
				}
				else
				{
					TableItem editbtn = editbtns[0];
					String name =editbtn.getText(0);
					String value =editbtn.getText(1);
					ConfigBtn newbtn = new ConfigBtn(shell.getShell());
					String[] temp = {name,value};
					newbtn.csbutton =temp;
					if(newbtn.open()==IDialogConstants.OK_ID)
					{
						editbtn.dispose();
						TableItem ti = new TableItem(table, SWT.NONE);
						String newkey = newbtn.csbutton[0];
						String newvalue = newbtn.csbutton[1];
						ti.setText(new String[]{newkey,newvalue});
					}
				}
			}
		});
		button_1.setText("修改按钮");
		new Label(shell, SWT.NONE);
		
		Button button_2 = new Button(shell, SWT.NONE);
		button_2.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				TableItem[] editbtns = table.getSelection();
				if(editbtns==null||editbtns.length==0)
				{
					MessageBox mb = new MessageBox(shell.getShell());
					mb.setText("提示");
					mb.setMessage("请选择一行按钮");
					mb.open();
				}
				else
				{
					for(int i=0;i<editbtns.length;i++)
					{
						TableItem editbtn = editbtns[i];
						editbtn.dispose();
					}
				}
			}
		});
		button_2.setText("删除按钮");
		new Label(shell, SWT.NONE);
		new Label(shell, SWT.NONE);
		
		table = new Table(shell, SWT.BORDER | SWT.FULL_SELECTION);
		table.setLayoutData(new GridData(SWT.FILL, SWT.FILL, true, true, 6, 1));
		TableColumn tc1=new TableColumn(table,SWT.CENTER);
		TableColumn tc2=new TableColumn(table,SWT.CENTER);
		tc1.setWidth(100);
		tc2.setWidth(300);
		tc1.setText("按钮名称");
		tc2.setText("命令行字符串");
		table.setHeaderVisible(true);
		table.setLinesVisible(true);
		
		Label label_2 = new Label(shell, SWT.NONE);
		label_2.setForeground(SWTResourceManager.getColor(SWT.COLOR_RED));
		label_2.setLayoutData(new GridData(SWT.LEFT, SWT.CENTER, false, false, 6, 1));
		label_2.setText("*输入多个命令可用“;”隔开");
		if(rm.Modulename!=null)
		{
			text_2.setText(rm.Modulename);
		}
		if(rm.startcmd!=null&&rm.startcmd.length>0)
		{
			String readstart="";
			for(int j = 0;j<rm.startcmd.length;j++)
			{
				readstart+=(rm.startcmd)[j]+";";
			}
			text.setText(readstart);
		}
		if(rm.stopcmd!=null&&rm.stopcmd.length>0)
		{
			String readstop="";
			for(int j = 0;j<rm.stopcmd.length;j++)
			{
				readstop+=(rm.stopcmd)[j]+";";
			}
			text_1.setText(readstop);
		}
		if(rm.custombutton.size()>0)
		{
		//	Iterator cbit = rm.custombutton.keySet().iterator();
			for(Iterator<String> cbit = rm.custombutton.keySet().iterator();cbit.hasNext();)
			{
				TableItem ti = new TableItem(table, SWT.NONE);
				String key = cbit.next();
				String[] value = rm.custombutton.get(key);
				String valuevalue ="";
				for(int j = 0;j<value.length;j++)
				{
					valuevalue+=value[j]+";";
				}
				ti.setText(new String[]{key,valuevalue});
			}
		}
		if(rm.netaddr!=null&&rm.netaddr.length()>0)
		{
			this.text_3.setText(rm.netaddr);
		}
		if(rm.logpath!=null&&rm.logpath.length()>0)
		{
			this.text_4.setText(rm.logpath);
		}
        return shell;
    }
	@Override
	protected void createButtonsForButtonBar(Composite parent) {
		// TODO Auto-generated method stub
		Button button = createButton(parent, IDialogConstants.OK_ID, "确定", true);
		button.addMouseListener(new MouseListener() {
			
			public void mouseUp(MouseEvent arg0) {
				// TODO Auto-generated method stub
			}
			public void mouseDown(MouseEvent arg0) {
				// TODO Auto-generated method stub
				String rmname = text_2.getText();
				String[] rmstart = text.getText().split(";");
				String[] rmstop = text_1.getText().split(";");
				String netaddr = text_3.getText();
				rm.startcmd = rmstart;
				rm.stopcmd = rmstop;
				rm.Modulename = rmname;
				rm.netaddr = netaddr;
				rm.logpath = text_4.getText();
				Map<String,String[]> custombuttons= new HashMap<String,String[]>();
				for(int i =0;i<table.getItemCount();i++)
				{
					TableItem tc1=table.getItem(i);
					String btnname = tc1.getText(0);
					String[] btncmd = tc1.getText(1).split(";");
					custombuttons.put(btnname,btncmd);
				}
				rm.custombutton=custombuttons;
			}
			public void mouseDoubleClick(MouseEvent arg0) {
				// TODO Auto-generated method stub
			}
		});
		createButton(parent, IDialogConstants.CANCEL_ID, "取消", true);
	}

}
