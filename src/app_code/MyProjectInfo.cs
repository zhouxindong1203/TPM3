using System.Collections.Generic;
using Common;
using Common.Database;
using TPM3.Sys;

namespace TPM3.wx
{
    public class MyProjectInfo
    {
        public static void OnInit()
        {
            nameIndexMap["���Ի���"] = 8;
            nameIndexMap["���Խ���"] = 9;
            nameIndexMap["����������"] = 50;  // ������ʱ�Ƿ�����һ������
            nameIndexMap["�����������ⷴ"] = 51;  // �Ƿ��ÿ�������������ɱ���

            // ��Ŀ����
            ProjectInfo.root = "00";
            ProjectInfo.TableName = "SYS�ĵ����ݱ�";
            ProjectInfo.VersionColumn = "���԰汾";
            ProjectInfo.OleType = "����";
            ProjectInfo.MaxAvailableTime = 30;

            ProjectInfo.DocDefault[null, "��ʶ�汾ǰ׺"] = "R";

            ProjectInfo.DocDefault["������ʼ�½ں�"] = 5;
            ProjectInfo.DocDefault["�ƻ���ʼ�½ں�"] = 8;
            ProjectInfo.DocDefault["�����ʼ�½ں�"] = 5;
            ProjectInfo.DocDefault["��¼��ʼ�½ں�"] = 2;
            ProjectInfo.DocDefault["�������ݷ���"] = true;

            string s = "������������ȫ�����Բ��豻ִ�л���ĳ��ԭ���²��Բ����޷�ִ��(�쳣��ֹ)��";
            ProjectInfo.DocDefault["ȱʡֵ_������ֹ����"] = s;
            s = "������������ȫ�����Բ��趼ͨ������־������Ϊ\"ͨ��\"��";
            ProjectInfo.DocDefault["ȱʡֵ_����ͨ��׼��"] = s;
        }

        /// <summary>
        /// ��־���Ƶ���־����λ�õ�ӳ���
        /// </summary>
        static Dictionary<string, int> nameIndexMap = new Dictionary<string, int>();
        public const string BitMapName = "λ�ر����б�";

        public static bool GetBoolValue(DBAccess dba, object pid, string name)
        {
            return GetBoolValue(dba, pid, null, name);
        }

        public static void SetBoolValue(DBAccess dba, object pid, string name, bool value)
        {
            SetBoolValue(dba, pid, null, name, value);
        }

        public static bool GetBoolValue(DBAccess dba, object pid, object vid, string name)
        {
            string s = ProjectInfo.GetDocString(dba, pid, vid, null, BitMapName);
            long i;
            if(!long.TryParse(s, out i)) i = 0;
            i = (i >> nameIndexMap[name]) & 1;
            return i == 1;
        }

        public static void SetBoolValue(DBAccess dba, object pid, object vid, string name, bool value)
        {
            string s = ProjectInfo.GetDocString(dba, pid, vid, null, BitMapName);
            long i;
            if(!long.TryParse(s, out i)) i = 0;

            long oldi = i;
            long mask = 1L << nameIndexMap[name];
            if(value) // ��1
                i = i | mask;
            else // ��0
                i = i & (~mask);
            if(i == oldi) return; // ����Ҫ����
            ProjectInfo.SetDocString(dba, pid, vid, null, BitMapName, i.ToString());
        }

        public static T GetProjectContent<T>(string title)
        {
            return ProjectInfo.GetProjectContent<T>(GlobalData.globalData.dbProject, GlobalData.globalData.projectID, title);
        }

        public static string GetProjectContent(string title)
        {
            return GetProjectContent<string>(title);
        }

        /// <summary>
        /// ��Ŀ��ʶ��
        /// </summary>
        public static string ProjectCode(DBAccess dba, object pid)
        {
            return ProjectInfo.GetProjectString(dba, pid, "��Ŀ��ʶ��");
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public static string ProjectName(DBAccess dba, object pid)
        {
            return ProjectInfo.GetProjectString(dba, pid, "��Ŀ����");
        }

        /// <summary>
        /// ��ȡƴװ�汾��ź�ı�ʶ�ţ����� TPM(R1)
        /// </summary>
        public static string VersionSign(DBAccess dba, object pid, object vid)
        {
            string s1 = ProjectCode(dba, pid);
            string pre = ProjectInfo.GetProjectString(dba, pid, "��ʶ�汾ǰ׺");
            int currentVerIndex = DBLayer1.GetVersionIndex(dba, vid);
            if(currentVerIndex > 0)
                s1 += "(" + pre + currentVerIndex + ")";
            return s1;
        }
    }
}
