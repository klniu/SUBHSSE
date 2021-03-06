﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckSpecial.aspx.cs" Inherits="FineUIPro.Web.Check.CheckSpecial" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>专项检查</title>
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
          .LabelColor
        {
            color: Red;
            font-size:small;
        }   
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
         .f-grid-row.Yellow
        {
            background-color: Yellow;
        } 
        .f-grid-row.Green
        {
            background-color: LightGreen;
        }
          .f-grid-row.Red
        {
            background-color: Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="专项检查" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="NewChcekId" DataIDField="NewChcekId" AllowSorting="true" SortField="CheckTime"
                SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True"
                OnRowCommand="Grid1_RowCommand">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                             <f:RadioButtonList runat="server" ID="rbStates" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="rbStates_SelectedIndexChanged">
                                <f:RadioItem Text="全部" Value="-1" Selected="true" />
                                <f:RadioItem Text="待提交" Value="0" />
                                <f:RadioItem Text="已提交" Value="1" />
                            </f:RadioButtonList>
                            <f:TextBox runat="server" Label="单位" ID="txtUnitName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="50px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="区域" ID="txtWorkAreaName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="160px" LabelWidth="50px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="180px" LabelWidth="80px">
                            </f:DatePicker>
                            <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="180px" LabelWidth="80px">
                            </f:DatePicker>
                              <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnPrint" ToolTip="打印" Icon="Printer" Hidden="true" runat="server"
                                        OnClick="btnPrint_Click">
                                    </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>    
                      <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="CheckSpecialCode" DataField="CheckSpecialCode"
                        SortField="CheckSpecialCode" FieldType="String" HeaderText="编号" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="WorkArea" DataField="WorkArea" SortField="WorkArea"
                        FieldType="String" HeaderText="检查区域" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" 
                        SortField="UnitName" FieldType="String" HeaderText="受检单位" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CheckTime" DataField="CheckTime" SortField="CheckTime"
                        FieldType="String" HeaderText="检查日期" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="CheckCount" DataField="CheckCount"
                        SortField="CheckCount" FieldType="Int" HeaderText="问题数量" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="Unqualified" DataField="Unqualified" ExpandUnusedSpace="true"
                        SortField="Unqualified" FieldType="String" HeaderText="问题描述" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="80px" ColumnID="CompleteStatusName" DataField="CompleteStatusName"
                        SortField="CompleteStatusName" FieldType="String" HeaderText="整改结果" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:LinkButtonField Width="140px" HeaderText="处理措施" ConfirmTarget="Parent" CommandName="click"
                                 TextAlign="Center"  DataTextField="HandleStepLink" ColumnID="HandleStepLink" />
                    <f:RenderField Width="80px" ColumnID="StatesName" DataField="StatesName"
                        SortField="StatesName" FieldType="String" HeaderText="状态" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
                        <f:ListItem Text="所有行" Value="100000" />
                    </f:DropDownList>
                    <f:ToolbarFill runat="server">
                    </f:ToolbarFill>                     
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="详细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1200px" Height="540px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true"
                Text="修改" Icon="Pencil" OnClick="btnMenuModify_Click">
            </f:MenuButton>
         <%--   <f:MenuButton ID="btnMenuRectify" EnablePostBack="true" runat="server" Hidden="true" Text="生成整改单" Icon="ScriptLightning"
                OnClick="btnMenuRectify_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnCompletedDate" EnablePostBack="true" runat="server" Hidden="true"
                Text="闭环" Icon="TimeGreen" OnClick="btnCompletedDate_Click">
            </f:MenuButton>--%>
            <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true"
                Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？" OnClick="btnMenuDel_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function onGridDataLoad(event) {
            this.mergeColumns(['CheckSpecialCode', 'CheckCount','CheckTime'], { depends: true });
//            this.mergeColumns(['CheckCount']);
//            this.mergeColumns(['CheckPersonName']);
//            this.mergeColumns(['CheckTime']);
//            this.mergeColumns(['FlowOperateName']);
        }  
    </script>
</body>
</html>
