﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    public static class APIRectifyNoticesService
    {
        #region 根据RectifyNoticesId获取风险巡检信息详细信息
        /// <summary>
        /// 根据RectifyNoticesId获取风险巡检信息详细信息
        /// </summary>
        /// <param name="rectifyNoticesId"></param>
        /// <returns></returns>
        public static Model.RectifyNoticesItem getRectifyNoticesById(string rectifyNoticesId)
        {
            var getRectifyNotices = from x in Funs.DB.Check_RectifyNotices
                                    where x.RectifyNoticesId == rectifyNoticesId
                                    select new Model.RectifyNoticesItem
                                    {
                                        RectifyNoticesId = x.RectifyNoticesId,
                                        ProjectId = x.ProjectId,
                                        ProjectName = Funs.DB.Base_Project.First(z => z.ProjectId == x.ProjectId).ProjectName,
                                        RectifyNoticesCode = x.RectifyNoticesCode,
                                        UnitId = x.UnitId,
                                        UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                        WorkAreaId = x.WorkAreaId,
                                        WorkAreaName = WorkAreaService.getWorkAreaNamesIds(x.WorkAreaId),
                                        CheckManNames = x.CheckManNames,
                                        CheckedDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CheckedDate),
                                        CheckedDateD = x.CheckedDate,
                                        HiddenHazardType = x.HiddenHazardType,
                                        HiddenHazardTypeName = x.HiddenHazardType == "2" ? "较大" : (x.HiddenHazardType == "3" ? "重大" : "一般"),
                                        CompleteManId = x.CompleteManId,
                                        CompleteManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompleteManId).UserName,
                                        SignPersonId = x.SignPerson,
                                        SignPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.SignPerson).UserName,
                                        SignDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.SignDate),
                                        ProfessionalEngineerId = x.ProfessionalEngineerId,
                                        ProfessionalEngineerName = Funs.DB.Sys_User.First(u => u.UserId == x.ProfessionalEngineerId).UserName,
                                        ProfessionalEngineerTime1 = string.Format("{0:yyyy-MM-dd HH:mm}", x.ProfessionalEngineerTime1),
                                        ProfessionalEngineerTime2 = string.Format("{0:yyyy-MM-dd HH:mm}", x.ProfessionalEngineerTime2),
                                        ConstructionManagerId = x.ConstructionManagerId,
                                        ConstructionManagerName = Funs.DB.Sys_User.First(u => u.UserId == x.ConstructionManagerId).UserName,
                                        ConstructionManagerTime1 = string.Format("{0:yyyy-MM-dd HH:mm}", x.ConstructionManagerTime1),
                                        ConstructionManagerTime2 = string.Format("{0:yyyy-MM-dd HH:mm}", x.ConstructionManagerTime2),
                                        ProjectManagerId = x.ProjectManagerId,
                                        ProjectManagerName = Funs.DB.Sys_User.First(u => u.UserId == x.ProjectManagerId).UserName,
                                        ProjectManagerTime1 = string.Format("{0:yyyy-MM-dd HH:mm}", x.ProjectManagerTime1),
                                        ProjectManagerTime2 = string.Format("{0:yyyy-MM-dd HH:mm}", x.ProjectManagerTime2),
                                        DutyPersonId = x.DutyPersonId,
                                        DutyPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.DutyPersonId).UserName,
                                        DutyPersonTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.DutyPersonTime),
                                        CompleteDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompleteDate),
                                        UnitHeadManId = x.UnitHeadManId,
                                        UnitHeadManName = Funs.DB.Sys_User.First(u => u.UserId == x.UnitHeadManId).UserName,
                                        UnitHeadManDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.UnitHeadManDate),
                                        CheckPersonId = x.CheckPerson,
                                        CheckPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.CheckPerson).UserName,
                                        ReCheckDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReCheckDate),
                                        //WrongContent =x.WrongContent,                                       
                                        //CompleteStatus=x.CompleteStatus,                                       
                                        //RectificationName=x.DutyPerson,                                       
                                        IsRectify = x.IsRectify ?? false,
                                        States = x.States,
                                        AttachUrl = APIUpLoadFileService.getFileUrl(x.RectifyNoticesId, null),
                                        BeAttachUrl = APIUpLoadFileService.getFileUrl(x.RectifyNoticesId + "#0", null),
                                        AfAttachUrl = APIUpLoadFileService.getFileUrl(x.RectifyNoticesId + "#1", null),
                                        RectifyNoticesItemItem = getRectifyNoticesItemItem(x.RectifyNoticesId),
                                        RectifyNoticesFlowOperateItem= getRectifyNoticesFlowOperateItem(x.RectifyNoticesId),
                                    };

            return getRectifyNotices.FirstOrDefault();
        }
        #endregion

        #region 根据隐患整改单ID 获取整改单明细信息
        /// <summary>
        ///  根据隐患整改单ID 获取整改单明细信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.RectifyNoticesItemItem> getRectifyNoticesItemItem(string rectifyNoticesId)
        {
            var getInfo = from x in Funs.DB.Check_RectifyNoticesItem
                          where x.RectifyNoticesId == rectifyNoticesId
                          orderby x.LimitTime descending
                          select new Model.RectifyNoticesItemItem
                          {
                              RectifyNoticesItemId = x.RectifyNoticesItemId,
                              RectifyNoticesId = x.RectifyNoticesId,
                              WrongContent = x.WrongContent,
                              Requirement = x.Requirement,
                              LimitTime = string.Format("{0:yyyy-MM-dd}", x.LimitTime),
                              RectifyResults = x.RectifyResults,
                              IsRectify = x.IsRectify ?? false,
                              PhotoBeforeUrl = APIUpLoadFileService.getFileUrl(x.RectifyNoticesItemId + "#1", null),
                              PhotoAfterUrl = APIUpLoadFileService.getFileUrl(x.RectifyNoticesItemId + "#2", null),
                          };
            return getInfo.ToList();
        }
        #endregion

        #region 根据隐患整改单ID 获取整改单审核信息
        /// <summary>
        ///  根据隐患整改单ID 获取整改单明细信息
        /// </summary>
        /// <returns></returns>
        public static List<Model.RectifyNoticesFlowOperateItem> getRectifyNoticesFlowOperateItem(string rectifyNoticesId)
        {
            var getInfo = from x in Funs.DB.Check_RectifyNoticesFlowOperate
                          where x.RectifyNoticesId == rectifyNoticesId
                          orderby x.OperateTime descending
                          select new Model.RectifyNoticesFlowOperateItem
                          {
                              FlowOperateId = x.FlowOperateId,
                              RectifyNoticesId = x.RectifyNoticesId,
                              OperateName = x.OperateName,
                              OperateManId = x.OperateManId,
                              OperateManName = Funs.DB.Sys_User.First(z => z.UserId == x.OperateManId).UserName,
                              OperateTime = string.Format("{0:yyyy-MM-dd}", x.OperateTime),
                              IsAgree = x.IsAgree,
                              Opinion = x.Opinion,
                              SignatureUrl = APIUpLoadFileService.getFileUrl(string.Empty, x.SignatureUrl),
                          };
            return getInfo.ToList();
        }
        #endregion

        #region 根据projectId、states获取风险信息（状态 0待提交；1待签发；2待整改；3待审核；4待复查；5已完成）
        /// <summary>
        /// 根据projectId、states获取风险信息（状态 0待提交；1待签发；2待整改；3待审核；4待复查；5已完成）
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public static List<Model.RectifyNoticesItem> getRectifyNoticesByProjectIdStates(string projectId, string states)
        {
            var getDataLists = (from x in Funs.DB.Check_RectifyNotices
                                where x.ProjectId == projectId && x.States == states
                                orderby x.CheckedDate descending
                                select new Model.RectifyNoticesItem
                                {
                                    RectifyNoticesId = x.RectifyNoticesId,
                                    ProjectId = x.ProjectId,
                                    ProjectName = Funs.DB.Base_Project.First(z => z.ProjectId == x.ProjectId).ProjectName,
                                    RectifyNoticesCode = x.RectifyNoticesCode,
                                    UnitId = x.UnitId,
                                    UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                    WorkAreaId = x.WorkAreaId,
                                    WorkAreaName = WorkAreaService.getWorkAreaNamesIds(x.WorkAreaId),
                                    CheckManNames = x.CheckManNames,
                                    CheckedDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CheckedDate),
                                    CheckedDateD = x.CheckedDate,
                                    HiddenHazardType = x.HiddenHazardType,
                                    HiddenHazardTypeName = x.HiddenHazardType == "2" ? "较大" : (x.HiddenHazardType == "3" ? "重大" : "一般"),
                                    CompleteManId = x.CompleteManId,
                                    CompleteManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompleteManId).UserName,
                                    SignPersonId = x.SignPerson,
                                    SignPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.SignPerson).UserName,
                                    SignDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.SignDate),

                                    DutyPersonId = x.DutyPersonId,
                                    DutyPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.DutyPersonId).UserName,
                                    DutyPersonTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.DutyPersonTime),

                                    CompleteDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompleteDate),
                                    UnitHeadManId = x.UnitHeadManId,
                                    UnitHeadManName = Funs.DB.Sys_User.First(u => u.UserId == x.UnitHeadManId).UserName,
                                    UnitHeadManDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.UnitHeadManDate),

                                    CheckPersonId = x.CheckPerson,
                                    CheckPersonName = Funs.DB.Sys_User.First(u => u.UserId == x.CheckPerson).UserName,
                                    ReCheckDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ReCheckDate),
                                    ReCheckOpinion = x.ReCheckOpinion,
                                    IsRectify = x.IsRectify ?? false,
                                    States = x.States,
                                    AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.RectifyNoticesId)).AttachUrl.Replace('\\', '/'),
                                }).ToList();
            return getDataLists;
        }
        #endregion

        #region 状态 -old
        /// <summary>
        /// 状态 -old
        /// </summary>
        /// <param name="SignDate"></param>
        /// <param name="CompleteDate"></param>
        /// <param name="ReCheckDate"></param>
        /// <returns></returns>
        public static string getStates(DateTime? SignDate, DateTime? CompleteDate, DateTime? ReCheckDate)
        {
            string states = string.Empty;
            if (!SignDate.HasValue)  // 待签发 0
            {
                states = "0";
            }
            else if (SignDate.HasValue && !CompleteDate.HasValue)   // 待整改 1
            {
                states = "1";
            }
            else if (CompleteDate.HasValue && !ReCheckDate.HasValue) // 待复查 2
            {
                states = "2";
            }
            else if (ReCheckDate.HasValue)  // 已闭环 3
            {
                states = "3";
            }
            return states;
        }
        #endregion

        #region 保存RectifyNotices
        /// <summary>
        /// 保存RectifyNotices
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static void SaveRectifyNotices(Model.RectifyNoticesItem rectifyNotices)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                Model.Check_RectifyNotices newRectifyNotices = new Model.Check_RectifyNotices
                {
                    RectifyNoticesId = rectifyNotices.RectifyNoticesId,
                    ProjectId = rectifyNotices.ProjectId,
                    RectifyNoticesCode = rectifyNotices.RectifyNoticesCode,
                    UnitId = rectifyNotices.UnitId,
                    WorkAreaId = rectifyNotices.WorkAreaId,
                    CheckManNames = rectifyNotices.CheckManNames,
                    CheckedDate = Funs.GetNewDateTime(rectifyNotices.CheckedDate),
                    HiddenHazardType = rectifyNotices.HiddenHazardType,
                    CompleteManId = rectifyNotices.CompleteManId,
                    States = rectifyNotices.States,
                };

                //// 新增整改单
                var isUpdate = db.Check_RectifyNotices.FirstOrDefault(x => x.RectifyNoticesId == newRectifyNotices.RectifyNoticesId);
                if (isUpdate == null)
                {
                    newRectifyNotices.RectifyNoticesId = SQLHelper.GetNewID();
                    newRectifyNotices.RectifyNoticesCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectRectifyNoticeMenuId, newRectifyNotices.ProjectId, newRectifyNotices.UnitId);
                    db.Check_RectifyNotices.InsertOnSubmit(newRectifyNotices);
                    db.SubmitChanges();
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectRectifyNoticeMenuId, rectifyNotices.ProjectId, rectifyNotices.UnitId, rectifyNotices.RectifyNoticesId, newRectifyNotices.CheckedDate);
                    //// 整改单附件
                    if (!string.IsNullOrEmpty(rectifyNotices.BeAttachUrl))
                    {
                        APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, newRectifyNotices.RectifyNoticesId + "#0", rectifyNotices.BeAttachUrl, "0");
                    }
                    //// 反馈单附件
                    if (!string.IsNullOrEmpty(rectifyNotices.AfAttachUrl))
                    {
                        APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, newRectifyNotices.RectifyNoticesId + "#1", rectifyNotices.AfAttachUrl, "0");
                    }
                    //// 整个单据附件
                    if (!string.IsNullOrEmpty(rectifyNotices.AttachUrl))
                    {
                        APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, newRectifyNotices.RectifyNoticesId, rectifyNotices.AttachUrl, "0");
                    }
                    //// 新增明细
                    if (rectifyNotices.RectifyNoticesItemItem != null && rectifyNotices.RectifyNoticesItemItem.Count() > 0)
                    {
                        foreach (var rItem in rectifyNotices.RectifyNoticesItemItem)
                        {
                            Model.Check_RectifyNoticesItem newItem = new Model.Check_RectifyNoticesItem
                            {
                                RectifyNoticesItemId = SQLHelper.GetNewID(),
                                RectifyNoticesId = newRectifyNotices.RectifyNoticesId,
                                WrongContent = rItem.WrongContent,
                                Requirement = rItem.Requirement,
                                LimitTime = Funs.GetNewDateTime(rItem.LimitTime),
                                RectifyResults = null,
                                IsRectify = null,
                            };
                            db.Check_RectifyNoticesItem.InsertOnSubmit(newItem);
                            db.SubmitChanges();

                            if (!string.IsNullOrEmpty(rItem.PhotoBeforeUrl))
                            {
                                APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, newItem.RectifyNoticesItemId + "#0", rItem.PhotoBeforeUrl, "0");
                            }
                        }
                    }
                    //// 回写巡检记录表
                    if (!string.IsNullOrEmpty(rectifyNotices.HazardRegisterId))
                    {
                        List<string> listIds = Funs.GetStrListByStr(rectifyNotices.HazardRegisterId, ',');
                        foreach (var item in listIds)
                        {
                            var getHazardRegister = db.HSSE_Hazard_HazardRegister.FirstOrDefault(x => x.HazardRegisterId == item);
                            if (getHazardRegister != null)
                            {
                                getHazardRegister.States = "3";
                                getHazardRegister.HandleIdea += "已升级为隐患整改单：" + newRectifyNotices.RectifyNoticesCode;
                                getHazardRegister.ResultId = newRectifyNotices.RectifyNoticesId;
                                getHazardRegister.ResultType = "1";
                                db.SubmitChanges();
                            }
                        }
                    }
                }
                else
                {
                    newRectifyNotices.RectifyNoticesId = isUpdate.RectifyNoticesId;
                    isUpdate.States = rectifyNotices.States;
                    if (newRectifyNotices.States == "0")  ////编制人 修改或提交
                    {
                        isUpdate.UnitId = rectifyNotices.UnitId;
                        isUpdate.WorkAreaId = rectifyNotices.WorkAreaId;
                        isUpdate.CheckManNames = rectifyNotices.CheckManNames;
                        isUpdate.CheckedDate = Funs.GetNewDateTime(rectifyNotices.CheckedDate);
                        isUpdate.HiddenHazardType = rectifyNotices.HiddenHazardType;                    
                        if (newRectifyNotices.States == "1")
                        {
                            isUpdate.SignPerson = rectifyNotices.SignPersonId;
                        }
                        db.SubmitChanges();
                        //// 删除明细表
                        var deleteItem = from x in db.Check_RectifyNoticesItem where x.RectifyNoticesId == isUpdate.RectifyNoticesId select x;
                        if (deleteItem.Count() > 0)
                        {
                            foreach (var cdeleteItem in deleteItem)
                            {
                                CommonService.DeleteAttachFileById(cdeleteItem.RectifyNoticesItemId);
                            }
                            db.Check_RectifyNoticesItem.DeleteAllOnSubmit(deleteItem);
                        }
                        //// 新增明细
                        if (rectifyNotices.RectifyNoticesItemItem != null && rectifyNotices.RectifyNoticesItemItem.Count() > 0)
                        {
                            foreach (var rItem in rectifyNotices.RectifyNoticesItemItem)
                            {
                                Model.Check_RectifyNoticesItem newItem = new Model.Check_RectifyNoticesItem
                                {
                                    RectifyNoticesItemId = SQLHelper.GetNewID(),
                                    RectifyNoticesId = newRectifyNotices.RectifyNoticesId,
                                    WrongContent = rItem.WrongContent,
                                    Requirement = rItem.Requirement,
                                    LimitTime = Funs.GetNewDateTime(rItem.LimitTime),
                                    RectifyResults = null,
                                    IsRectify = null,
                                };
                                db.Check_RectifyNoticesItem.InsertOnSubmit(newItem);
                                db.SubmitChanges();

                                if (!string.IsNullOrEmpty(rItem.PhotoBeforeUrl))
                                {
                                    APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, newItem.RectifyNoticesItemId + "#0", rItem.PhotoBeforeUrl, "0");
                                }
                            }
                        }
                    }
                    else if (newRectifyNotices.States == "2") ////总包单位项目安全经理 审核
                    {
                        /// 不同意 打回 同意抄送专业工程师、施工经理、项目经理 并下发分包接收人（也就是施工单位项目安全经理）
                        if (rectifyNotices.IsAgree == false) 
                        {
                            isUpdate.States = "0";
                        }
                        else
                        {
                            isUpdate.SignDate = DateTime.Now;
                            isUpdate.ProfessionalEngineerId = rectifyNotices.ProfessionalEngineerId;
                            isUpdate.ConstructionManagerId = rectifyNotices.ConstructionManagerId;
                            isUpdate.ProjectManagerId = rectifyNotices.ProjectManagerId;
                            isUpdate.DutyPersonId = rectifyNotices.DutyPersonId;
                        }
                        db.SubmitChanges();                       
                    }
                    else if (newRectifyNotices.States == "3") /// 施工单位项目安全经理 整改 提交施工单位项目负责人
                    {
                        //// 整改明细反馈
                        if (rectifyNotices.RectifyNoticesItemItem != null && rectifyNotices.RectifyNoticesItemItem.Count() > 0)
                        {
                            foreach (var rItem in rectifyNotices.RectifyNoticesItemItem)
                            {
                                var getUpdateItem = db.Check_RectifyNoticesItem.FirstOrDefault(x => x.RectifyNoticesItemId == rItem.RectifyNoticesItemId);
                                if (getUpdateItem != null)
                                {
                                    getUpdateItem.RectifyResults = rItem.RectifyResults;
                                    if (getUpdateItem.IsRectify != true)
                                    {
                                        getUpdateItem.IsRectify = null;
                                    }
                                    db.SubmitChanges();
                                }
                                if (!string.IsNullOrEmpty(rItem.PhotoAfterUrl))
                                {
                                    APIUpLoadFileService.SaveAttachUrl(Const.ProjectRectifyNoticeMenuId, rItem.RectifyNoticesItemId + "#0", rItem.PhotoAfterUrl, "0");
                                }
                            }
                        }
                      
                        isUpdate.CompleteDate = DateTime.Now;
                        isUpdate.UnitHeadManId = rectifyNotices.UnitHeadManId;
                        db.SubmitChanges();
                    }
                    else if (newRectifyNotices.States == "4")
                    { /// 施工单位项目负责人不同意 打回施工单位项目安全经理,同意提交安全经理/安全工程师复查
                        if (rectifyNotices.IsAgree == false)
                        {
                            isUpdate.States = "2";
                            isUpdate.CompleteDate = null;
                        }
                        else
                        {
                            isUpdate.UnitHeadManDate = DateTime.Now;
                            isUpdate.CheckPerson = rectifyNotices.CheckPersonId;
                        }
                        db.SubmitChanges();
                    }
                    else if (newRectifyNotices.States == "5")
                    {  ////安全经理/安全工程师 同意关闭，不同意打回施工单位项目安全经理
                        isUpdate.ReCheckOpinion = rectifyNotices.ReCheckOpinion;
                        if (rectifyNotices.IsAgree == false)
                        {
                            isUpdate.States = "2";
                            isUpdate.UnitHeadManDate = null;
                            isUpdate.CompleteDate = null;
                            isUpdate.ProfessionalEngineerTime2 = null;
                            isUpdate.ConstructionManagerTime2 = null;
                            isUpdate.ProjectManagerTime2 = null;
                        }
                        else
                        {                         
                            isUpdate.ReCheckDate = DateTime.Now;
                        }
                        db.SubmitChanges();
                        //// 整改明细反馈 复查 是否合格
                        if (rectifyNotices.RectifyNoticesItemItem != null && rectifyNotices.RectifyNoticesItemItem.Count() > 0)
                        {
                            foreach (var rItem in rectifyNotices.RectifyNoticesItemItem)
                            {
                                var getUpdateItem = db.Check_RectifyNoticesItem.FirstOrDefault(x => x.RectifyNoticesItemId == rItem.RectifyNoticesItemId);
                                if (getUpdateItem != null)
                                {
                                    getUpdateItem.IsRectify = rItem.IsRectify;
                                    db.SubmitChanges();
                                }
                            }
                        }
                    }
                }
                //// 增加审核记录
                if (rectifyNotices.RectifyNoticesFlowOperateItem != null && rectifyNotices.RectifyNoticesFlowOperateItem.Count() > 0)
                {
                    var getOperate = rectifyNotices.RectifyNoticesFlowOperateItem.FirstOrDefault();
                    if (getOperate != null)
                    {
                        Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                        {
                            FlowOperateId = SQLHelper.GetNewID(),
                            RectifyNoticesId = newRectifyNotices.RectifyNoticesId,
                            OperateName = getOperate.OperateName,
                            OperateManId = getOperate.OperateManId,
                            OperateTime = DateTime.Now,
                            IsAgree = getOperate.IsAgree,
                            Opinion = getOperate.Opinion,
                        };
                        db.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                        db.SubmitChanges();
                    }
                }
            }
        }
        #endregion
    }
}
