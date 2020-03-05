﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitEdit.aspx.cs" Inherits="FineUIPro.Web.SysManage.UnitEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑单位设置</title>
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
                    <f:TextBox ID="txtUnitCode" runat="server" Label="单位代码" Required="true" MaxLength="100" ShowRedStar="true" FocusOnPageLoad="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" >
                    </f:TextBox>
                     <f:TextBox ID="txtUnitName" runat="server" Label="单位名称" Required="true" MaxLength="200" ShowRedStar="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                </Items>
            </f:FormRow>         
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="ddlUnitTypeId" runat="server" Label="单位类型" EnableEdit="true"
                        ForceSelection="false">
                    </f:DropDownList>
                      <f:TextBox ID="txtCorporate" runat="server" Label="法人代表" MaxLength="100">
                    </f:TextBox>
                </Items>
            </f:FormRow>            
           
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTelephone" runat="server" Label="联系电话" MaxLength="20">
                    </f:TextBox>
                    <f:TextBox ID="txtEMail" runat="server" Label="邮箱" MaxLength="200">
                    </f:TextBox>
                </Items>
            </f:FormRow>            
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtFax" runat="server" Label="传真" MaxLength="20">
                    </f:TextBox>
                    <f:TextBox ID="txtAddress" runat="server" Label="单位地址" MaxLength="500">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtProjectRange" runat="server" Height="100px" Label="工程范围" MaxLength="1000">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:RadioButtonList runat="server" ID="rblIsThisUnit" Label="本单位" Enabled="false" Hidden="true">
                        <f:RadioItem Value="true" Text="是" />
                        <f:RadioItem Value="false" Text="否" Selected="true" />
                    </f:RadioButtonList>
                    <f:RadioButtonList runat="server" ID="rblIsBranch" Label="分公司" AutoPostBack="true" OnSelectedIndexChanged="rblIsBranch_SelectedIndexChanged">
                        <f:RadioItem Value="true" Text="是" />
                        <f:RadioItem Value="false" Text="否" Selected="true" />
                    </f:RadioButtonList>
                    <f:DropDownList runat="server" ID="drpSupUnit" Label="上级单位" Hidden="true" EnableEdit="true"></f:DropDownList>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"  Hidden="true"
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
