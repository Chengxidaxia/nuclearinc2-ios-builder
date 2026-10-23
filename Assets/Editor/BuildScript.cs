using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// batchmode 构建入口：Unity -batchmode -quit -projectPath . -buildTarget iOS -executeMethod BuildScript.BuildiOS
/// 说明：核能公司2 原始包是 Android 构建，这里只做「iOS 导出 + 关键设置」，不改游戏逻辑。
/// </summary>
public static class BuildScript
{
    private static readonly string[] Scenes =
    {
        "Assets/Scenes/Main Menu.unity",
        "Assets/Scenes/AES/lvl1_2.unity",
    };

    public static void BuildiOS()
    {
        // ---------- 基础标识 ----------
        PlayerSettings.companyName = "Nuclear Inc";
        PlayerSettings.productName = "Nuclear inc 2";

        string bundleId = System.Environment.GetEnvironmentVariable("IOS_BUNDLE_ID");
        if (string.IsNullOrEmpty(bundleId)) bundleId = "com.nuclearinc2.ios";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, bundleId);

        string ver = System.Environment.GetEnvironmentVariable("IOS_BUNDLE_VERSION");
        if (string.IsNullOrEmpty(ver)) ver = "1.4.30";
        PlayerSettings.bundleVersion = ver;

        // ---------- 屏幕 / 方向 ----------
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
        PlayerSettings.allowedAutorotateToPortrait = true;
        PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
        PlayerSettings.allowedAutorotateToLandscapeLeft = true;
        PlayerSettings.allowedAutorotateToLandscapeRight = true;
        PlayerSettings.statusBarHidden = true;

        // ---------- iOS 专属 ----------
        PlayerSettings.iOS.targetOSVersionString = "9.0";
        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
        PlayerSettings.iOS.scriptCallOptimization = MonoOptimizationLevel.Default;
        PlayerSettings.iOS.buildNumber = "1";

        // ---------- 场景列表 ----------
        var scenes = new System.Collections.Generic.List<EditorBuildSettingsScene>();
        foreach (var s in Scenes)
        {
            if (File.Exists(s))
            {
                scenes.Add(new EditorBuildSettingsScene(s, true));
                Debug.Log("[BuildScript] scene queued: " + s);
            }
            else
            {
                Debug.LogWarning("[BuildScript] scene MISSING: " + s);
            }
        }
        EditorBuildSettings.scenes = scenes.ToArray();

        // ---------- 输出 ----------
        string outDir = System.Environment.GetEnvironmentVariable("IOS_EXPORT_DIR");
        if (string.IsNullOrEmpty(outDir)) outDir = "Builds/iOS";
        Directory.CreateDirectory("Builds");

        var opts = new BuildPlayerOptions
        {
            scenes = Scenes,
            locationPathName = outDir,
            target = BuildTarget.iOS,
            options = BuildOptions.None,
        };

        Debug.Log("[BuildScript] == building iOS ==");
        var report = BuildPipeline.BuildPlayer(opts);
        var summary = report.summary;
        Debug.Log(string.Format("[BuildScript] result={0} errors={1} size={2}",
            summary.result, summary.totalErrors, summary.totalSize));

        if (summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.LogError("[BuildScript] BUILD FAILED: " + summary.result);
            EditorApplication.Exit(1);
        }
        Debug.Log("[BuildScript] BUILD SUCCEEDED -> " + outDir);
    }
}
