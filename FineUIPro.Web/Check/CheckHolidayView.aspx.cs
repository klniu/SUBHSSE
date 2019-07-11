﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Check
{
    public partial class CheckHolidayView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckHolidayId
        {
            get
            {
                return (string)ViewState["CheckHolidayId"];
            }
            set
            {
                ViewState["CheckHolidayId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_Check_CheckHolidayDetail> checkHolidayDetails = new List<Model.View_Check_CheckHolidayDetail>();
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
                hdAttachUrl.Text = string.Empty;
                hdId.Text = string.Empty;
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                List<Model.Base_Unit> thisUnit = BLL.UnitService.GetThisUnitDropDownList();
                string thisUnitId = string.Empty;
                string thisUnitName = string.Empty;
                if (thisUnit.Count > 0)
                {
                    thisUnitId = thisUnit[0].UnitId;
                    this.txtThisUnit.Text = thisUnit[0].UnitName;
                    this.txtMainUnitDeputy.Label = thisUnit[0].UnitName;
                }

                checkHolidayDetails.Clear();

                this.CheckHolidayId = Request.Params["CheckHolidayId"];
                var checkHoliday = BLL.Check_CheckHolidayService.GetCheckHolidayByCheckHolidayId(this.CheckHolidayId);
                if (checkHoliday != null)
                {
                    this.txtCheckHolidayCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckHolidayId);
                    if (checkHoliday.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkHoliday.CheckTime);
                    }
                    this.txtArea.Text = checkHoliday.Area;
                    if (!string.IsNullOrEmpty(checkHoliday.MainUnitPerson))
                    {
                        string personNames = string.Empty;
                        string[] unitIds = checkHoliday.MainUnitPerson.Split(',');
                        foreach (var item in unitIds)
                        {
                            Model.Sys_User user = BLL.UserService.GetUserByUserId(item);
                            if (user != null)
                            {
                                personNames += user.UserName + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(personNames))
                        {
                            personNames = personNames.Substring(0, personNames.LastIndexOf(","));
                        }
                        this.txtMainUnitPerson.Text = personNames;
                    }
                    if (!string.IsNullOrEmpty(checkHoliday.SubUnits))
                    {
                        string unitNames = string.Empty;
                        foreach (var item in checkHoliday.SubUnits.Split(','))
                        {
                            string name = BLL.UnitService.GetUnitNameByUnitId(item);
                            if (!string.IsNullOrEmpty(name))
                            {
                                unitNames += name + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(unitNames))
                        {
                            this.txtSubUnits.Text = unitNames.Substring(0, unitNames.LastIndexOf(","));
                        }
                        if (!string.IsNullOrEmpty(checkHoliday.SubUnitPerson))
                        {
                            string personNames = string.Empty;
                            foreach (var item in checkHoliday.SubUnitPerson.Split(','))
                            {
                                personNames += BLL.UserService.GetUserNameByUserId(item) + ",";
                            }
                            if (!string.IsNullOrEmpty(personNames))
                            {
                                this.txtSubUnitPerson.Text = personNames.Substring(0, personNames.LastIndexOf(","));
                            }
                        }
                    }
                    this.txtPartInPersonNames.Text = checkHoliday.PartInPersonNames;
                    if (checkHoliday.IsCompleted == true)
                    {
                        this.lbIsCompleted.Text = "已闭环";
                    }
                    else
                    {
                        this.lbIsCompleted.Text = "未闭环";
                    }
                    this.txtMainUnitDeputy.Text = checkHoliday.MainUnitDeputy;
                    if (checkHoliday.MainUnitDeputyDate != null)
                    {
                        this.txtMainUnitDeputyDate.Text = string.Format("{0:yyyy-MM-dd}", checkHoliday.MainUnitDeputyDate);
                    }
                    this.txtSubUnitDeputy.Text = checkHoliday.SubUnitDeputy;
                    if (checkHoliday.SubUnitDeputyDate != null)
                    {
                        this.txtSubUnitDeputyDate.Text = string.Format("{0:yyyy-MM-dd}", checkHoliday.SubUnitDeputyDate);
                    }
                    checkHolidayDetails = (from x in Funs.DB.View_Check_CheckHolidayDetail where x.CheckHolidayId == this.CheckHolidayId orderby x.CheckItem select x).ToList();
                }
                
                Grid1.DataSource = checkHolidayDetails;
                Grid1.DataBind();
                ChangeGridColor();
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCheckHolidayMenuId;
                this.ctlAuditFlow.DataId = this.CheckHolidayId;
            }
        }
        #endregion

        #region 改变Grid颜色
        private void ChangeGridColor()
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(Grid1.Rows[i].Values[5].ToString()))
                {
                    Grid1.Rows[i].RowCssClass = "red";
                }
                else if (string.IsNullOrEmpty(Grid1.Rows[i].Values[6].ToString()))
                {
                    Grid1.Rows[i].RowCssClass = "yellow";
                }
            }
        }
        #endregion

        #region 获取检查类型
        /// <summary>
        /// 获取检查类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCheckItemType(object CheckItem)
        {
            return BLL.Check_ProjectCheckItemSetService.ConvertCheckItemType(CheckItem);
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CheckHolidayId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckHoliday&menuId={1}&type=-1", this.CheckHolidayId, BLL.Const.ProjectCheckHolidayMenuId)));
            }
            
        }
        #endregion
    }
}