using UnityEngine;
using UnityEditor;
using Cinemachine;

[CustomEditor(typeof(CamOptions))]
public class CameraEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CamOptions tar = (CamOptions)target;
        Camera cam;

        if(GUILayout.Button("Следовать за игроком"))
        {
            cam = FindObjectOfType<Camera>();

            if (GameObject.Find("Player").transform)
                cam.GetComponentInChildren<CinemachineVirtualCamera>().Follow = GameObject.Find("Player").transform;
            else
                Debug.Log("Игрок не найден");
        }
    }
}
