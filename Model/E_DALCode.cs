using CodeHelper;
using IDBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// DAL实体参数
    /// </summary>
    public class E_DALCode
    {
        /// <summary>
        /// 列集合
        /// </summary>
        public List<ColumnInfo> Fieldlist { get; set; }

        /// <summary>
        /// 主键列
        /// </summary>
        public List<ColumnInfo> Keys { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 实体类名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 实体类命名空间
        /// </summary>
        public string Modelpath { get; set; }

        /// <summary>
        /// 数据库对象
        /// </summary>
        public IDbObject DbObject { get; set; }

        /// <summary>
        /// 类命名空间
        /// </summary>
        public string DALpath { get; set; }

        /// <summary>
        /// 类名称
        /// </summary>
        public string DALName { get; set; }

        /// <summary>
        /// 表说明
        /// </summary>
        public string TableDescription { get; set; }


        /// <summary>
        /// 查询条件
        /// </summary>
        public string strwhere { get; set; }

        /// <summary>
        /// 插入语句
        /// </summary>
        public string insertsql { get; set; }

        /// <summary>
        /// 更新语句
        /// </summary>
        public string updatesql { get; set; }

        /// <summary>
        /// 主键名称
        /// </summary>
        public string primarykeyname { get; set; }
    }
}
