﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 单位信息
    /// </summary>
    public static class UnitService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static Model.Base_Unit GetUnitByUnitId(string unitId)
        {
            return Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitId == unitId);
        }

        /// <summary>
        /// 获取单位信息是否存在
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static bool IsExitUnitByUnitName(string unitId, string unitName)
        {
            var unit = Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitId != unitId && x.UnitName == unitName);
            return (unit != null);
        }

        /// <summary>
        /// 获取单位信息是否存在
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static bool IsExitUnitByUnitCode(string unitId, string unitCode)
        {
            var unit = Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitId != unitId && x.UnitCode == unitCode);
            return (unit != null);
        }

        #region 单位信息维护
        /// <summary>
        /// 添加单位信息
        /// </summary>
        /// <param name="unit"></param>
        public static void AddUnit(Model.Base_Unit unit)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_Unit newUnit = new Model.Base_Unit
            {
                UnitId = unit.UnitId,
                UnitCode = unit.UnitCode,
                UnitName = unit.UnitName,
                UnitTypeId = unit.UnitTypeId,
                Corporate = unit.Corporate,
                Address = unit.Address,
                Telephone = unit.Telephone,
                Fax = unit.Fax,
                EMail = unit.EMail,
                ProjectRange = unit.ProjectRange,
                IsThisUnit = unit.IsThisUnit,
                IsBranch = unit.IsBranch,
                IsHide = false,
                DataSources = unit.DataSources,
                FromUnitId = unit.FromUnitId,
            };
            db.Base_Unit.InsertOnSubmit(newUnit);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改单位信息
        /// </summary>
        /// <param name="unit"></param>
        public static void UpdateUnit(Model.Base_Unit unit)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_Unit newUnit = db.Base_Unit.FirstOrDefault(e => e.UnitId == unit.UnitId);
            if (newUnit != null)
            {
                newUnit.UnitCode = unit.UnitCode;
                newUnit.UnitName = unit.UnitName;
                newUnit.UnitTypeId = unit.UnitTypeId;
                newUnit.Corporate = unit.Corporate;
                newUnit.Address = unit.Address;
                newUnit.Telephone = unit.Telephone;
                newUnit.Fax = unit.Fax;
                newUnit.EMail = unit.EMail;
                newUnit.ProjectRange = unit.ProjectRange;
                newUnit.IsThisUnit = unit.IsThisUnit;
                newUnit.IsBranch = unit.IsBranch;
                newUnit.IsHide = unit.IsHide;
                newUnit.FromUnitId = unit.FromUnitId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据单位ID删除单位信息
        /// </summary>
        /// <param name="unitId"></param>
        public static void DeleteUnitById(string unitId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_Unit newUnit = db.Base_Unit.FirstOrDefault(e => e.UnitId == unitId);
            if (newUnit != null)
            {
                newUnit.IsHide = true;
                UpdateUnit(newUnit);
            }
        }
        #endregion

        /// <summary>
        /// 获取单位下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetUnitDropDownList()
        {
            var list = (from x in Funs.DB.Base_Unit where (x.IsHide == null || x.IsHide == false) select x).OrderByDescending(x => x.IsThisUnit).ThenBy(x => x.UnitCode).ToList();
            return list;
        }

        /// <summary>
        /// 获取本单位下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetThisUnitDropDownList()
        {
            var list = (from x in Funs.DB.Base_Unit where x.IsThisUnit == true select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取分公司列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetBranchUnitList()
        {
            var list = (from x in Funs.DB.Base_Unit
                        where x.IsBranch == true && (x.IsHide == null || x.IsHide == false)
                        select x).OrderBy(x => x.UnitCode).ToList();
            return list;
        }

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetUnitListByProjectId(string projectId)
        {
            var units = (from x in Funs.DB.Base_Unit
                         where (x.IsHide == null || x.IsHide == false)
                         orderby x.UnitCode
                         select x).ToList();
            if (!string.IsNullOrEmpty(projectId))
            {
                units = (from x in units
                         join y in Funs.DB.Project_ProjectUnit on x.UnitId equals y.UnitId
                         where y.ProjectId == projectId
                         select x).ToList();
            }

            units = units.OrderByDescending(x => x.IsThisUnit).ThenBy(x => x.UnitCode).ToList();

            return units;
        }

        /// <summary>
        /// 根据项目Id获取单位名称下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetUnitByProjectIdList(string projectId)
        {
            var q = (from x in db.Base_Unit
                     join y in db.Project_ProjectUnit
                     on x.UnitId equals y.UnitId
                     where y.ProjectId == projectId && (x.IsHide == null || x.IsHide == false)
                     orderby x.UnitCode
                     select x).ToList();
            return q;
        }

        /// <summary>
        /// 根据项目Id获取单位名称下拉选择项
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetUnitByProjectIdUnitTypeList(string projectId, string unitType)
        {
            var q = (from x in db.Base_Unit
                     join y in db.Project_ProjectUnit
                     on x.UnitId equals y.UnitId
                     where y.ProjectId == projectId && (x.IsHide == null || x.IsHide == false) && y.UnitType == unitType
                     orderby x.UnitCode
                     select x).ToList();
            return q;
        }

        /// <summary>
        /// 根据项目Id获取单位名称下拉选择项（不包含某个单位）
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetUnitByProjectIdListNotContainOneUnit(string projectId, string unitId)
        {
            var q = (from x in db.Base_Unit
                     join y in db.Project_ProjectUnit
                     on x.UnitId equals y.UnitId
                     where y.ProjectId == projectId && (x.UnitId != unitId || unitId == null) && (x.IsHide == null || x.IsHide == false)
                     orderby x.UnitCode
                     select x).ToList();
            return q;
        }

        /// <summary>
        /// 获取单位名称
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static string GetUnitNameByUnitId(string unitId)
        {
            string name = string.Empty;
            var unit = Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitId == unitId);
            if (unit != null)
            {
                name = unit.UnitName;
            }
            return name;
        }

        #region 单位表下拉框
        /// <summary>
        ///  单位表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUnitDropDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "UnitId";
            dropName.DataTextField = "UnitName";
            dropName.DataSource = BLL.UnitService.GetUnitListByProjectId(projectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 获取分公司表下拉框
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="isShowPlease"></param>
        public static void InitBranchUnitDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "UnitId";
            dropName.DataTextField = "UnitName";
            dropName.DataSource = BLL.UnitService.GetBranchUnitList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  单位表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUnitNoUnitIdDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitId, bool isShowPlease)
        {
            dropName.DataValueField = "UnitId";
            dropName.DataTextField = "UnitName";
            dropName.DataSource = BLL.UnitService.GetUnitByProjectIdListNotContainOneUnit(projectId, unitId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  根据单位类型获取单位表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUnitByProjectIdUnitTypeDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitType, bool isShowPlease)
        {
            dropName.DataValueField = "UnitId";
            dropName.DataTextField = "UnitName";
            dropName.DataSource = BLL.UnitService.GetUnitByProjectIdUnitTypeList(projectId, unitType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        /// <summary>
        /// 根据项目Id获取单位名称下拉选择项(总包和施工分包方)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetMainAndSubUnitByProjectIdList(string projectId)
        {
            var q = (from x in db.Base_Unit
                     join y in db.Project_ProjectUnit
                     on x.UnitId equals y.UnitId
                     where y.ProjectId == projectId && (x.IsHide == null || x.IsHide == false)
                     && (y.UnitType == BLL.Const.ProjectUnitType_1 || y.UnitType == BLL.Const.ProjectUnitType_2) && (y.OutTime == null || y.OutTime >= Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01"))                     
                     select x).OrderByDescending(x=>x.IsThisUnit).ThenBy(x=>x.UnitCode).ToList();
            return q;
        }
        /// <summary>
        /// 单位下拉选择项（添加其他单位）
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="projectId"></param>
        /// <param name="isShowPlease"></param>
        public static void InitUnitOtherDropDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "UnitId";
            dropName.DataTextField = "UnitName";

            List<Model.Base_Unit> units = new List<Model.Base_Unit>();
            units.AddRange(BLL.UnitService.GetUnitListByProjectId(projectId));

            Model.Base_Unit other = new Model.Base_Unit();
            other.UnitName = "其他";
            other.UnitId = "0";
            units.Add(other);

            dropName.DataSource = units;
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
    }
}
