package com.wisoft.bo;

import java.util.List;

import com.wisoft.dao.UIcheckDao;
import com.wisoft.pojo.UIcheckInfo;
import com.wisoft.pojo.UIcheckView;

public class UIcheckBO {
	private UIcheckDao ucdao;
	
	public UIcheckDao getUcdao() {
		return ucdao;
	}

	public void setUcdao(UIcheckDao ucdao) {
		this.ucdao = ucdao;
	}

	/**
	 * 根据编号和顺序号获取图片
	 * @param checkno
	 * @param imgno
	 * @return
	 */
	public UIcheckView getSingleView(String checkno,String imgno)
	{
		List<UIcheckView> viewlist=ucdao.getViewListBycheckno(checkno,imgno);
		if(viewlist.size()>0)
			return viewlist.get(0);
		else return null;
	}
	
	public int getViewCount(String checkno)
	{
		int a =ucdao.getViewCount(checkno);
		return a;
	}
	
	/** 根据编号获取检查基本信息
	 * @param checkno
	 * @return
	 */
	public UIcheckInfo getUicheckInfo(String checkno)
	{
		return ucdao.getUicheckInfo(checkno);
	}
}
