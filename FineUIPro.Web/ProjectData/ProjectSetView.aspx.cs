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
                        this.txtProjectManager.Text = ProjectService.GetProjectManagerName(this.ProjectId);
                        this.txtConstructionManager.Text = ProjectService.GetConstructionManagerName(this.ProjectId);
                        this.txtHSSEManager.Text = ProjectService.GetHSSEManagerName(this.ProjectId);
                        if (project.ProjectState == Const.ProjectState_2)
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
                        Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(project.UnitId);
                        if (unit != null)
                        {
                            this.txtUnitName.Text = unit.UnitName;
                        }
                        if (project.IsForeign == true)
                        {
                            this.ckbIsForeign.Checked = true;
                        }
                        if (CommonService.GetIsThisUnit(Const.UnitId_6))
                        {
                            this.txtUnitName.Label = "所属分公司";
                        }

                        this.txtMapCoordinates.Text = project.MapCoordinates;
                    }
                }
            }
        }
    }
}