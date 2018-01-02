using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    /// <summary>
    /// 生成控制器代码
    /// </summary>
    public class BuilderControllersCode
    {
        /// <summary>
        /// 生成控制器代码实体类
        /// </summary>
        public E_ControllersCode eControllersCode { get; set; }

        /// <summary>
        /// 生成实体类代码
        /// </summary>
        public string CreatControllers()
        {
            string strclass = BuilderTools.LoadTemplate("code/Controllers.txt");
            strclass = this.ReplaceAll(strclass, eControllersCode);
            return strclass.ToString();
        }

        /// <summary>
        /// 替换模板结果
        /// </summary>
        private string ReplaceAll(string content, E_ControllersCode model)
        {
            foreach (var item in model.GetType().GetProperties())
            {
                content = content.Replace("{$" + item.Name.ToLower() + "}", item.GetValue(model).ToString());
            }
            return content;
        }
    }
}
