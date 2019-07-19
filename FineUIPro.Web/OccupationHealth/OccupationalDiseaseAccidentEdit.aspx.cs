﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.OccupationHealth
{
    public partial class OccupationalDiseaseAccidentEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string FileId
        {
            get
            {
                return (string)ViewState["FileId"];
            }
            set
            {
                ViewState["FileId"] = value;
            }
        }

        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.CurrUser.LoginProjectId, true);
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.FileId = Request.Params["FileId"];
                if (!string.IsNullOrEmpty(this.FileId))
                {
                    Model.OccupationHealth_OccupationalDiseaseAccident OccupationalDiseaseAccident = BLL.OccupationalDiseaseAccidentService.GetOccupationalDiseaseAccidentById(this.FileId);
                    if (OccupationalDiseaseAccident != null)
                    {
                        this.ProjectId = OccupationalDiseaseAccident.ProjectId;
                        this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.FileId);
                        this.txtFileName.Text = OccupationalDiseaseAccident.FileName;                        
                        if (!string.IsNullOrEmpty(OccupationalDiseaseAccident.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = OccupationalDiseaseAccident.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", OccupationalDiseaseAccident.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(OccupationalDiseaseAccident.FileContent);
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.OccupationalDiseaseAccidentMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;                    
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);                  
                    this.txtFileName.Text = this.SimpleForm1.Title;
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.OccupationalDiseaseAccidentMenuId;
                this.ctlAuditFlow.DataId = this.FileId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.OccupationHealth_OccupationalDiseaseAccident OccupationalDiseaseAccident = new Model.OccupationHealth_OccupationalDiseaseAccident
            {
                FileCode = this.txtFileCode.Text.Trim(),
                FileName = this.txtFileName.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                OccupationalDiseaseAccident.CompileMan = this.drpCompileMan.SelectedValue;
            }
            OccupationalDiseaseAccident.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            OccupationalDiseaseAccident.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            ////单据状态
            OccupationalDiseaseAccident.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                OccupationalDiseaseAccident.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.FileId))
            {
                OccupationalDiseaseAccident.FileId = this.FileId;
                BLL.OccupationalDiseaseAccidentService.UpdateOccupationalDiseaseAccident(OccupationalDiseaseAccident);
                BLL.LogService.AddSys_Log(this.CurrUser, OccupationalDiseaseAccident.FileCode, OccupationalDiseaseAccident.FileId, BLL.Const.OccupationalDiseaseAccidentMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.FileId = SQLHelper.GetNewID(typeof(Model.OccupationHealth_OccupationalDiseaseAccident));
                OccupationalDiseaseAccident.FileId = this.FileId;
                OccupationalDiseaseAccident.ProjectId = this.ProjectId;
                BLL.OccupationalDiseaseAccidentService.AddOccupationalDiseaseAccident(OccupationalDiseaseAccident);
                BLL.LogService.AddSys_Log(this.CurrUser, OccupationalDiseaseAccident.FileCode, OccupationalDiseaseAccident.FileId, BLL.Const.OccupationalDiseaseAccidentMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.OccupationalDiseaseAccidentMenuId, this.FileId, (type == BLL.Const.BtnSubmit ? true : false), this.txtFileName.Text.Trim(), "../OccupationHealth/OccupationalDiseaseAccidentView.aspx?FileId={0}");             
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
            if (string.IsNullOrEmpty(this.FileId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/OccupationalDiseaseAccidentAttachUrl&menuId={1}", FileId, BLL.Const.OccupationalDiseaseAccidentMenuId)));
        }
        #endregion
    }
}