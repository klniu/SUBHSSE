﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class HazardListEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string HazardId
        {
            get
            {
                return (string)ViewState["HazardId"];
            }
            set
            {
                ViewState["HazardId"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string HazardListTypeId
        {
            get
            {
                return (string)ViewState["HazardListTypeId"];
            }
            set
            {
                ViewState["HazardListTypeId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ////权限按钮方法
                this.GetButtonPower();
                BLL.ConstValue.InitConstValueDropDownList(this.ddlHelperMethod, ConstValue.Group_0006, true);
                BLL.ConstValue.InitConstValueDropDownList(this.ddlHazardLevel, ConstValue.Group_0007, true);

                this.HazardId = Request.Params["HazardId"];
                this.HazardListTypeId = Request.Params["HazardListTypeId"];
                if (!string.IsNullOrEmpty(this.HazardId))
                {
                    var q = BLL.HazardListService.GetHazardListById(this.HazardId);
                    if (q != null)
                    {
                        this.txtHazardCode.Text = q.HazardCode;
                        this.txtHazardItems.Text = q.HazardItems;
                        this.txtDefectsType.Text = q.DefectsType;
                        this.txtMayLeadAccidents.Text = q.MayLeadAccidents;
                        if (q.HelperMethod != "null")
                        {
                            this.ddlHelperMethod.SelectedValue = q.HelperMethod;
                        }
                        if (q.HazardJudge_L != null)
                        {
                            this.txtHazardJudge_L.Text = Convert.ToString(q.HazardJudge_L);
                        }
                        if (q.HazardJudge_E != null)
                        {
                            this.txtHazardJudge_E.Text = Convert.ToString(q.HazardJudge_E);
                        }
                        if (q.HazardJudge_C != null)
                        {
                            this.txtHazardJudge_C.Text = Convert.ToString(q.HazardJudge_C);
                        }
                        if (q.HazardJudge_D != null)
                        {
                            this.txtHazardJudge_D.Text = Convert.ToString(q.HazardJudge_D);
                        }
                        if (q.HazardLevel != "0")
                        {
                            this.ddlHazardLevel.SelectedValue = q.HazardLevel;
                        }
                        this.txtControlMeasures.Text = q.ControlMeasures;
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string upState)
        {
            Model.Technique_HazardList hazardList = new Model.Technique_HazardList
            {
                HazardCode = this.txtHazardCode.Text.Trim(),
                HazardItems = this.txtHazardItems.Text.Trim(),
                DefectsType = this.txtDefectsType.Text.Trim(),
                MayLeadAccidents = this.txtMayLeadAccidents.Text.Trim()
            };
            if (this.ddlHelperMethod.SelectedValue != "null")
            {
                hazardList.HelperMethod = this.ddlHelperMethod.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.txtHazardJudge_L.Text.Trim()))
            {
                try
                {
                    hazardList.HazardJudge_L = Convert.ToDecimal(this.txtHazardJudge_L.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入格式不正确，作业条件危险性评价(L)必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(this.txtHazardJudge_E.Text.Trim()))
            {
                try
                {
                    hazardList.HazardJudge_E = Convert.ToDecimal(this.txtHazardJudge_E.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入格式不正确，作业条件危险性评价(E)必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(this.txtHazardJudge_C.Text.Trim()))
            {
                try
                {
                    hazardList.HazardJudge_C = Convert.ToDecimal(this.txtHazardJudge_C.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入格式不正确，作业条件危险性评价(C)必须是数字！", MessageBoxIcon.Warning);
                    return;
                }

            }
            if (!string.IsNullOrEmpty(this.txtHazardJudge_D.Text.Trim()))
            {
                try
                {
                    hazardList.HazardJudge_D = Convert.ToDecimal(this.txtHazardJudge_D.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入格式不正确，作业条件危险性评价(D)必须是数字！", MessageBoxIcon.Warning);
                    return;
                }

            }
            if (this.ddlHazardLevel.SelectedValue != "null")
            {
                hazardList.HazardLevel = this.ddlHazardLevel.SelectedValue;
            }
            hazardList.ControlMeasures = this.txtControlMeasures.Text.Trim();
            hazardList.UpState = upState;            
            if (string.IsNullOrEmpty(this.HazardId))
            {
                hazardList.CompileMan = this.CurrUser.UserName;
                hazardList.UnitId = CommonService.GetUnitId(this.CurrUser.UnitId);
                hazardList.CompileDate = DateTime.Now;
                hazardList.IsPass = true;
                hazardList.HazardId = SQLHelper.GetNewID(typeof(Model.Technique_HazardList));
                HazardId = hazardList.HazardId;
                hazardList.HazardListTypeId = this.HazardListTypeId;
                BLL.HazardListService.AddHazardList(hazardList);
                BLL.LogService.AddSys_Log(this.CurrUser, hazardList.HazardCode, hazardList.HazardId, BLL.Const.HazardListMenuId, Const.BtnAdd);
            }
            else
            {
                hazardList.HazardId = this.HazardId;
                Model.Technique_HazardList hazard = BLL.HazardListService.GetHazardListById(this.HazardId);
                if (hazard != null)
                {
                    hazardList.HazardListTypeId = hazard.HazardListTypeId;
                }
                BLL.HazardListService.UpdateHazardList(hazardList);
                BLL.LogService.AddSys_Log(this.CurrUser, hazardList.HazardCode, hazardList.HazardId, BLL.Const.HazardListMenuId, Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_1);
        }

        /// <summary>
        /// 保存并上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_2);
            var unit = BLL.CommonService.GetIsThisUnit();
            if (unit != null && !string.IsNullOrEmpty(unit.UnitId))
            {
                UpHazardList(HazardId, unit.UnitId);//上报
            }
        }
        #endregion

        #region 上报到集团公司
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="lawRegulation"></param>
        public void UpHazardList(string hazardId, string unitId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTechnique_HazardListTableCompleted += new EventHandler<HSSEService.DataInsertTechnique_HazardListTableCompletedEventArgs>(poxy_DataInsertTechnique_HazardListTableCompleted);
            var hazardListList = from x in Funs.DB.Technique_HazardList
                                 where x.HazardId == hazardId && x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                 select new HSSEService.Technique_HazardList
                                 {
                                     HazardId = x.HazardId,
                                     HazardListTypeId = x.HazardListTypeId,
                                     HazardCode = x.HazardCode,
                                     HazardItems = x.HazardItems,
                                     DefectsType = x.DefectsType,
                                     MayLeadAccidents = x.MayLeadAccidents,
                                     HelperMethod = x.HelperMethod,
                                     HazardJudge_L = x.HazardJudge_L,
                                     HazardJudge_E = x.HazardJudge_E,
                                     HazardJudge_C = x.HazardJudge_C,
                                     HazardJudge_D = x.HazardJudge_D,
                                     HazardLevel = x.HazardLevel,
                                     ControlMeasures = x.ControlMeasures,
                                     CompileMan = x.CompileMan,
                                     CompileDate = x.CompileDate,
                                     UnitId = unitId,
                                     IsPass = null,
                                 };
            poxy.DataInsertTechnique_HazardListTableAsync(hazardListList.ToList());
        }

        /// <summary>
        /// 危险源清单明细从子单位上报到集团单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_HazardListTableCompleted(object sender, HSSEService.DataInsertTechnique_HazardListTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var hazardList = BLL.HazardListService.GetHazardListById(item);
                    if (hazardList != null)
                    {
                        hazardList.UpState = BLL.Const.UpState_3;
                        BLL.HazardListService.UpdateHazardListIsPass(hazardList);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【危险源清单明细】上报到集团公司" + idList.Count.ToString() + "条数据；", string.Empty, BLL.Const.HazardListMenuId, Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【危险源清单明细】上报到集团公司失败；", string.Empty, BLL.Const.HazardListMenuId, Const.BtnUploadResources);
            }
        }
        #endregion

        #region 验证危险源代码是否存在
        /// <summary>
        /// 验证危险源名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_HazardList.FirstOrDefault(x => x.IsPass == true && x.HazardListTypeId == this.HazardListTypeId && x.HazardCode == this.txtHazardCode.Text.Trim() && (x.HazardId != this.HazardId || (this.HazardId == null && x.HazardId != null)));
            if (q != null)
            {
                ShowNotify("输入的危险源代码已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }

            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HazardListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSaveUp))
                {
                    this.btnSaveUp.Hidden = false;
                }
            }
        }
        #endregion
    }
}