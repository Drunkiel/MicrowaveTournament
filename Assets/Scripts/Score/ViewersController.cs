using UnityEngine;

[System.Serializable]
public class ViewersController
{
    public GameObject[] blueLayer1;
    public GameObject[] blueLayer2;
    public GameObject[] blueLayer3;

    public GameObject[] redLayer1;
    public GameObject[] redLayer2;
    public GameObject[] redLayer3;

    public ScoreController _scoreController;

    public void SpawnViewers()
    {
        switch (_scoreController.playerOneWinnedMaps)
        {
            case 1:
                for (int i = 0; i < blueLayer1.Length; i++)
                {
                    blueLayer1[i].SetActive(true);
                }
                break;

            case 2:
                for (int i = 0; i < blueLayer2.Length; i++)
                {
                    blueLayer2[i].SetActive(true);
                }
                break;

            case 3:
                for (int i = 0; i < blueLayer3.Length; i++)
                {
                    blueLayer3[i].SetActive(true);
                }
                break;
        }

        switch (_scoreController.playerTwoWinnedMaps)
        {
            case 1:
                for (int i = 0; i < redLayer1.Length; i++)
                {
                    redLayer1[i].SetActive(true);
                }
                break;

            case 2:
                for (int i = 0; i < redLayer2.Length; i++)
                {
                    redLayer2[i].SetActive(true);
                }
                break;

            case 3:
                for (int i = 0; i < redLayer3.Length; i++)
                {
                    redLayer3[i].SetActive(true);
                }
                break;
        }
    }

    public void DespawnViewers()
    {
        for (int i = 0; i < blueLayer1.Length; i++)
        {
            blueLayer1[i].SetActive(false);
            blueLayer2[i].SetActive(false);
            
            redLayer1[i].SetActive(false);
            redLayer2[i].SetActive(false);
        }

        for (int i = 0; i < blueLayer3.Length; i++)
        {
            blueLayer3[i].SetActive(false);
            redLayer3[i].SetActive(false);
        }
    }
}
