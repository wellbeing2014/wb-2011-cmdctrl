package com.zxp.cctrl;

import java.util.EventListener;

public interface MinaConnStatusListener extends EventListener {
	public void statusChanged(MinaConnStatusEvent event);
}
