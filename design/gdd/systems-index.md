# Systems Index: 鎶垫姉杞村績 (The Eventide Age)

> **Status**: LOCKED (Index, SSOT-Aligned)
> **Created**: 2026-03-31
> **Last Updated**: 2026-04-08
> **Source Concept**: design/gdd/4x-game-concept.md
> **Rule Baseline**: design/gdd/ssot-core-rules.md (`EVA-SSOT-RULES-2026-04-08.v2`)

---

## Overview

鎶垫姉杞村績鏄竴娆惧洖鍚堝埗4X澶ф垬鐣ユ父鎴忥紝鏍稿績鏄?*涓嶅绉板崥寮?*鈥斺€旂帺瀹朵互鐡﹀笇寰峰笣鍥界殑瑙嗚瀵规姉榛勯噾棰嗙殑鍏ㄤ綋绯诲洿鍫点€傛父鎴忓洿缁曞啗浜嬶紙浠ｇ悊浜烘垬浜夈€佷笉瀵圭О闃插尽锛夈€佺粡娴庯紙灏侀攣涓庡弽灏侀攣銆佽兘婧愰噾铻嶏級銆佸浜わ紙鑱旂洘鏋勫缓銆佸崗璁璁級銆佹剰璇嗗舰鎬侊紙鎶垫姉鍙欎簨杈撳嚭锛夊洓鏉＄嚎灞曞紑銆?

鎵€鏈夌郴缁熷洿缁曚竴涓牳蹇冨惊鐜繍杞細**鍥炲悎闃舵 鈫?琛屽姩閫夋嫨 鈫?浠ｄ环缁撶畻 鈫?鏁屽鍙嶅簲**銆傜帺瀹剁殑姣忎竴涓喅绛栭兘鍦ㄥ唴閮紙娲剧郴婊℃剰搴︺€佺ぞ绋峰€硷級鍜屽閮紙澶栦氦鍏崇郴銆佸啗浜嬫€佸娍锛夊悓鏃跺煁涓嬩唬浠凤紝鐩村埌甯濆浗鑷繁鍏堝穿婧冩垨鐜╁鍦ㄤ换涓€缁村害鍙栧緱绐佺牬銆?

鎵ц鍙ｅ緞璇存槑锛欰1/A2/A3/J 鐨勫洖鍚?AP/鏃堕棿/鑳滆礋瀹氫箟浠?SSOT 涓哄噯锛岀储寮曞彧鍋氭槧灏勶紝涓嶉噸澶嶅畾涔夐槇鍊笺€?

---

## Systems Enumeration

| # | System Name | Category | Priority | DesignStatus | RuntimeStatus | TestStatus | Design Doc | Depends On |
|---|-------------|----------|----------|--------------|---------------|------------|------------|------------|
| A1 | 鍥炲悎涓诲惊鐜?| Core | MVP | Locked | Implemented | Automated | design/gdd/turn-loop.md | 鈥?|
| A2 | 鍥炲悎闃舵寮曟搸 | Core | MVP | Locked | Implemented | Automated | design/gdd/a2-phase-engine.md | A1 |
| A3 | 璧勬簮绯荤粺 | Core | MVP | Locked | Implemented | Automated | design/gdd/a3-resource-system.md | 鈥?|
| A4 | 瀛樻。绯荤粺 | Persistence | MVP | Draft | Implemented | Manual | design/gdd/a4-save-system.md | A1 |
| A5 | 娓告垙鏃堕挓 | Core | MVP | Locked | Implemented | Automated | design/gdd/a5-game-clock.md | A1 |
| B1 | 鏄熷竵-鑳芥簮閲戣瀺绯荤粺 | Economy | MVP | Designed | Implemented | Manual | design/gdd/b1-star-currency-energy-system.md | A3, H2 |
| B2 | 灏侀攣涓庡弽灏侀攣绯荤粺 | Economy | MVP | Designed | Implemented | Manual | design/gdd/b2-blockade-system.md | B1, A3 |
| B3 | 鑳芥簮璐告槗缃戠粶 | Economy | MVP | Designed | Implemented | Manual | design/gdd/b3-energy-trade-network.md | B1, H2 |
| B4 | 姹囩巼绯荤粺 | Economy | Alpha | Draft | Implemented | Manual | design/gdd/b4-exchange-rate.md | B1 |
| B5 | 缁忔祹缁撶畻绯荤粺 | Economy | MVP | Designed | Implemented | Automated | design/gdd/b5-economic-settlement.md | B1, B2, B3, A3 |
| C1 | 澶栦氦鍏崇郴绯荤粺 | Gameplay | MVP | Designed | Implemented | Automated | design/gdd/c1-diplomatic-relations.md | A3, H1 |
| C2 | 澶栦氦鍗忚绯荤粺 | Gameplay | MVP | Designed | Implemented | Manual | design/gdd/c2-diplomatic-protocols.md | C1, A3 |
| C3 | 鎰忚瘑褰㈡€佽緭鍑虹郴缁?| Gameplay | Alpha | Draft | Implemented | Manual | design/gdd/c3-ideology-system.md | C1, A3 |
| C4 | 鑱旂洘绯荤粺 | Gameplay | Alpha | Draft | Implemented | Manual | design/gdd/c4-alliance-system.md | C1, C2 |
| C5 | 鍥介檯缁勭粐绯荤粺 | Gameplay | Alpha | Draft | Implemented | Manual | design/gdd/c5-international-orgs.md | C1, C3 |
| D1 | 鍐涗簨琛屽姩绯荤粺 | Gameplay | MVP | Designed | Implemented | Automated | design/gdd/d1-military-operations.md | B1, H1, H2 |
| D2 | 鍐涗簨-鏀挎不鑱斿姩 | Gameplay | MVP | Draft | Implemented | Automated | design/gdd/d2-military-political-linkage.md | D1, E, C1 |
| D3 | 浠ｇ悊浜烘皯浜嬬郴缁?| Gameplay | Alpha | Draft | Implemented | Automated | design/gdd/d3-proxy-civil-affairs.md | D1, C4 |
| D4 | 鏍稿▉鎱戠郴缁?| Gameplay | Alpha | Draft | Implemented | Automated | design/gdd/d4-nuclear-deterrence.md | D1, F1 |
| D5 | 鎴樹簤缁撶畻绯荤粺 | Gameplay | MVP | Designed | Implemented | Automated | design/gdd/d5-war-resolution.md | D1, H1, H2 |
| D6 | 鍐涗簨绉戞妧绯荤粺 | Progression | Alpha | Draft | Implemented | Automated | design/gdd/d6-military-tech.md | B1, D1 |
| E | 鍐呴儴鏀挎不绯荤粺 | Gameplay | MVP | Draft | Implemented | Automated | design/gdd/e-internal-politics.md | A3, D2, C3, B5 |
| F1 | 鎯呮姤绯荤粺 | Gameplay | Alpha | Draft | Implemented | Automated | design/gdd/f1-intelligence-system.md | A1 |
| G | 鍔垮姏AI妗嗘灦 | AI | MVP | Designed | Implemented | Manual | design/gdd/g-faction-ai-framework.md | B1, C1, D1 |
| H1 | 鎴樼暐鍦板浘绯荤粺 | Presentation | MVP | Designed | Implemented | Automated | design/gdd/h1-strategic-map.md | 鈥?|
| H2 | 鎴樼暐鑺傜偣绯荤粺 | Presentation | MVP | Designed | Implemented | Automated | design/gdd/h2-strategic-node.md | H1 |
| H3 | 鍦板舰涓庤閲庣郴缁?| Gameplay | Alpha | Draft | Implemented | Automated | design/gdd/h3-terrain-vision.md | H1, H2, F1 |
| I1 | 浜嬩欢绯荤粺 | Narrative | Alpha | Draft | Implemented | Manual | design/gdd/i1-event-system.md | C1, D1, E |
| J | 鑳滆礋鍒ゅ畾绯荤粺 | Gameplay | MVP | Locked | Implemented | Automated | design/gdd/j-victory-defeat.md | C1, D1, B1, B5, D5 |
| K | UI妗嗘灦 | UI | MVP | Designed | Implemented | Automated | design/gdd/k-ui-framework.md | H1, A3 |
| L1 | 寮傛澶氫汉绯荤粺 | Meta | Full Vision | Draft | NotImplemented | None | design/gdd/l1-async-multiplayer.md | A1, G |
| L2 | 鏂版墜鏁欑▼绯荤粺 | Meta | Alpha | Draft | NotImplemented | None | design/gdd/l2-tutorial-system.md | K |
| L3 | Steam闆嗘垚 | Meta | Full Vision | Draft | NotImplemented | None | design/gdd/l3-steam-integration.md | A4 |
| L4 | 鏈湴鍖栫郴缁?| Meta | Full Vision | Draft | NotImplemented | None | design/gdd/l4-localization.md | K |

鐘舵€佸彛寰勮鏄庯細
- `DesignStatus`锛氭枃妗ｅ眰鐘舵€侊紙Locked / Designed / Draft锛夈€?
- `RuntimeStatus`锛氫唬鐮佸眰鐘舵€侊紙Implemented / NotImplemented锛夈€?
- `TestStatus`锛氶獙璇佸眰鐘舵€侊紙Automated / Manual / None锛夈€?
- `Automated` 浠?`EventideAge/Run All Tests` 褰撳墠瑕嗙洊涓哄噯銆?

---

## Categories

| Category | Description | Systems |
|----------|-------------|---------|
| **Core** | 椹卞姩娓告垙杩愯浆鐨勫熀纭€绯荤粺锛屾棤姝ゅ垯娓告垙鏃犳硶鍚姩 | A1, A2, A3, A4, A5 |
| **Economy** | 璧勬簮浜х敓銆佹秷鑰椼€佷氦鏄撳拰灏侀攣鐨勭粡娴庡惊鐜?| B1, B2, B3, B4, B5 |
| **Gameplay** | 瀹氫箟娓告垙鏍稿績鐜╂硶鍜岀瓥鐣ョ淮搴︾殑绯荤粺 | C1-C5, D1-D6, E, F1, H3, I1, J |
| **AI** | 鎵€鏈堿I鍔垮姏鐨勫喅绛栧拰琛屼负妗嗘灦 | G |
| **Presentation** | 鍦板浘鍜岃妭鐐圭殑瑙嗚鍛堢幇灞?| H1, H2 |
| **UI** | 闈㈠悜鐜╁鐨勬墍鏈夌晫闈㈠拰淇℃伅鏄剧ず妗嗘灦 | K |
| **Narrative** | 浜嬩欢銆佸ぇ浜嬭鍜屽彊浜嬬敓鎴愮郴缁?| I1 |
| **Progression** | 鐜╁鍜屽娍鍔涚殑鎴愰暱涓庤В閿佺郴缁?| D6 |
| **Persistence** | 瀛樻。銆佸洖鏀惧拰璁剧疆绯荤粺 | A4 |
| **Meta** | 娓告垙澶栫郴缁燂紙澶氫汉/鏁欑▼/骞冲彴闆嗘垚/鏈湴鍖栵級 | L1, L2, L3, L4 |

---

## Priority Tiers

| Tier | Definition | Target Milestone | Count |
|------|------------|------------------|-------|
| **MVP** | 鏍稿績寰幆鍙繍琛岀殑鏈€灏忕郴缁熼泦銆傛病鏈夎繖浜涙棤娉曟祴璇?娓告垙鏄惁鏈変箰瓒?銆?| 绗竴闃舵锛?涓湀锛?| 20 |
| **Alpha** | 瀹屾暣娓告垙绯荤粺rough form銆傛墍鏈夋牳蹇冩満鍒堕兘宸插瓨鍦紝鍐呭鍙～鍏呫€?| 绗簩/涓夐樁娈?| 11 |
| **Full Vision** | 鎶涘厜銆佽竟缂樻儏鍐点€佸彲閫夊姛鑳藉拰瀹屾暣鍐呭銆?| 绗洓闃舵/鍙戝竷 | 3 |

---

## Dependency Map

### Foundation Layer (no dependencies)

1. **A1 鍥炲悎涓诲惊鐜?* 鈥?鏈€楂樺眰椹卞姩锛屾暣涓父鎴忕殑蹇冭剰
2. **A3 璧勬簮绯荤粺** 鈥?7绉嶈祫婧愮殑瀹氫箟鍜屽熀纭€璁＄畻
3. **H1 鎴樼暐鍦板浘绯荤粺** 鈥?鍦板浘娓叉煋鍜屼氦浜掑熀纭€璁炬柦

### Core Layer (depends on Foundation)

1. **H2 鎴樼暐鑺傜偣绯荤粺** 鈥?鍦板浘涓婄殑浜夊ず瀵硅薄锛屼緷璧朒1
2. **B1 鏄熷竵-鑳芥簮閲戣瀺绯荤粺** 鈥?渚濊禆A3锛堣祫婧愬畾涔夛級
3. **F1 鎯呮姤绯荤粺** 鈥?渚濊禆A1锛堝洖鍚堣Е鍙戯級
4. **A4 瀛樻。绯荤粺** 鈥?渚濊禆A1

### Core-Mechanics Layer (depends on Core)

1. **B2 灏侀攣涓庡弽灏侀攣绯荤粺** 鈥?渚濊禆B1
2. **B5 缁忔祹缁撶畻绯荤粺** 鈥?渚濊禆B1, B2, B3, A3
3. **C1 澶栦氦鍏崇郴绯荤粺** 鈥?渚濊禆A3, H1
4. **D1 鍐涗簨琛屽姩绯荤粺** 鈥?渚濊禆B1, H1, H2
5. **E 鍐呴儴鏀挎不绯荤粺** 鈥?渚濊禆A3, D2, C3, B5
6. **K UI妗嗘灦** 鈥?渚濊禆H1, A3

### Feature Layer (depends on Core-Mechanics)

1. **A2 鍥炲悎闃舵寮曟搸** 鈥?渚濊禆A1
2. **B3 鑳芥簮璐告槗缃戠粶** 鈥?渚濊禆B1, H2
3. **B4 姹囩巼绯荤粺** 鈥?渚濊禆B1
4. **C2 澶栦氦鍗忚绯荤粺** 鈥?渚濊禆C1, A3
5. **C3 鎰忚瘑褰㈡€佽緭鍑虹郴缁?* 鈥?渚濊禆C1, A3
6. **D2 鍐涗簨-鏀挎不鑱斿姩** 鈥?渚濊禆D1, E, C1
7. **D5 鎴樹簤缁撶畻绯荤粺** 鈥?渚濊禆D1, H1, H2
8. **G 鍔垮姏AI妗嗘灦** 鈥?渚濊禆B1, C1, E, D1
9. **J 鑳滆礋鍒ゅ畾绯荤粺** 鈥?渚濊禆C1, D1, B1, B5, D5

### Advanced Feature Layer (depends on Feature)

1. **C4 鑱旂洘绯荤粺** 鈥?渚濊禆C1, C2
2. **C5 鍥介檯缁勭粐绯荤粺** 鈥?渚濊禆C1, C3
3. **D3 浠ｇ悊浜烘皯浜嬬郴缁?* 鈥?渚濊禆D1, C4
4. **D4 鏍稿▉鎱戠郴缁?* 鈥?渚濊禆D1, F1
5. **D6 鍐涗簨绉戞妧绯荤粺** 鈥?渚濊禆B1, D1
6. **H3 鍦板舰涓庤閲庣郴缁?* 鈥?渚濊禆H1, H2, F1
7. **I1 浜嬩欢绯荤粺** 鈥?渚濊禆C1, D1, E

### Polish Layer

1. **L1 寮傛澶氫汉绯荤粺** 鈥?渚濊禆A1, G
2. **L2 鏂版墜鏁欑▼绯荤粺** 鈥?渚濊禆K
3. **L3 Steam闆嗘垚** 鈥?渚濊禆A4
4. **L4 鏈湴鍖栫郴缁?* 鈥?渚濊禆K

---

## Recommended Design Order

| Order | System | Priority | Layer | Est. Effort |
|-------|--------|----------|-------|-------------|
| 1 | A1 鍥炲悎涓诲惊鐜?| MVP | Foundation | M |
| 2 | A3 璧勬簮绯荤粺 | MVP | Foundation | M |
| 3 | A5 娓告垙鏃堕挓 | MVP | Foundation | S |
| 4 | H1 鎴樼暐鍦板浘绯荤粺 | MVP | Foundation | M |
| 5 | H2 鎴樼暐鑺傜偣绯荤粺 | MVP | Core | M |
| 6 | B1 鏄熷竵-鑳芥簮閲戣瀺绯荤粺 | MVP | Core | M |
| 7 | B2 灏侀攣涓庡弽灏侀攣绯荤粺 | MVP | Core | M |
| 8 | B5 缁忔祹缁撶畻绯荤粺 | MVP | Core | M |
| 9 | D1 鍐涗簨琛屽姩绯荤粺 | MVP | Core | M |
| 10 | D5 鎴樹簤缁撶畻绯荤粺 | MVP | Feature | M |
| 11 | C1 澶栦氦鍏崇郴绯荤粺 | MVP | Core | M |
| 12 | C2 澶栦氦鍗忚绯荤粺 | MVP | Feature | M |
| 13 | E 鍐呴儴鏀挎不绯荤粺 | MVP | Feature | M |
| 14 | G 鍔垮姏AI妗嗘灦 | MVP | Feature | L |
| 15 | J 鑳滆礋鍒ゅ畾绯荤粺 | MVP | Feature | S |
| 16 | K UI妗嗘灦 | MVP | Presentation | L |
| 17 | B3 鑳芥簮璐告槗缃戠粶 | MVP | Core | M |
| 18 | D2 鍐涗簨-鏀挎不鑱斿姩 | MVP | Feature | M |
| 19 | C3 鎰忚瘑褰㈡€佽緭鍑虹郴缁?| Alpha | Feature | M |
| 20 | F1 鎯呮姤绯荤粺 | Alpha | Feature | M |
| 21 | A2 鍥炲悎闃舵寮曟搸 | Alpha | Feature | S |
| 22 | B4 姹囩巼绯荤粺 | Alpha | Feature | S |
| 23 | C4 鑱旂洘绯荤粺 | Alpha | Feature | M |
| 24 | C5 鍥介檯缁勭粐绯荤粺 | Alpha | Feature | S |
| 25 | D3 浠ｇ悊浜烘皯浜嬬郴缁?| Alpha | Feature | M |
| 26 | D4 鏍稿▉鎱戠郴缁?| Alpha | Feature | S |
| 27 | D6 鍐涗簨绉戞妧绯荤粺 | Alpha | Feature | M |
| 28 | H3 鍦板舰涓庤閲庣郴缁?| Alpha | Feature | S |
| 29 | I1 浜嬩欢绯荤粺 | Alpha | Narrative | L |
| 30 | L2 鏂版墜鏁欑▼绯荤粺 | Alpha | Meta | M |

**Effort estimates**: S = 1 session, M = 2-3 sessions, L = 4+ sessions

璇存槑锛氳椤哄簭浼樺厛瑕嗙洊 `MVP + Alpha` 鍏?30 椤癸紱`L1/L3/L4` 灞炰簬 Full Vision锛屽湪鍙戝竷鍓嶉樁娈靛崟鐙帓鏈熴€?
---

## Circular Dependencies

- **None found** 鉁?

鎵€鏈夌郴缁熶緷璧栭摼鍧囦负鍗曞悜锛屾棤寰幆銆?

**娉?*: E锛堝唴閮ㄦ斂娌荤郴缁燂級涓嶥2锛堝啗浜?鏀挎不鑱斿姩锛夊瓨鍦ㄥ弻鍚戝奖鍝嶏紙鍐涗簨琛屽姩褰卞搷娲剧郴锛屾淳绯诲奖鍝嶈鍔ㄥ彲鐢ㄦ€э級锛屼絾閫氳繃A3锛堣祫婧愶級浣滀负涓粙瑙ｈ€︼紝涓嶆瀯鎴愮湡姝ｇ殑寰幆渚濊禆銆?

---

## High-Risk Systems

| System | Risk Type | Risk Description | Mitigation |
|--------|-----------|-----------------|------------|
| **A1 鍥炲悎涓诲惊鐜?* | Design | 涓€鏃﹁璁℃湁闂锛屾墍鏈夌郴缁熷叏閮ㄩ噸鏉ワ紱闃舵椤哄簭鍜岃鍔ㄧ偣娑堣€楀奖鍝嶅叏灞€骞宠　 | 浼樺厛鍘熷瀷楠岃瘉锛涚敤鏈€绠€鍗曠殑鏁板€艰窇閫氬叏娴佺▼ |
| **B1 鏄熷竵-鑳芥簮閲戣瀺** | Design + Balance | 缁忔祹绯荤粺璁捐閿欒瀵艰嚧娓告垙鏃犳硶骞宠　锛涘皝閿?鍙嶅皝閿佹満鍒舵槸鏍稿績涔愯叮鏉ユ簮 | 鍙傝€僊achinations宸ュ叿妯℃嫙锛涘湪prototype闃舵閲嶇偣娴嬭瘯缁忔祹闂幆 |
| **G 鍔垮姏AI妗嗘灦** | Design | AI琛屼负濡傛灉涓嶅涓板瘜锛屾父鎴忓悗鏈熸棤鑱婏紱澶氬娍鍔涘叡鐢ㄥ悓涓€妗嗘灦浣嗕汉鏍煎樊寮傚ぇ | 绗簩闃舵浼樺厛鍋氾紱鍙傝€働aradox鐨凙I浜烘牸绯荤粺璁捐 |
| **C3 鎰忚瘑褰㈡€佽緭鍑?* | Design | 杞疄鍔涙暟鍊煎寲鍙兘鍙樻垚鏁板€兼父鎴忚€岄潪鍙欎簨宸ュ叿 | MVP闃舵鍏堝仛绠€鍖栫増锛堝崟鍚戞晥鏋滐紝鏆備笉寮曞叆鎰熸煋鐜囷級 |
| **I1 浜嬩欢绯荤粺** | Scope | 浜嬩欢閲忓ぇ锛?50+锛夛紝璐ㄩ噺闅句互淇濊瘉 | 绗竴闃舵鍙仛10-15涓牳蹇冧簨浠舵ā鏉匡紱鍚庣画鐢ㄦā鐗堝寲浜嬩欢鐢熸垚 |

---

## Progress Tracker

| Metric | Count |
|--------|-------|
| Total systems indexed | 34 |
| DesignStatus Locked | 5 |
| DesignStatus Designed | 12 |
| DesignStatus Draft | 17 |
| Runtime implemented | 30/34 |
| Runtime not implemented | 4/34 |
| Automated test coverage | 19/34 |
| Manual verification only | 11/34 |
| No test status | 4/34 |
| MVP priority systems | 20 |
| Alpha priority systems | 11 |
| Full Vision priority systems | 3 |

---

## Next Steps

- [x] Review and approve this systems enumeration (updated to 34 systems)
- [ ] Design MVP-tier systems first (use `/design-system [system-name]`)
- [ ] Run `/design-review` on each completed GDD
- [ ] Run `/gate-check pre-production` when MVP systems are designed
- [ ] Prototype the highest-risk system early (`/prototype [system]`)



