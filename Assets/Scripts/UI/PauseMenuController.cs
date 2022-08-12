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
    }

    [PunRPC]
    public void ResumeGameButton()
    {
        view.RPC("ResumeGame", RpcTarget.AllBuffered);
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
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    void OpenCloseUI(GameObject UI, bool OpenClose)
    {
        UI.SetActive(OpenClose);
    }

    [PunRPC]
    void PauseGameButton()
    {
        Time.timeScale = 0f;
        OpenCloseUI(UI, true);
        isGamePaused = true;
    }

    [PunRPC]
    void ResumeGame()
    {
        Time.timeScale = 1f;
        OpenCloseUI(UI, false);
        OpenCloseUI(OptionsUI, false);
        OpenCloseUI(CreditsUI, false);
        isGamePaused = false;
    }
}
