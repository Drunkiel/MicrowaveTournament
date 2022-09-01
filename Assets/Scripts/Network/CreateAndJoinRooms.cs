using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createField;
    public InputField joinField;

    public void CreateRoom()
    {
        string hostPassword = String.Concat(createField.text);

        if (createField.text != "" && hostPassword.Length >= 2) PhotonNetwork.CreateRoom(hostPassword);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinField.text);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Main");
    }
}
