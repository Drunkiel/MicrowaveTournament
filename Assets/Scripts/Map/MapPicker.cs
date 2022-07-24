using System.Linq;
using UnityEngine;

public class MapPicker : MonoBehaviour
{
    public GameObject[] allMaps;
    public bool[] pickedMaps;
    public int[] restOfTheMaps;

    public Transform placeToSpawn;

    void Start()
    {
        pickedMaps = new bool[allMaps.Length];
        Fill();
    }

    public void PickMap()
    {
        DestroyActualMap();

        //Check if have been any maps left
        if (restOfTheMaps.Length == 0)
        {
            Fill();
        }

        int ranNum = (int)Mathf.Round(Random.Range(0, restOfTheMaps.Length));

        Instantiate(allMaps[restOfTheMaps[ranNum]], new Vector3(0, -6, 0), Quaternion.Euler(0, 0, 0), placeToSpawn);

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
        GameObject map = GameObject.FindGameObjectWithTag("Wall");
        Destroy(map.transform.parent.gameObject);
    }
}
