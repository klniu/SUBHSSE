﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Text;
using System.IO;
using BLL;

namespace FineUIPro.Web.QualityAudit
{
    public partial class EquipmentQualityEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string EquipmentQualityId
        {
            get
            {
                return (string)ViewState["EquipmentQualityId"];
            }
            set
            {
                ViewState["EquipmentQualityId"] = value;
            }
        }
        /// <summary>
        /// 二维码路径id
        /// </summary>
        public string QRCodeAttachUrl
        {
            get
            {
                return (string)ViewState["QRCodeAttachUrl"];
            }
            set
            {
                ViewState["QRCodeAttachUrl"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
              
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, true);
                ///机具设备下拉框
                BLL.SpecialEquipmentService.InitSpecialEquipmentDropDownList(this.drpSpecialEquipmentId, true, true);
                this.EquipmentQualityId = Request.Params["EquipmentQualityId"];
                if (!string.IsNullOrEmpty(this.EquipmentQualityId))
                {
                    Model.QualityAudit_EquipmentQuality equipmentQuality = BLL.EquipmentQualityService.GetEquipmentQualityById(this.EquipmentQualityId);
                    if (equipmentQuality!=null)
                    {
                        this.txtEquipmentQualityCode.Text = CodeRecordsService.ReturnCodeByDataId(this.EquipmentQualityId);
                        if (!string.IsNullOrEmpty(equipmentQuality.UnitId))
                        {
                            this.drpUnitId.SelectedValue = equipmentQuality.UnitId;
                        }
                        if (!string.IsNullOrEmpty(equipmentQuality.SpecialEquipmentId))
                        {
                            this.drpSpecialEquipmentId.SelectedValue = equipmentQuality.SpecialEquipmentId;
                        }
                        this.txtEquipmentQualityName.Text = equipmentQuality.EquipmentQualityName;
                        this.txtSizeModel.Text = equipmentQuality.SizeModel;
                        this.txtFactoryCode.Text = equipmentQuality.FactoryCode;
                        this.txtCertificateCode.Text = equipmentQuality.CertificateCode;
                        if (equipmentQuality.CheckDate!=null)
                        {
                            this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentQuality.CheckDate);
                        }
                        if (equipmentQuality.LimitDate !=null)
                        {
                            this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentQuality.LimitDate);
                        }
                        if (equipmentQuality.InDate!=null)
                        {
                            this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentQuality.InDate);
                        }
                        if (equipmentQuality.OutDate!=null)
                        {
                            this.txtOutDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentQuality.OutDate);
                        }
                        this.txtApprovalPerson.Text = equipmentQuality.ApprovalPerson;
                        this.txtCarNum.Text = equipmentQuality.CarNum;
                        this.txtRemark.Text = equipmentQuality.Remark;
                    }
                }
                else
                {
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    ////自动生成编码
                    this.txtEquipmentQualityCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.EquipmentQualityMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                }

                if (Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden = true;
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
            if (this.drpUnitId.SelectedValue==BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpSpecialEquipmentId.SelectedValue==BLL.Const._Null)
            {
                Alert.ShowInTop("请选择特种机具设备类型", MessageBoxIcon.Warning);
                return;
            }
            SaveData(true);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose)
        {
            Model.QualityAudit_EquipmentQuality equipmentQuality = new Model.QualityAudit_EquipmentQuality
            {
                ProjectId = this.CurrUser.LoginProjectId,
                EquipmentQualityCode = this.txtEquipmentQualityCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue!=BLL.Const._Null)
            {
                equipmentQuality.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpSpecialEquipmentId.SelectedValue!=BLL.Const._Null)
            {
                equipmentQuality.SpecialEquipmentId = this.drpSpecialEquipmentId.SelectedValue;
            }
            equipmentQuality.EquipmentQualityName = this.txtEquipmentQualityName.Text.Trim();
            equipmentQuality.SizeModel = this.txtSizeModel.Text.Trim();
            equipmentQuality.FactoryCode = this.txtFactoryCode.Text.Trim();
            equipmentQuality.CertificateCode = this.txtCertificateCode.Text.Trim();
            equipmentQuality.CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
            equipmentQuality.LimitDate = Convert.ToDateTime(this.txtLimitDate.Text.Trim());
            equipmentQuality.InDate = Funs.GetNewDateTime(this.txtInDate.Text.Trim());
            equipmentQuality.OutDate = Funs.GetNewDateTime(this.txtOutDate.Text.Trim());
            equipmentQuality.ApprovalPerson = this.txtApprovalPerson.Text.Trim();
            equipmentQuality.CarNum = this.txtCarNum.Text.Trim();
            equipmentQuality.Remark = this.txtRemark.Text.Trim();
            equipmentQuality.CompileMan = this.CurrUser.UserId;
            equipmentQuality.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(EquipmentQualityId))
            {
                equipmentQuality.EquipmentQualityId = this.EquipmentQualityId;
                BLL.EquipmentQualityService.UpdateEquipmentQuality(equipmentQuality);
                BLL.LogService.AddSys_Log(this.CurrUser, equipmentQuality.EquipmentQualityCode, equipmentQuality.EquipmentQualityId,BLL.Const.EquipmentQualityMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.EquipmentQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_EquipmentQuality));
                equipmentQuality.EquipmentQualityId = this.EquipmentQualityId;
                BLL.EquipmentQualityService.AddEquipmentQuality(equipmentQuality);
                BLL.LogService.AddSys_Log(this.CurrUser, equipmentQuality.EquipmentQualityCode, equipmentQuality.EquipmentQualityId, BLL.Const.EquipmentQualityMenuId, BLL.Const.BtnAdd);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
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
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentQualityAttachUrl&menuId={1}type=-1", EquipmentQualityId, BLL.Const.EquipmentQualityMenuId)));
            }
            else
            {
                if (this.drpUnitId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                    return;
                }
                if (this.drpSpecialEquipmentId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择特种机具设备类型", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.EquipmentQualityId))
                {
                    SaveData(false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentQualityAttachUrl&menuId={1}", EquipmentQualityId, BLL.Const.EquipmentQualityMenuId)));
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFactoryCode.Text.Trim()))
            {
                Alert.ShowInTop("出厂编号不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpSpecialEquipmentId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择特种机具设备类型", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.EquipmentQualityId))
            {
                this.SaveData(false);
            }
            this.CreateCode_Simple(this.txtFactoryCode.Text.Trim());
        }

        //生成二维码方法一
        private void CreateCode_Simple(string nr)
        {
            var equipmentQuality = BLL.EquipmentQualityService.GetEquipmentQualityById(this.EquipmentQualityId);
            if (equipmentQuality != null)
            {
                BLL.UploadFileService.DeleteFile(Funs.RootPath, equipmentQuality.QRCodeAttachUrl);//删除二维码
                string imageUrl = string.Empty;
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = nr.Length;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                System.Drawing.Image image = qrCodeEncoder.Encode(nr, Encoding.UTF8);

                string filepath = Server.MapPath("~/") + BLL.UploadFileService.QRCodeImageFilePath;

                //如果文件夹不存在，则创建
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
                imageUrl = filepath + filename;

                System.IO.FileStream fs = new System.IO.FileStream(imageUrl, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

                fs.Close();
                image.Dispose();
                this.QRCodeAttachUrl = BLL.UploadFileService.QRCodeImageFilePath + filename;
                equipmentQuality.QRCodeAttachUrl = this.QRCodeAttachUrl;
                BLL.EquipmentQualityService.UpdateEquipmentQuality(equipmentQuality);
                this.btnQR.Hidden = false;
                this.btnQR.Text = "二维码重新生成";
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../Controls/SeeQRImage.aspx?EquipmentQualityId={0}", this.EquipmentQualityId), "二维码查看", 400, 400));
            }
            else
            {
                Alert.ShowInTop("操作有误，重新生成！", MessageBoxIcon.Warning);
            }

            //二维码解码
            //var codeDecoder = CodeDecoder(filepath);

            //this.Image1.ImageUrl = "~/image/" + filename + ".jpg";
        }
    }
}