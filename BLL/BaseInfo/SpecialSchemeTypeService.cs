﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class SpecialSchemeTypeService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_SpecialSchemeType GetSpecialSchemeTypeById(string specialSchemeTypeId)
        {
            return Funs.DB.Base_SpecialSchemeType.FirstOrDefault(e => e.SpecialSchemeTypeId == specialSchemeTypeId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddSpecialSchemeType(Model.Base_SpecialSchemeType specialSchemeType)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_SpecialSchemeType newSpecialSchemeType = new Model.Base_SpecialSchemeType
            {
                SpecialSchemeTypeId = specialSchemeType.SpecialSchemeTypeId,
                SpecialSchemeTypeCode = specialSchemeType.SpecialSchemeTypeCode,
                SpecialSchemeTypeName = specialSchemeType.SpecialSchemeTypeName,
                Remark = specialSchemeType.Remark
            };

            db.Base_SpecialSchemeType.InsertOnSubmit(newSpecialSchemeType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateSpecialSchemeType(Model.Base_SpecialSchemeType specialSchemeType)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_SpecialSchemeType newSpecialSchemeType = db.Base_SpecialSchemeType.FirstOrDefault(e => e.SpecialSchemeTypeId == specialSchemeType.SpecialSchemeTypeId);
            if (newSpecialSchemeType != null)
            {
                newSpecialSchemeType.SpecialSchemeTypeCode = specialSchemeType.SpecialSchemeTypeCode;
                newSpecialSchemeType.SpecialSchemeTypeName = specialSchemeType.SpecialSchemeTypeName;
                newSpecialSchemeType.Remark = specialSchemeType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="specialSchemeTypeId"></param>
        public static void DeleteSpecialSchemeTypeById(string specialSchemeTypeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_SpecialSchemeType specialSchemeType = db.Base_SpecialSchemeType.FirstOrDefault(e => e.SpecialSchemeTypeId == specialSchemeTypeId);
            {
                db.Base_SpecialSchemeType.DeleteOnSubmit(specialSchemeType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_SpecialSchemeType> GetSpecialSchemeTypeList()
        {
            var list = (from x in Funs.DB.Base_SpecialSchemeType orderby x.SpecialSchemeTypeCode select x).ToList();
            return list;
        }

        public static Model.Base_SpecialSchemeType GetSpecialSchemeTypeByName(string name)
        {
                return Funs.DB.Base_SpecialSchemeType.FirstOrDefault(e => e.SpecialSchemeTypeName == name);
        }
    }
}