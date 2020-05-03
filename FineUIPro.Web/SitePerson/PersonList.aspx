﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonList.aspx.cs" Inherits="FineUIPro.Web.SitePerson.PersonList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员信息</title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
          .f-grid-row.Red
        {
            background-color: red;
        }
        .LabelColor
        {
            color: Red;
            font-size:small;
        }   
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="250" Title="人员信息" TitleToolTip="人员信息" ShowBorder="true"
                ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft" Layout="Fit">
                <Items>
                    <f:Tree ID="tvProjectAndUnit" EnableCollapse="true" ShowHeader="false" Title="所属项目单位" OnNodeCommand="tvProjectAndUnit_NodeCommand" AutoLeafIdentification="true" runat="server" ShowBorder="false" 
                         EnableTextSelection="True">
                    </f:Tree>
                </Items>
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                        <Items>
                            <f:Button ID="btnPersonUnit" ToolTip="调整人员单位" Icon="TableRefresh" runat="server" Hidden="true" OnClick="btnPersonUnit_Click">
                             </f:Button>                           
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="人员信息"
                TitleToolTip="人员信息" AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="人员信息" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="PersonId" AllowCellEditing="true" ClicksToEdit="2"
                        DataIDField="PersonId" AllowSorting="true" SortField="CardNo,PersonName" SortDirection="ASC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10" EnableColumnLines="true"
                        OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                        AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True" EnableCheckBoxSelect="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:TextBox runat="server" Label="姓名" ID="txtPersonName" EmptyText="输入查询条件"
                                         Width="200px" LabelWidth="50px" LabelAlign="right">
                                    </f:TextBox>
                                     <f:TextBox runat="server" Label="卡号" ID="txtCardNo" EmptyText="输入查询条件"
                                         Width="200px" LabelWidth="50px" LabelAlign="right">
                                    </f:TextBox>
                                    <f:DropDownList ID="drpPost" runat="server" Label="岗位" EnableEdit="true" EnableMultiSelect="true" 
                                        Width="200px" LabelWidth="50px" LabelAlign="right" ForceSelection="false" EnableCheckBoxSelect="true">
                                    </f:DropDownList>
                                     <f:DropDownList ID="drpTreamGroup" runat="server" Label="班组" EnableEdit="true" 
                                        Width="200px" LabelWidth="50px" LabelAlign="right" ForceSelection="false">
                                    </f:DropDownList>                                    
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:CheckBox runat="server" ID="ckTrain" Label="未参加培训" LabelAlign="right"> 
                                    </f:CheckBox>
                                    <f:TextBox runat="server" Label="身份证" ID="txtIdentityCard" EmptyText="输入查询条件"
                                        Width="250px" LabelWidth="100px"
                                        LabelAlign="right">
                                    </f:TextBox>                                                                   
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                    <f:Button ID="btSearch" ToolTip="查询" Icon="SystemSearch" runat="server" OnClick="TextBox_TextChanged"></f:Button>
                                    <f:Button ID="btnNew" ToolTip="增加" Icon="Add" runat="server" Hidden="true" OnClick="btnNew_Click">
                                    </f:Button>
                                    <f:Button ID="btnPersonOut" ToolTip="批量出场" Icon="UserGo" runat="server" Hidden="true" OnClick="btnPersonOut_Click">
                                    </f:Button>
                                    <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationGet" Hidden="true" runat="server"
                                        OnClick="btnImport_Click">
                                    </f:Button>
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                            EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                    <f:Button ID="BtnAnalyse" ToolTip="扣分查询" Icon="ChartPie" runat="server" OnClick="BtnAnalyse_Click" Hidden="true"></f:Button>
                                    <f:Button ID="BtnBlackList" ToolTip="黑名单" Icon="ApplicationOsxTerminal" runat="server" OnClick="BtnBlackList_Click" Hidden="true"></f:Button>
                                    <f:Button ID="btnQR" ToolTip="批量生成二维码" Icon="TableRefresh" runat="server"  OnClick="btnQR_Click">
                             </f:Button>  
                                 </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <%--<f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>    --%>                          
                            <f:RenderField Width="120px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                                FieldType="String" HeaderText="姓名" HeaderTextAlign="Center"
                                TextAlign="Center">                          
                            </f:RenderField>
                             <f:RenderField HeaderText="卡号" ColumnID="CardNo" DataField="CardNo" SortField="CardNo"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="100px">                               
                            </f:RenderField>
                            <%--<f:RenderField HeaderText="发卡号" ColumnID="SendCardNo" DataField="SendCardNo" SortField="SendCardNo"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">                               
                            </f:RenderField>--%>
                             <f:RenderField HeaderText="性别" ColumnID="SexName" DataField="SexName" SortField="SexName"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Center" Width="60px">                               
                            </f:RenderField>
                            <f:RenderField HeaderText="岗位名称" ColumnID="WorkPostName" DataField="WorkPostName" SortField="WorkPostName"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="120px">                               
                            </f:RenderField>
                             <f:RenderField HeaderText="单位名称" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="250px">                               
                            </f:RenderField>   
                            <f:RenderField HeaderText="部门名称" ColumnID="DepartName" DataField="DepartName" SortField="DepartName"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="100px">                               
                            </f:RenderField>     
                            <%-- <f:RenderField HeaderText="身份证号" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="170px">                               
                            </f:RenderField> --%>                 
                            <f:TemplateField ColumnID="tfI" HeaderText="身份证号" Width="170px" HeaderTextAlign="Center" TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbI" runat="server" Text=' <%# Bind("IdentityCard") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField> 
                             <f:RenderField HeaderText="班组" ColumnID="TeamGroupName" DataField="TeamGroupName" SortField="TeamGroupName"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="100px">                               
                            </f:RenderField>
                             <f:RenderField HeaderText="作业区域" ColumnID="WorkAreaName" DataField="WorkAreaName" SortField="WorkAreaName"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="120px">                               
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="InTime" DataField="InTime" SortField="InTime"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="入场时间"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="OutTime" DataField="OutTime" SortField="OutTime"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="出场时间"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                              <f:RenderField HeaderText="电话" ColumnID="Telephone" DataField="Telephone" SortField="Telephone"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="110px">                               
                            </f:RenderField>
                             <f:RenderField HeaderText="人员在场" ColumnID="IsUsedName" DataField="IsUsedName" SortField="IsUsedName"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Center" Width="80px">                               
                            </f:RenderField>
                             <f:RenderField HeaderText="考勤卡启用" ColumnID="IsCardUsedName" DataField="IsCardUsedName" SortField="IsCardUsedName"
                                 FieldType="String" HeaderTextAlign="Center" TextAlign="Center" Width="90px">                               
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
                                <f:ListItem Text="所有行" Value="100000" />
                            </f:DropDownList>
                            <f:Label runat="server" Text="红色表示未进行过任何培训人员。"  CssClass="LabelColor"></f:Label>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="编辑人员信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="导入人员信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="1200px" Height="600px">
    </f:Window>
    <f:Window ID="Window3" Title="编辑人员信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window3_Close" IsModal="true"
        Width="800px" Height="550px">
    </f:Window>
    <f:Window ID="WindowPunishRecord" Title="处罚记录" Hidden="true" EnableIFrame="true"
        EnableMaximize="true" Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="1300px" Height="520px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="修改" Icon="Pencil">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Icon="Delete" Text="删除">
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
