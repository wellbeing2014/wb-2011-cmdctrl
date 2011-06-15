package com.zxp.cctrl.vo;

import java.io.BufferedReader;
import java.io.InputStreamReader;

import org.eclipse.swt.widgets.Display;
import org.eclipse.swt.widgets.Label;

	public class CmdClass implements Runnable {
		  private Display display;
		    private Label miniLabel;
		    
		    private static int index = 0; 

		    public CmdClass(Display display, Label label) {
		        this.display = display;
		        this.miniLabel = label;
		    }

		    public void run() {
		        try {
		          while (true) {
		             Thread.sleep(1000);
		                if (!this.display.isDisposed()) {
		                    Runnable runnable = new Runnable() {
		                        public void run() {
		                            // your source
		                        }
		                    };
		        
		                    display.asyncExec(runnable); // 关键在这一句上
		                }
		          }
		        } catch (Exception ex) {}
		    }
	}

