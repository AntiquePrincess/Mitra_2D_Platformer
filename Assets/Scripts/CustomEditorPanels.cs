using UnityEngine;
using UnityEditor;
using Cinemachine;
using System.Collections;

public class CustomEditorPanels : EditorWindow
{
    private static GameObject camera = Resources.Load("Camera") as GameObject;
    private static GameObject player = Resources.Load("Player") as GameObject;
    private static GameObject pauseMenu = Resources.Load("CanvasWithPauseMenu") as GameObject;

    [MenuItem("Custom/Create/Set Default Kit")]
    private static void SetDefKit()
    {
        CreatePlayer();
        CreatePauseMenu();
        StopTime(0.3f);
        CreateCamera();
    }

    [MenuItem("Custom/Create/Camera")]
    private static void CreateCamera()
    {
        camera.GetComponentInChildren<CinemachineVirtualCamera>().Follow = GameObject.Find("Player").transform;
        Instantiate(camera).name = "Camera";
    }

    [MenuItem("Custom/Create/Player")]
    private static void CreatePlayer()
    {
        Instantiate(player).name = "Player";
    }
    
    [MenuItem("Custom/Create/Pause Menu")]
    private static void CreatePauseMenu()
    {
        Instantiate(pauseMenu);
    }

    private static IEnumerator StopTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
}