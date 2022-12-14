using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditorInternal;

public static class Util
{
    [MenuItem(itemName: "Assets/Create/Kaiyum/UITween/Script/Create New UI Tween Script", isValidateFunction: false, priority: 51)]
    public static void CreateScriptFromTemplate()
    {
        KEditorUtil.ProjectResourceUtil.CreateScriptFromTemplate("NewUITween", "UITween", "Editor", "tmpl");
    }

    [MenuItem(itemName: "Assets/Create/Kaiyum/UITween/Script/Create New UI Animation Player Script", isValidateFunction: false, priority: 52)]
    public static void CreateAnimPlayerScriptFromTemplate()
    {
        KEditorUtil.ProjectResourceUtil.CreateScriptFromTemplate("NewUIAnimationPlayer", "UITween", "Editor", "tmpl", "uianimplayer.cs");
    }
}
