using UnityEngine;

[System.Serializable]
public class ViewersController
{
    public GameObject[] blueViewers;
    public GameObject[] redViewers;

    public ScoreController _scoreController;

    public void SpawnViewers()
    {
        if (_scoreController.PlayerOneWinnedMaps <= blueViewers.Length)
        {
            for (int i = 0; i < _scoreController.PlayerOneWinnedMaps; i++)
            {
                blueViewers[i].SetActive(true);
            }
        }

        if(_scoreController.PlayerTwoWinnedMaps <= redViewers.Length)
        {
            for (int i = 0; i < _scoreController.PlayerTwoWinnedMaps; i++)
            {
                redViewers[i].SetActive(true);
            }
        }
    }

    public void DespawnViewers()
    {
        for (int i = 0; i < 3; i++)
        {
            blueViewers[i].SetActive(false);
            redViewers[i].SetActive(false);
        }
    }
}
