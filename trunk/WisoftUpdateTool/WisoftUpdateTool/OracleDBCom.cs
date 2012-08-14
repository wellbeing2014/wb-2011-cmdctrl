/*
 * 由SharpDevelop创建。
 * 用户： wellbeing
 * 日期: 2012/8/14
 * 时间: 21:10
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;
namespace WisoftUpdateTool
{
	/// <summary>
 	/// 数据库通用操作类
 	/// </summary>
	public class OracleDBCom
	{
		protected  OracleConnection con;//连接对象
		public OracleDBCom()
		{
			
		}
		public OracleDBCom(string constr)
	    {
	   		con=new OracleConnection(constr);
	    }

		#region 打开数据库连接关闭数据库连接
		/// <summary>
		/// 打开数据库连接
		/// </summary>
		private  void Open()
		{
			//打开数据库连接
			if(con.State==ConnectionState.Closed)
			{
			try
			{
			 //打开数据库连接
			 con.Open();
			}
			catch(OracleException e)
			{
				 if (e.Code ==1017)
	        		MessageBox.Show("伤不起啊，你居然把用户名密码输错了，认真点行不行？");
	        	else if(e.Code == 12541)
	        		MessageBox.Show("尼玛，你给的数据库我根本连不上。");
	        	else 
	        		MessageBox.Show("歇菜，不知道数据库怎么了，反正是有问题。");
			}
			
			}
		}


  
		/// <summary>
		/// 关闭数据库连接
		/// </summary>
		private  void Close()
		{   
		//判断连接的状态是否已经打开
		if(con.State==ConnectionState.Open)
		{
		con.Close();
		}
		}
		#endregion

  #region 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对OracleDataReader进行Close )
  /// <summary>   
  /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对OracleDataReader进行Close )   
  /// </summary>   
  /// <param name="sql">查询语句</param>   
  /// <returns>OracleDataReader</returns>   
  public  OracleDataReader ExecuteReader(string sql)   
  {   
   OracleDataReader myReader;
   Open(); 
   OracleCommand cmd = new OracleCommand(sql, con);   
   myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);   
   return myReader;   
   
  }   
  #endregion

  #region 执行带参数的SQL语句   
  /// <summary>   
  /// 执行SQL语句，返回影响的记录数   
  /// </summary>   
  /// <param name="sql">SQL语句</param>   
  /// <returns>影响的记录数</returns>   
  public  int ExecuteSql(string sql, params OracleParameter[] cmdParms)   
  {   
     
   OracleCommand cmd = new OracleCommand(); 
  {   
   try  
   {   
    PrepareCommand(cmd, con, null, sql, cmdParms);   
    int rows = cmd.ExecuteNonQuery();   
    cmd.Parameters.Clear();   
    return rows;   
   }   
   catch (System.Data.OracleClient.OracleException e)   
   {   
    throw e;   
   }   
  }   
     
  }   
  #endregion

  #region 执行带参数的SQL语句   
  /// <summary>   
  /// 执行不带参数的SQL语句  
  /// </summary>   
  /// <param name="sql">SQL语句</param>     
  public  void ExecuteSql(string sql)   
  {        
   OracleCommand cmd = new OracleCommand(sql,con);  
   try  
   {   
    Open();
    cmd.ExecuteNonQuery();
    Close();
   }   
   catch (System.Data.OracleClient.OracleException e)   
   {   
    Close();
    throw e;   
   }     
  }   
  #endregion

  #region 执行SQL语句，返回数据到DataSet中
  /// <summary>
  /// 执行SQL语句，返回数据到DataSet中
  /// </summary>
  /// <param name="sql">sql语句</param>
  /// <returns>返回DataSet</returns>
  public  DataSet GetDataSet(string sql)
  {
   DataSet ds=new DataSet();
   try
   {
    Open();//打开数据连接
    OracleDataAdapter adapter=new OracleDataAdapter(sql,con);
    adapter.Fill(ds);
   }
   catch//(Exception ex)
   {
    
   }
   finally
   {
    Close();//关闭数据库连接
   }
   
   return ds;
  }
  #endregion
   
  #region 执行SQL语句，返回数据到自定义DataSet中
  /// <summary>
  /// 执行SQL语句，返回数据到DataSet中
  /// </summary>
  /// <param name="sql">sql语句</param>
  /// <param name="DataSetName">自定义返回的DataSet表名</param>
  /// <returns>返回DataSet</returns>
  public  DataSet GetDataSet(string sql,string DataSetName)
  {
   DataSet ds=new DataSet();
   Open();//打开数据连接
   OracleDataAdapter adapter=new OracleDataAdapter(sql,con);
   adapter.Fill(ds,DataSetName);
   Close();//关闭数据库连接
   return ds;
  }
  #endregion

  #region 执行Sql语句,返回带分页功能的自定义dataset
  /// <summary>
  /// 执行Sql语句,返回带分页功能的自定义dataset
  /// </summary>
  /// <param name="sql">Sql语句</param>
  /// <param name="PageSize">每页显示记录数</param>
  /// <param name="CurrPageIndex">当前页</param>
  /// <param name="DataSetName">返回dataset表名</param>
  /// <returns>返回DataSet</returns>
  public  DataSet GetDataSet(string sql,int PageSize,int CurrPageIndex,string DataSetName)
  {
   DataSet ds=new DataSet();
   Open();//打开数据连接
   OracleDataAdapter adapter=new OracleDataAdapter(sql,con);
   adapter.Fill(ds,PageSize * (CurrPageIndex - 1), PageSize,DataSetName);
   Close();//关闭数据库连接
   return ds;
  }
  #endregion

  #region 执行SQL语句，返回记录总数
  /// <summary>
  /// 执行SQL语句，返回记录总数
  /// </summary>
  /// <param name="sql">sql语句</param>
  /// <returns>返回记录总条数</returns>
  public  int GetRecordCount(string sql)
  {
   int recordCount = 0;
   Open();//打开数据连接
   OracleCommand command = new OracleCommand(sql,con);
   OracleDataReader dataReader = command.ExecuteReader();
   while(dataReader.Read())
   {
    recordCount++;
   }
   dataReader.Close();
   Close();//关闭数据库连接
   return recordCount;
  }
  #endregion
       
  #region 统计某表记录总数 
  /// <summary> 
  /// 统计某表记录总数 
  /// </summary> 
  /// <param name="KeyField">主键/索引键</param> 
  /// <param name="TableName">数据库.用户名.表名</param> 
  /// <param name="Condition">查询条件</param> 
  /// <returns>返回记录总数</returns> 
  public  int GetRecordCount(string keyField, string tableName, string condition) 
  { 
   int RecordCount = 0; 
   string sql = "select count(" + keyField + ") as count from " + tableName + " " + condition; 
   DataSet ds = GetDataSet(sql); 
   if (ds.Tables[0].Rows.Count > 0) 
   { 
    RecordCount =Convert.ToInt32(ds.Tables[0].Rows[0][0]); 
   } 
   ds.Clear(); 
   ds.Dispose(); 
   return RecordCount; 
  } 
  /// <summary> 
  /// 统计某表记录总数 
  /// </summary> 
  /// <param name="Field">可重复的字段</param> 
  /// <param name="tableName">数据库.用户名.表名</param> 
  /// <param name="condition">查询条件</param> 
  /// <param name="flag">字段是否主键</param> 
  /// <returns>返回记录总数</returns> 
  public  int GetRecordCount(string Field, string tableName, string condition, bool flag) 
  { 
   int RecordCount = 0; 
   if (flag) 
   { 
    RecordCount = GetRecordCount(Field, tableName, condition); 
   } 
   else 
   { 
    string sql = "select count(distinct(" + Field + ")) as count from " + tableName + " " + condition; 
    DataSet ds = GetDataSet(sql); 
    if (ds.Tables[0].Rows.Count > 0) 
    { 
     RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]); 
    } 
    ds.Clear(); 
    ds.Dispose(); 
   } 
   return RecordCount; 
  }
  #endregion


  

		#region 存储过程操作   
		/// <summary>   
		/// 执行存储过程，返回OracleDataReader ( 注意：调用该方法后，一定要对OracleDataReader进行Close )   
		/// </summary>   
		/// <param name="storedProcName">存储过程名</param>   
		/// <param name="parameters">存储过程参数</param>   
		/// <returns>OracleDataReader</returns>   
		public  OracleDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)   
		{     
			OracleDataReader returnReader;   
			Open();//打开数据连接  
			OracleCommand command = BuildQueryCommand(con,storedProcName, parameters);   
			command.CommandType = CommandType.StoredProcedure;   
			returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);   
			return returnReader;   
		}   
		
		/// <summary>   
		/// 执行存储过程，无返回结果   
		/// </summary>   
		/// <param name="storedProcName">存储过程名</param>   
		/// <param name="parameters">存储过程参数</param>   
		/// <returns>OracleDataReader</returns>   
		public  void ExceuteProcedure(string storedProcName, IDataParameter[] parameters)   
		{ 
			Open();//打开数据连接  
			OracleCommand command = BuildQueryCommand(con,storedProcName, parameters);   
			command.CommandType = CommandType.StoredProcedure;   
			command.ExecuteNonQuery();  
			Close();
		}   
		
		/// <summary>   
		/// 执行存储过程,返回DataSet   
		/// </summary>   
		/// <param name="storedProcName">存储过程名</param>   
		/// <param name="parameters">存储过程参数</param>   
		/// <param name="tableName">DataSet结果中的表名</param>   
		/// <returns>DataSet</returns>   
		public  DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)   
		{   
			DataSet dataSet = new DataSet();   
			Open();//打开数据连接   
			OracleDataAdapter adapter = new OracleDataAdapter();   
			adapter.SelectCommand = BuildQueryCommand(con,storedProcName, parameters);   
			adapter.Fill(dataSet, tableName);   
			Close();//关闭数据库连接   
			return dataSet;   
		}   
		
		
		/// <summary>   
		/// 执行存储过程，返回影响的行数       
		/// </summary>   
		/// <param name="storedProcName">存储过程名</param>   
		/// <param name="parameters">存储过程参数</param>   
		/// <param name="rowsAffected">影响的行数</param>   
		/// <returns></returns>   
		public  int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)   
		{   
		
		int result;   
		Open();   
		OracleCommand command = BuildIntCommand(con, storedProcName, parameters);   
		rowsAffected = command.ExecuteNonQuery();   
		result = (int)command.Parameters["ReturnValue"].Value;   
		Close(); 
		return result;   
		
		}   
		
		#endregion   

		#region 私有成员
		private  void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)   
		{   
			if (conn.State != ConnectionState.Open)   
			conn.Open();   
			cmd.Connection = conn;   
			cmd.CommandText = cmdText;   
			if (trans != null)   
			cmd.Transaction = trans;   
			cmd.CommandType = CommandType.Text;//cmdType;   
			if (cmdParms != null)   
			{   
			foreach (OracleParameter parameter in cmdParms)   
			{   
			if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&   
			(parameter.Value == null))   
			{   
			parameter.Value = DBNull.Value;   
			}   
			cmd.Parameters.Add(parameter);   
			}   
			}   
		} 
		
		/// <summary>   
		/// 构建 OracleCommand 对象(用来返回一个结果集，而不是一个整数值)   
		/// </summary>   
		/// <param name="connection">数据库连接</param>   
		/// <param name="storedProcName">存储过程名</param>   
		/// <param name="parameters">存储过程参数</param>   
		/// <returns>OracleCommand</returns>   
		private  OracleCommand BuildQueryCommand(OracleConnection connection,string storedProcName, IDataParameter[] parameters)   
		{   
			Open();//打开数据连接
			OracleCommand command = new OracleCommand(storedProcName, connection);   
			command.CommandType = CommandType.StoredProcedure;   
			foreach (OracleParameter parameter in parameters)   
			{   
			if (parameter != null)   
			{   
			// 检查未分配值的输出参数,将其分配以DBNull.Value.   
			if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&   
			(parameter.Value == null))   
			{   
			parameter.Value = DBNull.Value;   
			}   
			command.Parameters.Add(parameter);   
			}   
			}   
			return command;   
		}  
		
		/// <summary>   
		/// 创建 OracleCommand 对象实例(用来返回一个整数值)    
		/// </summary>   
		/// <param name="storedProcName">存储过程名</param>   
		/// <param name="parameters">存储过程参数</param>   
		/// <returns>OracleCommand 对象实例</returns>   
		private  OracleCommand BuildIntCommand(OracleConnection connection, string storedProcName, IDataParameter[] parameters)   
		{   
			OracleCommand command = BuildQueryCommand(connection, storedProcName, parameters);   
			command.Parameters.Add(new OracleParameter("ReturnValue",   
			OracleType.Int16, 4, ParameterDirection.ReturnValue,   
			false, 0, 0, string.Empty, DataRowVersion.Default, null));   
			return command;   
		}  
		
		#endregion
		      
		
	}
}

