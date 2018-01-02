using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 控制器生成配置
    /// </summary>
    public class E_ControllersCode
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string tablename { get; set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string modelname { get; set; }

        /// <summary>
        /// 主键名称
        /// </summary>
        public string primarykeyname { get; set; }

        /// <summary>
        /// 表说明
        /// </summary>
        public string tabledescription { get; set; }
    }
}
