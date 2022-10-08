using UnityEngine;
using Photon.Pun;

public class PauseMenuController : MonoBehaviourPunCallbacks
{
    public bool isGamePaused;

    public GameObject UI;
    public GameObject OptionsUI;
    public GameObject CreditsUI;

    public DisconnectPlayers _disconnectPlayers;
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
        Cursor.visible = false;
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
        _disconnectPlayers.Disconnect();
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
        Cursor.visible = true;
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
        Cursor.visible = true;
        PhotonNetwork.LoadLevel("Lobby");
    }
}
