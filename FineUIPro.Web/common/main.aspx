﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="FineUIPro.Web.common.main" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>一般岗位首页</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel2" />
    <f:Panel ID="Panel2" runat="server" ShowBorder="True" Layout="VBox" ShowHeader="false"
        BodyPadding="5">
        <Items>
            <f:Panel ID="Panel1" BoxFlex="1" runat="server" ShowBorder="false" ShowHeader="false"
                Layout="HBox" BoxConfigChildMargin="0 2 2 0" MarginBottom="5">
                <Items>       
                    <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                        EnableCollapse="false" ShowBorder="true" Title="左侧显示区" Width="570px" Layout="VBox"
                        ShowHeader="false" AutoScroll="true" BodyPadding="2px" IconFont="ArrowCircleLeft">
                        <Items>
                            <f:Form BodyPadding="1px" ID="form3" EnableCollapse="false" Layout="VBox" runat="server"
                                Title="最新资源" IconFont="Tag" ShowHeader="true" Height="230px" ShowBorder="false">
                                <Rows>
                                    <f:FormRow Margin="5 0 0 0">
                                        <Items>
                                            <f:Grid ID="GridNewDynamic" ShowBorder="false" ShowHeader="false" Title="最新动态" EnableCollapse="true"
                                                ShowGridHeader="false" runat="server" BoxFlex="1" DataKeyNames="Id" AllowCellEditing="true"
                                                EnableColumnLines="true" ClicksToEdit="2" DataIDField="Id" EnableRowDoubleClickEvent="true"
                                                OnRowDoubleClick="GridNewDynamic_RowDoubleClick" SortField="Date" SortDirection="DESC">
                                                <Columns>
                                                    <f:RenderField Width="100px" ColumnID="Type" DataField="Type" FieldType="String"
                                                        HeaderText="类别" HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>
                                                    <f:RenderField ColumnID="Name" DataField="Name" FieldType="String" Width="320px"
                                                        HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                    </f:RenderField>
                                                    <f:RenderField Width="90px" ColumnID="Date" DataField="Date" FieldType="Date" Renderer="Date"
                                                        RendererArgument="yyyy-MM-dd" HeaderText="日期" HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="Url" DataField="Url"
                                                        FieldType="String" HeaderText="路径" Hidden="true" HeaderTextAlign="Center">                                      
                                                    </f:RenderField>                                    
                                                </Columns>
                                                <Listeners>
                                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu1" />
                                            </Listeners>
                                            </f:Grid>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                            <f:Form BodyPadding="1px" ID="formRight1" EnableCollapse="false" Layout="VBox" runat="server"
                                Title="项目图片" IconFont="Tag" ShowHeader="true" Height="300px"  ShowBorder="false">
                                <Rows>
                                    <f:FormRow Margin="0 0 0 0">
                                        <Items>
                                            <f:ContentPanel ID="picContent" runat="server" ShowHeader="false" Margin="5 5 5 5">
                                               <script type="text/javascript">
                                                   var focus_width = 550
                                                   var focus_height = 240
                                                   var text_height = 17
                                                   var swf_height = focus_height + text_height
                                                   var texts = "<%=texts%>"
                                                   var pics = "<%=pics%> "
                                                   var links = "<%=links%>"
                                                   document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="' + focus_width + '" height="' + swf_height + '">');
                                                   document.write('<param name="allowScriptAccess" value="sameDomain"><param name="movie" value="../Images/focus.swf"><param name=wmode value=transparent><param name="quality" value="high">');
                                                   document.write('<param name="menu" value="false"><param name=wmode value="opaque">');
                                                   document.write('<param name="FlashVars" value="pics=' + pics + '&links=' + links + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '">');
                                                   document.write('<embed src="../Images/focus.swf" wmode="opaque" FlashVars="pics=' + pics + '&links=' + links + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '" menu="false" bgcolor="#DADADA" quality="high" width="' + focus_width + '" height="' + swf_height + '" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />');
                                                   document.write('</object>');
                                               </script>
                                             </f:ContentPanel> 
                                        </Items>
                                     </f:FormRow>                          
                                   </Rows>
                               </f:Form> 
                            </Items>
                      </f:Panel>             
                    <f:Panel runat="server" ID="Panel7" RegionPosition="Right" RegionSplit="true"
                        EnableCollapse="false" Title="右边显示区" ShowBorder="true" Width="570px" ShowHeader="false"
                        AutoScroll="true" BodyPadding="2px" IconFont="ArrowCircleRight">
                        <Items>
                         <f:Form BodyPadding="1px" ID="form4" EnableCollapse="false" Layout="VBox" runat="server" TitleToolTip="近三个月通知通告"
                                Title="通知通告" IconFont="Tag" ShowHeader="true" Height="230px" ShowBorder="false">
                                <Rows>
                                    <f:FormRow Margin="5 0 0 0">
                                        <Items>
                                            <f:Grid ID="GridNotice" ShowBorder="false" ShowHeader="false" Title="待办事项" EnableCollapse="true"
                                                ShowGridHeader="false" runat="server" BoxFlex="1" DataKeyNames="Id" AllowCellEditing="true"
                                                EnableColumnLines="true" ClicksToEdit="2" DataIDField="Id" EnableRowDoubleClickEvent="true"
                                                OnRowDoubleClick="GridNotice_RowDoubleClick" SortField="Date" SortDirection="DESC">
                                                <Columns>
                                                    <f:RenderField Width="100px" ColumnID="Type" DataField="Type" FieldType="String"
                                                        HeaderText="类别" HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>
                                                    <f:RenderField Width="320px" ColumnID="Name" DataField="Name" FieldType="String"
                                                        HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true" >
                                                    </f:RenderField>
                                                    <f:RenderField Width="90px" ColumnID="Date" DataField="Date" FieldType="Date" Renderer="Date"
                                                        RendererArgument="yyyy-MM-dd" HeaderText="日期" HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>
                                                    <f:RenderField Width="10px" ColumnID="Url" DataField="Url" FieldType="String" HeaderText="路径"
                                                        Hidden="true" HeaderTextAlign="Center">                                        
                                                    </f:RenderField>
                                                </Columns>
                                                <Listeners>
                                                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu2" />
                                                </Listeners>
                                            </f:Grid>
                                       </Items>
                                     </f:FormRow>                          
                                 </Rows>
                          </f:Form> 
                         <f:Form BodyPadding="1px" ID="form2" EnableCollapse="false" Layout="VBox" runat="server"
                                Title="资质警告" IconFont="Tag" ShowHeader="true" Height="300px"  ShowBorder="false">
                                <Rows>
                                    <f:FormRow Margin="5 0 0 0">
                                        <Items>
                                            <f:Grid ID="GridNewContent" ShowBorder="false" ShowHeader="false" Title="资质警告" EnableCollapse="true"
                                                ShowGridHeader="false" runat="server" BoxFlex="1" DataKeyNames="Id" AllowCellEditing="true"
                                                EnableColumnLines="true" ClicksToEdit="2" DataIDField="Id" EnableRowDoubleClickEvent="true"
                                                OnRowDoubleClick="GridNewContent_RowDoubleClick" SortField="Date" SortDirection="DESC">
                                                <Columns>
                                                   <f:RenderField Width="180px" ColumnID="Type" DataField="Type" FieldType="String"
                                                        HeaderText="类别" HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>
                                                    <f:RenderField Width="400px" ColumnID="Name" DataField="Name" FieldType="String"
                                                        HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true" >
                                                    </f:RenderField>                    
                                                </Columns>
                                                <Listeners>
                                                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu2" />
                                                </Listeners>
                                            </f:Grid>
                                        </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                        </Items>
                    </f:Panel>                                     
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="详细页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        Width="1000px" Height="640px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuOpen1" OnClick="btnMenuOpen1_Click" EnablePostBack="true"
            runat="server" Text="展开">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuClose1" OnClick="btnMenuClose1_Click" EnablePostBack="true"
            runat="server" Text="收起">
        </f:MenuButton>
    </f:Menu>
    <f:Menu ID="Menu2" runat="server">
        <f:MenuButton ID="btnMenuOpen2" OnClick="btnMenuOpen2_Click" EnablePostBack="true"
            runat="server" Text="展开">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuClose2" OnClick="btnMenuClose2_Click" EnablePostBack="true"
            runat="server" Text="收起">
        </f:MenuButton>
    </f:Menu>
     <f:Menu ID="Menu3" runat="server">
        <f:MenuButton ID="btnMenuOpen3" OnClick="btnMenuOpen3_Click" EnablePostBack="true"
            runat="server" Text="展开">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuClose3" OnClick="btnMenuClose3_Click" EnablePostBack="true"
            runat="server" Text="收起">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
        var menuID1 = '<%= Menu1.ClientID %>';
        var menuID2 = '<%= Menu2.ClientID %>';
        var menuID3 = '<%= Menu3.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu1(event, rowId) {
            F(menuID1).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function onRowContextMenu2(event, rowId) {
            F(menuID2).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function onRowContextMenu3(event, rowId) {
            F(menuID3).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGridNewDynamic() {
            __doPostBack(null, 'reloadGridNewDynamic');
        }
        function reloadGridNotice() {
            __doPostBack(null, 'reloadGridNotice');
        }
    </script>
</body>
</html>
