using UnityEngine;
using UnityEngine.SceneManagement;

public class MMB : MonoBehaviour // MMB - Main Menu Buttons
{
    public GameObject[] MenuScenes; // MainMenu, options and so one...
    [Tooltip("При запуске игры открывает главное меню, скрывая все остальные страницы.")]
    public bool LoadDefauldLayer;

    void Start()
    {
        LoadDefauldMenuScene();
    }

    public void PressStartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void PressOptionsButton()
    {
        SwitchMenuPage(1);
    }

    public void PressExitButton()
    {
        Application.Quit();
    }

    public void PressBackButton()
    {
        SwitchMenuPage(0);
    }

    void LoadDefauldMenuScene()
    {
        if (LoadDefauldLayer) SwitchMenuPage(0);
    }

    void SwitchMenuPage(int where) // Switch MainMenu to Options and so one...
    {
        for (int i = 0; i < MenuScenes.Length; i++)
        {
            if (i == where) MenuScenes[i].SetActive(true);
            else MenuScenes[i].SetActive(false);
        }
    }
}

/*
 * 0 - Main menu
 * 1 - Options menu
 */
