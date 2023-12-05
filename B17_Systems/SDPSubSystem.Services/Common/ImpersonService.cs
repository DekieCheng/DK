using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Services.Common
{
    public  class ImpersonService
    {
        /// <summary>
        /// 执行存储过程返回错误信息
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlXml">参数</param>
        /// <param name="code">错误代码</param>
        /// <returns></returns>
        public string RunProcedure(string procName, string sqlXml, out int code)
        {
            return ImpersonMsSql.RunProcedure(procName, sqlXml, out code);
        }

        /// <summary>
        /// 执行存储过程返回错误信息
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlXml">参数</param>
        /// <param name="code">错误代码</param>
        /// <returns></returns>
        public DataSet RunProcedure(string procName, string sqlXml, string tablename, out int code, string connstr)
        {
            return ImpersonMsSql.RunProcedure(procName, sqlXml, tablename, out code, connstr);
        }

        /// <summary>
        /// 执行存储过程返回错误信息
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlXml">参数</param>
        /// <param name="code">错误代码</param>
        /// <returns></returns>
        public DataSet RunProcedure(string procName, string sqlXml, string tablename, out int code, out string Msg, string connstr)
        {
            return ImpersonMsSql.RunProcedure(procName, sqlXml, tablename, out code, out Msg, connstr);
        }

        /// <summary>
        /// 执行存储过程返回错误信息
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlXml">参数</param>
        /// <param name="code">错误代码</param>
        /// <returns></returns>
        public DataSet RunProcedure(string procName, IDataParameter[] parameters, string tablename, string connstr)
        {
            return ImpersonMsSql.RunProcedure(procName, parameters, tablename, connstr);
        }


        /// <summary>
        /// 执行存储过程返回错误信息
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlXml">参数</param>
        /// <param name="code">错误代码</param>
        /// <returns></returns>
        public string RunProcedure(string procName, string sqlXml, out int code, string conn)
        {
            return ImpersonMsSql.RunProcedure(procName, sqlXml, out code, conn);
        }

        /// <summary>
        /// 执行储存过程返回查询结果，该储存过程必须只有两个参数分别为sqlxml和msg,其中msg为output 参数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="sqlXml"></param>
        /// <returns></returns>
        public System.Data.DataSet Query(string procName, string sqlXml, string conn = null)
        {
            SqlParameter[] para ={
                                  new SqlParameter("@sqlxml", SqlDbType.Xml),
                                  new SqlParameter("@msg", SqlDbType.VarChar, 1000)
                              };
            para[0].Value = sqlXml;
            para[1].Direction = ParameterDirection.Output;
            return ImpersonMsSql.RunProcedure(procName, para, "t1", conn);
        }

        /// <summary>
        /// 执行储存过程返回查询结果，该储存过程必须只有两个参数分别为sqlxml和msg,其中msg为output 参数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="sqlXml"></param>
        /// <returns></returns>
        public System.Data.DataSet Queryxmlvarchar(string procName, string sqlXml, string conn = null)
        {
            SqlParameter[] para ={
                                  new SqlParameter("@sqlxml", SqlDbType.VarChar,8000),
                                  new SqlParameter("@msg", SqlDbType.VarChar, 1000)
                              };
            para[0].Value = sqlXml;
            para[1].Direction = ParameterDirection.Output;
            return ImpersonMsSql.RunProcedure(procName, para, "t1", conn);
        }


        /// <summary>
        /// 执行储存过程返回查询结果，该储存过程必须只有两个参数分别为sqlxml和msg,其中msg为output 参数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="sqlXml"></param>
        /// <returns></returns>
        public System.Data.DataSet Query(string procName, string sqlXml, string msg, string conn = null)
        {
            SqlParameter[] para ={
                                  new SqlParameter("@sqlxml", SqlDbType.Xml),
                                  new SqlParameter("@msg", SqlDbType.VarChar, 1000)
                              };
            para[0].Value = sqlXml;
            para[1].Direction = ParameterDirection.Output;
            return ImpersonMsSql.RunProcedure(procName, para, "t1", conn);
        }


        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, string conn = null)
        {
            return ImpersonMsSql.ExecuteSql(SQLString, conn);
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet QueryBySql(string SQLString, string conn = null)
        {
            return ImpersonMsSql.Query(SQLString, conn);
        }
        /// <summary>
        /// 获取用户当前登录用户的基本信息（用于存储过程参数）
        /// </summary>
        /// <returns></returns>
    }
}
