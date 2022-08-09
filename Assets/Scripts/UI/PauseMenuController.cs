using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PauseMenuController : MonoBehaviour
{
    public bool isGamePaused;

    public GameObject UI;
    public GameObject OptionsUI;
    public GameObject CreditsUI;

    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    [PunRPC]
    void Controller()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {
            view.RPC("PauseGameButton", RpcTarget.AllBuffered);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
        {
            view.RPC("ResumeAndCloseUI", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void PauseGameButton()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        OpenCloseUI(UI, true);
    }

    [PunRPC]
    public void ResumeGameButton()
    {
        view.RPC("ResumeGame", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        OpenCloseUI(UI, false);
    }

    [PunRPC]
    void ResumeAndCloseUI()
    {
        ResumeGameButton();
        OpenCloseUI(OptionsUI, false);
        OpenCloseUI(CreditsUI, false);
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
