using UnityEditor;
public class BuildScript
{
    static void PerformBuild () {
        string [] scene = { "./Assets/Scenes/Menu/Menu.unity", "./Assets/Scenes/Maps/Map_01.unity" };
        BuildPipeline.BuildPlayer (scene,"./Builds/build.exe",
            BuildTarget.StandaloneWindows, BuildOptions.None);
    }
}