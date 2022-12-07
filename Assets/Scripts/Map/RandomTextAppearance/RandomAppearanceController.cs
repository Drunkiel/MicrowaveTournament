using UnityEngine;
using Photon.Pun;

public class RandomAppearanceController : MonoBehaviour
{
    public bool isBlueTeam;
    private float cooldown;

    public GameObject prefab;
    public ScoreController _scoreController;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_scoreController.playerOneWinnedMaps >= 1 || _scoreController.playerTwoWinnedMaps >= 1)
        {
            if (cooldown <= 0)
            {
                view.RPC("SpawnText", RpcTarget.AllBuffered);

                isBlueTeam = !isBlueTeam;
                cooldown = Random.Range(5, 10);
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    [PunRPC]
    void SpawnText()
    {
        GameObject[] viewers;

        if (isBlueTeam)
        {
            viewers = GameObject.FindGameObjectsWithTag("Viewer_Blue");

            if (viewers.Length > 0 && _scoreController.playerOneWinnedMaps >= 1) Instantiate(prefab, new Vector3(-8, 0, 10), Quaternion.Euler(0, -25, 0), transform);
        }
        else
        {
            viewers = GameObject.FindGameObjectsWithTag("Viewer_Red");

            if (viewers.Length > 0 && _scoreController.playerTwoWinnedMaps >= 1) Instantiate(prefab, new Vector3(8, 0, 10), Quaternion.Euler(0, 25, 0), transform);
        }
    }
}
