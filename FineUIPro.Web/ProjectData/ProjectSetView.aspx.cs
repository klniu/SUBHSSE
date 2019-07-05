﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectSetView : PageBase
    {
        /// <summary>
        /// 定义项
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();   

                this.ProjectId = Request.QueryString["ProjectId"];
                if (!String.IsNullOrEmpty(this.ProjectId))
                {
                    var project = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
                    if (project != null)
                    {
                        this.txtProjectCode.Text = project.ProjectCode;
                        this.txtProjectName.Text = project.ProjectName;
                        this.txtProjectAddress.Text = project.ProjectAddress;
                        this.txtRemark.Text = project.Remark;
                        if (project.StartDate.HasValue)
                        {
                            this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", project.StartDate);
                        }
                        if (project.EndDate.HasValue)
                        {
                            this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", project.EndDate);
                        }

                        this.txtShortName.Text = project.ShortName;
                        var projectType = BLL.ConstValue.GetConstByConstValueAndGroupId(project.ProjectType,BLL.ConstValue.Group_ProjectType);
                        if (projectType != null)
                        {
                            this.txtProjectType.Text = projectType.ConstText;
                        }
                        this.txtPostCode.Text = project.PostCode;
                        this.txtProjectManager.Text = BLL.ProjectService.GetProjectManagerName(this.ProjectId);
                        this.txtConstructionManager.Text = BLL.ProjectService.GetConstructionManagerName(this.ProjectId);
                        this.txtHSSEManager.Text = BLL.ProjectService.GetHSSEManagerName(this.ProjectId);
                        if (project.ProjectState == BLL.Const.ProjectState_2)
                        {
                            this.txtProjectState.Text = "暂停中";
                        }
                        else if (project.ProjectState == BLL.Const.ProjectState_3)
                        {
                            this.txtProjectState.Text = "已完工";
                        }
                        else
                        {
                            this.txtProjectState.Text = "施工中";
                        }
                    }
                }
            }
        }
    }
}