package com.zxp.cctrl;

import org.eclipse.jface.dialogs.Dialog;
import org.eclipse.jface.dialogs.IDialogConstants;
import org.eclipse.swt.SWT;
import org.eclipse.swt.events.DisposeEvent;
import org.eclipse.swt.events.DisposeListener;
import org.eclipse.swt.events.SelectionAdapter;
import org.eclipse.swt.events.SelectionEvent;
import org.eclipse.swt.graphics.Point;
import org.eclipse.swt.layout.GridData;
import org.eclipse.swt.layout.GridLayout;
import org.eclipse.swt.widgets.Button;
import org.eclipse.swt.widgets.Composite;
import org.eclipse.swt.widgets.Control;
import org.eclipse.swt.widgets.Display;
import org.eclipse.swt.widgets.Label;
import org.eclipse.swt.widgets.Shell;
import org.eclipse.swt.widgets.Text;
import org.eclipse.wb.swt.SWTResourceManager;

public class GobalConfDialog extends Dialog {
	
	private Text text;
	private Text text_1;
	
	Button btnNewButton;
	Label lblNewLabel ;

	/**
	 * Create the dialog.
	 * @param parentShell
	 */
	public GobalConfDialog(Shell parentShell) {
		super(parentShell);
	}
	
	@Override
	protected void configureShell(Shell newShell) {
		// TODO Auto-generated method stub
		super.configureShell(newShell);
		newShell.setText("配置");
	}
	
	@Override
	protected void okPressed() {
		// TODO Auto-generated method stub
		GobalConfigParameter cp = new GobalConfigParameter();
		cp.webip = this.text.getText().trim();
		cp.webport = this.text_1.getText().trim();
		FileConfig.writeGobalConfig(cp);
		super.okPressed();
	}
	
	
	
	private MinaConnStatusListener listener =new MinaConnStatusListener() {
		
		public void statusChanged(MinaConnStatusEvent event) {
			// TODO Auto-generated method stub
			final String aa = event.getStatus();
			try{
				Display.getDefault().syncExec(new Runnable() {
					 
					 public void run() {
						 lblNewLabel.setText(aa);
						if("正在连接".equals(aa))
						{
							btnNewButton.setEnabled(false);
						}
						else if("已暂停".equals(aa))
						{
							btnNewButton.setText("继续");
							btnNewButton.setEnabled(true);
						}
						else if("正常".equals(aa))
						{
							btnNewButton.setText("暂停");
							btnNewButton.setEnabled(true);
						}
						else if("连接失败".equals(aa))
						{
							btnNewButton.setText("重试");
							btnNewButton.setEnabled(true);
						}
					 }
				 });
			 }catch(Exception e){
				 
			 }
			 
			
				
		}
	};
	/**
	 * Create contents of the dialog.
	 * @param parent
	 */
	@Override
	protected Control createDialogArea(Composite parent) {
		Composite container = (Composite) super.createDialogArea(parent);
		container.setLayout(new GridLayout(4, false));
		new Label(container, SWT.NONE);
		new Label(container, SWT.NONE);
		new Label(container, SWT.NONE);
		new Label(container, SWT.NONE);
		new Label(container, SWT.NONE);
		
		Label lblWebip = new Label(container, SWT.NONE);
		lblWebip.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		lblWebip.setText("WEB\u7BA1\u7406IP:");
		
		text = new Text(container, SWT.BORDER);
		text.setText(FileConfig.readGobalConfig().webip);
		text.setLayoutData(new GridData(SWT.FILL, SWT.CENTER, true, false, 2, 1));
		new Label(container, SWT.NONE);
		
		Label label = new Label(container, SWT.NONE);
		label.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		label.setText("\u901A\u8BAF\u7AEF\u53E3:");
		
		text_1 = new Text(container, SWT.BORDER);
		text_1.setText(FileConfig.readGobalConfig().webport);
		text_1.setLayoutData(new GridData(SWT.FILL, SWT.CENTER, true, false, 2, 1));
		new Label(container, SWT.NONE);
		
		Label label_1 = new Label(container, SWT.NONE);
		label_1.setLayoutData(new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1));
		label_1.setText("\u901A\u8BAF\u72B6\u6001:");
		
		lblNewLabel = new Label(container, SWT.NONE);
		lblNewLabel.setForeground(SWTResourceManager.getColor(SWT.COLOR_RED));
		
		btnNewButton = new Button(container, SWT.NONE);
		btnNewButton.setEnabled(true);
		btnNewButton.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				String aa = btnNewButton.getText();
				if("继续".equals(aa))
					MinaConnServer.getInstance().resumeSendServer();
				if("暂停".equals(aa))
					MinaConnServer.getInstance().pauseSendServer();
				if("重试".equals(aa))
					MinaConnServer.getInstance().reConnect();
			}
		});
		String bb = MinaConnServer.getInstance().getStatus();
		lblNewLabel.setText(bb);
		if("正在连接".equals(bb))
		{
			btnNewButton.setEnabled(false);
		}
		else if("已暂停".equals(bb))
		{
			btnNewButton.setText("继续");
			btnNewButton.setEnabled(true);
		}
		else if("正常".equals(bb))
		{
			btnNewButton.setText("暂停");
			btnNewButton.setEnabled(true);
		}
		else if("连接失败".equals(bb))
		{
			btnNewButton.setText("重试");
			btnNewButton.setEnabled(true);
		}
		MinaConnServer.getInstance().addMinaConnStatusListener(listener);
		parent.addDisposeListener(new DisposeListener() {
			
			public void widgetDisposed(DisposeEvent e) {
				// TODO Auto-generated method stub
				MinaConnServer.getInstance().removeMinaConnStatusListener(listener);
			}
		});
		return container;
	}
	
	/**
	 * Create contents of the button bar.
	 * @param parent
	 */
	@Override
	protected void createButtonsForButtonBar(Composite parent) {
		createButton(parent, IDialogConstants.OK_ID, IDialogConstants.OK_LABEL,
				true);
		createButton(parent, IDialogConstants.CANCEL_ID,
				IDialogConstants.CANCEL_LABEL, false);
	}

	/**
	 * Return the initial size of the dialog.
	 */
	@Override
	protected Point getInitialSize() {
		return new Point(450, 300);
	}

}


