﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DayReportEdit.aspx.cs" Inherits="FineUIPro.Web.SitePerson.DayReportEdit" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑人工时日报</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
        
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDayReportCode" runat="server" Label="编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtCompileMan" runat="server" Label="编制人" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCompileDate" runat="server" Label="日期" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1" DataIDField="DayReportDetailId"
                        DataKeyNames="DayReportDetailId" EnableMultiSelect="false" ShowGridHeader="true" Height="350px"
                        EnableColumnLines="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"> 
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="单位名称" ExpandUnusedSpace="true">
                            </f:RenderField>
                          <%--  <f:RenderField Width="220px" ColumnID="StaffData" DataField="StaffData" SortField="StaffData"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="人员情况" ExpandUnusedSpace="true">
                            </f:RenderField>--%>
                            <f:RenderField Width="100px" ColumnID="WorkTime" DataField="WorkTime" SortField="WorkTime"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="平均工时">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="RealPersonNum" DataField="RealPersonNum" SortField="RealPersonNum"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="实际人数">
                            </f:RenderField>
                            <%--<f:RenderField Width="140px" ColumnID="DayNum" DataField="DayNum" SortField="DayNum"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="工作天数">
                            </f:RenderField>--%>
                            <f:RenderField Width="100px" ColumnID="PersonWorkTime" DataField="PersonWorkTime" SortField="PersonWorkTime"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="当日人工时">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="YearPersonWorkTime" DataField="YearPersonWorkTime" SortField="YearPersonWorkTime"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="当年人工时">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="TotalPersonWorkTime" DataField="TotalPersonWorkTime" SortField="TotalPersonWorkTime"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="累计人工时">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="编辑人工时明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1100px" Height="620px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Icon="Pencil" runat="server" Text="编辑">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script>
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>
