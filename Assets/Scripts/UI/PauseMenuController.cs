using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public bool isGamePaused;

    public GameObject UI;
    public GameObject OptionsUI;

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
            OptionsButton(false);
        }
    }

    void PauseGameButton()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        UI.SetActive(true);
    }

    public void ResumeGameButton()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        UI.SetActive(false);
    }

    public void OptionsButton(bool OpenClose)
    {
        OptionsUI.SetActive(OpenClose);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}
