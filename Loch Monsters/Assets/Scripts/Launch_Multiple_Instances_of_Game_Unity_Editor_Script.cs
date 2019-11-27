/* 
* Tested on Unity 2019.2.0f1
*
* Updated from https://www.appsfresh.com/blog/multiplayer/ (Credit: Yohann Taieb) - added precompiler 
* directives to make sure that when building unity doesn't complain.
* 
* After adding this script to your project the launch options will be avaliable under 
* File > Run Multiplayer > ...
*
* Make sure your build options are configured! This is specifically for Windows 
* but if you want to test on/for Mac OSX, Linux, Android, iOS change 'BuildTarget.StandaloneWindows64'
* to something else. Script only tested on Windows!
*
* Likewise goes for more than 4 instances - just add more menu items!
*
* More information about the build pipeline: https://docs.unity3d.com/Manual/BuildPlayerPipeline.html
*/

#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor;

public class MultiplayersBuildAndRun : Editor
{

    [MenuItem("File/Run Multiplayer/Windows/2 Players")]
    static void PerformWin64Build2()
    {
        PerformWin64Build(2);
    }


    [MenuItem("File/Run Multiplayer/Windows/3 Players")]
    static void PerformWin64Build3()
    {
        PerformWin64Build(3);
    }


    [MenuItem("File/Run Multiplayer/Windows/4 Players")]
    static void PerformWin64Build4()
    {
        PerformWin64Build(4);
    }


    static void PerformWin64Build(int playerCount)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
        for (int i = 1; i <= playerCount; i++)
        {
            BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Win64/" + GetProjectName() + i.ToString() + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.AutoRunPlayer);
        }
    }
    static string GetProjectName()
    {
        string[] s = Application.dataPath.Split('/');
        return s[s.Length - 2];
    }


    static string[] GetScenePaths()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];


        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }


        return scenes;
    }
}
#endif