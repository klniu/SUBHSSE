﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 人员信息
    /// </summary>
    public static class PersonService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static Model.SitePerson_Person GetPersonById(string personId)
        {
            return Funs.DB.SitePerson_Person.FirstOrDefault(e => e.PersonId == personId);
        }

        /// <summary>
        /// 根据项目单位获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static List<Model.SitePerson_Person> GetPersonLitsByprojectIdUnitId(string projectId, string unitId)
        {
            return (from x in Funs.DB.SitePerson_Person where x.ProjectId == projectId && x.UnitId == unitId select x).ToList();
        }

        /// <summary>
        /// 获取最大的人员位置
        /// </summary>
        /// <returns>最大的人员位置</returns>
        public static int? GetMaxPersonIndex(string projectId)
        {
            return (from x in Funs.DB.SitePerson_Person where x.ProjectId == projectId select x.PersonIndex).Max();
        }

        /// <summary>
        /// 根据单位Id查询所有人员的数量
        /// </summary>
        /// <param name="unitId">单位Id</param>
        /// <returns>人员的数量</returns>
        public static int GetPersonCountByUnitId(string unitId, string projectId)
        {
            var q = (from x in Funs.DB.SitePerson_Person where x.UnitId == unitId && x.ProjectId == projectId && x.IsUsed == true select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据单位Id查询所有HSE人员的数量
        /// </summary>
        /// <param name="unitId">单位Id</param>
        /// <returns>HSE人员的数量</returns>
        public static int GetHSEPersonCountByUnitId(string unitId, string projectId)
        {
            var q = (from x in Funs.DB.SitePerson_Person where x.UnitId == unitId && x.ProjectId == projectId && (x.WorkPostId == BLL.Const.PostEngineer || x.WorkPostId == BLL.Const.PostMaterialPrincipal) && x.IsUsed == true select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 获取所有人员位置集合
        /// </summary>
        /// <returns>所有人员位置集合</returns>
        public static List<int?> GetPersonIndexs(string projectId)
        {
            return (from x in Funs.DB.SitePerson_Person where x.ProjectId == projectId select x.PersonIndex).ToList();
        }

        /// <summary>
        /// 根据卡号查询人员信息
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <returns>人员实体</returns>
        public static Model.SitePerson_Person GetPersonByCardNo(string projectId, string cardNo)
        {
            return Funs.DB.SitePerson_Person.FirstOrDefault(e => e.ProjectId == projectId && e.CardNo == cardNo);
        }

        /// <summary>
        /// 根据卡号查询所有人员的数量
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <returns>人员的数量</returns>
        public static int GetPersonCountByCardNo(string projectId, string cardNo)
        {
            var q = (from x in Funs.DB.SitePerson_Person where x.ProjectId == projectId && x.CardNo == cardNo select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据人员姓名和所在单位判断人员是否存在
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="personName"></param>
        /// <returns></returns>
        public static bool IsExistPersonByUnit(string unitId, string personName, string projectId)
        {
            var q = from x in BLL.Funs.DB.SitePerson_Person where x.UnitId == unitId && x.PersonName == personName && x.ProjectId == projectId select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据身份证号Id获取人员的数量
        /// </summary>
        /// <param name="identityCard">身份证号</param>
        /// <returns>人员的数量</returns>
        public static int GetPersonCountByIdentityCard(string identityCard, string projectId)
        {
            var q = (from x in Funs.DB.SitePerson_Person where x.IdentityCard == identityCard && x.ProjectId == projectId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 获取人员信息列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.SitePerson_Person> GetPersonList(string projectId)
        {
            return (from x in Funs.DB.SitePerson_Person where x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 增加人员信息
        /// </summary>
        /// <param name="person">人员实体</param>
        public static void AddPerson(Model.SitePerson_Person person)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SitePerson_Person newPerson = new Model.SitePerson_Person
            {
                PersonId = person.PersonId,
                CardNo = person.CardNo,
                PersonName = person.PersonName,
                Sex = person.Sex,
                IdentityCard = person.IdentityCard,
                Address = person.Address,
                ProjectId = person.ProjectId,
                UnitId = person.UnitId,
                TeamGroupId = person.TeamGroupId,
                WorkAreaId = person.WorkAreaId,
                WorkPostId = person.WorkPostId
            };
            if (person.InTime.HasValue)
            {
                newPerson.InTime = person.InTime;
            }
            else
            {
                newPerson.InTime = Funs.GetNewDateTime(System.DateTime.Now.ToShortDateString());
            }
            newPerson.OutTime = person.OutTime;
            newPerson.OutResult = person.OutResult;
            newPerson.Telephone = person.Telephone;
            newPerson.PositionId = person.PositionId;
            newPerson.PostTitleId = person.PostTitleId;
            newPerson.PhotoUrl = person.PhotoUrl;
            newPerson.IsUsed = person.IsUsed;
            newPerson.IsCardUsed = person.IsCardUsed;
            newPerson.DepartId = person.DepartId;
            db.SitePerson_Person.InsertOnSubmit(newPerson);
            db.SubmitChanges();

            ///写入人员出入场时间表 
            Model.SitePerson_PersonInOut newPersonInOut = new Model.SitePerson_PersonInOut
            {
                ProjectId = person.ProjectId,
                UnitId = person.UnitId,
                PersonId = person.PersonId
            };
            if (newPerson.InTime.HasValue)
            {
                newPersonInOut.ChangeTime = person.InTime;
                newPersonInOut.IsIn = true;
                BLL.PersonInOutService.AddPersonInOut(newPersonInOut);
            }

            if (newPerson.OutTime.HasValue)
            {
                newPersonInOut.ChangeTime = person.OutTime;
                newPersonInOut.IsIn = false;
                BLL.PersonInOutService.AddPersonInOut(newPersonInOut);
            }

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.PersonListMenuId, person.ProjectId, person.UnitId, person.PersonId, person.InTime);
        }

        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="person">人员实体</param>
        public static void UpdatePerson(Model.SitePerson_Person person)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SitePerson_Person newPerson = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == person.PersonId);
            if (newPerson != null)
            {
                newPerson.CardNo = person.CardNo;
                newPerson.PersonName = person.PersonName;
                newPerson.Sex = person.Sex;
                newPerson.IdentityCard = person.IdentityCard;
                newPerson.Address = person.Address;
                newPerson.ProjectId = person.ProjectId;
                newPerson.UnitId = person.UnitId;
                newPerson.TeamGroupId = person.TeamGroupId;
                newPerson.WorkAreaId = person.WorkAreaId;
                newPerson.WorkPostId = person.WorkPostId;
                newPerson.InTime = person.InTime;
                newPerson.OutTime = person.OutTime;
                newPerson.OutResult = person.OutResult;
                newPerson.Telephone = person.Telephone;
                newPerson.PositionId = person.PositionId;
                newPerson.PostTitleId = person.PostTitleId;
                newPerson.PhotoUrl = person.PhotoUrl;
                newPerson.IsUsed = person.IsUsed;
                newPerson.IsCardUsed = person.IsCardUsed;
                newPerson.DepartId = person.DepartId;
                db.SubmitChanges();

                ///写入人员出入场时间表 
                Model.SitePerson_PersonInOut newPersonInOut = new Model.SitePerson_PersonInOut
                {
                    ProjectId = person.ProjectId,
                    UnitId = person.UnitId,
                    PersonId = person.PersonId
                };
                if (newPerson.InTime.HasValue)
                {
                    var inOutIn = BLL.PersonInOutService.GetPersonInOutByTimePersonId(person.PersonId, person.InTime.Value, true);
                    if (inOutIn == null)
                    {
                        newPersonInOut.ChangeTime = person.InTime;
                        newPersonInOut.IsIn = true;
                        BLL.PersonInOutService.AddPersonInOut(newPersonInOut);
                    }
                }

                if (newPerson.OutTime.HasValue)
                {
                    var inOutIn = BLL.PersonInOutService.GetPersonInOutByTimePersonId(person.PersonId, person.OutTime.Value, false);
                    if (inOutIn == null)
                    {
                        newPersonInOut.ChangeTime = person.OutTime;
                        newPersonInOut.IsIn = false;
                        BLL.PersonInOutService.AddPersonInOut(newPersonInOut);
                    }
                }
            }
        }

        /// <summary>
        /// 根据人员Id删除一个人员信息
        /// </summary>
        /// <param name="personId">人员Id</param>
        public static void DeletePerson(string personId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SitePerson_Person person = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == personId);
            if (person != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(personId);
                //删除管理人员资质
                Model.QualityAudit_ManagePersonQuality managePersonQuality = db.QualityAudit_ManagePersonQuality.FirstOrDefault(e => e.PersonId == personId);
                if (managePersonQuality != null)
                {
                    CodeRecordsService.DeleteCodeRecordsByDataId(managePersonQuality.ManagePersonQualityId);
                    CommonService.DeleteAttachFileById(managePersonQuality.ManagePersonQualityId);
                    db.QualityAudit_ManagePersonQuality.DeleteOnSubmit(managePersonQuality);
                    db.SubmitChanges();
                }
                //删除特岗人员资质
                var personQuality = PersonQualityService.GetPersonQualityByPersonId(personId);
                if (personQuality != null)
                {
                    CodeRecordsService.DeleteCodeRecordsByDataId(personQuality.PersonQualityId);//删除编号
                    CommonService.DeleteAttachFileById(personQuality.PersonQualityId);//删除附件
                    db.QualityAudit_PersonQuality.DeleteOnSubmit(personQuality);
                    db.SubmitChanges();
                }
                //删除安全人员资质
                Model.QualityAudit_SafePersonQuality safePersonQuality = db.QualityAudit_SafePersonQuality.FirstOrDefault(e => e.PersonId == personId);
                if (safePersonQuality != null)
                {
                    CodeRecordsService.DeleteCodeRecordsByDataId(safePersonQuality.SafePersonQualityId);
                    CommonService.DeleteAttachFileById(safePersonQuality.SafePersonQualityId);
                    db.QualityAudit_SafePersonQuality.DeleteOnSubmit(safePersonQuality);
                    db.SubmitChanges();
                }

                ///删除人员出入场记录
                BLL.PersonInOutService.DeletePersonInOutByPersonId(person.PersonId);

                db.SitePerson_Person.DeleteOnSubmit(person);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据身份证号获取人员信息
        /// </summary>
        /// <param name="identityCard">身份证号</param>
        /// <returns>人员信息</returns>
        public static Model.SitePerson_Person GetPersonByIdentityCard(string projectId, string identityCard)
        {
            if (!string.IsNullOrEmpty(identityCard))
            {
                return Funs.DB.SitePerson_Person.FirstOrDefault(e => e.ProjectId == projectId && e.IdentityCard == identityCard);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 保存发卡信息
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="cardNo"></param>
        public static void SaveSendCard(string personId, string cardNo, int personIndex)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SitePerson_Person card = db.SitePerson_Person.FirstOrDefault(e => e.CardNo == cardNo);
            if (card != null)
            {
                card.CardNo = null;
            }
            else
            {
                Model.SitePerson_Person person = db.SitePerson_Person.FirstOrDefault(e => e.PersonId == personId);
                person.CardNo = cardNo;
                person.PersonIndex = personIndex;
                //person.CardNo = sendCardNo;
            }

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据作业区域获取人员
        /// </summary>
        /// <param name="workAreaId"></param>
        /// <returns></returns>
        public static List<Model.SitePerson_Person> GetPersonListByWorkAreaId(string workAreaId)
        {
            return (from x in db.SitePerson_Person where x.WorkAreaId == workAreaId select x).ToList();
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitPersonByProjectUnitDropDownList(FineUIPro.DropDownList dropName, string projectId, string unitId, bool isShowPlease)
        {
            dropName.DataValueField = "PersonId";
            dropName.DataTextField = "PersonName";
            dropName.DataSource = GetPersonLitsByprojectIdUnitId(projectId, unitId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
