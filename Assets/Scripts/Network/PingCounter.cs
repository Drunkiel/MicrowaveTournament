using UnityEngine;
using Photon.Pun;
using TMPro;

public class PingCounter : MonoBehaviour
{
    public int ping;
    public TMP_Text displayText;

    // Start is called before the first frame update
    void Start()
    {
        displayText = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CountPing();
    }

    void CountPing()
    {
        ping = PhotonNetwork.GetPing();
        displayText.text = "Ping: " + ping.ToString();
    }
}
