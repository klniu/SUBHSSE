﻿namespace FineUIPro.Web.SysManage
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.UI;
    using BLL;

    public partial class UpdatePassword : PageBase
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
            string strSql = @"SELECT Users.UserId,Users.Account,Users.UserCode,Users.Password,Users.UserName,Users.RoleId,Users.UnitId,Users.IsPosts,Users.IsReplies,Users.IsDeletePosts,Users.IsPost,CASE WHEN  Users.IsPost=1 THEN '是' ELSE '否' END AS IsPostName,"
                          + @" CASE WHEN  Users.IsPosts=1 THEN '是' ELSE '否' END AS IsPostsName,CASE WHEN  Users.IsReplies=1 THEN '是' ELSE '否' END AS IsRepliesName ,CASE WHEN  Users.IsDeletePosts=1 THEN '是' ELSE '否' END AS IsDeletePostsName,Roles.RoleName,Unit.UnitName,Unit.UnitCode,Const13.ConstText AS RoleTypeName "
                          + @" From dbo.Sys_User AS Users"
                          + @" LEFT JOIN Sys_Role AS Roles ON Roles.RoleId=Users.RoleId"
                          + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId=Users.UnitId"
                          + @" LEFT JOIN Sys_Const AS Const13 ON Roles.RoleType=Const13.ConstValue AND Const13.GroupId='" + BLL.ConstValue.Group_0013 + "'"
                          + @" WHERE  Users.Account !='hfnbd'";           
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.CurrUser.UserId != BLL.Const.sysglyId)
            {
                strSql += " AND UserId = @UserId";
                listStr.Add(new SqlParameter("@UserId", this.CurrUser.UserId));
            }

            if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                strSql += " AND UserName LIKE @UserName";
                listStr.Add(new SqlParameter("@UserName", "%" + this.txtUserName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        
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
        
        #region  删除数据
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {               
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.UserService.UpdatePassword(rowID, BLL.Const.Password);
                }
                BindGrid();
                ShowNotify("密码已重置为原始密码!", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("请至少选中一行！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！",MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("UpdatePasswordEdit.aspx?userId={0}", Id, "编辑 - ")));
        }

        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnEdit_Click(null, null);
        }
        #endregion

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
    }
}