using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
  static void PerformBuild () {
        string [] scene = { "Assets/Scenes/Menu/Menu.unity", "Assets/Scenes/Maps/Map_01.unity" };
        BuildPipeline.BuildPlayer (scene, "./Build/Runnable/RUN.exe",
            BuildTarget.StandaloneWindows, BuildOptions.None);
    }
}
