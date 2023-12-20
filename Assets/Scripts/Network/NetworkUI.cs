using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button hostBTN;
    [SerializeField] private Button clientBTN;

    private void Awake()
    {
        hostBTN.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        clientBTN.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }
}
