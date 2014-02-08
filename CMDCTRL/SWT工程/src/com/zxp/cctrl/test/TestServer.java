package com.zxp.cctrl.test;

import org.eclipse.swt.SWT;
import org.eclipse.swt.events.SelectionAdapter;
import org.eclipse.swt.events.SelectionEvent;
import org.eclipse.swt.widgets.Button;
import org.eclipse.swt.widgets.Display;
import org.eclipse.swt.widgets.Shell;
import org.eclipse.swt.widgets.Text;

import com.zxp.cctrl.socket.ServerSocketHandle;
import org.eclipse.swt.events.ShellAdapter;
import org.eclipse.swt.events.ShellEvent;

public class TestServer {

	protected Shell shell;
	private Text text;

	/**
	 * Launch the application.
	 * @param args
	 */
	public static void main(String[] args) {
		try {
			TestServer window = new TestServer();
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
		createContents();
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
		shell.addShellListener(new ShellAdapter() {
			@Override
			public void shellClosed(ShellEvent e) {
				ServerSocketHandle.getInstance().stop();
			}
		});
		shell.setSize(450, 300);
		shell.setText("SWT Application");
		
		Button button = new Button(shell, SWT.CHECK);
		button.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				Button b = (Button)(e.widget);
				if(b.getSelection())
					ServerSocketHandle.getInstance().start();
				else
					ServerSocketHandle.getInstance().stop();
			}
		});
		button.setBounds(10, 34, 98, 17);
		button.setText("\u5F00\u542F\u670D\u52A1");
		
		text = new Text(shell, SWT.BORDER);
		text.setBounds(10, 70, 181, 23);
		
		Button btnNewButton = new Button(shell, SWT.NONE);
		btnNewButton.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				String str  =	text.getText().trim();
				ServerSocketHandle.getInstance().setMsgToPool(str);
			}
		});
		btnNewButton.setBounds(197, 66, 80, 27);
		btnNewButton.setText("\u53D1\u9001\u6D88\u606F");

	}
	
	
}
