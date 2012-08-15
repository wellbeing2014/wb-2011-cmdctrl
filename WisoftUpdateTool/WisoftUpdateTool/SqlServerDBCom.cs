/*
 * 由SharpDevelop创建。
 * 用户： ZhuXinpei
 * 日期: 2012/8/15
 * 时间: 9:15
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;


namespace WisoftUpdateTool
{
	/// <summary>
	/// Description of SqlServerDBCom.
	/// </summary>
	public class SqlServerDBCom
	{
		public SqlServerDBCom()
		{
		}

        /// 
        /// 通用数据库类
        /// 
        private  string ConnStr = null;

        public SqlServerDBCom(string constr)
        {            
            //ConnStr = ConfigurationManager.ConnectionStrings["conSql"].ConnectionString;
            ConnStr = constr;
        }
        
        /// 
        /// 返回connection对象
        /// 
        /// 
        public SqlConnection ReturnConn()
        {
        	try {
        		SqlConnection Conn = new SqlConnection(ConnStr);
            	Conn.Open();
            	return Conn;
        		
        	} catch (SqlException e) {
        		if(e.Number==18456)
        			throw(new Exception("敢问公公，你这个太监证哪里来的，假的吧，最近宫里的暗号对一下？\n不对啊，裤子脱了看看有没有鸡鸡。"));
        		if(e.Number==4060)
        			throw(new Exception("尼玛，这个数据库隐身了啊？，找不到啊~"));
        		if(e.Number==53)
        			throw(new Exception("我日，数据库地址是在火星吗？连不上啊~"));
        		if(e.Number==10061)
        			throw(new Exception("尼玛，敢玩我，数据库端口的洞呢，没洞怎么插~"));
        		return null;
        	}
            
        }
        public void Dispose(SqlConnection Conn)
        {
            if (Conn != null)
            {
                Conn.Close();
                Conn.Dispose();
            }
        }
        /// <summary>
        /// 运行SQL增删改语句
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string SQL)
        {
        	int i=0;
            SqlConnection Conn;
            Conn = ReturnConn();
            SqlCommand Cmd;
            Cmd = CreateCmd(SQL, Conn);
            try
            {
                i=Cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception(SQL+"执行失败");
            }
            Dispose(Conn);
            return i;
        }
        
        /// <summary>
        /// 运行SQL增删改语句
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public  int ExecuteNonQuery(string SQL,SqlParameter[] parameter)
        {
        	int rows=0;
            SqlConnection Conn;
            Conn = ReturnConn();
            SqlCommand Cmd;
            Cmd = CreateCmd(SQL, Conn);
            Cmd.Parameters.AddRange(parameter);
            try
            {
               rows = Cmd.ExecuteNonQuery();
               
            }
            catch
            {
                throw new Exception(SQL);
                
            }
            Dispose(Conn);
            return rows;
        }
        
        /// <summary>
        /// 运行SQL查询语句
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public  DataSet ExecuteQuery(string SQL,SqlParameter[] parameter)
        {
        	DataSet ds = new DataSet();
            SqlConnection Conn;
            Conn = ReturnConn();
            SqlCommand Cmd;
            Cmd = CreateCmd(SQL, Conn);
            if(parameter!=null) Cmd.Parameters.AddRange(parameter);
            SqlDataAdapter adapter;
            try
            {
            	adapter = new SqlDataAdapter(Cmd);
            	adapter.Fill(ds,"ds");
            }
            catch(Exception)
            {
                throw new Exception(SQL);
            }
            Dispose(Conn);
            return ds;
        }
        
        
        //执行单条插入语句，并返回id，不需要返回id的用ExceuteNonQuery执行。
		public  int ExecuteInsert(string sql,SqlParameter[] parameters)
        {
        	SqlConnection Conn;
            Conn = ReturnConn();
            SqlCommand Cmd;
            Cmd = CreateCmd(sql, Conn);
            if(parameters!=null) Cmd.Parameters.AddRange(parameters);
           
            try
            {
                
                Cmd.ExecuteNonQuery();
                Cmd.CommandText = @"select @@identity";
                int value = Int32.Parse(Cmd.ExecuteScalar().ToString());
                return value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
		
        /// <summary>
        /// 运行SQL查询语句
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public  DataSet ExecuteQuery(string SQL)
        {
        	DataSet ds = new DataSet();
            SqlConnection Conn;
            Conn = ReturnConn();
            SqlCommand Cmd;
            Cmd = CreateCmd(SQL, Conn);
            SqlDataAdapter adapter;
            try
            {
            	adapter = new SqlDataAdapter(Cmd);
            	adapter.Fill(ds,"ds");
            }
            catch(SqlException e)
            {
                throw new Exception(e.Message+SQL);
            }
            Dispose(Conn);
            return ds;
        }
        
        /// <summary>
        /// 分页SQL查询语句
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public  DataSet ExecuteQuery(string SQL,int startnum,int pagesize)
        {
        	DataSet ds = new DataSet();
            SqlConnection Conn;
            Conn = ReturnConn();
            SqlCommand Cmd;
            Cmd = CreateCmd(SQL, Conn);
            SqlDataAdapter adapter;
            try
            {
            	adapter = new SqlDataAdapter(Cmd);
            	adapter.Fill(ds,startnum,pagesize,"ds");
            }
            catch
            {
                throw new Exception(SQL);
            }
            Dispose(Conn);
            return ds;
        }
        
        /// 
        /// 运行SQL语句返回DataReader
        /// 
        /// 
        /// SqlDataReader对象.
        public SqlDataReader RunProcGetReader(string SQL)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlCommand Cmd;
            Cmd = CreateCmd(SQL, Conn);
            SqlDataReader Dr;
            try
            {
                Dr = Cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch
            {
                throw new Exception(SQL);
            }
            //Dispose(Conn);
            return Dr;
        }        /// 
        /// 生成Command对象
        /// 
        /// 
        /// 
        /// 
        public  SqlCommand CreateCmd(string SQL, SqlConnection Conn)
        {
            SqlCommand Cmd;
            Cmd = new SqlCommand(SQL, Conn);
            return Cmd;
        }        /// 
            
        
        /// 
        /// 检验是否存在数据
        /// 
        /// 
        public bool ExistDate(string SQL)
        {
            SqlConnection Conn;
            Conn = ReturnConn();
            SqlDataReader Dr;
            Dr = CreateCmd(SQL, Conn).ExecuteReader();
            if (Dr.Read())
            {
                Dispose(Conn);
                return true;
            }
            else
            {
                Dispose(Conn);
                return false;
            }
        }        /// 
        /// 返回SQL语句执行结果的第一行第一列
        /// 
        /// count(*)
        public  int ExecuteScalar(string SQL)
        {
            SqlConnection Conn;
            Conn = ReturnConn();
            SqlCommand cmd;
            int result=0;
            SqlDataReader Dr;
            try
            {
            	cmd = CreateCmd(SQL, Conn);
                Dr = cmd.ExecuteReader();
                if (Dr.Read())
                {
                	result = Int32.Parse(Dr[0].ToString());
                    Dr.Close();
                }
                else
                {
                    result = 0;
                    Dr.Close();
                }
            }
            catch
            {
                throw new Exception(SQL);
            }
            Dispose(Conn);
            return result;
        }        
        /// <summary>
        /// 返回 第一列第一行从SQL计算的值，如sum()
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public  double ExecuteSUM(string SQL,SqlParameter[] parameter)
        {
            SqlConnection Conn;
            Conn = ReturnConn();
            double result=0.00;
            SqlCommand cmd;
            SqlDataReader Dr;
            try
            {
            	cmd = CreateCmd(SQL, Conn);
            	if(parameter!=null) cmd.Parameters.AddRange(parameter);
                Dr = cmd.ExecuteReader();
                if (Dr.Read())
                {
                	try {
                		result = Convert.ToDouble(Dr[0].ToString());
                	} catch (Exception) {
                	}
                	
                    Dr.Close();
                }
                else
                {
                    Dr.Close();
                }
            }
            catch
            {
                throw new Exception(SQL);
                
            }
            Dispose(Conn);
            return result;
        }  
        
        public  bool insert(object obj)
		{
			List<SqlParameter> para = new List<SqlParameter>();
			try {
				//反射出类型
				Type objclass =obj.GetType();
				//拼SQL开始
				string sqltou = "insert into "+objclass.Name+"(";
				string sqlval="values('";
				//验证是否为pojo类。
				PropertyInfo ispojo = objclass.GetProperty("Px");
				if(ispojo==null)
				{
					return false;
				}
				//获取所有属性
				PropertyInfo[] fields = objclass.GetProperties();
				foreach(PropertyInfo field in fields)
				{
					//去掉验证属性及主键
					if(field.Name!="Px"&&field.Name!=ispojo.GetValue(obj,null).ToString())
					{
						object fvalue=field.GetValue(obj,null);
						SqlParameter tt = new SqlParameter();
						tt.ParameterName = "@"+field.Name;
						tt.Value=fvalue;
						//验证整形
						if(field.PropertyType.Name=="Int32")
						{
							tt.SqlDbType=SqlDbType.Int;
						}
						if(field.PropertyType.Name=="String")
						{
							tt.SqlDbType=SqlDbType.VarChar;
						}
						if(field.PropertyType.Name=="Boolean")
						{
							tt.SqlDbType=SqlDbType.Bit;
						}
						if(field.PropertyType.Name=="DateTime")
						{
							tt.SqlDbType=SqlDbType.DateTime;
						}
						//大字段
						if(field.PropertyType.Name=="Byte[]")
						{
							tt.SqlDbType=SqlDbType.Binary;
						}
						if(fvalue!=null)
						{
							para.Add(tt);
							sqltou+=field.Name+",";
							sqlval=sqlval.Substring(0,sqlval.Length-1)+tt.ParameterName+",'";
						}
						
					}
				}
				String sql=sqltou.Substring(0,sqltou.Length-1)+")"+sqlval.Substring(0,sqlval.Length-2)+");";
				//拼SQL结束。
				ExecuteQuery(sql,para.ToArray());
				return true;
			} catch (Exception) {
				throw(new Exception("插入失败"));
			}
		}
        
        public   int insertreturn(object obj)
		{
			int i =0;
			try {
				//反射出类型
				Type objclass =obj.GetType();
				//拼SQL开始
				string sqltou = "insert into "+objclass.Name+"(";
				string sqlval="values('";
				//验证是否为pojo类。
				PropertyInfo ispojo = objclass.GetProperty("Px");
				if(ispojo==null)
				{
					return i;
				}
				//获取所有属性
				PropertyInfo[] fields = objclass.GetProperties();
				foreach(PropertyInfo field in fields)
				{
					//去掉验证属性及主键
					if(field.Name!="Px"&&field.Name!=ispojo.GetValue(obj,null).ToString())
					{
						object fvalue=field.GetValue(obj,null);
						//验证整形，空的整形为0
						if(field.PropertyType.Name=="Int32")
						{
							
							sqltou+=field.Name+",";
							sqlval=sqlval.Substring(0,sqlval.Length-1)+fvalue.ToString()+",'";
						}
						else
						if(fvalue!=null&&fvalue.ToString()!=null)
						{
							sqltou+=field.Name+",";
							sqlval+=fvalue.ToString()+"','";
						}
					}
				}
				String sql=sqltou.Substring(0,sqltou.Length-1)+")"+sqlval.Substring(0,sqlval.Length-2)+");";
				//拼SQL结束。
				i=ExecuteNonQuery(sql,null);
				return i;
			} catch (Exception) {
				throw(new Exception("插入失败"));
			}
		}
		
        public   int insertReturnID(object obj)
		{
			
			List<SqlParameter> para = new List<SqlParameter>();
			try{
				//反射出类型
				Type objclass =obj.GetType();
				//拼SQL开始
				string sqltou = "insert into "+objclass.Name+"(";
				string sqlval="values('";
				//验证是否为pojo类。
				PropertyInfo ispojo = objclass.GetProperty("Px");
				if(ispojo==null)
				{
					return 0;
				}
				//获取所有属性
				PropertyInfo[] fields = objclass.GetProperties();
				foreach(PropertyInfo field in fields)
				{
					//去掉验证属性及主键
					if(field.Name!="Px"&&field.Name!=ispojo.GetValue(obj,null).ToString())
					{
						object fvalue=field.GetValue(obj,null);
						SqlParameter tt = new SqlParameter();
						tt.ParameterName = "@"+field.Name;
						tt.Value=fvalue;
						//验证整形
						if(field.PropertyType.Name=="Int32")
						{
							tt.SqlDbType=SqlDbType.Int;
						}
						if(field.PropertyType.Name=="String")
						{
							tt.SqlDbType=SqlDbType.VarChar;
						}
						if(field.PropertyType.Name=="Boolean")
						{
							tt.SqlDbType=SqlDbType.Bit;
						}
						if(field.PropertyType.Name=="DateTime")
						{
							tt.SqlDbType=SqlDbType.DateTime;
						}
						//大字段
						if(field.PropertyType.Name=="Byte[]")
						{
							tt.SqlDbType=SqlDbType.Binary;
						}
						if(fvalue!=null)
						{
							para.Add(tt);
							sqltou+=field.Name+",";
							sqlval=sqlval.Substring(0,sqlval.Length-1)+tt.ParameterName+",'";
						}
						
					}
				}
				String sql=sqltou.Substring(0,sqltou.Length-1)+")"+sqlval.Substring(0,sqlval.Length-2)+");";
				//拼SQL结束。
				return ExecuteInsert(sql,para.ToArray());
				
			} catch (Exception) {
				throw(new Exception("插入失败"));
			}
		}
        
		public  bool update(object obj)
		{
			List<SqlParameter> para = new List<SqlParameter>();
			try {
				//反射出类型
				Type objclass =obj.GetType();
				//拼SQL开始
				string sqltou = "update "+objclass.Name+" set ";
				
				//验证是否为pojo类。
				PropertyInfo ispojo = objclass.GetProperty("Px");
				if(ispojo==null)
				{
					return false;
				}
				PropertyInfo px = objclass.GetProperty(ispojo.GetValue(obj,null).ToString());
				if(px==null||px.GetValue(obj,null)==null)
				{
					return false;
				}
				//获取所有属性
				PropertyInfo[] fields = objclass.GetProperties();
				string tempsql ="";
				foreach(PropertyInfo field in fields)
				{
					//去掉验证属性及主键
					if(field.Name!="Px"&&field.Name!=ispojo.GetValue(obj,null).ToString())
					{
						object fvalue=field.GetValue(obj,null);
						
						SqlParameter tt = new SqlParameter();
						tt.ParameterName = "@"+field.Name;
						tt.IsNullable =true;
						if(fvalue!=null)
							tt.Value=fvalue;
						else 
							tt.Value =DBNull.Value;
						//验证整形
						if(field.PropertyType.Name=="Int32")
						{
							tt.SqlDbType=SqlDbType.Int;
						}
						if(field.PropertyType.Name=="String")
						{
							tt.SqlDbType=SqlDbType.VarChar;
						}
						if(field.PropertyType.Name=="Boolean")
						{
							tt.SqlDbType=SqlDbType.Bit;
						}
						if(field.PropertyType.Name=="DateTime")
						{
							tt.SqlDbType=SqlDbType.DateTime;
						}
						//大字段
						if(field.PropertyType.Name=="Byte[]")
						{
							tt.SqlDbType=SqlDbType.Binary;
						}
						para.Add(tt);
						tempsql=tempsql+field.Name+"="+tt.ParameterName+",";
						
					}
				}
				
				
				String sql=sqltou+tempsql.Substring(0,tempsql.Length-1)+" where "+px.Name+"="+px.GetValue(obj,null).ToString()+";";
				//拼SQL结束。
				ExecuteNonQuery(sql,para.ToArray());
				return true;
			} catch (Exception) {
				throw(new Exception("更新失败"));
			}
		}
		
		public  bool delete(object obj)
		{
			try {
				//反射出类型
				Type objclass =obj.GetType();
				//拼SQL开始
				string sqltou = "delete from "+objclass.Name+" where ";
				
				//验证是否为pojo类。
				PropertyInfo ispojo = objclass.GetProperty("Px");
				if(ispojo==null)
				{
					return false;
				}
				PropertyInfo px = objclass.GetProperty(ispojo.GetValue(obj,null).ToString());
				if(px==null||px.GetValue(obj,null)==null)
				{
					return false;
				}
				
				String sql=sqltou+px.Name+"="+px.GetValue(obj,null).ToString()+";";
				//拼SQL结束。
				ExecuteNonQuery(sql);
				return true;
			} catch (Exception) {
				throw(new Exception("删除失败"));
			}
		}
		
		public  bool CheckDBState()
		{
			SqlConnection Conn;
            Conn = ReturnConn();
            SqlCommand cmd;
            bool result=false;
            SqlDataReader Dr;
            try
            {
            	cmd = CreateCmd("select getdate()", Conn);
                Dr = cmd.ExecuteReader();
                if (Dr.Read())
                {
                	result = true;
                    Dr.Close();
                }
                else
                {
                    result = false;
                    Dr.Close();
                }
            }
            catch
            {
                throw new Exception("数据库异常~");
            }
            Dispose(Conn);
            return result;
		}
		
    }    
}
 

