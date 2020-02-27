﻿using BLL;
using System;
using System.IO;
using System.Text;
using System.Web.UI;
using ThoughtWorks.QRCode.Codec;

namespace FineUIPro.Web.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SeeQRImage : Page
    {
        #region QRCodeAttachUrl
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

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ShowQRCode();
            }
        }

        /// <summary>
        ///  二维码显示
        /// </summary>
        private void ShowQRCode()
        {
            if (!string.IsNullOrEmpty(Request.Params["PersonId"]))
            {
                var person = BLL.PersonService.GetPersonById(Request.Params["PersonId"]);
                if (person != null)
                {
                    if (!string.IsNullOrEmpty(person.QRCodeAttachUrl) && CreateQRCodeService.isHaveImage(person.QRCodeAttachUrl))
                    {
                        this.QRCodeAttachUrl = person.QRCodeAttachUrl;
                    }
                    else
                    {
                        this.CreateCode_Simple(Request.Params["strCode"]);
                        person.QRCodeAttachUrl = this.QRCodeAttachUrl;
                        BLL.PersonService.UpdatePerson(person);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["EquipmentQualityId"]))
            {
                var equipmentQuality = BLL.EquipmentQualityService.GetEquipmentQualityById(Request.Params["EquipmentQualityId"]);
                if (equipmentQuality != null)
                {
                    if (!string.IsNullOrEmpty(equipmentQuality.QRCodeAttachUrl) && CreateQRCodeService.isHaveImage(equipmentQuality.QRCodeAttachUrl))
                    {
                        this.QRCodeAttachUrl = equipmentQuality.QRCodeAttachUrl;
                    }
                    else
                    {
                        this.CreateCode_Simple(Request.Params["strCode"]);
                        equipmentQuality.QRCodeAttachUrl = this.QRCodeAttachUrl;
                        BLL.EquipmentQualityService.UpdateEquipmentQuality(equipmentQuality);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["GeneralEquipmentQualityId"]))
            {
                var generalEquipmentQuality = BLL.GeneralEquipmentQualityService.GetGeneralEquipmentQualityById(Request.Params["GeneralEquipmentQualityId"]);
                if (generalEquipmentQuality != null)
                {
                    if (!string.IsNullOrEmpty(generalEquipmentQuality.QRCodeAttachUrl) && CreateQRCodeService.isHaveImage(generalEquipmentQuality.QRCodeAttachUrl))
                    {
                        this.QRCodeAttachUrl = generalEquipmentQuality.QRCodeAttachUrl;
                    }
                    else
                    {
                        this.CreateCode_Simple(Request.Params["strCode"]);
                        generalEquipmentQuality.QRCodeAttachUrl = this.QRCodeAttachUrl;
                        BLL.GeneralEquipmentQualityService.UpdateGeneralEquipmentQuality(generalEquipmentQuality);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["ConstructSolutionId"]))
            {
                var constructSolution = BLL.ConstructSolutionService.GetConstructSolutionById(Request.Params["ConstructSolutionId"]);
                if (constructSolution != null)
                {
                    if (!string.IsNullOrEmpty(constructSolution.QRCodeAttachUrl) && CreateQRCodeService.isHaveImage(constructSolution.QRCodeAttachUrl))
                    {
                        this.QRCodeAttachUrl = constructSolution.QRCodeAttachUrl;
                    }
                    else
                    {
                        this.CreateCode_Simple(Request.Params["strCode"]);
                        constructSolution.QRCodeAttachUrl = this.QRCodeAttachUrl;
                        ConstructSolutionService.UpdateConstructSolution(constructSolution);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["TrainingPlanId"]))
            {
                var trainingPlan = BLL.TrainingPlanService.GetPlanById(Request.Params["TrainingPlanId"]);
                if (trainingPlan != null)
                {
                    if (!string.IsNullOrEmpty(trainingPlan.QRCodeUrl) && CreateQRCodeService.isHaveImage(trainingPlan.QRCodeUrl))
                    {
                        this.QRCodeAttachUrl = trainingPlan.QRCodeUrl;
                    }
                    else
                    {
                        this.CreateCode_Simple(Request.Params["strCode"]);
                        trainingPlan.QRCodeUrl = this.QRCodeAttachUrl;
                        TrainingPlanService.UpdatePlan(trainingPlan);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["TestPlanId"]))
            {
                var testPlan = BLL.TestPlanService.GetTestPlanById(Request.Params["TestPlanId"]);
                if (testPlan != null)
                {
                    if (!string.IsNullOrEmpty(testPlan.QRCodeUrl) && CreateQRCodeService.isHaveImage(testPlan.QRCodeUrl))
                    {
                        this.QRCodeAttachUrl = testPlan.QRCodeUrl;
                    }
                    else
                    {
                        this.CreateCode_Simple(Request.Params["strCode"]);
                        testPlan.QRCodeUrl = this.QRCodeAttachUrl;
                        TestPlanService.UpdateTestPlan(testPlan);
                    }
                }
            }

            this.Image1.ImageUrl = "~/" + this.QRCodeAttachUrl;            
        }

        /// <summary>
        /// 生成二维码方法一
        /// </summary>
        private void CreateCode_Simple(string nr)
        {
            try
            {
                string imageUrl = string.Empty;
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder
                {
                    QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                    QRCodeScale = nr.Length,
                    QRCodeVersion = 0,
                    QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
                };
                System.Drawing.Image image = qrCodeEncoder.Encode(nr, Encoding.UTF8);
                string filepath = Server.MapPath("~/") + UploadFileService.QRCodeImageFilePath;
                //如果文件夹不存在，则创建
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
                imageUrl = filepath + filename;
                FileStream fs = new FileStream(imageUrl, FileMode.OpenOrCreate, FileAccess.Write);
                image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Close();
                image.Dispose();
                this.QRCodeAttachUrl = UploadFileService.QRCodeImageFilePath + filename;
            }
            catch (Exception ex)
            {
                Alert.ShowInTop("操作有误，重新生成！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 重新生成二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReSave_Click(object sender, EventArgs e)
        {
            UploadFileService.DeleteFile(Funs.RootPath, this.QRCodeAttachUrl);//删除二维码
            this.QRCodeAttachUrl = string.Empty;
            this.ShowQRCode();
        }
   
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Params["PersonId"]))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("QRCodePrint.aspx?PersonId={0}", Request.Params["PersonId"], "打印 - ")));
            }
           else if (!string.IsNullOrEmpty(Request.Params["EquipmentQualityId"]))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("QRCodePrint.aspx?EquipmentQualityId={0}", Request.Params["EquipmentQualityId"], "打印 - ")));
            }
            else if (!string.IsNullOrEmpty(Request.Params["GeneralEquipmentQualityId"]))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("QRCodePrint.aspx?GeneralEquipmentQualityId={0}", Request.Params["GeneralEquipmentQualityId"], "打印 - ")));
            }
            else if(!string.IsNullOrEmpty(Request.Params["ConstructSolutionId"]))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("QRCodePrint.aspx?ConstructSolutionId={0}", Request.Params["ConstructSolutionId"], "打印 - ")));
            }
            else if(!string.IsNullOrEmpty(Request.Params["TrainingPlanId"]))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("QRCodePrint.aspx?TrainingPlanId={0}", Request.Params["TrainingPlanId"], "打印 - ")));
            }
            else if (!string.IsNullOrEmpty(Request.Params["TestPlanId"]))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("QRCodePrint.aspx?TestPlanId={0}", Request.Params["TestPlanId"], "打印 - ")));
            }
        }
    }
}