using UnityEngine;
using Photon.Pun;

public class PauseMenuController : MonoBehaviourPunCallbacks
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
        ResumeGame();
        PhotonNetwork.LeaveRoom();
    }

    public void CloseButton()
    {
        OpenCloseUI(OptionsUI, false);
        OpenCloseUI(CreditsUI, false);
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
        CloseButton();
        isGamePaused = false;
    }

    void OpenCloseUI(GameObject UI, bool OpenClose)
    {
        UI.SetActive(OpenClose);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}
