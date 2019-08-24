﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralEquipmentOutItemEdit.aspx.cs" Inherits="FineUIPro.Web.InApproveManager.GeneralEquipmentOutItemEdit"  ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑出场机具设备清单</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpSpecialEquipmentId" runat="server" Label="设备" Required="true"
                        ShowRedStar="true" LabelAlign="Right" LabelWidth="140px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSizeModel" runat="server" Label="规格型号" LabelAlign="Right" MaxLength="50"
                        LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtOwnerCheck" runat="server" Label="进场前自查自检情况" LabelAlign="Right"
                        MaxLength="50" LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCertificateNum" runat="server" Label="施工设备合格证号" LabelAlign="Right"
                        MaxLength="50" LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtInsuranceNum" runat="server" Label="保险单号" LabelAlign="Right" MaxLength="50"
                        LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpOutReason" runat="server" Label="出场理由" LabelAlign="Right"
                        LabelWidth="140px">
                        <f:ListItem Value="退回供应商" Text="退回供应商" Selected="true" />
                        <f:ListItem Value="转场" Text="转场" />
                        <f:ListItem Value="承包商物资" Text="承包商物资" />
                        <f:ListItem Value="其他" Text="其他" />
                    </f:DropDownList>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
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
