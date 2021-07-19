using UnityEngine;
using UnityEngine.SceneManagement;

public class GMB : MonoBehaviour // GMB - Game Menu Buttons
{
    public GameObject[] MenuScenes; // MainMenu, options and so one...
    [HideInInspector] public bool isPause = false;

    private void Start()
    {
        SwitchMenuPage(0);
        PressPauseButton();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PressPauseButton();
    }

    public void PressResumeButton()
    {
        Time.timeScale = 1f;
        isPause = false;

        SwitchMenuPage(-1);
    }

    public void PressPauseButton()
    {
        if (isPause) { PressResumeButton(); return; }

        Time.timeScale = 0f;
        isPause = true;

        SwitchMenuPage(0);
    }

    public void PressOptionsButton()
    {
        SwitchMenuPage(1);
    }

    public void PressExitToMM()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PressExitButton()
    {
        Application.Quit();
    }

    public void PressBackButton()
    {
        SwitchMenuPage(0);
    }

    void SwitchMenuPage(int where) // Switch MainMenu to Options and so one... (if where = -1, we'll off all the menu scenes)
    {
        for (int i = 0; i < MenuScenes.Length; i++)
        {
            if (i == where) MenuScenes[i].SetActive(true);
            else MenuScenes[i].SetActive(false);
        }
    }
}
