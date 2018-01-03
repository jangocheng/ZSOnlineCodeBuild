using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 页面参数
    /// </summary>
    public class E_PageParameter
    {
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string serverip { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string dbname { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string tablename { get; set; }

        /// <summary>
        /// 表名前缀
        /// </summary>
        public string prefix { get; set; }

        /// <summary>
        /// 数据库链接
        /// </summary>
        public string connstring
        {
            get
            {
                return $"Data Source={this.serverip};Initial Catalog={this.dbname};User ID={this.userid};Password={this.password}";
            }
        }

    }
}
