﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class Hazard_EnvironmentalRiskListService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 要求主键获取危险清单信息
        /// </summary>
        /// <param name="environmentalRiskListId">主键</param>
        /// <returns></returns>
        public static Model.Hazard_EnvironmentalRiskList GetEnvironmentalRiskList(string environmentalRiskListId)
        {
            return Funs.DB.Hazard_EnvironmentalRiskList.FirstOrDefault(e => e.EnvironmentalRiskListId == environmentalRiskListId);
        }

        /// <summary>
        /// 根据项目主键和开始、结束时间获得其他危险源辨识的数量
        /// </summary>
        /// <param name="projectId">项目主键</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static int GetEnvironmentalRiskCountByProjectIdAndDate(string projectId, DateTime startTime, DateTime endTime)
        {
            var q = (from x in Funs.DB.Hazard_EnvironmentalRiskList where x.ProjectId == projectId && x.CompileDate >= startTime && x.CompileDate <= endTime select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 增加危险源辨识与评价清单信息
        /// </summary>
        /// <param name="environmentalRiskList">危险源辨识与评价清单实体</param>
        public static void AddEnvironmentalRiskList(Model.Hazard_EnvironmentalRiskList environmentalRiskList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Hazard_EnvironmentalRiskList newEnvironmentalRiskList = new Model.Hazard_EnvironmentalRiskList
            {
                EnvironmentalRiskListId = environmentalRiskList.EnvironmentalRiskListId,
                ProjectId = environmentalRiskList.ProjectId,
                RiskCode = environmentalRiskList.RiskCode,
                CompileMan = environmentalRiskList.CompileMan,
                CompileDate = environmentalRiskList.CompileDate,
                AttachUrl = environmentalRiskList.AttachUrl,
                States = environmentalRiskList.States,
                Contents = environmentalRiskList.Contents,
                WorkAreaName = environmentalRiskList.WorkAreaName,
                IdentificationDate = environmentalRiskList.IdentificationDate,
                ControllingPerson = environmentalRiskList.ControllingPerson
            };
            Funs.DB.Hazard_EnvironmentalRiskList.InsertOnSubmit(newEnvironmentalRiskList);
            Funs.DB.SubmitChanges();

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectEnvironmentalRiskListMenuId, environmentalRiskList.ProjectId, null, environmentalRiskList.EnvironmentalRiskListId, environmentalRiskList.CompileDate);
        }

        /// <summary>
        /// 修改危险源辨识与评价清单信息
        /// </summary>
        /// <param name="environmentalRiskList">危险源辨识与评价清单实体</param>
        public static void UpdateEnvironmentalRiskList(Model.Hazard_EnvironmentalRiskList environmentalRiskList)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Hazard_EnvironmentalRiskList newEnvironmentalRiskList = db.Hazard_EnvironmentalRiskList.FirstOrDefault(e => e.EnvironmentalRiskListId == environmentalRiskList.EnvironmentalRiskListId);
            if (newEnvironmentalRiskList != null)
            {
                newEnvironmentalRiskList.RiskCode = environmentalRiskList.RiskCode;
                newEnvironmentalRiskList.CompileDate = environmentalRiskList.CompileDate;
                newEnvironmentalRiskList.CompileMan = environmentalRiskList.CompileMan;
                newEnvironmentalRiskList.AttachUrl = environmentalRiskList.AttachUrl;
                newEnvironmentalRiskList.States = environmentalRiskList.States;
                newEnvironmentalRiskList.Contents = environmentalRiskList.Contents;
                newEnvironmentalRiskList.WorkAreaName = environmentalRiskList.WorkAreaName;
                newEnvironmentalRiskList.IdentificationDate = environmentalRiskList.IdentificationDate;
                newEnvironmentalRiskList.ControllingPerson = environmentalRiskList.ControllingPerson;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据危险源辨识与评价清单Id删除一个危险源辨识与评价清单信息
        /// </summary>
        /// <param name="environmentalRiskListId">危险源辨识与评价清单Id</param>
        public static void DeleteEnvironmentalRiskListById(string environmentalRiskListId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Hazard_EnvironmentalRiskList newEnvironmentalRiskList = db.Hazard_EnvironmentalRiskList.FirstOrDefault(e => e.EnvironmentalRiskListId == environmentalRiskListId);
            if (newEnvironmentalRiskList != null)
            {
                ///删除工程师日志收集记录
                var flowOperate = from x in db.Sys_FlowOperate where x.DataId == newEnvironmentalRiskList.EnvironmentalRiskListId select x;
                if (flowOperate.Count() > 0)
                {
                    foreach (var item in flowOperate)
                    {
                        BLL.HSSELogService.CollectHSSELog(newEnvironmentalRiskList.ProjectId, item.OperaterId, item.OperaterTime, "25", "环境危险源辨识与评价", Const.BtnDelete,1);
                    }
                    ////删除审核流程表
                    BLL.CommonService.DeleteFlowOperateByID(newEnvironmentalRiskList.EnvironmentalRiskListId);
                }

                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(environmentalRiskListId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(environmentalRiskListId);
                db.Hazard_EnvironmentalRiskList.DeleteOnSubmit(newEnvironmentalRiskList);
                db.SubmitChanges();
            }
        }
    }
}
