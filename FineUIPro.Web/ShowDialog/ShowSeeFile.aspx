﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowSeeFile.aspx.cs" Async="true" Inherits="FineUIPro.Web.ShowDialog.ShowSeeFile" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑文档</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>           
           <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                 <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server" Height="30px">
                        <Items>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存"
                                Hidden="true" OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items> 
                    <f:HtmlEditor runat="server" Label="详细" ID="txtSeeFile" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="510">
                    </f:HtmlEditor>                  
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>  
    </form>
</body>
</html>
