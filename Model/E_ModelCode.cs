using CodeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 实体类参数
    /// </summary>
    public class E_ModelCode
    {
        /// <summary>
        /// 表字段集合
        /// </summary>
        public List<ColumnInfo> Fieldlist { get; set; }

        /// <summary>
        /// 实体类名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 需要继承的基类
        /// </summary>
        public string BaseClass { get; set; }

        /// <summary>
        /// 实体类说明（表说明）
        /// </summary>
        public string TableDescription { get; set; }
        
    }
}
