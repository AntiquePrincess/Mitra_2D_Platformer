using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tod : MonoBehaviour
{
    public GameObject respawn;
    public HP hp;
    public string SceneName;// В unity нужно написать какую сцену загрузить

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "tod")
        {
           _DMGtoHP();
        }
    }
    private void _DMGtoHP()
    {
        hp.health -= 5 * Time.deltaTime;
        if (hp.health <= 0)
        {
            Invoke("Restart", 1f);
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);//Можно сменить на каардинаты
        // SceneManager.LoadScene(SceneName);
    }
}
   