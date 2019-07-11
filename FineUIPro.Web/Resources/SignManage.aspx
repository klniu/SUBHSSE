﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignManage.aspx.cs" Inherits="FineUIPro.Web.Resources.SignManage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>标牌管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="标牌管理" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="SignManageId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="SignManageId" AllowSorting="true" SortField="SignType,SignCode" SortDirection="ASC" OnSort="Grid1_Sort" 
                AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" 
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" Width="980px" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                              <f:TextBox runat="server" Label="标牌编号" ID="txtSignCode" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                             </f:TextBox>
                             <f:DropDownList runat="server" ID="drpSignType" Label="标牌类型" EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                             </f:DropDownList>                    
                             <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                   <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="120px" ColumnID="SignTypeName" DataField="SignTypeName" SortField="SignTypeName"
                        FieldType="String" HeaderText="标牌类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                  
                    <f:RenderField Width="120px" ColumnID="SignName" DataField="SignName"
                        SortField="SignName" FieldType="String" HeaderText="标牌名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="SignCode" DataField="SignCode" SortField="SignCode"
                        FieldType="String" HeaderText="标牌编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                   <f:RenderField Width="120px" ColumnID="SignLen" DataField="SignLen" SortField="SignLen"
                        FieldType="String" HeaderText="标牌长度" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="SignWide" DataField="SignWide" SortField="SignWide"
                        FieldType="String" HeaderText="标牌宽度" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="SignHigh" DataField="SignHigh" SortField="SignHigh"
                        FieldType="String" HeaderText="标牌高度" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="SignThick" DataField="SignThick" SortField="SignThick"
                        FieldType="String" HeaderText="标牌厚度" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="Material" DataField="Material" SortField="Material"
                        FieldType="String" HeaderText="标牌材质" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="SignArea" DataField="SignArea" SortField="SignArea"
                        FieldType="String" HeaderText="标牌位置" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                     <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
    <f:Window ID="Window1" Title="标牌管理" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true"
        Width="800px" Height="540px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" Hidden="true"
            runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function onGridDataLoad(event) {
            this.mergeColumns(['SignTypeName']);
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
