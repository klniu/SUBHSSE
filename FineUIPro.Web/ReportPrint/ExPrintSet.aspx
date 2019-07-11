<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExPrintSet.aspx.cs" Inherits="Web.ReportPrint.ExPrintSet" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/chinaexcel.css"/>
    <script charset="gb2312" language="javascript" type="text/javascript" src="js/Common.js" ></script>
    <script src="js/functions.vbs" language="vbscript" type="text/vbscript"></script>    
        <script language="javascript" type="text/javascript">

            function InitFontname() {
                strFontnames = ChinaExcel.GetDisplayFontNames();
                var arrFontname = strFontnames.split('|');
                arrFontname.sort();
                var i;
                var sysFont;
                sysFont = "����";

                for (i = 0; i < arrFontname.length; i++) {
                    if (arrFontname[i] != "") {
                        var oOption = document.createElement("OPTION");
                        FontNameSelect.options.add(oOption);
                        oOption.innerText = arrFontname[i];
                        oOption.value = arrFontname[i];
                        if (arrFontname[i] == sysFont) oOption.selected = true;
                    }
                }
            } 

            function window_onresize() {
                var lWidth = document.body.offsetWidth;
                if (lWidth <= 0) lWidth = 1;
                ChinaExcel.style.width = lWidth;

                var lHeight = document.body.offsetHeight - parseInt(ChinaExcel.style.top);
                if (lHeight <= 0) lHeight = 1;
                ChinaExcel.style.height = lHeight;
            }

            function window_onload() {
                var aw = screen.availWidth;
                var ah = screen.availHeight;

                ChinaExcel.border = 0;
                ChinaExcel.style.left = 0;

                ChinaExcel.style.top = idTBFormat.offsetTop + idTBFormat.offsetHeight;
                var lWidth = document.body.offsetWidth;
                if (lWidth <= 0) lWidth = 1;
                ChinaExcel.style.width = lWidth;

                var lHeight = document.body.offsetHeight - parseInt(ChinaExcel.style.top);
                if (lHeight <= 0) lHeight = 1;
                ChinaExcel.style.height = lHeight;

                ChinaExcel.style.display = "";
                ChinaExcel.SetMaxRows(18);
                ChinaExcel.SetMaxCols(8);
                ChinaExcel.DesignMode = true;
                //Ĭ��״̬����ʾ������ƽ���(���Ĭ�ϴ򿪱�����ʱ��readyState����3���򿪻�ʧ��)	tcf
                ChinaExcel.SetOnlyShowTipMessage(true);

                InitFontname();
                init();              
            }

            function OnSetOnePrintPageDetailZoneRows() {
                nPageRows = ChinaExcel.GetOnePrintPageDetailZoneRows()
                varvalue = window.prompt("˵������ӡʱÿҳ��ʾ������,��������ͷ�ͱ�βҳ�š�ҳǰ�ŵ�������������ÿҳ��ӡ��������", "����ÿҳ��ӡ������");
                nRow=parseInt(varvalue);

                if (!isNaN(nRow)) {
                    ChinaExcel.SetOnePrintPageDetailZoneRows(nRow);
                }
            }            
    </script>
<script for="cbButton" event="onclick()"	language="JavaScript" >
	return onCbClickEvent(this);
</script>
</head>

<body id="mainbody" class="mainBody" language="javascript" onresize="return window_onresize()"
    onload="return window_onload()">
<form id ="form1" runat="server">
        <table class="cbToolbar" style="width: 100%;" border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td >
                   &nbsp;
                      <a  href="#" class="tbButton" onclick="onFileOpen()" title="�򿪱����ļ�" ><img alt="" style="vertical-align:middle" src="images/open.gif" width="16" height="16"/></a>
                  </td>
                  <td >
                      <a  href="#" class="tbButton" onclick="onReportSave('<%=reportId %>','<%=reportName %>')"  title="�����ļ�"><img alt="" style="vertical-align:middle;" src="images/save.gif" width="16" height="16"/></a>
                  </td>
                  <td>
                      <a href="#" class="tbButton"  onclick="PrintSetup()" title="��ӡ����"><img alt="" style="vertical-align:middle" src="images/printsetup.gif" width="16" height="16"/></a>
                  </td>

                   <td >
                      <a href="#" class="tbButton"  onclick="PrintpaperSet()" title="ҳ������"><img alt="" style="vertical-align:middle" src="images/printpaperset.gif" width="16" height="16"/></a>
                  </td>
                   <td class="tbDivider">
                       <a class="tbButton" onclick="OnSetOnePrintPageDetailZoneRows()" title="����ÿҳ��ӡ������" href="#"><img align="middle" alt="" src="images/desgin.gif" width="16" height="16" /></a>
                     <%--  <a href="#" class="tbButton"  onclick="OnSetOnePrintPageDetailZoneRows()" title="����ÿҳ��ӡ������"><img alt="" style="vertical-align:middle" src="images/bold.gif" width="16" height="16"/></a>--%>
                  </td>

                  <td>
                     <a  href="#" class="tbButton" onclick="onCut()"  title="����"><img alt="" style="vertical-align:middle" src="images/cut.gif" width="16" height="16"/></a>
                  </td>
                  <td>
                      <a  href="#" class="tbButton" onclick="onCopy()"  title="����"><img alt="" style="vertical-align:middle" src="images/copy.gif" width="16" height="16"/></a>
                  </td>
                  <td class="tbDivider">
                     <a  href="#" class="tbButton" onclick="onPaste()"  title="ճ��"><img alt="" style="vertical-align:middle" src="images/paste.gif" width="16" height="16"/></a>
                  </td>
                   <td>
                    <a  href="#"  class="tbButton" id="cmdCurrency" name="cbButton" title="���ҷ���" ><img alt="" style="vertical-align:middle" src="images/currency.gif" width="16" height="16"/></a>
                  </td>
                  <td>
                      <a  href="#" class="tbButton" id="cmdPercent" name="cbButton"  title="�ٷֺ�"><img alt="" style="vertical-align:middle" src="images/percent.gif" width="16" height="16"/></a>
                  </td>
                  <td class="tbDivider">
                     <a  href="#" class="tbButton" id="cmdThousand"  name="cbButton" title="ǧ��λ"><img alt="" style="vertical-align:middle" src="images/thousand.gif" width="16" height="16"/></a>
                  </td>
                  <td>
                     <a  href="#" class="tbButton" onclick="OnSetCellShowStyle()"  title="���õ�Ԫ��ʽ">
                         <img alt="" style="vertical-align:middle" src="images/cellstyle.gif" width="16" height="16"/></a>
                  </td>
                  <td><a class="tbButton" id="cmdChartWzd" title="ͼ����" href="#" name="cbButton"><img alt="" style="vertical-align:middle" src="images/chartw.gif" width="16" height="16" /></a></td>
                   
                  <td>
                      <a href="#" class="tbButton"  onclick="SlashSet()" title="��Ԫб������"><img alt="" style="vertical-align:middle" src="images/slashset.gif" width="16" height="16"/></a>
                  </td>
                 <%-- <td >
                      <asp:ImageButton ID="undo" runat="server" class="tbButton"  OnClientClick="return confirm(&quot;ȷ��Ҫ�ָ���ʼ������&quot;);" 
                           ImageUrl="images/undo.gif" ToolTip="�ָ���ʼ����" 
                           style="vertical-align:middle"  width="16" height="16" onclick="undo_Click" />
                  </td>--%>
                   <td runat="server" >
                      <asp:ImageButton ID="imgReturn" runat="server" class="tbButton"
                           ImageUrl="images/undo.gif" ToolTip="����" 
                           style="vertical-align:middle"  width="16px" height="16px" 
                           onclick="imgReturn_Click"/>
                  </td>
                <td style="width:100%" align="right">
                    <asp:Label runat="server" ID="lbReportName" style="font-size:14px;font-weight:bold;" ForeColor="Red"></asp:Label>
                   <%--<a  href="#" class="tbButton" onclick="DefineField('<%=reportName %>')" title="�����ֶ�" ><img alt="" style="vertical-align:middle" src="images/export.gif" width="16" height="16"/></a>--%>
                </td>
            </tr>
         </table>
    </form>
    <table class="cbToolbar" id="idTBFormat" cellpadding='0' cellspacing='0' width="100%">
            <tr>
                <td id="cmdFontName" title="����">
                    <select name="FontNameSelect" style="width: 220px; height: 23px" onchange="changeFontName(FontNameSelect.value)"
                        accesskey="v" size="1">
                    </select>
                </td>
                <td class="tbDivider" id="cmdFontSize" title="�ֺ�">
                    <select name="FontSizeSelect" style="width: 67px; height: 23px" onchange="changeFontSize(FontSizeSelect.value)"
                        accesskey="v" size="1">
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option selected="selected" value="10">10</option>
                        <option value="11">11</option>
                        <option value="12">12</option>
                        <option value="14">14</option>
                        <option value="16">16</option>
                        <option value="18">18</option>
                        <option value="20">20</option>
                        <option value="22">22</option>
                        <option value="24">24</option>
                        <option value="26">26</option>
                        <option value="28">28</option>
                        <option value="30">30</option>
                        <option value="36">36</option>
                        <option value="42">42</option>
                        <option value="48">48</option>
                        <option value="72">72</option>
                        <option value="100">100</option>
                        <option value="150">150</option>
                        <option value="300">300</option>
                        <option value="500">500</option>
                        <option value="800">800</option>
                        <option value="1200">1200</option>
                        <option value="2000">2000</option>
                    </select>
                </td>
                <td>
                    <a class="tbButton" id="cmdBold" title="����" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/bold.gif" width="16" height="16" /></a>
                </td>
                <td>
                    <a class="tbButton" id="cmdItalic" title="б��" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/italic.gif" width="16" height="16" /></a>
                </td>
                <td>
                    <a class="tbButton" id="cmdUnderline" title="�»���" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/underline.gif" width="16" height="16" /></a>
                </td>
                <td>
                    <a class="tbButton" id="cmdBackColor" title="����ɫ" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/backcolor.gif" width="16" height="16" /></a>
                </td>
                <td class="tbDivider">
                    <a class="tbButton" id="cmdForeColor" title="ǰ��ɫ" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/forecolor.gif" width="16" height="16" /></a>
                </td>
                <td>
                    <a class="tbButton" id="cmdWordWrap" title="�Զ�����" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/wordwrap.gif" width="16" height="16" /></a>
                </td>
                <td>
                    <a class="tbButton" id="cmdAlignLeft" title="�������" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/alignleft.gif" width="16" height="16" /></a>
                </td>
                <td>
                    <a class="tbButton" id="cmdAlignCenter" title="���ж���" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/aligncenter.gif" width="16" height="16" /></a>
                </td>
                <td>
                    <a class="tbButton" id="cmdAlignRight" title="���Ҷ���" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/alignright.gif" width="16" height="16" /></a>
                </td>
                <td>
                    <a class="tbButton" id="cmdAlignTop" title="���϶���" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/aligntop.gif" width="16" height="16" /></a>
                </td>
                <td>
                    <a class="tbButton" id="cmdAlignMiddle" title="��ֱ����" href="#" name="cbButton" sticky="true">
                        <img align="middle" alt="" src="images/alignmiddle.gif" width="16" height="16" /></a>
                </td>
                <td class="tbDivider">
                    <a class="tbButton" id="cmdAlignBottom" title="���¶���" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/alignbottom.gif" width="16" height="16" /></a>
                </td>
                <td id="cmdBoderType" title="�߿�����">
                    <select name="BorderTypeSelect" style="width: 109px; height: 23px" accesskey="v"
                        size="1">
                        <option value="0" selected="selected">ϸ��</option>
                        <option value="1">����</option>
                        <option value="2">����</option>
                        <option value="3">����</option>
                        <option value="4">����</option>
                        <option value="5">�㻮��</option>
                        <option value="6">��㻮��</option>
                    </select>
                </td>
                <td>
                    <a class="tbButton" id="cmdDrawBorder" title="���߿���" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/border.gif" width="16" height="16" /></a>
                </td>
                <td>
                    <a class="tbButton" id="cmdEraseBorder" title="Ĩ�߿���" href="#" name="cbButton">
                        <img align="middle" alt="" src="images/erase.gif" width="16" height="16" /></a>
                </td>
                <td class="tbDivider" width="100%">
                </td>
            </tr>
      </table>         
    <table  style="width: 100%;" border="0" cellpadding="1" cellspacing="1">
        <tr>
            <td style="width: 100%;">
                <object id="ChinaExcel"  name="ChinaExcel" style="left: 0px; top: 0px; width: 100%; height:500px; text-align:center;" 
                        classid="CLSID:15261F9B-22CC-4692-9089-0C40ACBDFDD8" codebase="../Downloads/chinaexcelweb.cab#version=3,8,9,2">
                    <param name="_Version" value="131072" />
                    <param name="_ExtentX" value="28205" />
                    <param name="_ExtentY" value="13229" />
                    <param name="_StockProps" value="0" />
                </object>
            </td>
        </tr>
    </table>       
    <script type="text/javascript" language="javascript">
         function init() {
             setInit('ReadExReportFile.aspx?reportId=' + " <%=reportId %>");                   
             ChinaExcel.SetOnlyShowTipMessage(false);
         }
    </script>
     <script language="vbscript" type="text/vbscript">
        Sub ChinaExcel_ShowCellChanged(Row, Col)
	      FontSizeSelect.Value = ChinaExcel.CellFontSize
	      FontNameSelect.value = ChinaExcel.CellFontName
          End Sub 
    </script>
</body>
</html>