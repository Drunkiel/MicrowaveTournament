using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createField;
    public InputField joinField;

    public IssuesController _issuesController;

    public void CreateRoom()
    {
        string hostPassword = String.Concat(createField.text);

        if (createField.text != "" && hostPassword.Length >= 2) PhotonNetwork.CreateRoom(hostPassword);
        else _issuesController.ShowIssue("Warning: check if your code meets the requirements");
    }

    public void JoinRoom()
    {
        string clientPassword = String.Concat(joinField.text);

        if (createField.text != "" && clientPassword.Length >= 2) PhotonNetwork.JoinRoom(clientPassword);
        else _issuesController.ShowIssue("Warning: check if your code is correct");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _issuesController.ShowIssue("Error {returnCode}: {message}");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Main");
    }
}
