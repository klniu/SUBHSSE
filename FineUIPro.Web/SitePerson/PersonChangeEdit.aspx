﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonChangeEdit.aspx.cs"
    Inherits="FineUIPro.Web.SitePerson.PersonChangeEdit" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>出入场</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="单位" EnableEdit="true" Required="true"
                        ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
              <f:FormRow>
                <Items>
                    <f:RadioButtonList runat="server" ID="rbIsIn" Label="出/入场"  Width="400px" 
                        LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                        <f:RadioItem Value="True" Text="入场"/>
                        <f:RadioItem Value="False" Selected="true" Text="出场" />
                    </f:RadioButtonList>
                    <f:DatePicker ID="txtChangeTime" runat="server" Label="时间" EnableEdit="true" ShowRedStar="true" Required="true">
                    </f:DatePicker> 
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                   <f:DropDownBox runat="server" ID="drpPerson" Label="现场人员" EmptyText="请从下拉表格中选择" MatchFieldWidth="false" LabelAlign="Right"
                        EnableMultiSelect="true">
                        <PopPanel>
                            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="PersonId" DataTextField="PersonName"
                                DataKeyNames="PersonId"  AllowSorting="true" SortField="PersonName" SortDirection="ASC" EnableColumnLines="true"
                                Hidden="true" Width="700px" Height="360px" EnableMultiSelect="true" KeepCurrentSelection="true" EnableCheckBoxSelect="True" PageSize="300">
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                                        <Items>
                                            <f:TextBox runat="server" Label="姓名" ID="txtPersonName" EmptyText="输入查询条件" 
                                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                                                </f:TextBox>
                                                <f:TextBox runat="server" Label="身份证号码" ID="txtIdentityCard" EmptyText="输入查询条件" 
                                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="90px">
                                                </f:TextBox>                                                                          
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>
                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" 
                                        HeaderTextAlign="Center" TextAlign="Center"/>
                                    <f:RenderField Width="150px" ColumnID="CardNo" DataField="CardNo" EnableFilter="true"
                                        SortField="CardNo" FieldType="String" HeaderText="卡号" HeaderTextAlign="Center"
                                        TextAlign="Left">                      
                                    </f:RenderField>
                                    <f:RenderField Width="150px" ColumnID="PersonName" DataField="PersonName" 
                                        SortField="PersonName" FieldType="String" HeaderText="姓名" HeaderTextAlign="Center"
                                        TextAlign="Left">                       
                                    </f:RenderField>
                                    <f:RenderField Width="150px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard" FieldType="String"
                                        HeaderText="身份证号码" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                    </f:RenderField>
                                </Columns>
                            </f:Grid>
                        </PopPanel>
                    </f:DropDownBox>
                </Items>
            </f:FormRow>          
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
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
