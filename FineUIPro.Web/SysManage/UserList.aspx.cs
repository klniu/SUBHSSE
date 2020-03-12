﻿namespace FineUIPro.Web.SysManage
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class UserList : PageBase
    { 
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                this.btnNew.OnClientClick = Window1.GetShowReference("UserListEdit.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }

                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Users.UserId,Users.Account,Users.UserCode,Users.Password,Users.UserName,Users.RoleId,Users.UnitId,Users.IsPosts,Users.IsReplies,Users.IsDeletePosts,Users.IsPost,CASE WHEN  Users.IsPost=1 THEN '是' ELSE '否' END AS IsPostName,Users.IdentityCard,Users.Telephone,Users.IsOffice,"
                          + @" CASE WHEN Users.IsPosts=1 THEN '是' ELSE '否' END AS IsPostsName,CASE WHEN  Users.IsReplies=1 THEN '是' ELSE '否' END AS IsRepliesName ,CASE WHEN  Users.IsDeletePosts=1 THEN '是' ELSE '否' END AS IsDeletePostsName,Roles.RoleName,Unit.UnitName,Unit.UnitCode,Const13.ConstText AS RoleTypeName"
                          + @" From dbo.Sys_User AS Users"
                          + @" LEFT JOIN Sys_Role AS Roles ON Roles.RoleId=Users.RoleId"
                          + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId=Users.UnitId"
                          + @" LEFT JOIN Sys_Const AS Const13 ON Roles.RoleType=Const13.ConstValue AND Const13.GroupId='" + BLL.ConstValue.Group_0013 + "'"
                          + @" WHERE Users.UserId !='" + Const.sysglyId + "' AND Users.UserId !='" + BLL.Const.hfnbdId + "'";

            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                strSql += " AND Users.UserName LIKE @UserName";
                listStr.Add(new SqlParameter("@UserName", "%" + this.txtUserName.Text.Trim() + "%"));
            }
            if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是企业单位或者管理员
            {
                strSql += " AND Users.UnitId = @UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            }

            if (!string.IsNullOrEmpty(this.txtUnitName.Text.Trim()))
            {
                strSql += " AND Unit.UnitName LIKE @UnitName";
                listStr.Add(new SqlParameter("@UnitName", "%" + this.txtUnitName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtRoleName.Text.Trim()))
            {
                strSql += " AND Roles.RoleName LIKE @RoleName";
                listStr.Add(new SqlParameter("@RoleName", "%" + this.txtRoleName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtRoleTypeName.Text.Trim()))
            {
                strSql += " AND RoleTypeName LIKE @RoleTypeName";
                listStr.Add(new SqlParameter("@RoleTypeName", "%" + this.txtRoleTypeName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.UserMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnImport.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        #region  删除数据
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                string strShowNotify = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var user = BLL.UserService.GetUserByUserId(rowID);
                    if (user != null)
                    {
                        string cont = judgementDelete(rowID);
                        if (string.IsNullOrEmpty(cont))
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, user.UserCode, user.UserId, BLL.Const.UserMenuId, BLL.Const.BtnAdd);
                            BLL.UserService.DeleteUser(rowID);
                        }
                        else
                        {
                            strShowNotify += "用户：" + user.UserName + cont;
                        }
                    }
                }

                BindGrid();
                if (!string.IsNullOrEmpty(strShowNotify))
                {
                    Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                }
                else
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
            }
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("UserListEdit.aspx?userId={0}", Id, "编辑 - ")));
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            if (Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.UserId == id) != null)
            {
                content += "已在【项目用户】中使用，不能删除！";
            }
            if (Funs.DB.Law_LawRegulationList.FirstOrDefault(x => x.CompileMan == id) != null)
            {
                content += "已在【法律法规】中使用，不能删除！";
            }
            if (Funs.DB.Law_HSSEStandardsList.FirstOrDefault(x => x.CompileMan == id) != null)
            {
                content += "已在【标准规范】中使用，不能删除！";
            }
            if (Funs.DB.ProjectData_FlowOperate.FirstOrDefault(x => x.OperaterId == id) != null)
            {
                content += "已在【报表审核】中使用，不能删除！";
            }

            return content;
        }
        #endregion

        /// <summary>
        /// 参与项目情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnPro_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ParticipateProject.aspx?userId={0}", Grid1.SelectedRowID, "参与项目情况 - "), "参与项目", 1000, 520));
        }

        protected string ConvertProject(object userId)
        {
            string projectName = string.Empty;
            if (userId != null)
            {
                var projectUsers = ProjectUserService.GetProjectUserByUserId(userId.ToString());

                if (projectUsers.Count() == 1)
                {
                    projectName = BLL.ProjectService.GetProjectByProjectId(projectUsers.FirstOrDefault().ProjectId).ProjectName;
                }
                else if (projectUsers.Count() > 1)
                {
                    projectName = "查看";
                }
                else
                {
                    projectName = "无";
                }
            }
            return projectName;
        }

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("UserIn.aspx", "导入 - ")));
        }
        #endregion

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}