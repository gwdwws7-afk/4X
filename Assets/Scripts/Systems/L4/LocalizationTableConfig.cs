using System;
using UnityEngine;

namespace EventideAge.Systems.L4
{
    [Serializable]
    public class LocalizedTextEntry
    {
        public string Key;
        [TextArea(1, 3)] public string ZhCN;
        [TextArea(1, 3)] public string ZhTW;
        [TextArea(1, 3)] public string EnUS;
        [TextArea(1, 3)] public string FaIR;
        [TextArea(1, 3)] public string ArSA;
        [TextArea(1, 3)] public string RuRU;
    }

    [CreateAssetMenu(fileName = "LocalizationTableConfig", menuName = "EventideAge/LocalizationTableConfig")]
    public class LocalizationTableConfig : ScriptableObject
    {
        [Header("Locale")]
        public string DefaultLocale = "zh-CN";
        public string[] SupportedLocales =
        {
            "zh-CN",
            "zh-TW",
            "en-US",
            "fa-IR",
            "ar-SA",
            "ru-RU"
        };

        [Header("Entries")]
        public LocalizedTextEntry[] Entries =
        {
            new LocalizedTextEntry
            {
                Key = "ui.main.start",
                ZhCN = "开始游戏",
                ZhTW = "開始遊戲",
                EnUS = "Start Game",
                FaIR = "شروع بازی",
                ArSA = "ابدأ اللعبة",
                RuRU = "Начать игру"
            },
            new LocalizedTextEntry
            {
                Key = "ui.main.load",
                ZhCN = "加载存档",
                ZhTW = "載入存檔",
                EnUS = "Load Save",
                FaIR = "بارگذاری ذخیره",
                ArSA = "تحميل الحفظ",
                RuRU = "Загрузить сохранение"
            },
            new LocalizedTextEntry
            {
                Key = "ui.tutorial.skip",
                ZhCN = "跳过教程",
                ZhTW = "跳過教學",
                EnUS = "Skip Tutorial",
                FaIR = "رد آموزش",
                ArSA = "تخطي الدليل",
                RuRU = "Пропустить обучение"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.title",
                ZhCN = "上线流程控制台",
                ZhTW = "上線流程控制台",
                EnUS = "Launch Flow Control"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.guide.toggle.show",
                ZhCN = "显示引导",
                ZhTW = "顯示引導",
                EnUS = "Show Guide"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.guide.toggle.hide",
                ZhCN = "隐藏引导",
                ZhTW = "隱藏引導",
                EnUS = "Hide Guide"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.run_turn",
                ZhCN = "推进整回合",
                ZhTW = "推進整回合",
                EnUS = "Run Full Turn"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.next_phase",
                ZhCN = "下一阶段",
                ZhTW = "下一階段",
                EnUS = "Next Phase"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.run_campaign",
                ZhCN = "推进到24回合",
                ZhTW = "推進到24回合",
                EnUS = "Run to Turn 24"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.diplomacy",
                ZhCN = "外交",
                ZhTW = "外交",
                EnUS = "Diplomacy"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.strategy",
                ZhCN = "战略",
                ZhTW = "戰略",
                EnUS = "Strategy"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.combat",
                ZhCN = "作战",
                ZhTW = "作戰",
                EnUS = "Combat"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.logistics",
                ZhCN = "后勤",
                ZhTW = "後勤",
                EnUS = "Logistics"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.intel",
                ZhCN = "情报",
                ZhTW = "情報",
                EnUS = "Intel"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.next_node",
                ZhCN = "切换节点",
                ZhTW = "切換節點",
                EnUS = "Switch Node"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.save",
                ZhCN = "保存",
                ZhTW = "保存",
                EnUS = "Save"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.load",
                ZhCN = "读取",
                ZhTW = "讀取",
                EnUS = "Load"
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.guide.body",
                ZhCN = "推荐流程\n1. 外交：先稳住关系基线。\n2. 战略：执行意识形态行动积累优势。\n3. 作战：对当前目标节点执行代理或特战行动。\n4. 后勤：执行结算并关注健康度告警。\n5. 情报：回合结束前对目标节点执行侦察。",
                ZhTW = "推薦流程\n1. 外交：先穩住關係基線。\n2. 戰略：執行意識形態行動累積優勢。\n3. 作戰：對當前目標節點執行代理或特戰行動。\n4. 後勤：執行結算並關注健康度告警。\n5. 情報：回合結束前對目標節點執行偵察。",
                EnUS = "Recommended flow\n1. Diplomacy: lock a stable relation baseline.\n2. Strategy: use ideology action to build momentum.\n3. Combat: run proxy/spec-op on current target node.\n4. Logistics: execute settlement and review health alerts.\n5. Intel: recon target node before ending turn."
            },
            new LocalizedTextEntry
            {
                Key = "ui.launch.hint",
                ZhCN = "此面板用于验证“地图→外交→战报→事件”的完整循环。",
                ZhTW = "此面板用於驗證「地圖→外交→戰報→事件」的完整循環。",
                EnUS = "Use this panel to validate the full loop: Map -> Diplomacy -> Report -> Event."
            },
            new LocalizedTextEntry
            {
                Key = "ui.map.hotspot",
                ZhCN = "热点",
                ZhTW = "熱點",
                EnUS = "HOTSPOT"
            },
            new LocalizedTextEntry
            {
                Key = "ui.diplomacy.action.locked",
                ZhCN = "外交行动已锁定",
                ZhTW = "外交行動已鎖定",
                EnUS = "ACTION LOCKED"
            },
            new LocalizedTextEntry
            {
                Key = "ui.report.summary",
                ZhCN = "回合总结",
                ZhTW = "回合總結",
                EnUS = "TURN SUMMARY"
            },
            new LocalizedTextEntry
            {
                Key = "ui.event.option.preview",
                ZhCN = "选项预览",
                ZhTW = "選項預覽",
                EnUS = "OPTION PREVIEW"
            }
        };

        public string ResolveText(LocalizedTextEntry entry, string locale)
        {
            if (entry == null)
            {
                return string.Empty;
            }

            string normalized = string.IsNullOrWhiteSpace(locale) ? "en-US" : locale.Trim();
            switch (normalized)
            {
                case "zh-CN":
                    return entry.ZhCN;
                case "zh-TW":
                    return entry.ZhTW;
                case "fa-IR":
                    return string.IsNullOrWhiteSpace(entry.FaIR) ? entry.EnUS : entry.FaIR;
                case "ar-SA":
                    return string.IsNullOrWhiteSpace(entry.ArSA) ? entry.EnUS : entry.ArSA;
                case "ru-RU":
                    return string.IsNullOrWhiteSpace(entry.RuRU) ? entry.EnUS : entry.RuRU;
                default:
                    return entry.EnUS;
            }
        }
    }
}
