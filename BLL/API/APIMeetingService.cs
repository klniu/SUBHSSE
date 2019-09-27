﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    /// <summary>
    /// 会议服务类
    /// </summary>
    public static class APIMeetingService
    {
        #region 根据MeetingId获取会议详细信息
        /// <summary>
        /// 根据MeetingId获取会议详细信息
        /// </summary>
        /// <param name="meetingId">会议ID</param>
        /// <param name="meetingType">会议类型(C-班前会；W-周例会；M-例会；S-专题例会；A-其他会议)</param>
        /// <returns>会议详细</returns>
        public static Model.MeetingItem getMeetingByMeetingId(string meetingId,string meetingType)
        {
            Model.MeetingItem getMeetItem = new Model.MeetingItem();
            if (meetingType == "C")
            {
                getMeetItem = (from x in Funs.DB.Meeting_ClassMeeting
                               join y in Funs.DB.Sys_User on x.CompileMan equals y.UserId
                               where x.ClassMeetingId == meetingId
                               select new Model.MeetingItem
                               {
                                   MeetingId = x.ClassMeetingId,
                                   MeetingCode = x.ClassMeetingCode,
                                   MeetingName = x.ClassMeetingName,
                                   MeetingDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ClassMeetingDate),
                                   MeetingContents = x.ClassMeetingContents,
                                   MeetingPlace = x.MeetingPlace,
                                   MeetingType = meetingType,
                                   MeetingHours = x.MeetingHours ?? 0,
                                   MeetingHostMan = x.MeetingHostMan,
                                   AttentPerson = x.AttentPerson,
                                   CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                   CompileManId = x.CompileMan,
                                   CompileManName = y.UserName,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == y.UnitId).UnitName,
                                   AttachUrl=Funs.DB.AttachFile.First(z=>z.ToKeyId == x.ClassMeetingId).AttachUrl.Replace('\\', '/'),
                               }).FirstOrDefault();
            }
            else if (meetingType == "W")
            {
                getMeetItem = (from x in Funs.DB.Meeting_WeekMeeting
                               join y in Funs.DB.Sys_User on x.CompileMan equals y.UserId
                               where x.WeekMeetingId == meetingId
                               select new Model.MeetingItem
                               {
                                   MeetingId = x.WeekMeetingId,
                                   MeetingCode = x.WeekMeetingCode,
                                   MeetingName = x.WeekMeetingName,
                                   MeetingDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.WeekMeetingDate),
                                   MeetingContents = x.WeekMeetingContents,
                                   MeetingPlace = x.MeetingPlace,
                                   MeetingType = meetingType,
                                   MeetingHours = x.MeetingHours ?? 0,
                                   MeetingHostMan = x.MeetingHostMan,
                                   AttentPerson = x.AttentPerson,
                                   CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                   CompileManId = x.CompileMan,
                                   CompileManName = y.UserName,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == y.UnitId).UnitName,
                                   AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.WeekMeetingId).AttachUrl.Replace('\\', '/'),
                               }).FirstOrDefault();
            }
            else if (meetingType == "M")
            {
                getMeetItem = (from x in Funs.DB.Meeting_MonthMeeting
                               join y in Funs.DB.Sys_User on x.CompileMan equals y.UserId
                               where x.MonthMeetingId == meetingId
                               select new Model.MeetingItem
                               {
                                   MeetingId = x.MonthMeetingId,
                                   MeetingCode = x.MonthMeetingCode,
                                   MeetingName = x.MonthMeetingName,
                                   MeetingDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.MonthMeetingDate),
                                   MeetingContents = x.MonthMeetingContents,
                                   MeetingPlace = x.MeetingPlace,
                                   MeetingType = meetingType,
                                   MeetingHours = x.MeetingHours ?? 0,
                                   MeetingHostMan = x.MeetingHostMan,
                                   AttentPerson = x.AttentPerson,
                                   CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                   CompileManId = x.CompileMan,
                                   CompileManName = y.UserName,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == y.UnitId).UnitName,
                                   AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.MonthMeetingId).AttachUrl.Replace('\\', '/'),
                               }).FirstOrDefault();
            }
            else if (meetingType == "S")
            {
                getMeetItem = (from x in Funs.DB.Meeting_SpecialMeeting
                               join y in Funs.DB.Sys_User on x.CompileMan equals y.UserId
                               where x.SpecialMeetingId == meetingId
                               select new Model.MeetingItem
                               {
                                   MeetingId = x.SpecialMeetingId,
                                   MeetingCode = x.SpecialMeetingCode,
                                   MeetingName = x.SpecialMeetingName,
                                   MeetingDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.SpecialMeetingDate),
                                   MeetingContents = x.SpecialMeetingContents,
                                   MeetingPlace = x.MeetingPlace,
                                   MeetingType = meetingType,
                                   MeetingHours = x.MeetingHours ?? 0,
                                   MeetingHostMan = x.MeetingHostMan,
                                   AttentPerson = x.AttentPerson,
                                   CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                   CompileManId = x.CompileMan,
                                   CompileManName = y.UserName,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == y.UnitId).UnitName,
                                   AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.SpecialMeetingId).AttachUrl.Replace('\\', '/'),
                               }).FirstOrDefault();
            }
            else
            {
                getMeetItem = (from x in Funs.DB.Meeting_AttendMeeting
                               join y in Funs.DB.Sys_User on x.CompileMan equals y.UserId
                               where x.AttendMeetingId == meetingId
                               select new Model.MeetingItem
                               {
                                   MeetingId = x.AttendMeetingId,
                                   MeetingCode = x.AttendMeetingCode,
                                   MeetingName = x.AttendMeetingName,
                                   MeetingDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.AttendMeetingDate),
                                   MeetingContents = x.AttendMeetingContents,
                                   MeetingPlace = x.MeetingPlace,
                                   MeetingType = meetingType,
                                   MeetingHours = x.MeetingHours ?? 0,
                                   MeetingHostMan = x.MeetingHostMan,
                                   AttentPerson = x.AttentPerson,
                                   CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                   CompileManId = x.CompileMan,
                                   CompileManName = y.UserName,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == y.UnitId).UnitName,
                                   AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.AttendMeetingId).AttachUrl.Replace('\\', '/'),
                               }).FirstOrDefault();
            }
            return getMeetItem;
        }
        #endregion

        #region 根据projectId、meetingType获取会议列表
        /// <summary>
        /// 根据projectId、meetingType获取会议列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="meetingType">会议类型(C-班前会；W-周例会；M-例会；S-专题例会；A-其他会议)</param>
        /// <param name="states">状态（0-待提交；1-已提交）</param>
        /// <returns></returns>
        public static List<Model.MeetingItem> getMeetingByProjectIdStates(string projectId, string meetingType,string states)
        {
            List<Model.MeetingItem> getMeetItem = new List<Model.MeetingItem>();
            if (meetingType == "C")
            {
                getMeetItem = (from x in Funs.DB.Meeting_ClassMeeting
                               join y in Funs.DB.Sys_User on x.CompileMan equals y.UserId
                               where x.ProjectId == projectId && ((states == "0" && (x.States =="0" || x.States==null)) 
                                    || (states == "1" && (x.States =="1" || x.States =="2")))
                               select new Model.MeetingItem
                               {
                                   MeetingId = x.ClassMeetingId,
                                   ProjectId=x.ProjectId,
                                   MeetingCode = x.ClassMeetingCode,
                                   MeetingName = x.ClassMeetingName,
                                   MeetingDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.ClassMeetingDate),
                                   MeetingContents = x.ClassMeetingContents,
                                   MeetingPlace = x.MeetingPlace,
                                   MeetingType = meetingType,
                                   MeetingHours = x.MeetingHours ?? 0,
                                   MeetingHostMan = x.MeetingHostMan,
                                   AttentPerson = x.AttentPerson,
                                   CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                   CompileManId = x.CompileMan,
                                   CompileManName = y.UserName,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == y.UnitId).UnitName,
                                   AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.ClassMeetingId).AttachUrl.Replace('\\', '/'),
                               }).ToList();
            }
            else if (meetingType == "W")
            {
                getMeetItem = (from x in Funs.DB.Meeting_WeekMeeting
                               join y in Funs.DB.Sys_User on x.CompileMan equals y.UserId
                               where x.ProjectId == projectId && ((states == "0" && (x.States == "0" || x.States == null))
                                    || (states == "1" && (x.States == "1" || x.States == "2")))
                               select new Model.MeetingItem
                               {
                                   MeetingId = x.WeekMeetingId,
                                   ProjectId = x.ProjectId,
                                   MeetingCode = x.WeekMeetingCode,
                                   MeetingName = x.WeekMeetingName,
                                   MeetingDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.WeekMeetingDate),
                                   MeetingContents = x.WeekMeetingContents,
                                   MeetingPlace = x.MeetingPlace,
                                   MeetingType = meetingType,
                                   MeetingHours = x.MeetingHours ?? 0,
                                   MeetingHostMan = x.MeetingHostMan,
                                   AttentPerson = x.AttentPerson,
                                   CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                   CompileManId = x.CompileMan,
                                   CompileManName = y.UserName,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == y.UnitId).UnitName,
                                   AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.WeekMeetingId).AttachUrl.Replace('\\', '/'),
                               }).ToList();
            }
            else if (meetingType == "M")
            {
                getMeetItem = (from x in Funs.DB.Meeting_MonthMeeting
                               join y in Funs.DB.Sys_User on x.CompileMan equals y.UserId
                               where x.ProjectId == projectId && ((states == "0" && (x.States == "0" || x.States == null))
                                    || (states == "1" && (x.States == "1" || x.States == "2")))
                               select new Model.MeetingItem
                               {
                                   MeetingId = x.MonthMeetingId,
                                   ProjectId = x.ProjectId,
                                   MeetingCode = x.MonthMeetingCode,
                                   MeetingName = x.MonthMeetingName,
                                   MeetingDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.MonthMeetingDate),
                                   MeetingContents = x.MonthMeetingContents,
                                   MeetingPlace = x.MeetingPlace,
                                   MeetingType = meetingType,
                                   MeetingHours = x.MeetingHours ?? 0,
                                   MeetingHostMan = x.MeetingHostMan,
                                   AttentPerson = x.AttentPerson,
                                   CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                   CompileManId = x.CompileMan,
                                   CompileManName = y.UserName,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == y.UnitId).UnitName,
                                   AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.MonthMeetingId).AttachUrl.Replace('\\', '/'),
                               }).ToList();
            }
            else if (meetingType == "S")
            {
                getMeetItem = (from x in Funs.DB.Meeting_SpecialMeeting
                               join y in Funs.DB.Sys_User on x.CompileMan equals y.UserId
                               where x.ProjectId == projectId && ((states == "0" && (x.States == "0" || x.States == null))
                                    || (states == "1" && (x.States == "1" || x.States == "2")))
                               select new Model.MeetingItem
                               {
                                   MeetingId = x.SpecialMeetingId,
                                   ProjectId = x.ProjectId,
                                   MeetingCode = x.SpecialMeetingCode,
                                   MeetingName = x.SpecialMeetingName,
                                   MeetingDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.SpecialMeetingDate),
                                   MeetingContents = x.SpecialMeetingContents,
                                   MeetingPlace = x.MeetingPlace,
                                   MeetingType = meetingType,
                                   MeetingHours = x.MeetingHours ?? 0,
                                   MeetingHostMan = x.MeetingHostMan,
                                   AttentPerson = x.AttentPerson,
                                   CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                   CompileManId = x.CompileMan,
                                   CompileManName = y.UserName,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == y.UnitId).UnitName,
                                   AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.SpecialMeetingId).AttachUrl.Replace('\\', '/'),
                               }).ToList();
            }
            else
            {
                getMeetItem = (from x in Funs.DB.Meeting_AttendMeeting
                               join y in Funs.DB.Sys_User on x.CompileMan equals y.UserId
                               where x.ProjectId == projectId && ((states == "0" && (x.States == "0" || x.States == null))
                                    || (states == "1" && (x.States == "1" || x.States == "2")))
                               select new Model.MeetingItem
                               {
                                   MeetingId = x.AttendMeetingId,
                                   ProjectId = x.ProjectId,
                                   MeetingCode = x.AttendMeetingCode,
                                   MeetingName = x.AttendMeetingName,
                                   MeetingDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.AttendMeetingDate),
                                   MeetingContents = x.AttendMeetingContents,
                                   MeetingPlace = x.MeetingPlace,
                                   MeetingType = meetingType,
                                   MeetingHours = x.MeetingHours ?? 0,
                                   MeetingHostMan = x.MeetingHostMan,
                                   AttentPerson = x.AttentPerson,
                                   CompileDate = string.Format("{0:yyyy-MM-dd HH:mm}", x.CompileDate),
                                   CompileManId = x.CompileMan,
                                   CompileManName = y.UserName,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(u => u.UnitId == y.UnitId).UnitName,
                                   AttachUrl = Funs.DB.AttachFile.First(z => z.ToKeyId == x.AttendMeetingId).AttachUrl.Replace('\\', '/'),
                               }).ToList();
            }
            return getMeetItem;
        }
        #endregion

        #region 保存Meeting
        /// <summary>
        /// 保存Meeting
        /// </summary>
        /// <param name="meeting">会议信息</param>
        /// <returns></returns>
        public static void SaveMeeting(Model.MeetingItem meeting)
        {
            Model.SUBHSSEDB db = Funs.DB;
            if (meeting.MeetingType == "C")
            {
                Model.Meeting_ClassMeeting newClassMeeting = new Model.Meeting_ClassMeeting
                {
                    ProjectId = meeting.ProjectId,
                    ClassMeetingCode = meeting.MeetingCode,
                    ClassMeetingName = meeting.MeetingName,
                    ClassMeetingDate = Funs.GetNewDateTime(meeting.MeetingDate),
                    ClassMeetingContents = meeting.MeetingContents,
                    CompileMan = meeting.CompileManId,
                    CompileDate = Funs.GetNewDateTime(meeting.CompileDate),
                    MeetingPlace = meeting.MeetingPlace,
                    MeetingHours = meeting.MeetingHours,
                    MeetingHostMan = meeting.MeetingHostMan,
                    AttentPerson = meeting.AttentPerson,
                    States = Const.State_2,
                };

                if (meeting.States != "1")
                {
                    newClassMeeting.States = Const.State_0;
                }

                var updateMeet = Funs.DB.Meeting_ClassMeeting.FirstOrDefault(x => x.ClassMeetingId == meeting.MeetingId);
                if (updateMeet == null)
                {
                    newClassMeeting.ClassMeetingId = SQLHelper.GetNewID();
                    newClassMeeting.ClassMeetingCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectClassMeetingMenuId, newClassMeeting.ProjectId, null);
                    ClassMeetingService.AddClassMeeting(newClassMeeting);
                }
                else
                {
                    updateMeet.ClassMeetingName = newClassMeeting.ClassMeetingName;
                    updateMeet.ClassMeetingDate = newClassMeeting.ClassMeetingDate;
                    updateMeet.ClassMeetingContents = newClassMeeting.ClassMeetingContents;
                    updateMeet.CompileMan = newClassMeeting.CompileMan;
                    updateMeet.States = newClassMeeting.States;
                    updateMeet.MeetingPlace = newClassMeeting.MeetingPlace;
                    updateMeet.MeetingHours = newClassMeeting.MeetingHours;
                    updateMeet.MeetingHostMan = newClassMeeting.MeetingHostMan;
                    updateMeet.AttentPerson = newClassMeeting.AttentPerson;
                }
                if (meeting.States == "1")
                {                  
                    CommonService.btnSaveData(meeting.ProjectId, Const.ProjectClassMeetingMenuId, newClassMeeting.ClassMeetingId, newClassMeeting.CompileMan, true, newClassMeeting.ClassMeetingName, "../Meeting/ClassMeetingView.aspx?ClassMeetingId={0}");
                }

                ////保存附件
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(meeting.AttachUrl, 10, null), meeting.AttachUrl, Const.ProjectClassMeetingMenuId, newClassMeeting.ClassMeetingId);
            }
            else if (meeting.MeetingType == "W")
            {
                Model.Meeting_WeekMeeting newWeekMeeting = new Model.Meeting_WeekMeeting
                {
                    ProjectId = meeting.ProjectId,
                    WeekMeetingCode = meeting.MeetingCode,
                    WeekMeetingName = meeting.MeetingName,
                    WeekMeetingDate = Funs.GetNewDateTime(meeting.MeetingDate),
                    WeekMeetingContents = meeting.MeetingContents,
                    CompileMan = meeting.CompileManId,
                    CompileDate = Funs.GetNewDateTime(meeting.CompileDate),
                    MeetingPlace = meeting.MeetingPlace,
                    MeetingHours = meeting.MeetingHours,
                    MeetingHostMan = meeting.MeetingHostMan,
                    AttentPerson = meeting.AttentPerson,
                    States = Const.State_2,
                };

                if (meeting.States != "1")
                {
                    newWeekMeeting.States = Const.State_0;
                }

                var updateMeet = Funs.DB.Meeting_WeekMeeting.FirstOrDefault(x => x.WeekMeetingId == meeting.MeetingId);
                if (updateMeet == null)
                {
                    newWeekMeeting.WeekMeetingId = SQLHelper.GetNewID();
                    newWeekMeeting.WeekMeetingCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectWeekMeetingMenuId, newWeekMeeting.ProjectId, null);
                    WeekMeetingService.AddWeekMeeting(newWeekMeeting);
                }
                else
                {
                    updateMeet.WeekMeetingName = newWeekMeeting.WeekMeetingName;
                    updateMeet.WeekMeetingDate = newWeekMeeting.WeekMeetingDate;
                    updateMeet.WeekMeetingContents = newWeekMeeting.WeekMeetingContents;
                    updateMeet.CompileMan = newWeekMeeting.CompileMan;
                    updateMeet.States = newWeekMeeting.States;
                    updateMeet.MeetingPlace = newWeekMeeting.MeetingPlace;
                    updateMeet.MeetingHours = newWeekMeeting.MeetingHours;
                    updateMeet.MeetingHostMan = newWeekMeeting.MeetingHostMan;
                    updateMeet.AttentPerson = newWeekMeeting.AttentPerson;
                }
                if (meeting.States == "1")
                {
                    CommonService.btnSaveData(meeting.ProjectId, Const.ProjectWeekMeetingMenuId, newWeekMeeting.WeekMeetingId, newWeekMeeting.CompileMan, true, newWeekMeeting.WeekMeetingName, "../Meeting/WeekMeetingView.aspx?WeekMeetingId={0}");
                }

                ////保存附件
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(meeting.AttachUrl, 10, null), meeting.AttachUrl, Const.ProjectWeekMeetingMenuId, newWeekMeeting.WeekMeetingId);
            }
            else if (meeting.MeetingType == "M")
            {
                Model.Meeting_MonthMeeting newMonthMeeting = new Model.Meeting_MonthMeeting
                {
                    ProjectId = meeting.ProjectId,
                    MonthMeetingCode = meeting.MeetingCode,
                    MonthMeetingName = meeting.MeetingName,
                    MonthMeetingDate = Funs.GetNewDateTime(meeting.MeetingDate),
                    MonthMeetingContents = meeting.MeetingContents,
                    CompileMan = meeting.CompileManId,
                    CompileDate = Funs.GetNewDateTime(meeting.CompileDate),
                    MeetingPlace = meeting.MeetingPlace,
                    MeetingHours = meeting.MeetingHours,
                    MeetingHostMan = meeting.MeetingHostMan,
                    AttentPerson = meeting.AttentPerson,
                    States = Const.State_2,
                };

                if (meeting.States != "1")
                {
                    newMonthMeeting.States = Const.State_0;
                }

                var updateMeet = Funs.DB.Meeting_MonthMeeting.FirstOrDefault(x => x.MonthMeetingId == meeting.MeetingId);
                if (updateMeet == null)
                {
                    newMonthMeeting.MonthMeetingId = SQLHelper.GetNewID();
                    newMonthMeeting.MonthMeetingCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectMonthMeetingMenuId, newMonthMeeting.ProjectId, null);
                    MonthMeetingService.AddMonthMeeting(newMonthMeeting);
                }
                else
                {
                    updateMeet.MonthMeetingName = newMonthMeeting.MonthMeetingName;
                    updateMeet.MonthMeetingDate = newMonthMeeting.MonthMeetingDate;
                    updateMeet.MonthMeetingContents = newMonthMeeting.MonthMeetingContents;
                    updateMeet.CompileMan = newMonthMeeting.CompileMan;
                    updateMeet.States = newMonthMeeting.States;
                    updateMeet.MeetingPlace = newMonthMeeting.MeetingPlace;
                    updateMeet.MeetingHours = newMonthMeeting.MeetingHours;
                    updateMeet.MeetingHostMan = newMonthMeeting.MeetingHostMan;
                    updateMeet.AttentPerson = newMonthMeeting.AttentPerson;
                }
                if (meeting.States == "1")
                {
                    CommonService.btnSaveData(meeting.ProjectId, Const.ProjectMonthMeetingMenuId, newMonthMeeting.MonthMeetingId, newMonthMeeting.CompileMan, true, newMonthMeeting.MonthMeetingName, "../Meeting/MonthMeetingView.aspx?MonthMeetingId={0}");
                }

                ////保存附件
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(meeting.AttachUrl, 10, null), meeting.AttachUrl, Const.ProjectMonthMeetingMenuId, newMonthMeeting.MonthMeetingId);
            }
            else if (meeting.MeetingType == "S")
            {
                Model.Meeting_SpecialMeeting newSpecialMeeting = new Model.Meeting_SpecialMeeting
                {
                    ProjectId = meeting.ProjectId,
                    SpecialMeetingCode = meeting.MeetingCode,
                    SpecialMeetingName = meeting.MeetingName,
                    SpecialMeetingDate = Funs.GetNewDateTime(meeting.MeetingDate),
                    SpecialMeetingContents = meeting.MeetingContents,
                    CompileMan = meeting.CompileManId,
                    CompileDate = Funs.GetNewDateTime(meeting.CompileDate),
                    MeetingPlace = meeting.MeetingPlace,
                    MeetingHours = meeting.MeetingHours,
                    MeetingHostMan = meeting.MeetingHostMan,
                    AttentPerson = meeting.AttentPerson,
                    States = Const.State_2,
                };

                if (meeting.States != "1")
                {
                    newSpecialMeeting.States = Const.State_0;
                }

                var updateMeet = Funs.DB.Meeting_SpecialMeeting.FirstOrDefault(x => x.SpecialMeetingId == meeting.MeetingId);
                if (updateMeet == null)
                {
                    newSpecialMeeting.SpecialMeetingId = SQLHelper.GetNewID();
                    newSpecialMeeting.SpecialMeetingCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectSpecialMeetingMenuId, newSpecialMeeting.ProjectId, null);
                    SpecialMeetingService.AddSpecialMeeting(newSpecialMeeting);
                }
                else
                {
                    updateMeet.SpecialMeetingName = newSpecialMeeting.SpecialMeetingName;
                    updateMeet.SpecialMeetingDate = newSpecialMeeting.SpecialMeetingDate;
                    updateMeet.SpecialMeetingContents = newSpecialMeeting.SpecialMeetingContents;
                    updateMeet.CompileMan = newSpecialMeeting.CompileMan;
                    updateMeet.States = newSpecialMeeting.States;
                    updateMeet.MeetingPlace = newSpecialMeeting.MeetingPlace;
                    updateMeet.MeetingHours = newSpecialMeeting.MeetingHours;
                    updateMeet.MeetingHostMan = newSpecialMeeting.MeetingHostMan;
                    updateMeet.AttentPerson = newSpecialMeeting.AttentPerson;
                }
                if (meeting.States == "1")
                {
                    CommonService.btnSaveData(meeting.ProjectId, Const.ProjectSpecialMeetingMenuId, newSpecialMeeting.SpecialMeetingId, newSpecialMeeting.CompileMan, true, newSpecialMeeting.SpecialMeetingName, "../Meeting/SpecialMeetingView.aspx?SpecialMeetingId={0}");
                }

                ////保存附件
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(meeting.AttachUrl, 10, null), meeting.AttachUrl, Const.ProjectSpecialMeetingMenuId, newSpecialMeeting.SpecialMeetingId);
            }
            else
            {
                Model.Meeting_AttendMeeting newAttendMeeting = new Model.Meeting_AttendMeeting
                {
                    ProjectId = meeting.ProjectId,
                    AttendMeetingCode = meeting.MeetingCode,
                    AttendMeetingName = meeting.MeetingName,
                    AttendMeetingDate = Funs.GetNewDateTime(meeting.MeetingDate),
                    AttendMeetingContents = meeting.MeetingContents,
                    CompileMan = meeting.CompileManId,
                    CompileDate = Funs.GetNewDateTime(meeting.CompileDate),
                    MeetingPlace = meeting.MeetingPlace,
                    MeetingHours = meeting.MeetingHours,
                    MeetingHostMan = meeting.MeetingHostMan,
                    AttentPerson = meeting.AttentPerson,
                    States = Const.State_2,
                };

                if (meeting.States != "1")
                {
                    newAttendMeeting.States = Const.State_0;
                }

                var updateMeet = Funs.DB.Meeting_AttendMeeting.FirstOrDefault(x => x.AttendMeetingId == meeting.MeetingId);
                if (updateMeet == null)
                {
                    newAttendMeeting.AttendMeetingId = SQLHelper.GetNewID();
                    newAttendMeeting.AttendMeetingCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectAttendMeetingMenuId, newAttendMeeting.ProjectId, null);
                    AttendMeetingService.AddAttendMeeting(newAttendMeeting);
                }
                else
                {
                    updateMeet.AttendMeetingName = newAttendMeeting.AttendMeetingName;
                    updateMeet.AttendMeetingDate = newAttendMeeting.AttendMeetingDate;
                    updateMeet.AttendMeetingContents = newAttendMeeting.AttendMeetingContents;
                    updateMeet.CompileMan = newAttendMeeting.CompileMan;
                    updateMeet.States = newAttendMeeting.States;
                    updateMeet.MeetingPlace = newAttendMeeting.MeetingPlace;
                    updateMeet.MeetingHours = newAttendMeeting.MeetingHours;
                    updateMeet.MeetingHostMan = newAttendMeeting.MeetingHostMan;
                    updateMeet.AttentPerson = newAttendMeeting.AttentPerson;
                }
                if (meeting.States == "1")
                {
                    CommonService.btnSaveData(meeting.ProjectId, Const.ProjectAttendMeetingMenuId, newAttendMeeting.AttendMeetingId, newAttendMeeting.CompileMan, true, newAttendMeeting.AttendMeetingName, "../Meeting/AttendMeetingView.aspx?AttendMeetingId={0}");
                }

                ////保存附件
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(meeting.AttachUrl, 10, null), meeting.AttachUrl, Const.ProjectAttendMeetingMenuId, newAttendMeeting.AttendMeetingId);
            }
        }
        #endregion
    }
}