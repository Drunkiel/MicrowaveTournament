using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public string password;
    public TMP_Text passwordText;
    public InputField joinField;

    public IssuesController _issuesController;

    void Start()
    {
        ResetPassword();
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(password, new RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRoom()
    {
        string clientPassword = joinField.text.Trim();

        if (joinField.text != "" && clientPassword.Length == 5) PhotonNetwork.JoinRoom(clientPassword);
        else _issuesController.ShowIssue("Warning: check if your code is the same as host code");
    }

    public void CopyPassword()
    {
        TextEditor te = new TextEditor();
        te.text = password;
        te.SelectAll();
        te.Copy();
    }

    void ResetPassword()
    {
        password = RandomPassword();
        passwordText.text = password;
    }

    string RandomPassword()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] stringChars = new char[5];

        for (int i = 0; i < stringChars.Length; i++)
        {
            int randomNumber = Random.Range(0, chars.Length);
            stringChars[i] = chars[randomNumber];
        }

        var finalString = new string(stringChars);
        return finalString;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Main");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _issuesController.ShowIssue("Error: " + returnCode + "; " + message);
        ResetPassword();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _issuesController.ShowIssue("Error: " + returnCode + "; " + message);
    }
}
