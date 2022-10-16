using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class Util
{
    [MenuItem(itemName: "Assets/Create/UITween/Script/Create New UI Tween Script", isValidateFunction: false, priority: 51)]
    public static void CreateScriptFromTemplate()
    {
        var path = "";
        string[] files = Directory.GetFiles("Assets/", "*.txt", SearchOption.AllDirectories);
        if (files != null && files.Length > 0)
        {
            foreach (var f in files)
            {
                if (f.Contains("sp_uitween_template_donot_rename_this_folder"))
                {
                    path = f;
                }
            }
        }
        if (string.IsNullOrEmpty(path) == false)
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "NewUITween.cs");
        }
    }
}
