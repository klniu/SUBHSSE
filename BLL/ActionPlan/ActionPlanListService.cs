﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 实施计划
    /// </summary>
    public static class ActionPlanListService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取实施计划
        /// </summary>
        /// <param name="actionPlanListId"></param>
        /// <returns></returns>
        public static Model.ActionPlan_ActionPlanList GetActionPlanListById(string actionPlanListId)
        {
            return Funs.DB.ActionPlan_ActionPlanList.FirstOrDefault(e => e.ActionPlanListId == actionPlanListId);
        }

        /// <summary>
        /// 根据日期获取实施计划集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>实施计划集合</returns>
        public static List<Model.ActionPlan_ActionPlanList> GetActionPlanListsByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.ActionPlan_ActionPlanList where x.CompileDate >= startTime && x.CompileDate <= endTime && x.ProjectId == projectId orderby x.CompileDate select x).ToList();
        }

        /// <summary>
        /// 添加实施计划
        /// </summary>
        /// <param name="actionPlanList"></param>
        public static void AddActionPlanList(Model.ActionPlan_ActionPlanList actionPlanList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ActionPlan_ActionPlanList newActionPlanList = new Model.ActionPlan_ActionPlanList
            {
                ActionPlanListId = actionPlanList.ActionPlanListId,
                ProjectId = actionPlanList.ProjectId,
                ActionPlanListCode = actionPlanList.ActionPlanListCode,
                ActionPlanListName = actionPlanList.ActionPlanListName,
                VersionNo = actionPlanList.VersionNo,
                ProjectType = actionPlanList.ProjectType,
                ActionPlanListContents = actionPlanList.ActionPlanListContents,
                CompileMan = actionPlanList.CompileMan,
                CompileDate = actionPlanList.CompileDate,
                States = actionPlanList.States
            };
            db.ActionPlan_ActionPlanList.InsertOnSubmit(newActionPlanList);
            db.SubmitChanges();

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectActionPlanListMenuId, newActionPlanList.ProjectId, null, newActionPlanList.ActionPlanListId, newActionPlanList.CompileDate);
                        
        }

        /// <summary>
        /// 修改实施计划
        /// </summary>
        /// <param name="actionPlanList"></param>
        public static void UpdateActionPlanList(Model.ActionPlan_ActionPlanList actionPlanList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ActionPlan_ActionPlanList newActionPlanList = db.ActionPlan_ActionPlanList.FirstOrDefault(e => e.ActionPlanListId == actionPlanList.ActionPlanListId);
            if (newActionPlanList != null)
            {
                //newActionPlanList.ProjectId = actionPlanList.ProjectId;
                newActionPlanList.ActionPlanListCode = actionPlanList.ActionPlanListCode;
                newActionPlanList.ActionPlanListName = actionPlanList.ActionPlanListName;
                newActionPlanList.VersionNo = actionPlanList.VersionNo;
                newActionPlanList.ProjectType = actionPlanList.ProjectType;
                newActionPlanList.ActionPlanListContents = actionPlanList.ActionPlanListContents;
                newActionPlanList.CompileMan = actionPlanList.CompileMan;
                newActionPlanList.CompileDate = actionPlanList.CompileDate;
                newActionPlanList.States = actionPlanList.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除实施计划
        /// </summary>
        /// <param name="actionPlanListId"></param>
        public static void DeleteActionPlanListById(string actionPlanListId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ActionPlan_ActionPlanList actionPlanList = db.ActionPlan_ActionPlanList.FirstOrDefault(e => e.ActionPlanListId == actionPlanListId);
            if (actionPlanList != null)
            {
                ///删除工程师日志收集记录
                var  flowOperate = from x in db.Sys_FlowOperate where x.DataId ==   actionPlanList.ActionPlanListId select x;
                if (flowOperate.Count() > 0)
                {
                    foreach (var item in flowOperate)
                    {
                        BLL.HSSELogService.CollectHSSELog(actionPlanList.ProjectId, item.OperaterId, item.OperaterTime, "31", "施工HSE实施计划", Const.BtnDelete, 1);
                    }
                    ////删除审核流程表
                    BLL.CommonService.DeleteFlowOperateByID(actionPlanList.ActionPlanListId);
                }    
                ///删除附件
                BLL.CommonService.DeleteAttachFileById(actionPlanListId);
                ////删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(actionPlanList.ActionPlanListId);
                db.ActionPlan_ActionPlanList.DeleteOnSubmit(actionPlanList);
                db.SubmitChanges();
            }
        }
    }
}
