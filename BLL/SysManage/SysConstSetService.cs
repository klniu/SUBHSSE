﻿namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class SysConstSetService
    {
        /// <summary>
        /// 系统环境设置 菜单模式
        /// </summary>
        public const int SysSet_0 = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        public static bool? IsAuto(int setId)
        {
            var q = Funs.DB.Sys_Set.FirstOrDefault(x => x.SetId == setId);
            if (q != null)
            {
                return q.IsAuto;

            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        public static Model.Sys_Set GetSysSet(int setId)
        {
            return Funs.DB.Sys_Set.FirstOrDefault(x => x.SetId == setId);
        }

        /// <summary>
        /// 获取及格分数
        /// </summary>
        /// <returns></returns>
        public static int getPassScore()
        {
            int passScore = 80;
            var testRule = Funs.DB.Sys_TestRule.FirstOrDefault();
            if (testRule != null)
            {
                passScore = testRule.PassingScore;
            }
            return passScore;
        }

        #region 菜单编码模板
        /// <summary>
        /// 获取菜单编码模板信息 根据MenuId
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static Model.Sys_CodeTemplateRule GetCodeTemplateRuleByMenuId(string menuId)
        {
            return Funs.DB.Sys_CodeTemplateRule.FirstOrDefault(x => x.MenuId == menuId);
        }
        
        /// <summary>
        /// 菜单编码模板信息
        /// </summary>
        /// <param name="codeTemplateRule"></param>
        public static void AddCodeTemplateRule(Model.Sys_CodeTemplateRule codeTemplateRule)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Sys_CodeTemplateRule newCodeTemplateRule = new Model.Sys_CodeTemplateRule
            {
                CodeTemplateRuleId = SQLHelper.GetNewID(typeof(Model.Sys_CodeTemplateRule)),
                MenuId = codeTemplateRule.MenuId,
                Template = codeTemplateRule.Template,
                Symbol = codeTemplateRule.Symbol,
                IsProjectCode = codeTemplateRule.IsProjectCode,
                Prefix = codeTemplateRule.Prefix,
                IsUnitCode = codeTemplateRule.IsUnitCode,
                Digit = codeTemplateRule.Digit,
                IsFileCabinetA = codeTemplateRule.IsFileCabinetA,
                IsFileCabinetB = codeTemplateRule.IsFileCabinetB
            };
            db.Sys_CodeTemplateRule.InsertOnSubmit(newCodeTemplateRule);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改菜单编码模板信息
        /// </summary>
        /// <param name="codeTemplateRule"></param>
        public static void UpdateCodeTemplateRule(Model.Sys_CodeTemplateRule codeTemplateRule)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Sys_CodeTemplateRule updateCodeTemplateRule = db.Sys_CodeTemplateRule.FirstOrDefault(e => e.MenuId == codeTemplateRule.MenuId);
            if (updateCodeTemplateRule != null)
            {
                updateCodeTemplateRule.Template = codeTemplateRule.Template;
                updateCodeTemplateRule.Symbol = codeTemplateRule.Symbol;
                updateCodeTemplateRule.IsProjectCode = codeTemplateRule.IsProjectCode;
                updateCodeTemplateRule.Prefix = codeTemplateRule.Prefix;
                updateCodeTemplateRule.IsUnitCode = codeTemplateRule.IsUnitCode;
                updateCodeTemplateRule.Digit = codeTemplateRule.Digit;
                updateCodeTemplateRule.IsFileCabinetA = codeTemplateRule.IsFileCabinetA;
                updateCodeTemplateRule.IsFileCabinetB = codeTemplateRule.IsFileCabinetB;   
                db.SubmitChanges();
            }
        }
        #endregion

        #region 业务审批流程
        /// <summary>
        /// 获取业务审批流程信息 根据MenuId
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static Model.Sys_MenuFlowOperate GetMenuFlowOperateByMenuId(string menuId)
        {
            return Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(x => x.MenuId == menuId);
        }

        /// <summary>
        /// 业务审批流程信息
        /// </summary>
        /// <param name="MenuFlowOperate"></param>
        public static void AddMenuFlowOperate(Model.Sys_MenuFlowOperate MenuFlowOperate)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Sys_MenuFlowOperate newMenuFlowOperate = new Model.Sys_MenuFlowOperate
            {
                FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_MenuFlowOperate)),
                MenuId = MenuFlowOperate.MenuId,
                FlowStep = MenuFlowOperate.FlowStep,
                AuditFlowName = MenuFlowOperate.AuditFlowName,
                RoleId = MenuFlowOperate.RoleId,
                IsFlowEnd = MenuFlowOperate.IsFlowEnd
            };
            db.Sys_MenuFlowOperate.InsertOnSubmit(newMenuFlowOperate);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改业务审批流程信息
        /// </summary>
        /// <param name="MenuFlowOperate"></param>
        public static void UpdateMenuFlowOperate(Model.Sys_MenuFlowOperate MenuFlowOperate)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Sys_MenuFlowOperate updateMenuFlowOperate = db.Sys_MenuFlowOperate.FirstOrDefault(e => e.MenuId == MenuFlowOperate.MenuId);
            if (updateMenuFlowOperate != null)
            {
                updateMenuFlowOperate.MenuId = MenuFlowOperate.MenuId;
                updateMenuFlowOperate.FlowStep = MenuFlowOperate.FlowStep;
                updateMenuFlowOperate.AuditFlowName = MenuFlowOperate.AuditFlowName;
                updateMenuFlowOperate.RoleId = MenuFlowOperate.RoleId;
                updateMenuFlowOperate.IsFlowEnd = MenuFlowOperate.IsFlowEnd;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除工作流信息
        /// </summary>
        /// <param name="audiFlowId"></param>
        public static void DeleteMenuFlowOperateByFlowOperateId(string flowOperateId)
        {
            var flow = Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(e => e.FlowOperateId == flowOperateId);
            if (flow != null)
            {
                Funs.DB.Sys_MenuFlowOperate.DeleteOnSubmit(flow);
                Funs.DB.SubmitChanges();
            }
        }

        #region 删除审核步骤
        /// <summary>
        /// 删除审核步骤
        /// </summary>
        /// <param name="flowOperateId">主键ID</param>
        /// <returns></returns>
        public static void DeleteMenuFlowOperateLicense(string flowOperateId)
        {
            var delteFlow = Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(x => x.FlowOperateId == flowOperateId);
            if (delteFlow != null)
            {
                var isSort = Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(x => x.FlowStep == delteFlow.FlowStep);
                if (isSort == null)
                {
                    var updateSort = from x in Funs.DB.Sys_MenuFlowOperate
                                     where  x.FlowStep > delteFlow.FlowStep
                                     select x;
                    foreach (var item in updateSort)
                    {
                        item.FlowStep -= 1;
                    }
                }
                else
                {
                    var isGroup = Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(x => x.FlowStep == delteFlow.FlowStep && x.GroupNum == delteFlow.GroupNum);
                    if (isGroup == null)
                    {
                        var updateGroup = from x in Funs.DB.Sys_MenuFlowOperate
                                          where x.FlowStep == delteFlow.FlowStep && x.GroupNum > delteFlow.GroupNum
                                          select x;
                        foreach (var item in updateGroup)
                        {
                            item.GroupNum -= 1;
                        }
                    }
                    else
                    {
                        var isOrder = Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(x =>  x.FlowStep == delteFlow.FlowStep && x.GroupNum == delteFlow.GroupNum && x.OrderNum > delteFlow.OrderNum);
                        if (isOrder != null)
                        {
                            isOrder.OrderNum -= 1;
                        }
                    }
                }

                Funs.DB.Sys_MenuFlowOperate.DeleteOnSubmit(delteFlow);
                Funs.SubmitChanges();
            }
        }
        #endregion
        #endregion
    }
}
