﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.Text;
using System.IO;

namespace FineUIPro.Web.EduTrain
{
    public partial class KnowledgeDB : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                btnDeleteDetail.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                btnDeleteDetail.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());
                btnAuditResources.OnClientClick = Window4.GetShowReference("KnowledgeDBItemAudit.aspx") + "return false;";
                btnSelectColumns.OnClientClick = Window5.GetShowReference("KnowledgeDBItemSelectCloumn.aspx");
                InitTreeMenu();
            }
        }

        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.trKnowledgeDB.Nodes.Clear();
            this.trKnowledgeDB.ShowBorder = false;
            this.trKnowledgeDB.ShowHeader = false;
            this.trKnowledgeDB.EnableIcons = true;
            this.trKnowledgeDB.AutoScroll = true;
            this.trKnowledgeDB.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "应知应会库",
                NodeID = "0",
                Expanded = true
            };
            this.trKnowledgeDB.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, "0");
        }

        /// <summary>
        /// 加载树
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            var dt = BLL.KnowledgeService.GetKnowLedgeBySupKnowledgeId(menuId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.KnowledgeName,
                        ToolTip = dr.KnowledgeName,
                        NodeID = dr.KnowledgeId,
                        EnableClickEvent = true
                    };
                    nodes.Add(tn);
                    BoundTree(tn.Nodes, dr.KnowledgeId);
                }
            }
        }

        /// <summary>
        /// 添加应知应会库类型按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.trKnowledgeDB.SelectedNode != null)
            {
                Model.Training_Knowledge knowledge = BLL.KnowledgeService.GetKnowLedgeById(this.trKnowledgeDB.SelectedNode.NodeID);
                if ((knowledge != null && knowledge.IsEndLever == false) || this.trKnowledgeDB.SelectedNode.NodeID == "0")   //根节点或者非末级节点，可以增加
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("KnowledgeDBEdit.aspx?SupKnowledgeId={0}", this.trKnowledgeDB.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("选择的项已是末级！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 修改安全试题类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.trKnowledgeDB.SelectedNode != null)
            {
                if (this.trKnowledgeDB.SelectedNode.NodeID != "0")   //非根节点可以编辑
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("KnowledgeDBEdit.aspx?KnowledgeId={0}", this.trKnowledgeDB.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("根节点无法编辑！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除安全试题类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.trKnowledgeDB.SelectedNode != null)
            {
                var q = BLL.KnowledgeService.GetKnowLedgeById(this.trKnowledgeDB.SelectedNode.NodeID);

                if (q != null && BLL.KnowledgeService.IsDeleteKnowledge(this.trKnowledgeDB.SelectedNode.NodeID))
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, q.KnowledgeCode, q.KnowledgeId, BLL.Const.KnowledgeDBMenuId, BLL.Const.BtnDelete);
                    BLL.KnowledgeItemService.DeleteKnowledgeItemList(this.trKnowledgeDB.SelectedNode.NodeID);
                    BLL.KnowledgeService.DeleteKnowledge(this.trKnowledgeDB.SelectedNode.NodeID);
                    InitTreeMenu();
                }
                else
                {
                    ShowNotify("存在下级菜单或已增加资源数据或者为内置项，不允许删除！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择删除项！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Tree点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trKnowledgeDB_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            string strSql = "SELECT KnowledgeItemId,KnowledgeId,KnowledgeItemCode,KnowledgeItemName,Remark,CompileMan,CompileDate,AuditMan,AuditDate,IsPass,CompileManName,AuditManName  " +
                " FROM dbo.View_Training_KnowledgeItem " +
                " WHERE KnowledgeId=@KnowledgeId and IsPass=@IsPass";
            List<SqlParameter> listStr = new List<SqlParameter>
            {
                new SqlParameter("@KnowledgeId", this.trKnowledgeDB.SelectedNode.NodeID),
                new SqlParameter("@IsPass", true)
            };
            if (!string.IsNullOrEmpty(this.KnowledgeItemName.Text.Trim()))
            {
                strSql += " AND KnowledgeItemName LIKE @KnowledgeItemName";
                listStr.Add(new SqlParameter("@KnowledgeItemName", "%" + this.KnowledgeItemName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 增加安全试题库明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (this.trKnowledgeDB.SelectedNode != null)
            {
                Model.Training_Knowledge knowledge = BLL.KnowledgeService.GetKnowLedgeById(this.trKnowledgeDB.SelectedNode.NodeID);
                if ((knowledge == null|| knowledge.IsEndLever != true))   //根节点或者非末级节点，可以增加
                {
                    ShowNotify("请选择正确节点增加！", MessageBoxIcon.Warning);
                    return;
                }

                if (this.trKnowledgeDB.SelectedNode.Nodes.Count == 0)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("KnowledgeDBItemEdit.aspx?KnowledgeId={0}", this.trKnowledgeDB.SelectedNode.NodeID, "编辑 - ")));

                }
                else
                {
                    ShowNotify("请选择末级节点！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 编辑安全试题库明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditDetail_Click(object sender, EventArgs e)
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
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            string knowledgeItemId = Grid1.SelectedRowID;

            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("KnowledgeDBItemEdit.aspx?KnowledgeItemId={0}", knowledgeItemId, "编辑 - ")));
        }

        /// <summary>
        /// 删除安全试题库明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

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
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var knowledgeItem = BLL.KnowledgeItemService.GetKnowledgeItemById(rowID);
                    if (knowledgeItem != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, knowledgeItem.KnowledgeItemCode, knowledgeItem.KnowledgeItemId, BLL.Const.KnowledgeDBMenuId, BLL.Const.BtnDelete);
                        BLL.KnowledgeItemService.DeleteKnowledgeItem(rowID);
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }

        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
         
        }

        /// <summary>
        /// Grid1行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// Grid1排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗口1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            InitTreeMenu();
        }

        /// <summary>
        /// 关闭弹出窗口2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 上传资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (this.trKnowledgeDB.SelectedNode != null)
            {
                if (this.trKnowledgeDB.SelectedNode.Nodes.Count == 0 && this.trKnowledgeDB.SelectedNode.NodeID != "0")
                {
                    PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("KnowledgeDBItemUpload.aspx?KnowledgeId={0}", this.trKnowledgeDB.SelectedNode.NodeID, "编辑 - ")));

                }
                else
                {
                    ShowNotify("请选择末级节点！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        #region 导出
        /// <summary>
        /// 关闭导出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window5_Close(object sender, WindowCloseEventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1, e.CloseArgument.Split('#')));
            Response.End();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid, string[] columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            List<string> columnHeaderTexts = new List<string>(columns);
            List<int> columnIndexs = new List<int>();
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                if (columnHeaderTexts.Contains(column.HeaderText))
                {
                    sb.AppendFormat("<td>{0}</td>", column.HeaderText);
                    columnIndexs.Add(column.ColumnIndex);
                }
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                int columnIndex = 0;
                foreach (object value in row.Values)
                {
                    if (columnIndexs.Contains(columnIndex))
                    {
                        string html = value.ToString();
                        if (html.StartsWith(Grid.TEMPLATE_PLACEHOLDER_PREFIX))
                        {
                            // 模板列                            
                            string templateID = html.Substring(Grid.TEMPLATE_PLACEHOLDER_PREFIX.Length);
                            Control templateCtrl = row.FindControl(templateID);
                            html = GetRenderedHtmlSource(templateCtrl);
                        }
                        //else
                        //{
                        //    // 处理CheckBox             
                        //    if (html.Contains("f-grid-static-checkbox"))
                        //    {
                        //        if (!html.Contains("f-checked"))
                        //        {
                        //            html = "×";
                        //        }
                        //        else
                        //        {
                        //            html = "√";
                        //        }
                        //    }
                        //    // 处理图片                           
                        //    if (html.Contains("<img"))
                        //    {
                        //        string prefix = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, ""); 
                        //        html = html.Replace("src=\"", "src=\"" + prefix);
                        //    }
                        //}
                        sb.AppendFormat("<td>{0}</td>", html);
                    }
                    columnIndex++;
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        /// <summary>        
        /// 获取控件渲染后的HTML源代码        
        /// </summary>        
        /// <param name="ctrl"></param>        
        /// <returns></returns>        
        private string GetRenderedHtmlSource(Control ctrl)
        {
            if (ctrl != null)
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        ctrl.RenderControl(htw);
                        return sw.ToString();
                    }
                }
            }
            return String.Empty;
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.KnowledgeDBMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnNewDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnEdit.Hidden = false;
                    this.btnEditDetail.Hidden = false;
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                    this.btnDeleteDetail.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnUploadResources))
                {
                    this.btnUploadResources.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnAuditResources.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnOut))
                {
                    this.btnSelectColumns.Hidden = false;
                }
            }
        }
        #endregion
    }
}