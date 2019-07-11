﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KnowledgeDB.aspx.cs" Inherits="FineUIPro.Web.EduTrain.KnowledgeDB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>应知应会库</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="250" Title="应知应会库" TitleToolTip="应知应会库" ShowBorder="true"
                ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:Button ID="btnNew" Icon="Add" runat="server" OnClick="btnNew_Click" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnEdit" Icon="Pencil" runat="server" OnClick="btnEdit_Click" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnDelete" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                                runat="server" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                    <f:Tree ID="trKnowledgeDB" Width="200px" EnableCollapse="true" ShowHeader="true"
                        Title="应知应会库" OnNodeCommand="trKnowledgeDB_NodeCommand" AutoLeafIdentification="true"
                        runat="server" EnableTextSelection="True">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="应知应会库明细" TitleToolTip="应知应会库明细"
                Layout="VBox">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="KnowledgeItemId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="KnowledgeItemId" AllowSorting="true" SortField="KnowledgeItemCode"
                        SortDirection="ASC" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand" AllowPaging="true"
                        IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableColumnLines="true"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                        OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                                <Items>
                                    <f:TextBox ID="KnowledgeItemName" runat="server" Label="名称" EmptyText="输入查询名称" AutoPostBack="true"
                                        OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="90px" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewDetail_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnEditDetail" ToolTip="编辑" Icon="Pencil" runat="server" OnClick="btnEditDetail_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnDeleteDetail" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDeleteDetail_Click"
                                        runat="server" Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnUploadResources" runat="server" Icon="SystemNew" OnClick="btnUploadResources_Click"
                                        ToolTip="上传资源" Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnAuditResources" runat="server" Icon="ZoomIn" ToolTip="审核资源" Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnSelectColumns" runat="server" ToolTip="导出" Icon="FolderUp" EnablePostBack="false" Hidden="true">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:TemplateField Width="120px" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center"
                                SortField="KnowledgeItemCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblKnowledgeItemCode" runat="server" Text='<%# Bind("KnowledgeItemCode") %>'
                                        ToolTip='<%#Bind("KnowledgeItemCode") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="250px" HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="KnowledgeItemName">
                                <ItemTemplate>
                                    <asp:Label ID="lblKnowledgeItemName" runat="server" Text='<%# Bind("KnowledgeItemName") %>'
                                        ToolTip='<%#Bind("KnowledgeItemName") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="500px" HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="Remark"  ExpandUnusedSpace="True">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>' ToolTip='<%#Bind("Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
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
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="应知应会库类型" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="400px" Height="200px">
    </f:Window>
    <f:Window ID="Window2" Title="应知应会库" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="500px" Height="250px">
    </f:Window>
    <f:Window ID="Window3" Title="上传资源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        Height="500px">
    </f:Window>
    <f:Window ID="Window4" Title="审核资源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        Height="500px">
    </f:Window>
    <f:Window ID="Window5" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window5_Close" IsModal="true"
        Width="450px" Height="250px" EnableAjax="false">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Hidden="true">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
