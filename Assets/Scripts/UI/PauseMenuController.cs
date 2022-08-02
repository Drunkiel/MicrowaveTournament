using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public bool isGamePaused;

    public GameObject UI;
    public GameObject OptionsUI;
    public GameObject CreditsUI;

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    void Controller()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {
            PauseGameButton();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
        {
            ResumeGameButton();
            OpenCloseUI(OptionsUI, false);
            OpenCloseUI(CreditsUI, false);
        }
    }

    void PauseGameButton()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        OpenCloseUI(UI, true);
    }

    public void ResumeGameButton()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        OpenCloseUI(UI, false);
    }

    public void OptionsButton()
    {
        OpenCloseUI(OptionsUI, true);
    }

    public void CreditsButton()
    {
        OpenCloseUI(CreditsUI, true);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }

    void OpenCloseUI(GameObject UI, bool OpenClose)
    {
        UI.SetActive(OpenClose);
    }
}
