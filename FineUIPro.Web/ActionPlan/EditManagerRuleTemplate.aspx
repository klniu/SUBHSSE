﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditManagerRuleTemplate.aspx.cs" Inherits="FineUIPro.Web.ActionPlan.EditManagerRuleTemplate" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编制管理规定发布</title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="true" ShowHeader="false" Layout="VBox"
        Margin="5px" BodyPadding="5px">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="false" ShowHeader="false" Title="管理规定发布" EnableCollapse="true" OnSort="Grid1_Sort" IsDatabasePaging="true"
                runat="server" BoxFlex="1" DataKeyNames="ManageRuleId" AllowCellEditing="true" OnPageIndexChange="Grid1_PageIndexChange"
                EnableColumnLines="true" ClicksToEdit="1" DataIDField="ManageRuleId" AllowSorting="true" AllowPaging="true" PageSize="10"
                SortField="ManageRuleCode" OnRowCommand="Grid1_RowCommand">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:TextBox runat="server" Label="编号" ID="txtManageRuleCode" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="60px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="名称" ID="txtManageRuleName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="60px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="分类" ID="txtManageRuleTypeName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="60px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存" OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:CheckBoxField ColumnID="ckbIsSelected" Width="50px" RenderAsStaticField="false" 
                        AutoPostBack="true" CommandName="IsSelected" HeaderText="选择" HeaderTextAlign="Center" />
                    <f:TemplateField Width="225px" HeaderText="文件编号" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="ManageRuleCode">
                        <ItemTemplate>
                            <asp:Label ID="lblManageRuleCode" runat="server" Text='<%# Bind("ManageRuleCode") %>'
                                ToolTip='<%#Bind("ManageRuleCode") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="250px" HeaderText="文件名称" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="ManageRuleName">
                        <ItemTemplate>
                            <asp:Label ID="lblManageRuleName" runat="server" Text='<%# Bind("ManageRuleName") %>'
                                ToolTip='<%#Bind("ManageRuleName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="130px" HeaderText="分类" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="ManageRuleTypeName">
                        <ItemTemplate>
                            <asp:Label ID="lblManageRuleTypeName" runat="server" Text='<%# Bind("ManageRuleTypeName") %>'
                                ToolTip='<%#Bind("ManageRuleTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="380px" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="Remark" ExpandUnusedSpace="true">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>' ToolTip='<%#Bind("Remark") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                </Columns>
                <PageItems>
                    <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                    </f:ToolbarSeparator>
                    <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                    </f:ToolbarText>
                    <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <f:ListItem Text="10" Value="10" />
                        <f:ListItem Text="15" Value="15" />
                        <f:ListItem Text="20" Value="20" />
                        <f:ListItem Text="25" Value="25" />
                        <f:ListItem Text="所有行" Value="10000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
            <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true" Hidden="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
