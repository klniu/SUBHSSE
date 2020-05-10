﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPlan.aspx.cs" Inherits="FineUIPro.Web.Test.TestPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>制定计划</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
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
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="制定计划" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="TestPlanId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="TestPlanId" AllowSorting="true" SortField="TestStartTime"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                             <f:RadioButtonList ID="ckStates" runat="server" AutoPostBack="true" Label="状态" LabelAlign="Right"
                                AutoColumnWidth="true" OnSelectedIndexChanged="TextBox_TextChanged">                               
                                <f:RadioItem Text="全部" Value="-2" Selected="true"/>
                                <f:RadioItem Text="待发布" Value="0" />
                                <f:RadioItem Text="待考试" Value="1" />
                               <f:RadioItem Text="考试中" Value="2" />
                                <f:RadioItem Text="已结束" Value="3" />
                                <f:RadioItem Text="已作废" Value="-1" />
                            </f:RadioButtonList>                                                  
                            <f:TextBox ID="txtName" runat="server" Label="查询" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="350px" LabelWidth="80px" LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>    
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true"
                                runat="server">
                            </f:Button>                   
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                   
                    <f:RenderField Width="90px" ColumnID="PlanCode" DataField="PlanCode" SortField="PlanCode"
                        HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="PlanName" DataField="PlanName" FieldType="String"
                        HeaderText="计划名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>                   
                    <f:RenderField Width="150px" ColumnID="TestStartTime" DataField="TestStartTime" 
                          HeaderText="扫码开始时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="150px" ColumnID="TestEndTime" DataField="TestEndTime" 
                          HeaderText="扫码结束时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="150px" ColumnID="TestPalce" DataField="TestPalce" FieldType="String"
                        HeaderText="考试地点" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="PlanManName" DataField="PlanManName" FieldType="String"
                        HeaderText="制定人" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="PlanDate" DataField="PlanDate" FieldType="Date"
                        Renderer="Date" HeaderText="制定时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
              <%--       <f:RenderField Width="150px" ColumnID="ActualTime" DataField="ActualTime" 
                          HeaderText="结束时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>--%>
                      <f:RenderField Width="80px" ColumnID="StatesName" DataField="StatesName" FieldType="String"
                        HeaderText="状态" HeaderTextAlign="Center" TextAlign="Left">
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
                        <f:ListItem Text="所有行" Value="10000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="制定计划" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="1200px" Height="560px">
    </f:Window>

    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="TableEdit" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnQR" OnClick="btnQR_Click" EnablePostBack="true"
            runat="server" Text="二维码"  Icon="Shading">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
            Hidden="true">
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
