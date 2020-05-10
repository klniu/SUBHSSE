﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 知识竞赛计划
    /// </summary>
    public static class ServerTestPlanService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取知识竞赛计划
        /// </summary>
        /// <param name="TestPlanId"></param>
        /// <returns></returns>
        public static Model.Test_TestPlan GetTestPlanById(string TestPlanId)
        {
            return Funs.DB.Test_TestPlan.FirstOrDefault(e => e.TestPlanId == TestPlanId);
        }

        /// <summary>
        /// 添加知识竞赛计划
        /// </summary>
        /// <param name="TestPlan"></param>
        public static void AddTestPlan(Model.Test_TestPlan TestPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Test_TestPlan newTestPlan = new Model.Test_TestPlan
            {
                TestPlanId = TestPlan.TestPlanId,
                PlanCode = TestPlan.PlanCode,
                PlanName = TestPlan.PlanName,
                PlanManId = TestPlan.PlanManId,
                PlanDate = TestPlan.PlanDate,
                TestStartTime = TestPlan.TestStartTime,
                TestEndTime = TestPlan.TestEndTime,
                Duration = TestPlan.Duration,
                TotalScore = TestPlan.TotalScore,
                QuestionCount = TestPlan.QuestionCount,
                TestPalce = TestPlan.TestPalce,
                QRCodeUrl = TestPlan.QRCodeUrl,
                SValue = TestPlan.SValue,
                MValue = TestPlan.MValue,
                JValue = TestPlan.JValue,
                States = TestPlan.States,
            };
            db.Test_TestPlan.InsertOnSubmit(newTestPlan);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改知识竞赛计划
        /// </summary>
        /// <param name="TestPlan"></param>
        public static void UpdateTestPlan(Model.Test_TestPlan TestPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Test_TestPlan newTestPlan = db.Test_TestPlan.FirstOrDefault(e => e.TestPlanId == TestPlan.TestPlanId);
            if (newTestPlan != null)
            {
                newTestPlan.PlanCode = TestPlan.PlanCode;
                newTestPlan.PlanName = TestPlan.PlanName;
                newTestPlan.PlanManId = TestPlan.PlanManId;
                newTestPlan.PlanDate = TestPlan.PlanDate;
                newTestPlan.TestStartTime = TestPlan.TestStartTime;
                newTestPlan.TestEndTime = TestPlan.TestEndTime;
                newTestPlan.Duration = TestPlan.Duration;
                newTestPlan.TotalScore = TestPlan.TotalScore;
                newTestPlan.QuestionCount = TestPlan.QuestionCount;
                newTestPlan.TestPalce = TestPlan.TestPalce;
                newTestPlan.QRCodeUrl = TestPlan.QRCodeUrl;
                newTestPlan.SValue = TestPlan.SValue;
                newTestPlan.MValue = TestPlan.MValue;
                newTestPlan.JValue = TestPlan.JValue;
                newTestPlan.States = TestPlan.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除知识竞赛计划
        /// </summary>
        /// <param name="TestPlanId"></param>
        public static void DeleteTestPlanById(string TestPlanId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Test_TestPlan TestPlan = db.Test_TestPlan.FirstOrDefault(e => e.TestPlanId == TestPlanId);
            if (TestPlan != null)
            {
                ///删除编码表记录
                CodeRecordsService.DeleteCodeRecordsByDataId(TestPlan.TestPlanId);
                ////删除附件表
                CommonService.DeleteAttachFileById(TestPlan.TestPlanId);
                ///删除题目类型
                ServerTestPlanTrainingService.DeleteTestPlanTrainingByTestPlanId(TestPlan.TestPlanId);
                db.Test_TestPlan.DeleteOnSubmit(TestPlan);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 结束考试
        /// </summary>
        /// <returns></returns>
        public static string EndTestPlan(string testPlanId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string info = string.Empty;
            var getTestPlans = from x in db.Test_TestPlan
                               where x.States == Const.State_2 && x.TestEndTime.Value.AddMinutes(x.Duration.Value) < DateTime.Now
                               && (testPlanId == null || x.TestPlanId == testPlanId)
                               select x;
            foreach (var getTestPlan in getTestPlans)
            {
                string itemInfo = string.Empty;
                var getTestRecords = from x in db.Test_TestRecord
                                     where x.TestPlanId == testPlanId && (!x.TotalScore.HasValue || !x.TestEndTime.HasValue)
                                     select x;
                //// 考试下所有考试都交卷
                if (getTestRecords.Count() > 0)
                {
                    //// 扫码结束时间 + 考试时长 小于当前时间 考试结束 所有人自动交卷
                    if (getTestPlan.TestStartTime.Value.AddMinutes(getTestPlan.Duration.Value) < DateTime.Now)
                    {
                        foreach (var itemRecord in getTestRecords)
                        {
                            itemRecord.TestEndTime = itemRecord.TestStartTime.Value.AddMinutes(itemRecord.Duration.Value);
                            itemRecord.TestScores = db.Test_TestRecordItem.Where(x => x.TestRecordId == itemRecord.TestRecordId).Sum(x => x.SubjectScore) ?? 0;
                            db.SubmitChanges();
                        }
                    }
                    else
                    {
                        itemInfo = getTestPlan.PlanCode + "竞赛中还有未交卷人员，且未达到考试结束时间！";
                        info += itemInfo;
                    }
                }

                if (string.IsNullOrEmpty(itemInfo))
                {
                    getTestPlan.States = Const.State_3;
                    getTestPlan.ActualTime = DateTime.Now;
                    db.SubmitChanges();
                }
            }
            if (getTestPlans.Count() == 0 && !string.IsNullOrEmpty(testPlanId))
            {
                info = "当前竞赛还不到结束时间！";
            }
            return info;
        }
    }
}
