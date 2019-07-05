﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 管理规定分类
    /// </summary>
    public static class ManageRuleTypeService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取管理规定分类
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_ManageRuleType GetManageRuleTypeById(string manageRuleTypeId)
        {
            return Funs.DB.Base_ManageRuleType.FirstOrDefault(e => e.ManageRuleTypeId == manageRuleTypeId);
        }
        
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddManageRuleType(Model.Base_ManageRuleType manageRuleType)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_ManageRuleType newManageRuleType = new Model.Base_ManageRuleType
            {
                ManageRuleTypeId = manageRuleType.ManageRuleTypeId,
                ManageRuleTypeCode = manageRuleType.ManageRuleTypeCode,
                ManageRuleTypeName = manageRuleType.ManageRuleTypeName,
                Remark = manageRuleType.Remark
            };

            db.Base_ManageRuleType.InsertOnSubmit(newManageRuleType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateManageRuleType(Model.Base_ManageRuleType manageRuleType)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_ManageRuleType newManageRuleType = db.Base_ManageRuleType.FirstOrDefault(e => e.ManageRuleTypeId == manageRuleType.ManageRuleTypeId);
            if (newManageRuleType != null)
            {
                newManageRuleType.ManageRuleTypeCode = manageRuleType.ManageRuleTypeCode;
                newManageRuleType.ManageRuleTypeName = manageRuleType.ManageRuleTypeName;
                newManageRuleType.Remark = manageRuleType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除管理规定类别信息
        /// </summary>
        /// <param name="manageRuleTypeId"></param>
        public static void DeleteManageRuleTypeById(string manageRuleTypeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_ManageRuleType manageRuleType = db.Base_ManageRuleType.FirstOrDefault(e => e.ManageRuleTypeId == manageRuleTypeId);
            {
                db.Base_ManageRuleType.DeleteOnSubmit(manageRuleType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取管理规定类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_ManageRuleType> GetManageRuleTypeList()
        {
            var list = (from x in Funs.DB.Base_ManageRuleType orderby x.ManageRuleTypeCode select x).ToList();           
            return list;
        }
    }
}