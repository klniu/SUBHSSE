Public Sub mnuFileNew_click()
	If ChinaExcel.IsModified() Then '�ĵ��Ѿ�������
		rtn = MsgBox( "�ĵ��ѱ����ģ��Ƿ񱣴棿", vbExclamation Or vbYesNoCancel)
		If rtn = vbYes Then
			mnuFileSave_click
		ElseIf rtn = vbCancel Then
			Exit Sub
		End If
	End If
	ChinaExcel.SetMaxRows(0)
	ChinaExcel.SetMaxRows(18)
	ChinaExcel.SetMaxCols(8)
	ChinaExcel.FormProtect = false
	'menu_init
End Sub

'��������
Public Sub mnuEditHyperlink_click()
	strUrl = InputBox( "�����볬�����ӵ�ַ��", "��������", "HTTP://" )
	ChinaExcel.SetCellURLType ChinaExcel.Row,ChinaExcel.Col,strUrl
End Sub

'���ô���
Public Sub cmdBold_click()
	ChinaExcel.Bold = not ChinaExcel.Bold
End Sub

'����б��
Public Sub cmdItalic_click()
	ChinaExcel.Italic = not ChinaExcel.Italic
End Sub

'�����»���
Public Sub cmdUnderline_click()
	ChinaExcel.Underline = not ChinaExcel.Underline
End Sub

'���ñ���ɫ
Public Sub cmdBackColor_click()
	ChinaExcel.OnSetCellBkColor
End Sub

'����ǰ��ɫ
Public Sub cmdForeColor_click()
	ChinaExcel.OnSetTextColor
End Sub

'�Զ�����
Public Sub cmdWordWrap_click()
	ChinaExcel.AutoWrap = not ChinaExcel.AutoWrap
	nMenuID = MenuOcx.GetMenuID("AutoWrap")
	MenuOcx.SetMenuChecked nMenuID,ChinaExcel.AutoWrap
End Sub

'�������
Public Sub cmdAlignLeft_click()
	ChinaExcel.HorzTextAlign = 1
End Sub

'���ж���
Public Sub cmdAlignCenter_click()
	ChinaExcel.HorzTextAlign = 2
End Sub

'���Ҷ���
Public Sub cmdAlignRight_click()
	ChinaExcel.HorzTextAlign = 3
End Sub

'���϶���
Public Sub cmdAlignTop_click()
	ChinaExcel.VertTextAlign = 1
End Sub

'��ֱ���ж���
Public Sub cmdAlignMiddle_click()
	ChinaExcel.VertTextAlign = 2
End Sub

'���¶���
Public Sub cmdAlignBottom_click()
	ChinaExcel.VertTextAlign = 3
End Sub

'���߿���
Public Sub cmdDrawBorder_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		.DrawCellBorder  StartRow, StartCol, EndRow, EndCol, BorderTypeSelect.value, 0,0
        End With
End Sub

'Ĩ����
Public Sub cmdEraseBorder_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		.ClearCellBorder  StartRow, StartCol, EndRow, EndCol,0
        End With
End Sub

'���ҷ���
Public Sub cmdCurrency_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		.SetCellDigitShowStyle StartRow, StartCol, EndRow, EndCol,2,2
    End With
End Sub

'�ٷֺ�
Public Sub cmdPercent_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		.SetCellDigitShowStyle StartRow, StartCol, EndRow, EndCol,4,2
	End With
End Sub

'ǧ��λ
Public Sub cmdThousand_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		.SetCellDigitShowStyle StartRow, StartCol, EndRow, EndCol,5,2
	End With
End Sub

'���ڳ���������
Public Sub cmdAbout_click()
	ChinaExcel.AboutBox
End Sub

'������
Public Sub cmdInsertCol_click()
	ChinaExcel.OnInsertBeforeCol
End Sub

'������
Public Sub cmdInsertRow_click()
	ChinaExcel.OnInsertBeforeRow
End Sub

'���뵥Ԫ
Public Sub cmdInsertCell_click()
	ChinaExcel.OnInsertCell
End Sub

'ɾ����Ԫ
Public Sub cmdDeleteCell_click()
	ChinaExcel.OnDeleteCell
End Sub

'ɾ����
Public Sub cmdDeleteCol_click()
	ChinaExcel.OnDeleteCol
End Sub

'ɾ����
Public Sub cmdDeleteRow_click()
	ChinaExcel.OnDeleteRow
End Sub


'ˮƽ���
Public Sub cmdFormulaSumH_click()
	With ChinaExcel
		StartCol = 0: StartRow = 0: EndCol = 0: EndRow = 0
		.GetSelectRegionWeb StartRow,StartCol,EndRow,EndCol
		.AutoSum StartRow,StartCol,EndRow,EndCol,2
	End With
End Sub

'��ֱ���
Public Function InStrL(inString, srchString)
                                    '�˺������ڲ�ѯsrchString���ִ��ڸ��ִ�inString�е����һ��λ��
    If srchString = "" Then
        InStrL = 0
        Exit Function
    End If
    If Len(srchString) Then
        Do
            iLastPos = iCurPos
            iCurPos = InStr(iCurPos + 1, inString, srchString, vbTextCompare)
        Loop Until iCurPos = 0
    End If
    InStrL = iLastPos
End Function

Public Function StrGetSinglePara(ByVal strCellPara, ByVal strCharacter)
                    '���뵥Ԫ����strCellPara�������ִ�strCharacter,�����ɷ��������ִ��е��ַ���ֵ

    strChar1 = "<" & Trim(strCharacter) & ">"
    strChar2 = "</" & Trim(strCharacter) & ">"
    iStart = InStrL(strCellPara, strChar1)
    iEnd = InStrL(strCellPara, strChar2)
    If iStart > 0 And iEnd > iStart Then
        iCharacterLen = Len(Trim(strCharacter)) + 2
        iStart = iStart + iCharacterLen
        StrGetSinglePara = Trim(Mid(strCellPara, iStart, iEnd - iStart))
    Else
        StrGetSinglePara = ""
    End If
End Function

Public Function GetCellDefineValue(ByVal nRow,ByVal nCol)
    strCellPara = ChinaExcel.GetCellStatDefine(nRow, nCol)
    If Trim(strCellPara) <> "" Then
        strFldName = StrGetSinglePara(strCellPara, "fieldname")
    else
        strFldName=""
    end if
    GetCellDefineValue=strFldName
End Function

Public Function GetCellColName(nRow, nCol)
    strName = ChinaExcel.GetCellName(nRow,nCol)
    strNameA=""
    
    for iCount=1 to Len(strName)
        If Not IsNumeric(Mid(strName,iCount, 1)) Then
            strNameA = strNameA & Mid(strName,iCount, 1)
        else
            exit for
        End If
    next
    GetCellColName = strNameA
End Function


Public Sub cmdFormulaSumV_click()
	With ChinaExcel
'		StartCol = 0: StartRow = 0: EndCol = 0: EndRow = 0
'		.GetSelectRegionWeb StartRow,StartCol,EndRow,EndCol
'		.AutoSum StartRow,StartCol,EndRow,EndCol,1
        nRow=.Row
        nCol=.Col
'ʹ���ֶν������
'        strValue=GetCellDefineValue(nRow,nCol)
'        if strValue="" then
'            msgbox "û���ҵ�������ֶ���"
'            exit sub
'        end if
'        .SetCellShowVal nRow+1,nCol,"=sum(@"+strValue+")"
'ʹ�ù�ʽ���
        strValue=GetCellDefineValue(nRow,nCol)
        if strValue="" then
            msgbox "û���ҵ�������ֶ���"
            exit sub
        end if
        strValueA=GetCellColName(nRow,nCol)
        if strValueA="" then
            msgbox "��ȡ��������"
            exit sub
        end if
        qs=msgbox("�������ѡ[YES],�ֶ������ѡ[NO]",vbYesNo + vbQuestion,"ѯ��")
        if qs=vbyes then
            .SetCellShowVal nRow+1,nCol,"=sum(" & strValueA & nRow & ":" & strValueA & "0)"
        else
            .SetCellShowVal nRow+1,nCol,"=sum(@" & strValue & ")"
        end if
	End With
End Sub

'˫�����
Public Sub cmdFormulaSumHV_click()
	With ChinaExcel
		StartCol = 0: StartRow = 0: EndCol = 0: EndRow = 0
		.GetSelectRegionWeb StartRow,StartCol,EndRow,EndCol
		.AutoSum StartRow,StartCol,EndRow,EndCol,3
	End With
End Sub


'ͼ����
Public Sub mnuDataWzdChart_click()
	ChinaExcel.OnChartWizard
End Sub

'����ͼƬΪԭʼ��С
Public Sub mnuSetCellImageOriginalSize_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		for row = StartRow to EndRow
			for col = StartCol to EndCol
				.SetCellImageSize row,col,1		
			next
		next
		.Refresh
	End With
End Sub

'����ͼƬΪ��Ԫ��С
Public Sub mnuSetCellImageCellSize_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		for row = StartRow to EndRow
			for col = StartCol to EndCol
				.SetCellImageSize row,col,0		
			next
		next
		.Refresh
	End With
End Sub

'ɾ��ͼƬ
Public Sub mnuDeleteCellImage_click()
	ChinaExcel.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
	ChinaExcel.DeleteCellImage StartRow, StartCol, EndRow, EndCol
End Sub

'����ÿҳ��ӡ������
Public Sub mnuSetOnePrintPageDetailZoneRows_click()
	nPageRows = ChinaExcel.GetOnePrintPageDetailZoneRows()
	nRow = InputBox( "˵������ӡʱÿҳ��ʾ������,��������ͷ�ͱ�βҳ�š�ҳǰ�ŵ�����(���Ϊ0��,���ʾû������ÿҳ��ӡ������,ϵͳ��ȱʡ���з�ҳ)�� ������ÿҳ��ӡ��������", "����ÿҳ��ӡ������", nPageRows )
	If nRow <> "" Then ChinaExcel.SetOnePrintPageDetailZoneRows nRow
End Sub


'*****************************************************************
'**********      �����б���е��¼�
'*****************************************************************
'��������
Public Sub changeFontName( ByVal value )
    With ChinaExcel
        lFontName = value
        .CellFontName = lFontName
    End With
End Sub

'�����ֺ�
Public Sub changeFontSize( ByVal value )
    With ChinaExcel
        lFontSize = value
		.CellFontSize = lFontSize
    End With

End Sub
