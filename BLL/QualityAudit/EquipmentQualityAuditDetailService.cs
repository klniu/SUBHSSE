﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特种设备资质审查明细
    /// </summary>
    public static class EquipmentQualityAuditDetailService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取特种设备资质审查明细
        /// </summary>
        /// <param name="EquipmentQualityAuditDetailId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_EquipmentQualityAuditDetail GetEquipmentQualityAuditDetailById(string auditDetailId)
        {
            return Funs.DB.QualityAudit_EquipmentQualityAuditDetail.FirstOrDefault(e => e.AuditDetailId == auditDetailId);
        }

        /// <summary>
        /// 获取时间段的审查明细集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.QualityAudit_EquipmentQualityAuditDetail> GetListByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in Funs.DB.QualityAudit_EquipmentQualityAuditDetail where x.ProjectId == projectId && x.AuditDate >= startTime && x.AuditDate <= endTime orderby x.AuditDate select x).ToList();
        }

        /// <summary>
        /// 获取时间段的审查明细数量
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int GetCountByDate(string projectId, DateTime startTime, DateTime endTime)
        {
            var q = (from x in Funs.DB.QualityAudit_EquipmentQualityAuditDetail where x.ProjectId == projectId && x.AuditDate >= startTime && x.AuditDate <= endTime orderby x.AuditDate select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 添加特种设备资质审查明细
        /// </summary>
        /// <param name="auditDetail"></param>
        public static void AddEquipmentQualityAuditDetail(Model.QualityAudit_EquipmentQualityAuditDetail auditDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.QualityAudit_EquipmentQualityAuditDetail newEquipmentQualityAuditDetail = new Model.QualityAudit_EquipmentQualityAuditDetail
            {
                AuditDetailId = auditDetail.AuditDetailId,
                ProjectId = auditDetail.ProjectId,
                EquipmentQualityId = auditDetail.EquipmentQualityId,
                AuditContent = auditDetail.AuditContent,
                AuditMan = auditDetail.AuditMan,
                AuditDate = auditDetail.AuditDate,
                AuditResult = auditDetail.AuditResult
            };
            db.QualityAudit_EquipmentQualityAuditDetail.InsertOnSubmit(newEquipmentQualityAuditDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改特种设备资质审查明细
        /// </summary>
        /// <param name="auditDetail"></param>
        public static void UpdateEquipmentQualityAuditDetail(Model.QualityAudit_EquipmentQualityAuditDetail auditDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.QualityAudit_EquipmentQualityAuditDetail newEquipmentQualityAuditDetail = db.QualityAudit_EquipmentQualityAuditDetail.FirstOrDefault(e => e.AuditDetailId == auditDetail.AuditDetailId);
            if (newEquipmentQualityAuditDetail != null)
            {
                newEquipmentQualityAuditDetail.AuditContent = auditDetail.AuditContent;
                newEquipmentQualityAuditDetail.AuditMan = auditDetail.AuditMan;
                newEquipmentQualityAuditDetail.AuditDate = auditDetail.AuditDate;
                newEquipmentQualityAuditDetail.AuditResult = auditDetail.AuditResult;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除特种设备资质审查明细
        /// </summary>
        /// <param name="auditDetailId"></param>
        public static void DeleteEquipmentQualityAuditDetailById(string auditDetailId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.QualityAudit_EquipmentQualityAuditDetail auditDetail = db.QualityAudit_EquipmentQualityAuditDetail.FirstOrDefault(e => e.AuditDetailId == auditDetailId);
            if (auditDetail != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(auditDetail.AuditDetailId);
                db.QualityAudit_EquipmentQualityAuditDetail.DeleteOnSubmit(auditDetail);
                db.SubmitChanges();
            }
        }
    }
}
