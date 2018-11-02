using System.Collections.Generic;
using System.Xml.Serialization;
using C1.Win.C1TrueDBGrid;
using Common;

namespace TPM3.Sys
{
    /// <summary>
    /// �û�������Ϣ
    /// </summary>
    [XmlType(TypeName = "us")]
    public class UserSetting
    {
        private UserSetting()
        {
            rowSizeingList = new int[10];
            for(int i = 0; i < rowSizeingList.Length; i++)
                rowSizeingList[i] = (int)RowSizingEnum.IndividualRows;
        }

        private static UserSetting _default = new UserSetting();

        public static UserSetting Default
        {
            get { return _default; }
            set
            {
                if(value != null && value.Version == CurrentVersion)
                    _default = value;
            }
        }

        public const int CurrentVersion = 8;

        /// <summary>
        /// �����ļ��汾��
        /// </summary>
        [XmlElement(ElementName = "ver")]
        public int Version = CurrentVersion;

        /// <summary>
        /// ���һ�ε�¼���û���ID
        /// </summary>
        [XmlElement(ElementName = "lu")]
        public object lastUserId = null;

        /// <summary>
        /// ���һ��ѡȡ����Ŀ
        /// </summary>
        [XmlElement(ElementName = "lp")]
        public object lastProjectID = null;


        /// <summary>
        /// ���һ������Ŀ�д򿪵İ汾ID
        /// ���û��ƥ��İ汾�����Զ�������һ���汾
        /// </summary>
        [XmlElement(ElementName = "lv")]
        public object lastVersionID = null;


        /// <summary>
        /// �Ƿ��Զ����������ݿ�
        /// </summary>
        [XmlElement(ElementName = "ao")]
        public bool AutoOpenLastDatabase = true;

        /// <summary>
        /// ���򿪵����ݿ���
        /// </summary>
        [XmlElement(ElementName = "ld")]
        public string LastDatabaseName = null;

        public const int MaxDatabaseHistoryCount = 10;

        /// <summary>
        /// ��ǰ�򿪵����ݿ��б����µ������
        /// </summary>
        [XmlElement(ElementName = "dl")]
        public List<string> lastDatabaseList = new List<string>();

        /// <summary>
        /// �Ƿ�ʹ���ⲿ��XML�ļ�
        /// </summary>
        [XmlElement(ElementName = "uox")]
        public bool UseOuterXmlfile = false;

        /// <summary>
        /// �ⲿXML�ļ���
        /// </summary>
        [XmlElement(ElementName = "oxf")]
        public string OuterXmlfileName = null;

        /// <summary>
        /// �û�������
        /// </summary>
        [XmlElement(ElementName = "fs")]
        public string FlexStyle = "Ĭ��";

        /// <summary>
        /// flex������
        /// </summary>
        [XmlElement(ElementName = "fg")]
        public List<ColumnPropList> FlexGridColumnProps;

        /// <summary>
        ///  �������Բ����ܹ���ӵ���󸽼���
        /// </summary>
        [XmlElement(ElementName = "mafs")]
        public int MaxAccsForStep = 10;

        /// <summary>
        /// ������ƴ����Զ����õ����߶�
        /// </summary>
        [XmlElement(ElementName = "AutoCaseRowSize")]
        public int AutoCaseRowSize = 5;

        /// <summary>
        /// ������������������Ϣ�������ʾ�߶�
        /// ֻ���Ӳ������ʱ�ĸ߶�
        /// </summary>
        [XmlElement(ElementName = "ItemInfoHeight")]
        public int ItemInfoHeight = -1;

        /// <summary>
        /// �Ӳ�����������������ʾʱ�ĸ߶�
        /// </summary>
        [XmlElement(ElementName = "ItemUCSplit")]
        public float ItemUCSplit = float.NaN;

        /// <summary>
        /// ������Ϣ���ֵĸ߶�
        /// </summary>
        [XmlElement(ElementName = "UsecaseInfoHeight")]
        public int UsecaseInfoHeight = -1;

        [XmlElement(ElementName = "sifh")]
        public float SubItemsFrmHeight = float.NaN;

        /// <summary>
        /// �ڹ��캯���г�ʼ��
        /// ���� RowSizingEnum
        /// </summary>
        [XmlElement(ElementName = "rsl")]
        public int[] rowSizeingList;

        [XmlElement(ElementName = "sit")]
        public int summaryItemType = 1;
    }

    public enum SummaryItemType
    {
        /// <summary>
        /// ͳ�����в�����
        /// </summary>
        [EnumDescription("ͳ�����в�����")]
        All = 1,

        /// <summary>
        /// ��ͳ��Ҷ�ڵ�Ĳ����������Ӳ�����򲻲μ�ͳ��
        /// </summary>
        [EnumDescription("����׼��Ĳ�����")]
        NoLeaf = 2,
    }
}
