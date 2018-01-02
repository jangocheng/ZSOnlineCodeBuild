using CodeHelper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    /// <summary>
    /// 生成数据操作类
    /// </summary>
    public class BuilderDALCode
    {
        /// <summary>
        /// 生成实体类的参数实体
        /// </summary>
        public E_DALCode eDALCode { get; set; }

        /// <summary>
        /// 生成实体类代码
        /// </summary>
        public string CreatDAL()
        {
            string strclass= BuilderTools.LoadTemplate("code/DAL.txt");
            eDALCode.strwhere = this.GetWhereCode();
            eDALCode.insertsql = this.GetInsertSql();
            eDALCode.updatesql = this.GetUpdateSql();
            eDALCode.primarykeyname = this.GetPrimaryKeyName();
            strclass = this.ReplaceAll(strclass, eDALCode);
            return strclass.ToString();
        }

        /// <summary>
        /// 替换模板结果
        /// </summary>
        private string ReplaceAll(string content,E_DALCode model)
        {
            foreach (var item in model.GetType().GetProperties())
            {
                content = content.Replace("{$" + item.Name.ToLower() + "}", item.GetValue(model).ToString());
            }
            return content;
        }
        
        /// <summary>
        /// 获取查询条件代码
        /// </summary>
        /// <returns></returns>
        private string GetWhereCode()
        {
            StringBuilder strcode = new StringBuilder();
            foreach (ColumnInfo field in eDALCode.Fieldlist)
            {
                if(field.Description.IndexOf("search")>-1)
                {
                    string columnType = CodeCommon.DbTypeToCS(field.TypeName);
                    string AttrType = BuilderTools.GetAttrType(columnType); //属性数据类型
                    string where = $"" + '"' + $"{field.ColumnName} = @{field.ColumnName}" + '"';
                    string description = field.Description.Replace(":search", "");
                    switch (AttrType)
                    {
                        case "int":
                            strcode.Append(@"
            if (model."+ field.ColumnName + " > 0) //"+ description + @"
            {
                strWhere.AddWhere(" + where + @");
            }");
                            break;
                        case "string":
                            strcode.Append(@"
            if (!string.IsNullOrEmpty(model." + field.ColumnName + ")) //" + description + @"
            {
                strWhere.AddWhere(" + where + @");
            }");
                            break;

                        case "DateTime":
                            strcode.Append(@"
            if (model." + field.ColumnName + " != null) //" + description + @"
            {
                strWhere.AddWhere(" + where + @");
            }");
                            break;
                    }
                }
            }
            return strcode.ToString();
        }
        
        /// <summary>
        /// 获取插入语句
        /// </summary>
        private string GetInsertSql()
        {
            StringBuilder strcode = new StringBuilder();
            string columns = "";
            string columnval = "";
            foreach (ColumnInfo field in eDALCode.Fieldlist)
            {
                if (!field.IsPrimaryKey)
                {
                    columns += "[" + field.ColumnName + "],";
                    columnval += "@" + field.ColumnName + ",";
                }
            }
            columns = columns.TrimEnd(',');
            columnval = columnval.TrimEnd(',');
            strcode.Append($@"INSERT INTO {eDALCode.TableName}
                ({columns}) 
                VALUES 
                ({columnval});select @@IDENTITY;");
                return strcode.ToString();
        }

        /// <summary>
        /// 获取更新语句
        /// </summary>
        private string GetUpdateSql()
        {
            StringBuilder strcode = new StringBuilder();
            string columns = "";
            string PrimaryKey = "";
            foreach (ColumnInfo field in eDALCode.Fieldlist)
            {
                if (!field.IsPrimaryKey)
                {
                    columns += "[" + field.ColumnName + "]=@"+ field.ColumnName + ",";
                }
                else
                {
                    PrimaryKey = field.ColumnName + "=" + "@" + field.ColumnName;
                }
            }
            columns = columns.TrimEnd(',');
            strcode.Append($@"UPDATE {eDALCode.TableName}
                           SET {columns}
                         WHERE  PrimaryKey");
            return strcode.ToString();
        }

        /// <summary>
        /// 获取主键ID
        /// </summary>
        private string GetPrimaryKeyName()
        {
            foreach (ColumnInfo field in eDALCode.Fieldlist)
            {
                if (field.IsPrimaryKey)
                {
                    return field.ColumnName;
                }
            }
            return "";
        }
    }
}
