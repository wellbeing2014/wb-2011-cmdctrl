package com.wisoft.servlet;

import java.io.IOException;
import java.io.OutputStream;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.context.ApplicationContext;
import org.springframework.web.context.support.WebApplicationContextUtils;

import com.wisoft.bo.UIcheckBO;
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
		String checkno = request.getParameter("checkno");
		String imgno = request.getParameter("imgno");
		ac2 = WebApplicationContextUtils.getWebApplicationContext(getServletContext());
		UIcheckBO bo = (UIcheckBO)ac2.getBean("UIcheckBO");
		UIcheckView ucv = bo.getSingleView(checkno, imgno);
		
		response.setContentType("image/jpeg");
		OutputStream outs = response.getOutputStream(); 
		outs.write(ucv.getCheckedimage());
		outs.flush();
		
	}

}
