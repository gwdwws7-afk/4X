# R4-01 UI Spec v1

- Stage: R4 UI Productization (2026-06-08 ~ 2026-06-21)
- Task: R4-01 UI spec lock (information hierarchy / status semantics / feedback semantics)
- Draft Date: 2026-04-23
- Status: Ready for review

## 1. Scope

This spec targets the four highest-frequency player surfaces:

1. Map (`MapPanelUI`)
2. Diplomacy (`DiplomacyPanelUI`)
3. Battle Report (`ActionLogUI`, `NotificationPanelUI`)
4. Events (`EventPanelUI`)

Reference implementation paths:

- `Assets/Scripts/UI/MapPanelUI.cs`
- `Assets/Scripts/UI/DiplomacyPanelUI.cs`
- `Assets/Scripts/UI/ActionLogUI.cs`
- `Assets/Scripts/UI/EventPanelUI.cs`
- `Assets/Scripts/UI/UIManager.cs`

## 2. Product Goals

1. Players can answer three questions in <= 5 seconds on each surface:
   - What changed?
   - Why did it change?
   - What can I do next?
2. No silent state transition in turn flow (all impactful changes are visible and traceable).
3. Map -> Diplomacy -> Battle Report -> Event can be navigated with consistent status language.

## 3. Global UI Rules

## 3.1 Information Hierarchy

1. Primary band: current turn, phase, AP, victory/defeat risk.
2. Secondary band: local context (selected node/faction/event subject).
3. Tertiary band: provenance and consequence (source system + delta + trigger condition).

## 3.2 Status Semantics

Use four canonical status tags across all four surfaces:

- `Stable`: no immediate risk
- `Warning`: recoverable pressure
- `Critical`: hard-gate risk within 1-2 turns
- `Locked`: action unavailable due to AP/phase/requirement

## 3.3 Feedback Semantics

1. Every action result must include `delta + reason + next hint`.
2. Every system message must include source prefix:
   - `[Map]`, `[Diplomacy]`, `[Report]`, `[Event]`
3. Every irreversible decision must have an explicit confirmation line with cost and lockout.

## 4. Surface Specs

## 4.1 Map Surface

Primary tasks:

1. Identify node ownership and control-point trend.
2. Identify route integrity and blockade impact.
3. Jump to affected diplomacy/event context.

Required modules:

1. Node status card (owner, control points, last-turn delta, contested marker).
2. Route strip (open/disrupted/blocked + expected economic impact).
3. Hotspot queue (top 3 conflict-priority nodes with reason tags).

Acceptance:

1. Node click updates card within 150ms.
2. Control-point delta always shows source label (`combat`, `event`, `diplomacy effect`).
3. At least one direct navigation entry to diplomacy or event context.

## 4.2 Diplomacy Surface

Primary tasks:

1. Evaluate relation trend and agreement effects.
2. Execute diplomacy actions with explicit AP/resource cost.
3. Understand expected impact and failure risk.

Required modules:

1. Faction relation matrix (current value + 3-turn trend).
2. Agreement panel (active, pending, blocked, expired).
3. Action predictor (expected delta range + risk note).

Acceptance:

1. Any diplomacy action button shows AP cost and lock reason.
2. Agreement state changes emit a report entry and optional event hook.
3. Player can trace a diplomacy result back to exact trigger.

## 4.3 Battle Report Surface

Primary tasks:

1. Review this-turn outcomes quickly.
2. Separate high-impact and low-impact logs.
3. Locate associated map node or diplomacy source.

Required modules:

1. Priority feed (`Critical/Warning/Stable` buckets).
2. Delta chips (resource/faction/control-point changes).
3. Jump links (to map node or diplomacy target).

Acceptance:

1. Critical entries pinned to top and never hidden by default.
2. Every entry includes timestamp (turn/phase) and source system.
3. Filters by `system`, `severity`, and `phase` are available.

## 4.4 Event Surface

Primary tasks:

1. Read event consequence clearly before commit.
2. Compare options on immediate vs delayed impact.
3. Understand follow-up hooks and cooldown.

Required modules:

1. Event header (type, urgency, expiration turn).
2. Option comparison table (cost, immediate delta, delayed delta, risk).
3. Consequence preview (affected systems and expected direction).

Acceptance:

1. No event option lacks consequence text.
2. Expiring events display countdown and default fallback behavior.
3. Chosen option writes a report entry and affected-surface hint.

## 5. R4-01 Delivery Checklist

1. UI spec markdown finalized and reviewed.
2. Canonical status vocabulary aligned in UI text map.
3. Four-surface acceptance checklist approved by QA + Design + Tech.

## 6. R4-02 Handover Targets

1. Build/update prefab set for four high-frequency surfaces.
2. Implement status tag visuals and feedback components.
3. Add regression checks for:
   - status rendering consistency
   - cross-surface jump links
   - locked-action reason visibility
