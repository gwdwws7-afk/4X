---
name: start
description: "First-time onboarding for existing Unity project"
argument-hint: "[no arguments]"
user-invocable: true
allowed-tools: Read, Glob, Grep, Bash
---

# 项目启动

这是一个已存在的Unity 4X策略游戏项目。所有30个游戏系统已实现。

## 立即执行步骤

### 1. 验证Unity项目
```
1. 打开 Unity Editor
2. 加载 C:\test\4X 项目
3. 执行菜单: EventideAge → Setup Complete Scene (one click)
   备选: EventideAge → Setup Project (alias, same pipeline)
4. 检查 Console 是否有红色错误
```

### 2. 运行编译测试
如有编译错误，根据错误信息修复。常见问题：
- 缺少 using 语句 → 添加对应命名空间
- 类型不匹配 → 检查属性类型
- 空引用 → 确保ScriptableObject已创建

### 3. 运行单元测试
```
Menu: EventideAge → Run All Tests
```

### 4. 报告状态
报告：
- 编译是否通过
- 测试通过/失败数量
- 需要修复的问题

## 项目当前状态

- ✅ 30个游戏系统已实现
- ✅ UI组件框架已创建
- ✅ 测试框架已创建
- ⬜ Unity编译验证待完成
- ⬜ UI Prefab待创建
- ⬜ 游戏平衡待测试

## 下一步选项

1. **编译验证** - 在Unity中检查编译
2. **代码审查** - 检查现有代码问题
3. **UI完善** - 创建Unity UI Prefab
4. **游戏平衡** - 调整数值
5. **功能开发** - 继续实现缺失功能
