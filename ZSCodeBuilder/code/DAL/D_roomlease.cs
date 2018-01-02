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
	/// 数据访问类:D_roomlease
	/// </summary>
	public partial class D_roomlease
	{
		public D_roomlease()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(tb_roomlease model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from tb_roomlease");
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
		public bool Add(tb_roomlease model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_roomlease(");
			strSql.Append("id,roomid,company,time,linkman,phone,emergencylinkman,emergencyphone,addtime)");
			strSql.Append(" values (");
			strSql.Append("@id,@roomid,@company,@time,@linkman,@phone,@emergencylinkman,@emergencyphone,@addtime)");
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
		public bool Update(tb_roomlease model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder setSql=new StringBuilder();
			strSql.Append("update tb_roomlease set ");
			if(!String.IsNullOrEmpty(model.roomid))
			{
				setSql.Append( "roomid=@roomid,");
			}
			if(!String.IsNullOrEmpty(model.company))
			{
				setSql.Append( "company=@company,");
			}
			if(!String.IsNullOrEmpty(model.time))
			{
				setSql.Append( "time=@time,");
			}
			if(!String.IsNullOrEmpty(model.linkman))
			{
				setSql.Append( "linkman=@linkman,");
			}
			if(!String.IsNullOrEmpty(model.phone))
			{
				setSql.Append( "phone=@phone,");
			}
			if(!String.IsNullOrEmpty(model.emergencylinkman))
			{
				setSql.Append( "emergencylinkman=@emergencylinkman,");
			}
			if(!String.IsNullOrEmpty(model.emergencyphone))
			{
				setSql.Append( "emergencyphone=@emergencyphone,");
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
		public bool Delete(tb_roomlease model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_roomlease ");
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
			strSql.Append("delete from tb_roomlease ");
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
		public List<tb_roomlease> GetList(tb_roomlease model, ref int total)
		{
			List<tb_roomlease> list;
			StringBuilder strSql=new StringBuilder();
			StringBuilder whereSql = new StringBuilder(" where 1 = 1 ");
			strSql.Append(" select  ROW_NUMBER() OVER(ORDER BY id desc) AS RID, * from tb_roomlease ");
			if(!String.IsNullOrEmpty(model.roomid))
			{
				whereSql.Append( " and roomid=@roomid");
			}
			if(!String.IsNullOrEmpty(model.company))
			{
				whereSql.Append( " and company=@company");
			}
			if(!String.IsNullOrEmpty(model.time))
			{
				whereSql.Append( " and time=@time");
			}
			if(!String.IsNullOrEmpty(model.linkman))
			{
				whereSql.Append( " and linkman=@linkman");
			}
			if(!String.IsNullOrEmpty(model.phone))
			{
				whereSql.Append( " and phone=@phone");
			}
			if(!String.IsNullOrEmpty(model.emergencylinkman))
			{
				whereSql.Append( " and emergencylinkman=@emergencylinkman");
			}
			if(!String.IsNullOrEmpty(model.emergencyphone))
			{
				whereSql.Append( " and emergencyphone=@emergencyphone");
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
				list = conn.Query <tb_roomlease>(pageSqlStr, model)?.ToList();
				total = conn.ExecuteScalar<int>(CountSql, model);
			}
			return list;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public tb_roomlease GetInfo(tb_roomlease model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * from tb_roomlease");
			strSql.Append("  where id=@id ");
			using (IDbConnection conn = DapperHelper.OpenConnection())
			{
				model = conn.Query <tb_roomlease>(strSql.ToString(), model)?.FirstOrDefault();
			}
			return model;
		}
		#endregion  Method
	}
}

