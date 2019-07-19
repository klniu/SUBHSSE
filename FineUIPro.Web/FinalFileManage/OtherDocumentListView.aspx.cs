﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.FinalFileManage
{
    public partial class OtherDocumentListView : PageBase
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
                this.FileId = Request.Params["FileId"];
                if (!string.IsNullOrEmpty(this.FileId))
                {
                    Model.FinalFileManage_OtherDocumentList OtherDocumentList = BLL.OtherDocumentListService.GetOtherDocumentListById(this.FileId);
                    if (OtherDocumentList != null)
                    {
                        ///读取编号
                        this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.FileId);
                        this.txtFileName.Text = OtherDocumentList.FileName;
                        this.txtKeyWords.Text = OtherDocumentList.KeyWords;

                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", OtherDocumentList.CompileDate);
                        this.drpCompileMan.Text = BLL.UserService.GetUserNameByUserId(OtherDocumentList.CompileMan);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(OtherDocumentList.FileContent);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.OtherDocumentListMenuId;
                this.ctlAuditFlow.DataId = this.FileId;
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
            if (!string.IsNullOrEmpty(this.FileId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/OtherDocumentListAttachUrl&menuId={1}&type=-1", FileId, BLL.Const.OtherDocumentListMenuId)));
            }            
        }
        #endregion
    }
}