package com.wisoft.servlet;

import java.io.IOException;
import java.io.OutputStream;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.context.ApplicationContext;
import org.springframework.web.context.support.WebApplicationContextUtils;

import com.wisoft.dao.UIcheckDao;
import com.wisoft.pojo.UIcheckView;
//import com.wisoft.common.dao.*;
/**
 * Servlet implementation class BugQuery
 */
public class BugQuery extends HttpServlet {
	private static final long serialVersionUID = 1L;
	ApplicationContext ac2=null;
    /**
     * @see HttpServlet#HttpServlet()
     */
    public BugQuery() {
        super();
        // TODO Auto-generated constructor stub
    }

	@Override
	protected void service(HttpServletRequest request, HttpServletResponse response)
			throws ServletException, IOException {
		// TODO Auto-generated method stub
		request.setCharacterEncoding("UTF-8");
		String msg = request.getParameter("aaaa");
		ac2 = WebApplicationContextUtils.getWebApplicationContext(getServletContext());
		UIcheckDao uicheckdao = (UIcheckDao)ac2.getBean("UIcheckDao");
		List<UIcheckView> viewlist = uicheckdao.find("from UIcheckView");
		
		response.setContentType("image/jpeg");
		//response.setContentType("text/html;charset=GBK");
		OutputStream outs = response.getOutputStream(); 
		outs.write(viewlist.get(0).getSrcimage());
		outs.flush();
		response.getWriter().println("服务器吐出了："+msg);
		
	}

}
