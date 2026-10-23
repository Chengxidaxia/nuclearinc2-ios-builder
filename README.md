# nuclearinc2-ios-builder

把《核能公司2》(Nuclear inc 2) 的 Unity 工程编译成 iOS 设备版 `.ipa` 的云端构建仓库。

## 这个仓库是什么

本仓库**不是**原始游戏源码，而是从 Android APK 反编译重建出来的 Unity 工程：

| 项 | 值 |
|---|---|
| 引擎 | Unity **2017.3.1f1**（与原始包一致，`ProjectSettings/ProjectVersion.txt`） |
| 来源 | `apk/plugin_out/assets/bin/Data` → **AssetRipper 2.0.0** → Unity Project |
| 场景 | `Assets/Scenes/Main Menu.unity`（主菜单）、`Assets/Scenes/AES/lvl1_2.unity`（游戏） |
| 脚本 | `Assets/Scripts/Assembly-CSharp/`（137 个 .cs，来自程序集反编译） |
| 构建 | `.github/workflows/build-ios.yml`（macOS runner） |

> 与同账号的 `krkrsdl3-ios-builder` 不同：那边是 C++ 引擎源码 + `cmake`；
> 本作是 Unity 闭源引擎，**必须由 Unity 编辑器导出 Xcode 工程**，再 `xcodebuild`。

## 构建原理

```
macos-14 runner
  ├─ 下载安装 Unity 2017.3.1f1 + iOS Build Support（官方 CDN，静默 installer）
  ├─ 激活 Unity 许可证
  ├─ Unity -batchmode -executeMethod BuildScript.BuildiOS   → Builds/iOS（Xcode 工程）
  ├─ xcodebuild -sdk iphoneos CODE_SIGNING_ALLOWED=NO       → Unity-iPhone.app
  ├─ 校验 .app 内可执行文件存在且 > 1MB
  ├─ 补 Info.plist（CFBundleIdentifier / UILaunchScreen / UIFileSharingEnabled …）
  └─ zip Payload → nuclearinc2-device.ipa（未签名）+ artifact + Release
```

## 前置条件（必须配）

在仓库 **Settings → Secrets and variables → Actions** 里添加以下之一：

| Secret | 说明 |
|---|---|
| `UNITY_LICENSE` | Unity 许可证 `.ulf` 文件全文（推荐） |
| `UNITY_EMAIL` + `UNITY_PASSWORD` | Unity 账号（Personal 版即可，但可能有 2FA 问题） |

可选 Variable：`IOS_BUNDLE_ID`（默认 `com.nuclearinc2.ios`）。

## 触发

- push 到 `main` 自动构建
- 或在 Actions 页面手动 `Run workflow`

产物：`nuclearinc2-ios-ipa` artifact，以及 Release `ios-build-<run_number>`。

## 安装

这是**未签名 ipa**，不能直接双击安装：

- **TrollStore**（iOS 14.0–16.6.1 等受影响版本）：直接装，永久
- **Sideloadly / AltStore / SideStore**：用 Apple ID 重签，免费账号 7 天

## 已知差异 / 限制

- 广告 SDK（GoogleMobileAds）原为 Android 实现，iOS 上不可用，需保持禁用
- 部分 3D Mesh 在反编译阶段读取失败（`sharedassets1.assets` CompressedMesh），可能缺个别模型
- 反编译重建的工程不可能与原始工程 100% 等价，UI/动画可能需要微调
