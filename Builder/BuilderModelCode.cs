using CodeHelper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Builder
{
    /// <summary>
    /// 生成实体类
    /// </summary>
    public class BuilderModelCode
    {
        /// <summary>
        /// 生成实体类的参数实体
        /// </summary>
        public E_ModelCode eModelCode { get; set; }

        /// <summary>
        /// 生成实体类代码
        /// </summary>
        public string CreatModel()
        {
            StringBuilder strclass = new StringBuilder();
            //类引用
            strclass.AppendLine(@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;");

            //命名空间
            strclass.AppendLine("namespace Model");
            strclass.AppendLine("{");
            //类说明
            strclass.Append($@"
    /// <summary>
    /// {eModelCode.TableDescription}
    /// </summary>
    public class E_{eModelCode.ModelName.Substring(0, 1).ToUpper() + eModelCode.ModelName.Substring(1, eModelCode.ModelName.Length - 1)}");
            strclass.AppendLine(string.IsNullOrEmpty(eModelCode.BaseClass) ? "" : ":" + eModelCode.BaseClass);
            strclass.AppendLine("   {");
            //字段属性
            strclass.AppendLine(CreatModelMethod());
            strclass.AppendLine("   }");
            strclass.AppendLine("}");
            return strclass.ToString();
        }

        /// <summary>
        /// 生成属性集合
        /// </summary>
        public string CreatModelMethod()
        {
            StringBuilder strclass = new StringBuilder();
            foreach (ColumnInfo field in eModelCode.Fieldlist)
            {
                string columnName = field.ColumnName;  //列名
                string columnTypedb = field.TypeName;  //类型
                bool IsIdentity = field.IsIdentity;    //是否自增标识
                bool ispk = field.IsPrimaryKey;        //是否主键
                bool cisnull = field.Nullable;         //
                string deText = field.Description;     //属性说明
                string columnType = CodeCommon.DbTypeToCS(columnTypedb);
                string AttrType = BuilderTools.GetAttrType(columnType); //属性数据类型

                strclass.Append($@"
        /// <summary>
        /// {deText}
        /// </summary>
        public {AttrType} {columnName} ");
                strclass.AppendLine("{ get; set; }");

            }
            return strclass.ToString();
        }

        

    }
}
