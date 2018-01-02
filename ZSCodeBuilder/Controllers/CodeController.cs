using Builder;
using CodeHelper;
using DbObjects.SQL2012;
using Model;
using Model.zTree;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZSCodeBuilder.Controllers
{
    public class CodeController : Controller
    {
        // GET: Code
        public ActionResult BuilderCode()
        {
            return View();
        }

        /// <summary>
        /// 获取树节点集合
        /// </summary>
        [Route("code/getdatabasetree")]
        public JsonResult GetTreeNodeList(E_PageParameter ePageParameter)
        {
            DbObject db = new DbObject(ePageParameter.connstring);

            List<E_TreeNode> NodeList = new List<E_TreeNode>();
            var tables = db.GetTableViews(ePageParameter.dbname);
            foreach (var item in tables)
            {
                E_TreeNode nodetable = new E_TreeNode();
                nodetable.id = 0;
                nodetable.name = item;
                nodetable.nodetype = 1;
                List<ColumnInfo> ColumnList = db.GetColumnInfoList(ePageParameter.dbname, item);
                List<E_TreeNode> columnnodelist = new List<E_TreeNode>();
                foreach (var columnitem in ColumnList)
                {
                    E_TreeNode nodecolumn = new E_TreeNode();
                    nodecolumn.id = 0;
                    nodecolumn.name = columnitem.ColumnName + "（" + columnitem.TypeName + "）" + columnitem.Description;
                    nodecolumn.children = null;
                    nodecolumn.nodetype = 2;
                    columnnodelist.Add(nodecolumn);
                }
                nodetable.children = columnnodelist;
                NodeList.Add(nodetable);
            }
            return Json(NodeList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 生成对象实体
        /// </summary>
        public ActionResult BuilderModel(E_PageParameter ePageParameter)
        {
            DbObject db = new DbObject(ePageParameter.connstring);
            BuilderModelCode builderModel = new BuilderModelCode();
            E_ModelCode eModelCode = new E_ModelCode();
            eModelCode.Fieldlist = db.GetColumnInfoList(ePageParameter.dbname, ePageParameter.tablename);
            eModelCode.ModelName = ePageParameter.tablename.Split('_')[1];
            eModelCode.BaseClass = "E_BaseModel";
            DataRow tableDescRow = db.GetTablesExProperty(ePageParameter.dbname).Select("objname='" + ePageParameter.tablename + "'").FirstOrDefault();
            if (tableDescRow != null)
            {
                eModelCode.TableDescription = tableDescRow["value"]?.ToString();
            }
            builderModel.eModelCode = eModelCode;
            string modelStr = builderModel.CreatModel();
            ViewBag.CodeHtml = modelStr;
            return PartialView("~/Views/Code/BuilderModel.cshtml");
        }

        /// <summary>
        /// 生成对象操作
        /// </summary>
        public ActionResult BuilderDAL(E_PageParameter ePageParameter)
        {
            BuilderDALCode builderDAL = new BuilderDALCode();
            DbObject db = new DbObject(ePageParameter.connstring);
            E_DALCode eDalCode = new E_DALCode();
            string tname = ePageParameter.tablename.Split('_')[1];
            List<ColumnInfo> list = db.GetColumnInfoList(ePageParameter.dbname, ePageParameter.tablename);
            eDalCode.Fieldlist = list;
            eDalCode.TableName = ePageParameter.tablename;
            eDalCode.ModelName = "E_" + tname.Substring(0, 1).ToUpper() + tname.Substring(1, tname.Length - 1);
            eDalCode.Modelpath = "Model";
            eDalCode.DbObject = db;
            eDalCode.Keys = list.Where(a => a.IsPrimaryKey).ToList();
            eDalCode.DALpath = "DAL";
            eDalCode.DALName = "D_" + tname.Substring(0, 1).ToUpper() + tname.Substring(1, tname.Length - 1);
            DataRow tableDescRow = db.GetTablesExProperty(ePageParameter.dbname).Select("objname='" + ePageParameter.tablename + "'").FirstOrDefault();
            if (tableDescRow != null)
            {
                eDalCode.TableDescription = tableDescRow["value"]?.ToString();
            }
            builderDAL.eDALCode = eDalCode;
            string modelStr = builderDAL.CreatDAL();
            ViewBag.CodeHtml = modelStr;
            return PartialView("~/Views/Code/BuilderDAL.cshtml");
        }

        /// <summary>
        /// 生成对象控制器
        /// </summary>
        public ActionResult BuilderControllers(E_PageParameter ePageParameter)
        {
            BuilderControllersCode builderControllers = new BuilderControllersCode();
            DbObject db = new DbObject(ePageParameter.connstring);
            E_ControllersCode eControllersCode = new E_ControllersCode();
            string tname = ePageParameter.tablename.Split('_')[1];
            List<ColumnInfo> list = db.GetColumnInfoList(ePageParameter.dbname, ePageParameter.tablename);
            eControllersCode.primarykeyname = list.Where(a => a.IsPrimaryKey).ToList().First().ColumnName;
            eControllersCode.modelname = "E_"+ tname.Substring(0, 1).ToUpper() + tname.Substring(1, tname.Length - 1);
            eControllersCode.tablename = tname.Substring(0, 1).ToUpper() + tname.Substring(1, tname.Length - 1);
            DataRow tableDescRow = db.GetTablesExProperty(ePageParameter.dbname).Select("objname='" + ePageParameter.tablename + "'").FirstOrDefault();
            if (tableDescRow != null)
            {
                eControllersCode.tabledescription = tableDescRow["value"]?.ToString();
            }
            builderControllers.eControllersCode = eControllersCode;

            string modelStr = builderControllers.CreatControllers();
            ViewBag.CodeHtml = modelStr;
            return PartialView("~/Views/Code/BuilderControllers.cshtml");
        }

        /// <summary>
        /// 生成对象列表页
        /// </summary>
        public ActionResult BuilderPageList(E_PageParameter ePageParameter)
        {
            return PartialView("~/Views/Code/BuilderPageList.cshtml");
        }

        /// <summary>
        /// 生成对象编辑页
        /// </summary>
        public ActionResult BuilderPageEdit(E_PageParameter ePageParameter)
        {
            return PartialView("~/Views/Code/BuilderPageEdit.cshtml");
        }
    }
}