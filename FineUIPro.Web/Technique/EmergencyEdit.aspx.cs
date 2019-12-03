﻿using BLL;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Technique
{
    public partial class EmergencyEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 安全评价
        /// </summary>
        public string EmergencyId
        {
            get
            {
                return (string)ViewState["EmergencyId"];
            }
            set
            {
                ViewState["EmergencyId"] = value;
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
                //应急类型下拉选项
                BLL.EmergencyTypeService.InitEmergencyTypeDropDownList(this.ddlEmergencyType, true);

                this.EmergencyId = Request.Params["EmergencyId"];
                if (!string.IsNullOrEmpty(this.EmergencyId))
                {
                    var Emergency = BLL.EmergencyService.GetEmergencyListById(this.EmergencyId);
                    if (Emergency != null)
                    {
                        this.txtEmergencyCode.Text = Emergency.EmergencyCode;
                        this.txtEmergencyName.Text = Emergency.EmergencyName;
                        this.txtSummary.Text = Emergency.Summary;
                        if (!string.IsNullOrEmpty(Emergency.EmergencyTypeId))
                        {
                            this.ddlEmergencyType.SelectedValue = Emergency.EmergencyTypeId;
                        }
                        this.txtRemark.Text = Emergency.Remark;                      
                    }
                }
            }
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
            SaveData(BLL.Const.UpState_1);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
                UpEmergency(this.EmergencyId, unit.UnitId);//上报
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string upState)
        {
            Model.Technique_Emergency emergency = new Model.Technique_Emergency
            {
                UpState = upState,
                EmergencyCode = this.txtEmergencyCode.Text.Trim(),
                EmergencyName = this.txtEmergencyName.Text.Trim(),
                Summary = this.txtSummary.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };
            ////应急预案类型下拉框
            if (this.ddlEmergencyType.SelectedValue != BLL.Const._Null)
            {
                if (!string.IsNullOrEmpty(this.ddlEmergencyType.SelectedValue))
                {
                    emergency.EmergencyTypeId = this.ddlEmergencyType.SelectedValue;
                }
                else
                {
                    var emergencyType = BLL.EmergencyTypeService.GetEmergencyTypeByName(this.ddlEmergencyType.Text);
                    if (emergencyType != null)
                    {
                        emergency.EmergencyTypeId = emergencyType.EmergencyTypeId;
                    }
                    else
                    {
                        Model.Base_EmergencyType newEmergencyType = new Model.Base_EmergencyType
                        {
                            EmergencyTypeId = SQLHelper.GetNewID(typeof(Model.Base_EmergencyType)),
                            EmergencyTypeName = this.ddlEmergencyType.Text
                        };
                        BLL.EmergencyTypeService.AddEmergencyType(newEmergencyType);
                        emergency.EmergencyTypeId = newEmergencyType.EmergencyTypeId;
                    }
                }
            }

            if (string.IsNullOrEmpty(this.EmergencyId))
            {
                emergency.CompileMan = this.CurrUser.UserName;
                emergency.UnitId = this.CurrUser.UnitId;
                emergency.CompileDate = DateTime.Now;
                emergency.IsPass = true;
                this.EmergencyId = emergency.EmergencyId = SQLHelper.GetNewID(typeof(Model.Technique_Emergency));
                BLL.EmergencyService.AddEmergencyList(emergency);
                BLL.LogService.AddSys_Log(this.CurrUser, emergency.EmergencyCode, emergency.EmergencyId, BLL.Const.EmergencyMenuId, Const.BtnAdd);
            }
            else
            {
                emergency.EmergencyId = this.EmergencyId;
                BLL.EmergencyService.UpdateEmergencyList(emergency);
                BLL.LogService.AddSys_Log(this.CurrUser, emergency.EmergencyCode, emergency.EmergencyId, BLL.Const.EmergencyMenuId, Const.BtnModify);
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Emergency&type=-1", this.EmergencyId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.EmergencyId))
                {
                    SaveData(BLL.Const.UpState_1);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Emergency&menuId={1}", this.EmergencyId, BLL.Const.EmergencyMenuId)));
            }
        }
        #endregion

        #region 应急预案上报到集团公司
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emergencyId"></param>
        /// <param name="unitId"></param>
        private void UpEmergency(string emergencyId, string unitId)
        {
            ////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTechnique_EmergencyTableCompleted += new EventHandler<HSSEService.DataInsertTechnique_EmergencyTableCompletedEventArgs>(poxy_DataInsertTechnique_EmergencyTableCompleted);
            var emergency = from x in Funs.DB.View_Technique_Emergency
                            join y in Funs.DB.AttachFile on x.EmergencyId equals y.ToKeyId
                            where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                            select new HSSEService.Technique_Emergency
                            {
                                EmergencyId = x.EmergencyId,
                                EmergencyTypeId = x.EmergencyTypeId,
                                EmergencyCode = x.EmergencyCode,
                                EmergencyName = x.EmergencyName,
                                Summary = x.Summary,
                                Remark = x.Remark,
                                CompileMan = x.CompileMan,
                                CompileDate = x.CompileDate,
                                IsPass = null,
                                UnitId = unitId,
                                AttachFileId = y.AttachFileId,
                                ToKeyId = y.ToKeyId,
                                AttachSource = y.AttachSource,
                                AttachUrl = y.AttachUrl,
                                ////附件转为字节传送
                                FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                            };
            poxy.DataInsertTechnique_EmergencyTableAsync(emergency.ToList());
        }

        /// <summary>
        /// 应急预案上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_EmergencyTableCompleted(object sender, HSSEService.DataInsertTechnique_EmergencyTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var emergency = BLL.EmergencyService.GetEmergencyListById(item);
                    if (emergency != null)
                    {
                        emergency.UpState = BLL.Const.UpState_3;
                        BLL.EmergencyService.UpdateEmergencyList(emergency);
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, "【应急预案】上报到集团公司" + idList.Count.ToString() + "条数据；", 
                    string.Empty, BLL.Const.EmergencyMenuId, Const.BtnUploadResources);
            }
            else
            {
                
                BLL.LogService.AddSys_Log(this.CurrUser, "【应急预案】上报到集团公司失败；", string.Empty, BLL.Const.EmergencyMenuId, Const.BtnUploadResources);
            }
        }
        #endregion

        #region 验证编号、名称否存在
        /// <summary>
        /// 验证编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_Emergency.FirstOrDefault(x => x.IsPass == true && x.EmergencyCode == this.txtEmergencyCode.Text.Trim() && (x.EmergencyId != this.EmergencyId || (this.EmergencyId == null && x.EmergencyId != null)));
            if (q != null)
            {
                ShowNotify("输入的应急预案编号已存在！", MessageBoxIcon.Warning);
            }
            var q2 = Funs.DB.Technique_Emergency.FirstOrDefault(x => x.IsPass == true && x.EmergencyName == this.txtEmergencyName.Text.Trim() && (x.EmergencyId != this.EmergencyId || (this.EmergencyId == null && x.EmergencyId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的应急预案名称已存在！", MessageBoxIcon.Warning);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.EmergencyMenuId);
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