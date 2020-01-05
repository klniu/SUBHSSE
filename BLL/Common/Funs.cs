namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Data.Linq;
    using System.Web;
    using System.Text;

    /// <summary>
    /// ͨ�÷����ࡣ
    /// </summary>
    public static class Funs
    {
        /// <summary>
        /// ά��һ��DB����
        /// </summary>
        private static Dictionary<int, Model.SUBHSSEDB> dataBaseLinkList = new System.Collections.Generic.Dictionary<int, Model.SUBHSSEDB>();


        /// <summary>
        /// ά��һ��DB����
        /// </summary>
        public static System.Collections.Generic.Dictionary<int, Model.SUBHSSEDB> DBList
        {
            get
            {
                return dataBaseLinkList;
            }
        }

        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        private static string connString;

        /// <summary>
        /// ���ݿ������ַ�����
        /// </summary>
        public static string ConnString
        {
            get
            {
                if (connString == null)
                {
                    throw new NotSupportedException("�����������ַ�����");
                }

                return connString;
            }

            set
            {
                if (connString != null)
                {
                    throw new NotSupportedException("���������ã�");
                }

                connString = value;
            }
        }

        /// <summary>
        /// ϵͳ����
        /// </summary>
        public static string SystemName
        {
            get;
            set;
        }

        /// <summary>
        /// ������·��
        /// </summary>
        public static string RootPath
        {
            get;
            set;
        }

        /// <summary>
        /// ���ŷ�����·��
        /// </summary>
        public static string CNCECPath
        {
            get;
            set;
        }

        /// <summary>
        /// ����汾
        /// </summary>
        public static string SystemVersion
        {
            get;
            set;
        }

        /// <summary>
        /// ÿҳ����
        /// </summary>
        public static int PageSize
        {
            get;
            set;
        } = 15;

        /// <summary>
        /// ���ݿ������ġ�
        /// </summary>
        public static Model.SUBHSSEDB DB
        {
            get
            {
                if (!DBList.ContainsKey(System.Threading.Thread.CurrentThread.ManagedThreadId))
                {
                    DBList.Add(System.Threading.Thread.CurrentThread.ManagedThreadId, new Model.SUBHSSEDB(connString));
                }

                // DBList[System.Threading.Thread.CurrentThread.ManagedThreadId].CommandTimeout = 1200;
                return DBList[System.Threading.Thread.CurrentThread.ManagedThreadId];
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="password">����ǰ������</param>
        /// <returns>���ܺ������</returns>
        public static string EncryptionPassword(string password)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", null);

            //return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        }

        ///// <summary>
        ///// ΪĿ����������� "��ѡ��" ��
        ///// </summary>
        ///// <param name="DLL">Ŀ��������</param>
        //public static void PleaseSelect(System.Web.UI.WebControls.DropDownList DDL)
        //{
        //    DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- ��ѡ�� -", "0"));
        //    return;
        //}

        /// <summary>
        /// ΪĿ����������� "��ѡ��" ��
        /// </summary>
        /// <param name="DLL">Ŀ��������</param>
        public static void FineUIPleaseSelect(FineUIPro.DropDownList DDL)
        {
            DDL.Items.Insert(0, new FineUIPro.ListItem("- ��ѡ�� -", BLL.Const._Null));
            return;
        }
              
        /// <summary>
        /// ΪĿ�����������ѡ������
        /// </summary>
        /// <param name="DLL">Ŀ��������</param>
        public static void FineUIPleaseSelect(FineUIPro.DropDownList DDL, string text)
        {
            DDL.Items.Insert(0, new FineUIPro.ListItem(text, BLL.Const._Null));
            return;
        }

        /// <summary>
        /// ΪĿ����������� "���±���" ��
        /// </summary>
        /// <param name="DLL">Ŀ��������</param>
        public static void ReCompileSelect(System.Web.UI.WebControls.DropDownList DDL)
        {
            DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("���±���", "0"));
            return;
        }

        /// <summary>
        /// ҳ��������
        /// </summary>
        /// <param name="DLL">Ŀ��������</param>
        public static void DropDownPageSize(FineUIPro.DropDownList DDL)
        {
            DDL.Items.Insert(0, new FineUIPro.ListItem("10", "10"));
            DDL.Items.Insert(1, new FineUIPro.ListItem("20", "20", true));
            DDL.Items.Insert(2, new FineUIPro.ListItem("30", "30"));
            DDL.Items.Insert(3, new FineUIPro.ListItem("50", "50"));
            DDL.Items.Insert(4, new FineUIPro.ListItem("������", "1000000"));
            return;
        }

        /// <summary>
        /// �ַ����Ƿ�Ϊ������
        /// </summary>
        /// <param name="decimalStr">Ҫ�����ַ���</param>
        /// <returns>�����ǻ��</returns>
        public static bool IsDecimal(string decimalStr)
        {
            if (String.IsNullOrEmpty(decimalStr))
            {
                return false;
            }

            try
            {
                Convert.ToDecimal(decimalStr, NumberFormatInfo.InvariantInfo);
                return true;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return false;
            }
        }

        /// <summary>
        /// �ж�һ���ַ����Ƿ�������
        /// </summary>
        /// <param name="integerStr">Ҫ�����ַ���</param>
        /// <returns>�����ǻ��</returns>
        public static bool IsInteger(string integerStr)
        {
            if (String.IsNullOrEmpty(integerStr))
            {
                return false;
            }

            try
            {
                Convert.ToInt32(integerStr, NumberFormatInfo.InvariantInfo);
                return true;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return false;
            }
        }

        /// <summary>
        /// ��ȡ�µ�����
        /// </summary>
        /// <param name="number">Ҫת��������</param>
        /// <returns>�µ�����</returns>
        public static string InterceptDecimal(object number)
        {
            if (number == null)
            {
                return null;
            }
            decimal newNumber = 0;
            string newNumberStr = "";
            int an = -1;
            string numberStr = number.ToString();
            int n = numberStr.IndexOf(".");
            if (n == -1)
            {
                return numberStr;
            }
            for (int i = n + 1; i < numberStr.Length; i++)
            {
                string str = numberStr.Substring(i, 1);
                if (str == "0")
                {
                    if (GetStr(numberStr, i))
                    {
                        an = i;
                        break;
                    }
                }
            }
            if (an == -1)
            {
                newNumber = Convert.ToDecimal(numberStr);
            }
            else if (an == n + 1)
            {

                newNumberStr = numberStr.Substring(0, an - 1);
                newNumber = Convert.ToDecimal(newNumberStr);
            }
            else
            {
                newNumberStr = numberStr.Substring(0, an);
                newNumber = Convert.ToDecimal(newNumberStr);
            }
            return newNumber.ToString();
        }

        /// <summary>
        /// �ж��ַ����ӵ�nλ��ʼ�Ժ��Ƿ�Ϊ0
        /// </summary>
        /// <param name="number">Ҫ�жϵ��ַ���</param>
        /// <param name="n">��ʼ��λ��</param>
        /// <returns>false����Ϊ0��true��Ϊ0</returns>
        public static bool GetStr(string number, int n)
        {
            for (int i = n; i < number.Length; i++)
            {
                if (number.Substring(i, 1) != "0")
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ��ȡ�ַ�������
        /// </summary>
        /// <param name="str">Ҫ��ȡ���ַ���</param>
        /// <param name="n">����</param>
        /// <returns>��ȡ���ַ���</returns>
        public static string GetSubStr(object str, object n)
        {
            if (str != null)
            {
                if (str.ToString().Length > Convert.ToInt32(n))
                {
                    return str.ToString().Substring(0, Convert.ToInt32(n)) + "....";
                }
                else
                {
                    return str.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// ���ݱ�ʶ�����ַ���list
        /// </summary>
        /// <param name="str"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<string> GetStrListByStr(string str, char n)
        {
            List<string> strList = new List<string>();
            if (!string.IsNullOrEmpty(str))
            {
                strList.AddRange(str.Split(n));
            }

            return strList;
        }

        #region ����ת��
        /// <summary>
        /// �����ı�ת����������
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewDecimalOrZero(string value)
        {
            decimal revalue = 0;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = decimal.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }

        /// <summary>
        /// �����ı�ת����������
        /// </summary>
        /// <returns></returns>
        public static decimal? GetNewDecimal(string value)
        {
            decimal? revalue = null;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = decimal.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }

        /// <summary>
        /// �����ı�ת����������
        /// </summary>
        /// <returns></returns>
        public static int? GetNewInt(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    return Int32.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// �����ı�ת����������
        /// </summary>
        /// <returns></returns>
        public static int GetNewIntOrZero(string value)
        {
            int revalue = 0;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = Int32.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }
        #endregion

        /// <summary>
        /// ָ���ϴ��ļ�������
        /// </summary>
        /// <returns></returns>
        public static string GetNewFileName()
        {
            Random rm = new Random(Environment.TickCount);
            return DateTime.Now.ToString("yyyyMMddhhmmss") + rm.Next(1000, 9999).ToString();
        }

        /// <summary>
        /// ָ���ϴ��ļ�������
        /// </summary>
        /// <returns></returns>
        public static string GetNewFileName(DateTime? dateTime)
        {
            string str = string.Empty;
            Random rm = new Random(System.Environment.TickCount);
            if (dateTime.HasValue)
            {
                str= dateTime.Value.ToString("yyyyMMddhhmmss") + rm.Next(1000, 9999).ToString();
            }
            return str;
        }

        #region ʱ��ת��
        /// <summary>
        /// �����ı�ת��ʱ������
        /// </summary>
        /// <returns></returns>
        public static DateTime? GetNewDateTime(string time)
        {
            try
            {
                if (!String.IsNullOrEmpty(time))
                {
                    return DateTime.Parse(time);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return null;
            }
        }

        /// <summary>
        /// �����ı�ת��ʱ�����ͣ���ʱ��Ĭ�ϵ�ǰʱ�䣩
        /// </summary>
        /// <returns></returns>
        public static DateTime GetNewDateTimeOrNow(string time)
        {
            try
            {
                if (!String.IsNullOrEmpty(time))
                {
                    return DateTime.Parse(time);
                }
                else
                {
                    return System.DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return System.DateTime.Now;
            }
        }

        /// <summary>
        /// ����ʱ���ȡ���ĸ�����
        /// </summary>
        /// <returns></returns>
        public static string GetQuarterlyByTime(DateTime time)
        {
            string quarterly = string.Empty;
            string yearName = time.Year.ToString();
            int month = time.Month;
            string name = string.Empty;
            if (month >= 1 && month <= 3)
            {
                name = "��һ����";
            }
            else if (month >= 4 && month <= 6)
            {
                name = "�ڶ�����";
            }
            else if (month >= 7 && month <= 9)
            {
                name = "��������";
            }
            else if (month >= 10 && month <= 12)
            {
                name = "���ļ���";
            }

            quarterly = yearName + "��" + name;
            return quarterly;
        }

        /// <summary>
        /// ����ʱ���ȡ���ĸ�����
        /// </summary>
        /// <returns></returns>
        public static int GetNowQuarterlyByTime(DateTime time)
        {
            int quarterly = 0;
            int month = time.Month;
            if (month >= 1 && month <= 3)
            {
                quarterly = 1;
            }
            else if (month >= 4 && month <= 6)
            {
                quarterly = 2;
            }
            else if (month >= 7 && month <= 9)
            {
                quarterly = 3;
            }
            else if (month >= 10 && month <= 12)
            {
                quarterly = 4;
            }

            return quarterly;
        }

        /// <summary>
        /// �����»�ȡ���ĸ�����
        /// </summary>
        /// <returns></returns>
        public static int GetNowQuarterlyByMonth(int month)
        {
            int quarterly = 0;
            if (month >= 1 && month <= 3)
            {
                quarterly = 1;
            }
            else if (month >= 4 && month <= 6)
            {
                quarterly = 2;
            }
            else if (month >= 7 && month <= 9)
            {
                quarterly = 3;
            }
            else if (month >= 10 && month <= 12)
            {
                quarterly = 4;
            }

            return quarterly;
        }
        /// <summary>
        /// ����ʱ���ȡ���ĸ�����
        /// </summary>
        /// <returns></returns>
        public static string GetQuarterlyNameByMonth(int month)
        {
            string name = string.Empty;
            if (month >= 1 && month <= 3)
            {
                name = "��һ����";
            }
            else if (month >= 4 && month <= 6)
            {
                name = "�ڶ�����";
            }
            else if (month >= 7 && month <= 9)
            {
                name = "��������";
            }
            else if (month >= 10 && month <= 12)
            {
                name = "���ļ���";
            }

            return name;
        }
        /// <summary>
        /// ����ʱ���ȡ���ϡ��°���
        /// </summary>
        /// <returns></returns>
        public static int GetNowHalfYearByTime(DateTime time)
        {
            int quarterly = 1;
            int month = time.Month;
            if (month >= 1 && month <= 6)
            {
                quarterly = 1;
            }
            else
            {
                quarterly = 2;
            }

            return quarterly;
        }

        /// <summary>
        /// ����ʱ���ȡ���ϡ��°���
        /// </summary>
        /// <returns></returns>
        public static int GetNowHalfYearByMonth(int month)
        {
            int halfYear = 1;
            if (month >= 1 && month <= 6)
            {
                halfYear = 1;
            }
            else
            {
                halfYear = 2;
            }

            return halfYear;
        }

        /// <summary>
        /// ����ʱ���ȡ���ϡ��°���
        /// </summary>
        /// <returns></returns>
        public static string GetNowHalfYearNameByTime(DateTime time)
        {
            string quarterly = "�ϰ���";
            int month = time.Month;
            if (month >= 1 && month <= 6)
            {
                quarterly = "�ϰ���";
            }
            else
            {
                quarterly = "�°���";
            }

            return quarterly;
        }
        #endregion

        /// <summary>
        /// ����·�
        /// </summary>
        /// <param name="datetime2"></param>
        /// <param name="datetime2"></param>
        /// <returns></returns>
        public static int CompareMonths(DateTime datetime1, DateTime datetime2)
        {
            DateTime dt = datetime1;
            DateTime dt2 = datetime2;
            if (DateTime.Compare(dt, dt2) < 0)
            {
                dt2 = dt;
                dt = datetime2;
            }
            int year = dt.Year - dt2.Year;
            int month = dt.Month - dt2.Month;
            month = year * 12 + month;
            if (dt.Day - dt2.Day < -15)
            {
                month--;
            }
            else if (dt.Day - dt2.Day > 14)
            {
                month++;
            }
            return month;
        }


        public static DateTime GetQuarterlyMonths(string year, string quarterly)
        {
            string startMonth = string.Empty;
            if (quarterly == "1")
            {
                startMonth = "1";
            }
            else if (quarterly == "2")
            {
                startMonth = "4";
            }
            else if (quarterly == "3")
            {
                startMonth = "7";
            }
            else if (quarterly == "4")
            {
                startMonth = "10";
            }
            return Funs.GetNewDateTimeOrNow(year + "-" + startMonth + "-01");
        }

        #region  ��ȡ��д����¼�
        public static string NumericCapitalization(decimal num)
        {
            string str1 = "��Ҽ��������½��ƾ�";            //0-9����Ӧ�ĺ��� 
            string str2 = "��Ǫ��ʰ��Ǫ��ʰ��Ǫ��ʰԪ�Ƿ�"; //����λ����Ӧ�ĺ��� 
            string str3 = "";    //��ԭnumֵ��ȡ����ֵ 
            string str4 = "";    //���ֵ��ַ�����ʽ 
            string str5 = "";  //����Ҵ�д�����ʽ 
            int i;    //ѭ������ 
            int j;    //num��ֵ����100���ַ������� 
            string ch1 = "";    //���ֵĺ������ 
            string ch2 = "";    //����λ�ĺ��ֶ��� 
            int nzero = 0;  //����������������ֵ�Ǽ��� 
            int temp;            //��ԭnumֵ��ȡ����ֵ 

            num = Math.Round(Math.Abs(num), 2);    //��numȡ����ֵ����������ȡ2λС�� 
            str4 = ((long)(num * 100)).ToString();        //��num��100��ת�����ַ�����ʽ 
            j = str4.Length;      //�ҳ����λ 
            if (j > 15) { return "���"; }
            str2 = str2.Substring(15 - j);   //ȡ����Ӧλ����str2��ֵ���磺200.55,jΪ5����tr2=��ʰԪ�Ƿ� 

            //ѭ��ȡ��ÿһλ��Ҫת����ֵ 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //ȡ����ת����ĳһλ��ֵ 
                temp = Convert.ToInt32(str3);      //ת��Ϊ���� 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //����ȡλ����ΪԪ�����ڡ������ϵ�����ʱ 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "��" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //��λ�����ڣ��ڣ���Ԫλ�ȹؼ�λ 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "��" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //�����λ����λ��Ԫλ�������д�� 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //���һλ���֣�Ϊ0ʱ�����ϡ����� 
                    str5 = str5 + '��';
                }
            }
            if (num == 0)
            {
                str5 = "��Ԫ��";
            }
            return str5;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public static void SubmitChanges()
        {
            try
            {
                DB.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch (ChangeConflictException ex)
            {
                foreach (ObjectChangeConflict occ in DB.ChangeConflicts)
                {
                    //�����ǽ����ͻ�����ַ�����ѡһ�ּ���
                    //// ʹ�õ�ǰ���ݿ��е�ֵ������Linq������ʵ������ֵ
                    //occ.Resolve(RefreshMode.OverwriteCurrentValues);
                    //// ʹ��Linq������ʵ������ֵ�����ǵ�ǰ���ݿ��е�ֵ
                    //occ.Resolve(RefreshMode.KeepCurrentValues);
                    // ֻ����ʵ������иı���ֶε�ֵ�������ı�������
                    occ.Resolve(RefreshMode.KeepChanges);
                }
                // ����ط�Ҫע�⣬Catch�����У�����ǰ��ֻ��ָ���������������ͻ������ط�����Ҫ�ٴ��ύ���£������Ļ���ֵ    //�Ż��ύ�����ݿ⡣
                DB.SubmitChanges();                
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
        }
    }
}

