package com.wisoft.dao;

import java.util.List;

import com.wisoft.common.dao.BaseDao;
import com.wisoft.pojo.UIcheckInfo;
import com.wisoft.pojo.UIcheckView;

public class UIcheckDao extends BaseDao {
	
	public List<UIcheckView> getViewListBycheckno(String checkno,String imgno)
	{
		List<UIcheckView> viewlist = super.find("from UIcheckView where checkno=? and imageno =?",new Object[]{checkno,imgno});
		return viewlist;
	}
	
	public int getViewCount(String checkno)
	{
		int a=0;
		try
		{
			a=super.findCountsByHql("select count(id) from UIcheckView where checkno=?", new Object[]{checkno});
		}
		catch(Exception e)
		{
			System.out.print(e.toString());
		}
		return a;
	}
	
	
	public UIcheckInfo getUicheckInfo(String checkno)
	{
		List<UIcheckInfo> checkinfo = super.find("from UIcheckInfo where checkno=? ",new Object[]{checkno});
		if(checkinfo.size()>0)
			return checkinfo.get(0);
		else
			return null;
	}
}
