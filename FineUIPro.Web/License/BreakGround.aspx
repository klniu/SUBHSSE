﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BreakGround.aspx.cs"
    Inherits="FineUIPro.Web.License.BreakGround" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>动土作业票</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="动土作业票" EnableCollapse="true"
                runat="server" BoxFlex="1"  DataKeyNames="BreakGroundId" AllowCellEditing="true"  ClicksToEdit="2" 
                DataIDField="BreakGroundId" AllowSorting="true"
                SortField="ApplyDate" SortDirection="DESC" EnableColumnLines="true" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>  
                            <f:DropDownList ID="drpUnit" runat="server" Label="申请单位" Width="350px" LabelWidth="80px"
                                EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>   
                            <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                            <f:RadioButtonList ID="drpStates" runat="server" Width="400px" 
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:RadioButtonList>                            
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>                            
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
                    <f:RenderField Width="90px" ColumnID="LicenseCode" DataField="LicenseCode"
                        SortField="LicenseCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="210px" ColumnID="ApplyUnitName" DataField="ApplyUnitName" SortField="ApplyUnitName"
                        FieldType="String" HeaderText="申请单位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="160px" ColumnID="WorkPalce" DataField="WorkPalce" SortField="WorkPalce"
                        FieldType="String" HeaderText="作业地点" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>         
                     <f:RenderField Width="100px" ColumnID="WorkDepth" DataField="WorkDepth" SortField="WorkDepth"
                        FieldType="String" HeaderText="开挖深度" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> 
                    <f:RenderField Width="140px" ColumnID="ApplyDate" DataField="ApplyDate" SortField="ApplyDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd HH:mm" HeaderText="申请日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="ValidityStartTime" DataField="ValidityStartTime" SortField="ValidityStartTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd HH:mm" HeaderText="有效期开始时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="ValidityEndTime" DataField="ValidityEndTime" SortField="ValidityEndTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd HH:mm" HeaderText="有效期结束时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="WorkMeasures" DataField="WorkMeasures" 
                        SortField="WorkMeasures" FieldType="String" HeaderText="作业内容、机具及安全措施" HeaderTextAlign="Center"
                        TextAlign="Left">
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
    <f:Window ID="Window1" Title="动土作业票" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1000px" Height="640px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" EnablePostBack="true"
            runat="server" Text="查看"  Icon="TableGo">
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
