﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysConstSet.aspx.cs" Inherits="FineUIPro.Web.SysManage.SysConstSet" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统环境设置</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
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
    <f:PageManager ID="PageManager1" runat="server" />         
      <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%" MarginRight="5px">
            <Items>
               <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="510px" ShowBorder="true"
                    TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server" 
                    ActiveTabIndex="1">              
                   <Tabs>                   
                        <f:Tab ID="Tab1" Title="系统环境" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>
                                 <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:CheckBox ID="ckSynchronization" runat="server" Label="是否自动提取数据" LabelWidth="200px"> 
                                                </f:CheckBox>
                                                <f:Label ID="Label7" runat="server" Text="说明：与集团数据交换数据是否自动提取。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtNumberBox" runat="server" Label="有效期警示" LabelWidth="200px"> </f:NumberBox>
                                                <f:Label ID="Label9" runat="server" Text="说明：用于资质有效期警示时间。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>                                 
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:CheckBox ID="ckIsMonthReportGetAVG" runat="server" Label="人工时月报是否按平均数取值" LabelWidth="200px"> 
                                                </f:CheckBox>
                                                <f:Label ID="Label10" runat="server" Text="说明：人工时月报统计时，是否按照平均人数、平均工时数及平均天数取值。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="txtMonthReportFreezeDay" runat="server" Label="管理月报冻结日期" NoDecimal="true" NoNegative="true" MinValue="1" LabelWidth="200px"> 
                                                </f:NumberBox>
                                                <f:Label ID="Label11" runat="server" Text="说明：项目现场管理月报的冻结日期，如设置为4，则当月月报在次月4日24时以后冻结，不可再进行修改。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                         <f:FormRow ColumnWidths="35% 65%" runat="server" ID="frFlowOperate" Hidden="true">
                                            <Items>
                                                 <f:CheckBox ID="ckMenuFlowOperate" runat="server" Label="固定流程" LabelWidth="200px"> 
                                                </f:CheckBox>
                                                <f:Label ID="Label12" runat="server" Text="说明：设置固定流程单据审核流程按照已定义流程审核，不设置在在单据中自主选择流程。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <f:FormRow ColumnWidths="35% 65%" runat="server" ID="frChangeData" Hidden="true">
                                            <Items>
                                                 <f:CheckBox ID="ckChangeData" runat="server" Label="博晟数据提取" LabelWidth="200px"> 
                                                </f:CheckBox>
                                                <f:Label ID="Label13" runat="server" Text="说明：设置单位人员培训从博晟提取数据，不设置不从博晟提取。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>
                                        <%--<f:FormRow ColumnWidths="35% 65%">
                                            <Items>
                                                <f:NumberBox ID="NumberBox1" runat="server" Label="企业安全管理资料月报截止日期" LabelWidth="200px"> 
                                                </f:NumberBox>
                                                <f:Label ID="Label12" runat="server" Text="说明：项目现场每月上报企业安全管理资料截止日期。" MarginLeft="50px"></f:Label>
                                            </Items>
                                        </f:FormRow>--%>                                        
                                    </Rows>
                                 </f:Form>
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                   <Items>
                                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                                        OnClick="btnSave_Click">
                                    </f:Button>    
                                    <f:Button ID="btnArrowRefresh" Icon="ArrowRefresh" runat="server" ToolTip="多附件路径转换" OnClick="btnArrowRefresh_Click">
                                    </f:Button>                
                                </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab2" Title="菜单功能" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                             <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                   <Items>
                                     <f:RadioButtonList ID="rblMenuType" runat="server" Width="120px"
                                         OnSelectedIndexChanged="rblMenuType_SelectedIndexChanged" AutoPostBack="true">
                                        <f:RadioItem Value="Menu_Server" Text="本部" />
                                        <f:RadioItem Value="Menu_Project" Text="项目" Selected="true" />
                                    </f:RadioButtonList>
                                    <f:DropDownBox runat="server" ID="drpMenu" Values="henan" EmptyText="请选择末级菜单" Width="500px" 
                                        EnableMultiSelect="false" AutoPostBack="true" OnTextChanged="drpMenu_TextChanged">
                                        <PopPanel>
                                            <f:Tree ID="treeMenu" ShowHeader="false" Hidden="true" runat="server" EnableSingleExpand="true">
                                            </f:Tree>
                                        </PopPanel>
                                    </f:DropDownBox>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnTab2Save" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm2"
                                        OnClick="btnTab2Save_Click">
                                    </f:Button>                                     
                                </Items>
                              </f:Toolbar>
                            </Toolbars>                            
                            <Items>
                                <f:TabStrip ID="TabStrip2" CssClass="f-tabstrip-theme-simple" ShowBorder="true" Height="400px"
                                    TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" 
                                    runat="server" ActiveTabIndex="2">
                                    <Tabs>
                                        <f:Tab ID="TabCode" Title="文档编码及归类" BodyPadding="5px" Layout="Fit" runat="server">
                                           <Items>
                                            <f:Form ID="SimpleForm2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                <Rows>                                        
                                                  <f:FormRow>
                                                    <Items>                                                       
                                                        <f:CheckBox runat="server" ID="ckProjectCode" Label="项目号" LabelWidth="150px"></f:CheckBox>                                                        
                                                        <f:Label runat="server" Text="说明：编号前是否添加项目代号。" MarginLeft="50px"></f:Label>
                                                    </Items>
                                                  </f:FormRow>  
                                                   <f:FormRow>
                                                    <Items>                                                       
                                                       <f:TextBox runat="server" ID="txtPrefix" Label="前缀" MaxLength="50" EmptyText="请输入前缀" LabelWidth="150px"></f:TextBox>
                                                       <f:Label ID="Label1" runat="server" Text="说明：编号添加前缀的字符。" MarginLeft="50px"></f:Label>
                                                    </Items>
                                                  </f:FormRow>   
                                                  <f:FormRow>
                                                    <Items>
                                                        <f:CheckBox runat="server" ID="ckUnitCode" Label="单位代号" LabelWidth="150px" Width="500px"></f:CheckBox>                                                        
                                                        <f:Label ID="Label2" runat="server" Text="说明：编号前是否添加单位代号。" MarginLeft="50px"></f:Label>
                                                    </Items>
                                                  </f:FormRow>   
                                                  <f:FormRow>
                                                    <Items>
                                                       <f:NumberBox runat="server" ID="txtDigit" Label="流水号位数" NoDecimal="true" NoNegative="true" LabelWidth="150px" ></f:NumberBox>
                                                       <f:Label ID="Label3" runat="server" Text="说明：编号最后生成流水号位数。" MarginLeft="50px"></f:Label>
                                                    </Items>
                                                  </f:FormRow>
                                                  <f:FormRow>
                                                    <Items>
                                                        <f:TextBox runat="server" ID="txtSymbol" Label="间隔符" MaxLength="50" ShowRedStar="true" Required="true" LabelWidth="150px"></f:TextBox>
                                                        <f:Label ID="Label4" runat="server" Text="说明：前缀字符与流水号之间连接字符。" MarginLeft="50px"></f:Label>
                                                    </Items>
                                                  </f:FormRow>   
                                                  <f:FormRow>
                                                    <Items>                                                        
                                                        <f:CheckBox runat="server" ID="ckIsFileCabinetA" Label="文件柜A(集团检查类)" LabelWidth="150px"></f:CheckBox>
                                                        <f:Label ID="Label5" runat="server" Text="说明：业务菜单生成的文档归类。" MarginLeft="50px"></f:Label>
                                                    </Items>
                                                  </f:FormRow> 
                                                   <f:FormRow Hidden="true">
                                                    <Items>                                                        
                                                        <f:CheckBox runat="server" ID="ckIsFileCabinetB" Label="文件柜B(内业)" LabelWidth="150px"></f:CheckBox>
                                                        <f:Label ID="Label6" runat="server" Text="说明：业务菜单生成的文档归类。" MarginLeft="50px"></f:Label>
                                                    </Items>
                                                  </f:FormRow>                                                                                                                                             
                                                </Rows>
                                            </f:Form>
                                         </Items>
                                        </f:Tab>
                                        <f:Tab ID="Tab4" Title="模板设置" BodyPadding="5px"  Layout="Fit" runat="server">
                                            <Items>
                                             <f:HtmlEditor runat="server" Label="模板" ID="txtTemplate" ShowLabel="false"
                                                Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="380px">
                                               </f:HtmlEditor>
                                            </Items>
                                        </f:Tab>
                                        <f:Tab ID="Tab5" Title="单据流程" BodyPadding="5px"  Layout="Fit" runat="server">
                                           <Items>
                                            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true"
                                                runat="server" BoxFlex="1" DataKeyNames="FlowOperateId" AllowCellEditing="true" ClicksToEdit="2"
                                                DataIDField="FlowOperateId" AllowSorting="true" SortField="FlowStep" SortDirection="ASC" 
                                                OnSort="Grid1_Sort" AllowPaging="true"  EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                                                EnableTextSelection="True">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar4" Position="Top"  ToolbarAlign="Right" runat="server">
                                                        <Items>                                                            
                                                            <f:Button ID="btnFlowOperateNew" ToolTip="增加" Icon="Add" runat="server"  OnClick="btnFlowOperateNew_Click" />
                                                            <f:Button ID="btnFlowOperateDelete" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnFlowOperateDelete_Click" runat="server">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:RenderField Width="100px" ColumnID="FlowStep" DataField="FlowStep" FieldType="Int" HeaderText="审批步骤"  HeaderTextAlign="Center" TextAlign="Center">
                                                    </f:RenderField>
                                                        <f:RenderField Width="250px" ColumnID="AuditFlowName" DataField="AuditFlowName"  FieldType="String" HeaderText="步骤名称"  HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>
                                                    <f:TemplateField Width="300px" HeaderText="审批角色" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true"> 
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" runat="server"  Text='<%# ConvertRole(Eval("RoleId")) %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </f:TemplateField>
                                                    <f:CheckBoxField Width="80px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsFlowEnd" HeaderText="是否结束" />
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                        </f:Tab>
                                    </Tabs>
                                </f:TabStrip>
                             </Items>                           
                        </f:Tab>
                        <f:Tab ID="Tab3" Title="其他设置" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                            <Items>                                                               
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                                   <Items>
                                    <f:Button ID="Button2" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                                        OnClick="btnTab3Save_Click">
                                    </f:Button>                                     
                                </Items>
                              </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                    </Tabs>
                </f:TabStrip>
              </Items>          
        </f:Panel>
        <f:Window ID="Window1" Title="流程步骤设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Self" EnableResize="true" runat="server" IsModal="true"  OnClose="Window1_Close"
            Width="640px" Height="320px">
    </f:Window>
    </form>
</body>
</html>
