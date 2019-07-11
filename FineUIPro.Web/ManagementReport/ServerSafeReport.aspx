﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerSafeReport.aspx.cs"
    Inherits="FineUIPro.Web.ManagementReport.ServerSafeReport" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row.blue
        {
            background-color: #38B0DE;
            background-image: none;
        }        
        .f-grid-row.red
        {
            background-color: #FFD202;
        }
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                Layout="Fit" EnableCollapse="true" Width="250" Title="安全文件上报" TitleToolTip="安全文件上报"
                ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="trSafeReport" Width="200px" Title="安全文件上报" ShowHeader="false" OnNodeCommand="trSafeReport_NodeCommand"
                         AutoLeafIdentification="true" runat="server" AutoScroll="true" >
                    </f:Tree>
                </Items>
                <Toolbars>
                <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:Button ID="btnNew" Icon="Add" ToolTip="新增" runat="server" OnClick="btnNew_Click" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnEdit" Icon="Pencil" ToolTip="编辑" runat="server" OnClick="btnEdit_Click" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnDelete" Icon="Delete" ToolTip="删除" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                                runat="server" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="安全文件明细"
                TitleToolTip="安全文件明细" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="txtName" runat="server" Label="查询" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="90px" LabelAlign="Right">
                            </f:TextBox> 
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnItemAdd" Icon="Add" ToolTip="新增" runat="server" OnClick="btnItemAdd_Click" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                     <f:TabStrip ID="TabStrip" CssClass="f-tabstrip-theme-simple" Height="460px" ShowBorder="true"
                        TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server">
                        <Tabs>
                            <f:Tab ID="formTab" Title="项目安全文件" BodyPadding="5px" Layout="Fit" IconFont="Bookmark"
                                runat="server" TitleToolTip="项目安全文件情况" Margin="0 5 0 5">
                                <Items>
                                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                                    runat="server" BoxFlex="1" DataKeyNames="SafeReportItemId" AllowCellEditing="true"
                                    ClicksToEdit="2" DataIDField="SafeReportItemId" AllowSorting="true" SortField="ProjectCode"
                                    SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true"
                                    AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">                                    
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                                        <f:RenderField Width="220px" ColumnID="ProjectName" DataField="ProjectName" SortField="ProjectName"
                                            FieldType="String" HeaderText="项目" HeaderTextAlign="Center" ExpandUnusedSpace="true">
                                        </f:RenderField>
                                         <f:RenderField Width="200px" ColumnID="SafeReportName" DataField="SafeReportName" SortField="SafeReportName"
                                            FieldType="String" HeaderText="标题" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField> 
                                        <f:RenderField Width="120px" ColumnID="RequestTime" DataField="RequestTime" SortField="RequestTime"
                                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="要求上报时间"
                                            HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                         <f:RenderField Width="100px" ColumnID="UpReportTime" DataField="UpReportTime" SortField="UpReportTime"
                                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="上报时间"
                                            HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                         <f:RenderField Width="90px" ColumnID="StatesName" DataField="StatesName" SortField="StatesName"
                                            FieldType="String" HeaderText="上报状态" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>                         
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
                                        </f:DropDownList>
                                    </PageItems>
                                </f:Grid> 
                                </Items>
                            </f:Tab>
                            <f:Tab ID="toTab" Title="分公司安全文件" BodyPadding="5px" Layout="Fit" IconFont="Bookmark"
                                runat="server" TitleToolTip="分公司安全文件情况">
                                <Items>
                                   <f:Grid ID="Grid2" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                                    runat="server" BoxFlex="1" DataKeyNames="SafeReportUnitItemId" AllowCellEditing="true"
                                    ClicksToEdit="2" DataIDField="SafeReportUnitItemId" AllowSorting="true" SortField="UnitCode"
                                    SortDirection="DESC" OnSort="Grid2_Sort" EnableColumnLines="true"
                                    AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid2_PageIndexChange"
                                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid2_RowDoubleClick">                                    
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                                        <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                            FieldType="String" HeaderText="单位" HeaderTextAlign="Center" ExpandUnusedSpace="true">
                                        </f:RenderField>
                                         <f:RenderField Width="200px" ColumnID="SafeReportName" DataField="SafeReportName" SortField="SafeReportName"
                                            FieldType="String" HeaderText="标题" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField> 
                                        <f:RenderField Width="120px" ColumnID="RequestTime" DataField="RequestTime" SortField="RequestTime"
                                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="要求上报时间"
                                            HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                         <f:RenderField Width="100px" ColumnID="UpReportTime" DataField="UpReportTime" SortField="UpReportTime"
                                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="上报时间"
                                            HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                         <f:RenderField Width="90px" ColumnID="StatesName" DataField="StatesName" SortField="StatesName"
                                            FieldType="String" HeaderText="上报状态" HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>                         
                                    </Columns>
                                    <Listeners>
                                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                                    </Listeners>
                                    <PageItems>
                                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                                        </f:ToolbarSeparator>
                                        <f:ToolbarText ID="ToolbarText2" runat="server" Text="每页记录数：">
                                        </f:ToolbarText>
                                        <f:DropDownList runat="server" ID="ddlPageSize2" Width="80px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlPageSize2_SelectedIndexChanged">
                                            <f:ListItem Text="10" Value="10" />
                                            <f:ListItem Text="15" Value="15" />
                                            <f:ListItem Text="20" Value="20" />
                                            <f:ListItem Text="25" Value="25" />
                                        </f:DropDownList>
                                    </PageItems>
                                </f:Grid>  
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="安全上报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="380px">
    </f:Window>
    <f:Window ID="Window2" Title="安全上报详情" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="1024px" Height="430px">
    </f:Window>   
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="查看" Hidden="true" Icon="Pencil">
        </f:MenuButton>
         <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server"
            Text="删除">
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
