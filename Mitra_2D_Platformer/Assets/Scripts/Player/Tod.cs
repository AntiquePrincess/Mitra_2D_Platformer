using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tod : MonoBehaviour
{
    public GameObject respawn;
    public string SceneName;// В unity нужно написать какую сцену загрузить

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "tod")
        {
            Invoke("Restart", 3f);
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);//Можно сменить на каардинаты
        // SceneManager.LoadScene(SceneName);
    }
}
   