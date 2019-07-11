﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiddenRectificationList.aspx.cs" Inherits="FineUIPro.Web.HiddenInspection.HiddenRectificationList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>日常巡检</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
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
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="日常巡检" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="HazardRegisterId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="HazardRegisterId" AllowSorting="true" SortField="RectificationTime"
                SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                OnRowCommand="Grid1_RowCommand" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:RadioButtonList ID="ckType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"
                                Width="330px">
                                <f:RadioItem Value="0" Text="全部" />
                                <f:RadioItem Value="D" Selected="True" Text="日检" />
                                <f:RadioItem Value="W" Text="周检" />
                                <f:RadioItem Value="M" Text="月检" />
                            </f:RadioButtonList>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="检查人" ID="txtCheckMan" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="210px" LabelWidth="80px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="检查项" ID="txtType" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="210px" LabelWidth="80px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="区域" ID="txtWorkAreaName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="210px" LabelWidth="80px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="责任单位" ID="txtResponsibilityUnitName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="250px"
                                LabelWidth="80px">
                            </f:TextBox>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DatePicker ID="txtStartTime" runat="server" Label="检查时间" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="80px">
                            </f:DatePicker>
                            <f:Label ID="Label3" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker ID="txtEndTime" runat="server" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                Width="130px">
                            </f:DatePicker>
                            <f:DatePicker ID="txtStartRectificationTime" runat="server" Label="整改时间" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="80px">
                            </f:DatePicker>
                            <f:Label ID="Label1" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker ID="txtEndRectificationTime" runat="server" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                Width="130px">
                            </f:DatePicker>
                            <f:DropDownList ID="drpStates" runat="server" Label="状态" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"
                                LabelWidth="70px" LabelAlign="Right" Width="170px">
                            </f:DropDownList>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:HiddenField runat="server" ID="hdRemark">
                            </f:HiddenField>
                            <f:Button ID="btnNew" Icon="Add" runat="server" OnClick="btnNew_Click" ToolTip="编制">
                            </f:Button>
                            <f:Button ID="btnPrint" OnClick="btnPrint_Click" runat="server" ToolTip="打印" Icon="Printer" Hidden="true"
                                >
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:CheckBoxField ColumnID="ckbIsSelected" Width="50px" RenderAsStaticField="false"
                                AutoPostBack="true" CommandName="IsSelected" HeaderText="选择" HeaderTextAlign="Center" />
                    <f:TemplateField ColumnID="tfPageIndex" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center" EnableLock="true" Locked="False">
                        <ItemTemplate>
                            <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="90px" ColumnID="CheckManName" DataField="CheckManName" SortField="CheckManName"
                        FieldType="String" HeaderText="检查人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="CheckTime" DataField="CheckTime" SortField="CheckTime"
                        HeaderText="检查时间" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="WorkAreaName" DataField="WorkAreaName" SortField="WorkAreaName"
                        FieldType="String" HeaderText="区域" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="230px" ColumnID="ResponsibilityUnitName" DataField="ResponsibilityUnitName"
                        SortField="ResponsibilityUnitName" FieldType="String" HeaderText="责任单位" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="RegisterTypesName" DataField="RegisterTypesName"
                        SortField="RegisterTypesName" FieldType="String" HeaderText="检查项" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="350px" ColumnID="RegisterDef" DataField="RegisterDef" SortField="RegisterDef"
                        FieldType="String" HeaderText="问题描述" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="Rectification" DataField="Rectification" SortField="Rectification"
                        FieldType="String" HeaderText="采取措施" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="ResponsibilityManName" DataField="ResponsibilityManName"
                        SortField="ResponsibilityManName" FieldType="String" HeaderText="责任人" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="RectificationPeriod" DataField="RectificationPeriod"
                        SortField="RectificationPeriod" FieldType="Date" Renderer="Date" HeaderText="整改期限"
                        TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="RectificationTime" DataField="RectificationTime"
                        SortField="RectificationTime" HeaderText="整改时间" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CutPayment" DataField="CutPayment" FieldType="String"
                        SortField="CutPayment" HeaderText="处罚金额" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="StatesStr" DataField="StatesStr" SortField="StatesStr"
                        FieldType="String" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <%--<f:TemplateField ColumnID="tfImageUrl" Width="280px" HeaderText="整改前图片" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnImageUrl" runat="server" Text='<%# ConvertImageUrl(Eval("HazardRegisterId")) %>'></asp:LinkButton>
                        </ItemTemplate>
                    </f:TemplateField>--%>
                    <f:TemplateField ColumnID="tfImageUrl1" Width="150px" HeaderText="整改前图片" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbImageUrl" runat="server" Text='<%# ConvertImageUrlByImage(Eval("HazardRegisterId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfImageUrl2" Width="150px" HeaderText="整改后图片" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# ConvertImgUrlByImage(Eval("HazardRegisterId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <%--<f:TemplateField ColumnID="tfRectificationImageUrl" Width="280px" HeaderText="整改后图片"
                        HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnRectificationImageUrl" runat="server" Text='<%#ConvertImgUrl(Eval("HazardRegisterId")) %>'></asp:LinkButton>
                        </ItemTemplate>
                    </f:TemplateField>--%>
                    <f:LinkButtonField Width="50px" HeaderText="删除" ConfirmText="确定要删除此条信息吗？" ConfirmTarget="Parent" ColumnID="Del"
                        CommandName="del" TextAlign="Center" ToolTip="删除" Icon="Delete" />
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
                        <f:ListItem Text="所有行" Value="10000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="日常巡检" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="900px" Height="580px">
    </f:Window>
    <f:Window ID="Window2" Title="违章处罚通知单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1024px" Height="600px">
    </f:Window>
    <f:Window ID="Window3" Title="检查小结" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window3_Close" IsModal="true"
        Width="600px" Height="300px">
    </f:Window>
    <f:Window ID="Window4" Title="检查打印" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="1200px" Height="600px">
    </f:Window>
    <f:Window ID="Window5" Title="检查打印" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="1200px" Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnModify" EnablePostBack="true" runat="server" Text="编辑" 
                OnClick="btnMenuModify_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnRectify" EnablePostBack="true" runat="server" Text="整改" 
                OnClick="btnMenuRectify_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnConfirm" EnablePostBack="true" runat="server" Text="确认" 
                OnClick="btnMenuConfirm_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuSee" EnablePostBack="true" runat="server" Text="查看" 
                OnClick="btnMenuSee_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" 
                ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
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
    </script>
</body>
</html>
