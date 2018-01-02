﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Model;
using System.Linq;
using System.Collections.Generic;
using Dapper;
namespace DAL
{
	/// <summary>
	/// 数据访问类:D_copyroomservice
	/// </summary>
	public partial class D_copyroomservice
	{
		public D_copyroomservice()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(tb_copyroomservice model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from tb_copyroomservice");
			strSql.Append("  where id=@id ");
			using (IDbConnection conn = DapperHelper.OpenConnection())
			{
				int count = conn.Execute(strSql.ToString(), model);
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(tb_copyroomservice model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_copyroomservice(");
			strSql.Append("id,copyroomid,name,intro,addtime)");
			strSql.Append(" values (");
			strSql.Append("@id,@copyroomid,@name,@intro,@addtime)");
			using (IDbConnection conn = DapperHelper.OpenConnection())
			{
				int count = conn.Execute(strSql.ToString(), model);
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(tb_copyroomservice model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder setSql=new StringBuilder();
			strSql.Append("update tb_copyroomservice set ");
			if(!String.IsNullOrEmpty(model.copyroomid))
			{
				setSql.Append( "copyroomid=@copyroomid,");
			}
			if(!String.IsNullOrEmpty(model.name))
			{
				setSql.Append( "name=@name,");
			}
			if(!String.IsNullOrEmpty(model.intro))
			{
				setSql.Append( "intro=@intro,");
			}
			if(model.addtime!=null)
			{
				setSql.Append( "addtime=@addtime,");
			}
			strSql.Append(setSql.ToString().TrimEnd(','));
			strSql.Append(" where id=@id ");
			using (IDbConnection conn = DapperHelper.OpenConnection())
			{
				int count = conn.Execute(strSql.ToString(), model);
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(tb_copyroomservice model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_copyroomservice ");
			strSql.Append(" where id=@id " );
			using (IDbConnection conn = DapperHelper.OpenConnection())
			{
				int count = conn.Execute(strSql.ToString(), model);
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_copyroomservice ");
			strSql.Append(" where id in ("+idlist + ")  ");
			using (IDbConnection conn = DapperHelper.OpenConnection())
			{
				int count = conn.Execute(strSql.ToString());
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<tb_copyroomservice> GetList(tb_copyroomservice model, ref int total)
		{
			List<tb_copyroomservice> list;
			StringBuilder strSql=new StringBuilder();
			StringBuilder whereSql = new StringBuilder(" where 1 = 1 ");
			strSql.Append(" select  ROW_NUMBER() OVER(ORDER BY id desc) AS RID, * from tb_copyroomservice ");
			if(!String.IsNullOrEmpty(model.copyroomid))
			{
				whereSql.Append( " and copyroomid=@copyroomid");
			}
			if(!String.IsNullOrEmpty(model.name))
			{
				whereSql.Append( " and name=@name");
			}
			if(!String.IsNullOrEmpty(model.intro))
			{
				whereSql.Append( " and intro=@intro");
			}
			if(model.addtime!=null)
			{
				whereSql.Append( " and addtime=@addtime");
			}
			strSql.Append(whereSql);
			string CountSql = "SELECT COUNT(1) as RowsCount FROM (" + strSql.ToString() + ") AS CountList";
			string pageSqlStr = "select * from ( " + strSql.ToString() + " ) as Temp_PageData where Temp_PageData.RID BETWEEN {0} AND {1}";
			pageSqlStr = string.Format(pageSqlStr, (model.PageSize * (model.PageIndex - 1) + 1).ToString(), (model.PageSize * model.PageIndex).ToString());
			using (IDbConnection conn = DapperHelper.OpenConnection())
			{
				list = conn.Query <tb_copyroomservice>(pageSqlStr, model)?.ToList();
				total = conn.ExecuteScalar<int>(CountSql, model);
			}
			return list;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public tb_copyroomservice GetInfo(tb_copyroomservice model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * from tb_copyroomservice");
			strSql.Append("  where id=@id ");
			using (IDbConnection conn = DapperHelper.OpenConnection())
			{
				model = conn.Query <tb_copyroomservice>(strSql.ToString(), model)?.FirstOrDefault();
			}
			return model;
		}
		#endregion  Method
	}
}

