﻿
public class NameMapFactory : INameMapFactory
{
    public IDictionary<int, IEnumerable<char>> MakeKakuMap()
    {

        return
            new Dictionary<int, IEnumerable<char>>
        {
//            { 1, new[]{'一', '乙'}},
            { 2, new[]{'九', '七', '十', '人', '二', '入', '八', '力', '刀', '丁', '又', '了', '乃', '卜'}},
            { 3, new[]{'夕', '千', '川', '大', '工', '士', '久', '之', '也', }},
            { 4, new[]{'月', '午', '公', '心', '太', '仁', '互', '勿', }},
            { 5, new[]{'田', '正', '石', '冬', '北', '央', '平', '由', '功', '司', '末', '未', '巧', '弘' }},
            { 6, new[]{'早', '百', '名', '考', '色', '有', '羊', '衣', '成', '臼', '芝', '朱', '旬', '巡', '壮', '帆', '伊', '伍', '凪', '汐', }},
            { 7, new[]{'里', '希', '利', '冷', '志', '芯', '伽', '吾', '芥', }},
            { 8, new[]{'雨', '学', '空', '青', '林', '国', '知', '直', '東', '歩', '明', '門', '夜', '始', '波', '和', '英', '芽', '季', '典', '承', '武', '宗', '佳', '刹', '昇', '拓', '弥', '侑', '忽', '怜', '茄', '茅', '茉', '祁', }},
            { 9, new[]{'音', '草', '科', '海', '秋', '春', '俊', '俐', '珀', '玲', '祐', '耶', '茜', '郁', }},
            { 10, new[]{'桜', '悟', '恥', '栞', '桐', '紘', '莞', '莉', }},
            { 11, new[]{'雪', '宿', '進', '都', '健', '康', '菜', '啓', '渉', '崇', '唯', '悠', '涼', '庵', '惚', '脩', }},
            { 12, new[]{'森', '雲', '絵', '晴', '葉', '陽', '結', '最', '達', '博', '統', '詞', '創', '滋', '愉', '惣', '絢', '翔', }},
            { 13, new[]{'園', '遠', '詩', '想', '鉄', '路', '誠', '嗣', '愁', '禅', '鈴', '零', '楼', '楓', '蒔', }},
            { 14, new[]{'聡'}},
            { 15, new[]{'横', '輪', '蔵', '潮', '論', '縁', '輝', '賜', '潤', '穂', '徹', '撤', '凜', '凛', '遼', }},
            { 16, new[]{'憲', '樹', '憶', '薫', '頼', '澪', '蕗', }},
            { 17, new[]{'優', '頻', '翼', '濡', '篠', '駿', }},
            { 18, new[]{'織', '瞬', '藍', '鵜', }},
            { 19, new[]{'識', '鶏', '鯨', '瀬', '霧', }},
            { 20, new[]{'護', '響', '譲', '巌', }},
            { 21, new[]{'鶴', '纏', }},
//            { 22, new[]{'驚', '襲', '籠', '灘', '穰', '讃', '饗', '驍', '鷗'}},
//            { 23, new[]{'鑑', '巖', '鱒', '鷲'}},
//            { 24, new[]{'鱗', '鷺', '鷹', '麟'}},
//            { 29, new[]{'鬱'}},
        };
    }
}
