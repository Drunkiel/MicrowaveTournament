using System.Linq;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class MapPicker : MonoBehaviour
{
    public GameObject[] allMaps;
    public bool[] pickedMaps;
    public int[] restOfTheMaps;

    public Transform placeToSpawn;

    void Start()
    {
        if (!PhotonNetwork.IsMasterClient) Destroy(GetComponent<MapPicker>());

        pickedMaps = new bool[allMaps.Length];
        Fill();
    }

    public void PickMap()
    {
        //Reseting players
        ScoreController _scoreController = GameObject.FindGameObjectWithTag("CameraUI").GetComponentInChildren<ScoreController>();
        _scoreController._gameState.ResetPlayers();

        if (!PhotonNetwork.IsMasterClient) return;

        //Deleting map
        DestroyActualMap();

        //Check if have been any maps left
        if (restOfTheMaps.Length == 0)
        {
            Fill();
        }

        int ranNum = (int)Mathf.Round(Random.Range(0, restOfTheMaps.Length));

        PhotonNetwork.Instantiate(Path.Combine("Maps", allMaps[restOfTheMaps[ranNum]].name), new Vector3(0, -6, 0), Quaternion.Euler(0, 0, 0));

        restOfTheMaps = restOfTheMaps.Except(new int[] { restOfTheMaps[ranNum] }).ToArray();
        ReFillMaps();
    }

    void ReFillMaps()
    {
        for (int i = 0; i < pickedMaps.Length; i++)
        {
            pickedMaps[i] = true;

            for (int j = 0; j < restOfTheMaps.Length; j++)
            {
                if (i == restOfTheMaps[j]) pickedMaps[i] = false;
            }
        }
    }

    void Fill()
    {
        restOfTheMaps = new int[allMaps.Length];

        for (int i = 0; i < restOfTheMaps.Length; i++)
        {
            restOfTheMaps[i] = i;
            pickedMaps[i] = false;
        }
    }

    void DestroyActualMap()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        GameObject[] maps = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject map in maps)
        {
            PhotonNetwork.Destroy(map.transform.parent.gameObject);
        }
    }
}
