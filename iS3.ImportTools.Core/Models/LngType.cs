using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.Core.Models
{
    public enum LngType
    {
        [Description("阿拉伯文")]
        ar = 0,
        [Description("阿拉伯文(阿拉伯联合酋长国)")]
        ar_AE = 1,
        [Description("阿拉伯文(巴林)")]
        ar_BH = 2,
        [Description("阿拉伯文(阿尔及利亚)")]
        ar_DZ = 3,
        [Description("阿拉伯文(埃及)")]
        ar_EG = 4,
        [Description("阿拉伯文(伊拉克)")]
        ar_IQ = 5,
        [Description("阿拉伯文(约旦)")]
        ar_JO = 6,
        [Description("阿拉伯文(科威特)")]
        ar_KW = 7,
        [Description("阿拉伯文(黎巴嫩)")]
        ar_LB = 8,
        [Description("阿拉伯文(利比亚)")]
        ar_LY = 9,
        [Description("阿拉伯文(摩洛哥)")]
        ar_MA = 10,
        [Description("阿拉伯文(阿曼)")]
        ar_OM = 11,
        [Description("阿拉伯文(卡塔尔)")]
        ar_QA = 12,
        [Description("阿拉伯文(沙特阿拉伯)")]
        ar_SA = 13,
        [Description("阿拉伯文(苏丹)")]
        ar_SD = 14,
        [Description("阿拉伯文(叙利亚)")]
        ar_SY = 15,
        [Description("阿拉伯文(突尼斯)")]
        ar_TN = 16,
        [Description("阿拉伯文(也门)")]
        ar_YE = 17,
        [Description("白俄罗斯文")]
        be = 18,
        [Description("白俄罗斯文(白俄罗斯)")]
        be_BY = 19,
        [Description("保加利亚文")]
        bg = 20,
        [Description("保加利亚文(保加利亚)")]
        bg_BG = 21,
        [Description("加泰罗尼亚文")]
        ca = 22,
        [Description("加泰罗尼亚文(西班牙)")]
        ca_ES = 23,
        [Description("加泰罗尼亚文(西班牙,Euro)")]
        ca_ES_EURO = 24,
        [Description("捷克文")]
        cs = 25,
        [Description("捷克文(捷克共和国)")]
        cs_CZ = 26,
        [Description("丹麦文")]
        da = 27,
        [Description("丹麦文(丹麦)")]
        da_DK = 28,
        [Description("德文")]
        de = 29,
        [Description("德文(奥地利)")]
        de_AT = 30,
        [Description("德文(奥地利,Euro)")]
        de_AT_EURO = 31,
        [Description("德文(瑞士)")]
        de_CH = 32,
        [Description("德文(德国)")]
        de_DE = 33,
        [Description("德文(德国,Euro)")]
        de_DE_EURO = 34,
        [Description("德文(卢森堡)")]
        de_LU = 35,
        [Description("德文(卢森堡,Euro)")]
        de_LU_EURO = 36,
        [Description("希腊文")]
        el = 37,
        [Description("希腊文(希腊)")]
        el_GR = 38,
        [Description("英文")]
        en = 39,
        [Description("英文(澳大利亚)")]
        en_AU = 40,
        [Description("英文(加拿大)")]
        en_CA = 41,
        [Description("英文(英国)")]
        en_GB = 42,
        [Description("英文(爱尔兰)")]
        en_IE = 43,
        [Description("英文(爱尔兰,Euro)")]
        en_IE_EURO = 44,
        [Description("英文(新西兰)")]
        en_NZ = 45,
        [Description("英文(美国)")]
        en_US = 46,
        [Description("英文(南非)")]
        en_ZA = 47,
        [Description("西班牙文")]
        es = 48,
        [Description("西班牙文(玻利维亚)")]
        es_BO = 49,
        [Description("西班牙文(阿根廷)")]
        es_AR = 50,
        [Description("西班牙文(智利)")]
        es_CL = 51,
        [Description("西班牙文(哥伦比亚)")]
        es_CO = 52,
        [Description("西班牙文(哥斯达黎加)")]
        es_CR = 53,
        [Description("西班牙文(多米尼加共和国)")]
        es_DO = 54,
        [Description("西班牙文(厄瓜多尔)")]
        es_EC = 55,
        [Description("西班牙文(西班牙)")]
        es_ES = 56,
        [Description("西班牙文(西班牙,Euro)")]
        es_ES_EURO = 57,
        [Description("西班牙文(危地马拉)")]
        es_GT = 58,
        [Description("西班牙文(洪都拉斯)")]
        es_HN = 59,
        [Description("西班牙文(墨西哥)")]
        es_MX = 60,
        [Description("西班牙文(尼加拉瓜)")]
        es_NI = 61,
        [Description("爱沙尼亚文")]
        et = 62,
        [Description("西班牙文(巴拿马)")]
        es_PA = 63,
        [Description("西班牙文(秘鲁)")]
        es_PE = 64,
        [Description("西班牙文(波多黎哥)")]
        es_PR = 65,
        [Description("西班牙文(巴拉圭)")]
        es_PY = 66,
        [Description("西班牙文(萨尔瓦多)")]
        es_SV = 67,
        [Description("西班牙文(乌拉圭)")]
        es_UY = 68,
        [Description("西班牙文(委内瑞拉)")]
        es_VE = 69,
        [Description("爱沙尼亚文(爱沙尼亚)")]
        et_EE = 70,
        [Description("芬兰文")]
        fi = 71,
        [Description("芬兰文(芬兰)")]
        fi_FI = 72,
        [Description("芬兰文(芬兰,Euro)")]
        fi_FI_EURO = 73,
        [Description("法文")]
        fr = 74,
        [Description("法文(比利时)")]
        fr_BE = 75,
        [Description("法文(比利时,Euro)")]
        fr_BE_EURO = 76,
        [Description("法文(加拿大)")]
        fr_CA = 77,
        [Description("法文(瑞士)")]
        fr_CH = 78,
        [Description("法文(法国)")]
        fr_FR = 79,
        [Description("法文(法国,Euro)")]
        fr_FR_EURO = 80,
        [Description("法文(卢森堡)")]
        fr_LU = 81,
        [Description("法文(卢森堡,Euro)")]
        fr_LU_EURO = 82,
        [Description("克罗地亚文")]
        hr = 83,
        [Description("克罗地亚文(克罗地亚)")]
        hr_HR = 84,
        [Description("匈牙利文")]
        hu = 85,
        [Description("匈牙利文(匈牙利)")]
        hu_HU = 86,
        [Description("冰岛文")]
        is_ = 87,                        //is 与保留字相当
        [Description("冰岛文(冰岛)")]
        is_IS = 88,
        [Description("意大利文")]
        it = 89,
        [Description("意大利文(瑞士)")]
        it_CH = 90,
        [Description("意大利文(意大利)")]
        it_IT = 91,
        [Description("意大利文(意大利,Euro)")]
        it_IT_EURO = 92,
        [Description("希伯来文")]
        iw = 93,
        [Description("希伯来文(以色列)")]
        iw_IL = 94,
        [Description("日文")]
        ja = 95,
        [Description("日文(日本)")]
        ja_JP = 96,
        [Description("朝鲜文")]
        ko = 97,
        [Description("朝鲜文(南朝鲜)")]
        ko_KR = 98,
        [Description("立陶宛文")]
        lt = 99,
        [Description("立陶宛文(立陶宛)")]
        lt_LT = 100,
        [Description("拉托维亚文(列托)")]
        lv = 101,
        [Description("拉托维亚文(列托)(拉脱维亚)")]
        lv_LV = 102,
        [Description("马其顿文")]
        mk = 103,
        [Description("马其顿文(马其顿王国)")]
        mk_MK = 104,
        [Description("荷兰文")]
        nl = 105,
        [Description("荷兰文(比利时)")]
        nl_BE = 106,
        [Description("荷兰文(比利时,Euro)")]
        nl_BE_EURO = 107,
        [Description("荷兰文(荷兰)")]
        nl_NL = 108,
        [Description("荷兰文(荷兰,Euro)")]
        nl_NL_EURO = 109,
        [Description("挪威文")]
        no = 110,
        [Description("挪威文(挪威)")]
        no_NO = 111,
        [Description("挪威文(挪威,Nynorsk)")]
        no_NO_NY = 112,
        [Description("波兰文")]
        pl = 113,
        [Description("波兰文(波兰)")]
        pl_PL = 114,
        [Description("葡萄牙文")]
        pt = 115,
        [Description("葡萄牙文(巴西)")]
        pt_BR = 116,
        [Description("葡萄牙文(葡萄牙)")]
        pt_PT = 117,
        [Description("葡萄牙文(葡萄牙,Euro)")]
        pt_PT_EURO = 118,
        [Description("罗马尼亚文")]
        ro = 119,
        [Description("罗马尼亚文(罗马尼亚)")]
        ro_RO = 120,
        [Description("俄文")]
        ru = 121,
        [Description("俄文(俄罗斯)")]
        ru_RU = 122,
        [Description("塞波尼斯-克罗地亚文")]
        sh = 123,
        [Description("塞波尼斯-克罗地亚文(南斯拉夫)")]
        sh_YU = 124,
        [Description("斯洛伐克文")]
        sk = 125,
        [Description("斯洛伐克文(斯洛伐克)")]
        sk_SK = 126,
        [Description("斯洛文尼亚文")]
        sl = 127,
        [Description("斯洛文尼亚文(斯洛文尼亚)")]
        sl_SI = 128,
        [Description("阿尔巴尼亚文")]
        sq = 129,
        [Description("阿尔巴尼亚文(阿尔巴尼亚)")]
        sq_AL = 130,
        [Description("塞尔维亚文")]
        sr = 131,
        [Description("塞尔维亚文(南斯拉夫)")]
        sr_YU = 132,
        [Description("瑞典文")]
        sv = 133,
        [Description("瑞典文(瑞典)")]
        sv_SE = 134,
        [Description("泰文")]
        th = 135,
        [Description("泰文(泰国)")]
        th_TH = 136,
        [Description("土耳其文")]
        tr = 137,
        [Description("土耳其文(土耳其)")]
        tr_TR = 138,
        [Description("乌克兰文")]
        uk = 139,
        [Description("乌克兰文(乌克兰)")]
        uk_UA = 140,
        [Description("中文")]
        zh = 141,
        [Description("中文(中国)")]
        zh_CN = 142,
        [Description("中文(香港)")]
        zh_HK = 143,
        [Description("中文(台湾)")]
        zh_TW = 144,
    }
}
