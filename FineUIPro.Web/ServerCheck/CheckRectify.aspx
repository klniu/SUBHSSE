﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckRectify.aspx.cs"
    Inherits="FineUIPro.Web.ServerCheck.CheckRectify" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>监督检查整改</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }   
        .f-grid-row.yellow
        {
            background-color: YellowGreen;
            background-image: none;
        }
        
        .f-grid-row.red
        {
            background-color: Yellow;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全监督检查整改" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="CheckRectifyId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="CheckRectifyId" AllowSorting="true" SortField="CheckDate"
                SortDirection="DESC" OnSort="Grid1_Sort"  EnableColumnLines="true"
                AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">    
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
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
                    <f:GroupField HeaderText="检查对象" TextAlign="Center" HeaderTextAlign="Center">
                        <Columns>
                            <f:TemplateField Width="240px" HeaderText="检查单位" ColumnID="UnitName" HeaderTextAlign="Center"
                                TextAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("UnitName") %>' ToolTip='<%# Bind("UnitName") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="250px" HeaderText="项目" ColumnID="ProjectId" HeaderTextAlign="Center"
                                TextAlign="Left"  ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectId" runat="server" Text='<%# Bind("ProjectId") %>' ToolTip='<%# Bind("ProjectId") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
                    </f:GroupField>
                    <f:RenderField Width="100px" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="受检时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="IssueMan" DataField="IssueMan" SortField="IssueMan"
                        FieldType="String" HeaderText="签发人" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="IssueDate" DataField="IssueDate" SortField="IssueDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="签发时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="75px" ColumnID="HandleState" DataField="HandleState" FieldType="String"
                        HeaderText="状态" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="70px" ColumnID="TotalCount" DataField="TotalCount" FieldType="String"
                        HeaderText="总项" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="75px" ColumnID="CompleteCount" DataField="CompleteCount" FieldType="String"
                        HeaderText="完成项" TextAlign="Center" HeaderTextAlign="Center">
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
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" runat="server" Hidden="true" ShowHeader="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" OnClose="Window1_Close"
        Title="编辑安全监督检查整改" EnableIFrame="true" Height="600px" Width="1300px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="TabEdit">
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
