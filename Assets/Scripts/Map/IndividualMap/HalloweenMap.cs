using System.IO;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class HalloweenMap : MonoBehaviour
{
    //Display
    public GameObject splashPrefab;
    public int score;
    public TMP_Text scoreText;
    public TMP_Text timeText;

    //Time
    public float remainingTime;
    public float displayTime;

    //Bools
    public bool isAfterTrigger;
    public TriggerController _triggerController;
    ScoreController _scoreController;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient) Destroy(GetComponent<HalloweenMap>());
        view = GetComponent<PhotonView>();
        _scoreController = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        view.RPC("Splash", RpcTarget.AllBuffered);

        if (remainingTime > 0) CheckTimer();
        else
        {
            view.RPC("GameEnd", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void Splash()
    {
        if (_triggerController.isTriggered && !isAfterTrigger)
        {
            PhotonNetwork.Instantiate(Path.Combine("Particles", splashPrefab.name), new Vector3(0, 3.3f, 0), Quaternion.Euler(-90, 0, 0));
            score++;
            scoreText.text = score.ToString();
            _scoreController._gameState.ResetBall(_scoreController._gameState.num);
            isAfterTrigger = true;
        }
        else
        {
            isAfterTrigger = false;
        }
    }

    void CheckTimer()
    {
        remainingTime -= Time.deltaTime;

        displayTime = Mathf.FloorToInt(remainingTime % 60);
        timeText.text = displayTime.ToString();
    }

    [PunRPC]
    void GameEnd()
    {
        _scoreController.team.color = new Color32(0, 255, 19, 255);
        _scoreController.team.text = scoreText.text;
        _scoreController._gameState.RoundWin();
        _scoreController._gameState.GameWin();
    }
}
