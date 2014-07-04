package com.zxp.cctrl;


import org.eclipse.jface.dialogs.Dialog;
import org.eclipse.jface.dialogs.IDialogConstants;
import org.eclipse.swt.SWT;
import org.eclipse.swt.events.MouseEvent;
import org.eclipse.swt.events.MouseListener;
import org.eclipse.swt.layout.GridData;
import org.eclipse.swt.layout.GridLayout;
import org.eclipse.swt.widgets.Button;
import org.eclipse.swt.widgets.Composite;
import org.eclipse.swt.widgets.Control;
import org.eclipse.swt.widgets.Label;
import org.eclipse.swt.widgets.Shell;
import org.eclipse.swt.widgets.Text;

public class ConfigBtn extends Dialog {
	public String[] csbutton = null;
	private Text text;
	private Text text_1;
	protected ConfigBtn(Shell parentShell) {
			super(parentShell);
			// TODO Auto-generated constructor stub
		}
	@Override
	protected void configureShell(Shell newShell) {
		// TODO Auto-generated method stub
		super.configureShell(newShell);
		if(csbutton==null)
			newShell.setText("新建按钮");
		else newShell.setText("修改按钮");
	}
	@Override
	protected Control createDialogArea(Composite parent) {
		// TODO Auto-generated method stub
		 Composite shell = (Composite) super.createDialogArea(parent);
		 shell.setLayout(new GridLayout(2, false));
		 
		 Label label = new Label(shell, SWT.NONE);
		 label.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		 label.setText("按钮名称");
		 
		 text = new Text(shell, SWT.BORDER);
		 text.setLayoutData(new GridData(SWT.FILL, SWT.CENTER, true, false, 1, 1));
		 
		 Label label_1 = new Label(shell, SWT.NONE);
		 label_1.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		 label_1.setText("按钮命令");
		 
		 text_1 = new Text(shell, SWT.BORDER);
		 text_1.setLayoutData(new GridData(SWT.FILL, SWT.CENTER, true, false, 1, 1));
		 
		 //
		 if(csbutton!=null)
		 {
			 String csname = csbutton[0].toString();
			 String cscmd = csbutton[1].toString();
			 if(csname!=null&&!"".equals(csname))
			 {
				 text.setText(csname);
			 }
			 if(cscmd!=null&&!"".equals(cscmd))
			 {
				 text_1.setText(cscmd);
			 }
		 }
		 return shell;
	}
	@Override
	protected void createButtonsForButtonBar(Composite parent) {
		// TODO Auto-generated method stub
		Button button = createButton(parent, IDialogConstants.OK_ID, "确定", true);
		button.addMouseListener(new MouseListener() {

			public void mouseDoubleClick(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			public void mouseDown(MouseEvent arg0) {
				// TODO Auto-generated method stub
				String[] csbutton1 ={text.getText(),text_1.getText()};
				csbutton = csbutton1;
			}

			public void mouseUp(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}
		});
		createButton(parent, IDialogConstants.CANCEL_ID, "取消", true);
	}
}
