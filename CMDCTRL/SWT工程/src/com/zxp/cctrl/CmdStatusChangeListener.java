package com.zxp.cctrl;

import java.util.EventListener;


public abstract class CmdStatusChangeListener implements EventListener
{
	 public abstract void cmdStatusChanged(CmdStatusChangeEvent e);
	
}