﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportDetailEdit.aspx.cs"
    Inherits="FineUIPro.Web.SitePerson.MonthReportDetailEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑单位人工时月报</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Label ID="lbStaffData" runat="server" Label="">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="txtStaffData" runat="server" Label="">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtWorkTime" NoDecimal="false" NoNegative="false" MaxValue="15"
                    MinValue="0" runat="server" Required="true" Label="平均工时" ShowRedStar="true" AutoPostBack="true" OnTextChanged="txtWorkTime_TextChanged">
                    </f:NumberBox>                                        
                    <f:NumberBox ID="txtDayNum" NoDecimal="false" NoNegative="false" MaxValue="31"  MarginLeft="30"
                    MinValue="0" runat="server" Label="天数" ShowRedStar="true" Required="true" AutoPostBack="true" OnTextChanged="txtWorkTime_TextChanged">
                    </f:NumberBox>
                </Items>                  
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" AllowCellEditing="true"
                        ClicksToEdit="1" DataIDField="MonthReportUnitDetailId" DataKeyNames="MonthReportUnitDetailId"
                        EnableMultiSelect="false" ShowGridHeader="true" Height="420px" EnableColumnLines="true">                      
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="100px" ColumnID="WorkPostName" DataField="WorkPostName" SortField="WorkPostName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="岗位">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="CheckPersonNum" DataField="CheckPersonNum"
                                SortField="CheckPersonNum" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="考勤人数">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="RealPersonNum" DataField="RealPersonNum" FieldType="Int"
                                HeaderText="实际人数" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:NumberBox ID="txtRealPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                        runat="server" Required="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="PersonWorkTime" DataField="PersonWorkTime"
                                FieldType="Double" HeaderText="人工时数" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:NumberBox ID="txtPersonWorkTime" NoDecimal="false" NoNegative="true" MinValue="0"
                                        runat="server" Required="true" Readonly="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="300px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                ExpandUnusedSpace="true" HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:TextBox ID="txtRemark" runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                             <f:RenderField Width="1px" ColumnID="MonthReportUnitDetailId" DataField="MonthReportUnitDetailId" 
                                FieldType="String" HeaderText="主键"  Hidden="true" HeaderTextAlign="Center">                                        
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="trAVG">
                <Items>
                    <f:NumberBox ID="txtRealPersonNum2" NoDecimal="false" NoNegative="false"
                        MinValue="0" runat="server" Label="平均人数">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
<script type="text/javascript">
    var txtWorkTimeClientID = '<%= txtWorkTime.ClientID %>';
    var txtDayNumClientID = '<%= txtDayNum.ClientID %>';
    function onGridAfterEdit(event, value, params) {
        var me = this, columnId = params.columnId, rowId = params.rowId;
        if (columnId === 'RealPersonNum') {
            var realPersonNum = me.getCellValue(rowId, 'RealPersonNum');
            var realPersonNumStr;
            if (realPersonNum.toString() == "") {
                realPersonNumStr = 0;
            }
            else {
                realPersonNumStr = parseFloat(realPersonNum);
            }
            me.updateCellValue(rowId, 'PersonWorkTime', (realPersonNumStr * F(txtWorkTimeClientID).getValue() * F(txtDayNumClientID).getValue()).toFixed(2));
        }
    }
</script>
