using UnityEngine;

public class MapPicker : MonoBehaviour
{
    public GameObject[] allMaps;
    public bool[] pickedMap;

    public Transform placeToSpawn;

    void Start()
    {
        pickedMap = new bool[allMaps.Length];
    }

    public void PickMap()
    {
        DestroyActualMap();

        int ranNum = (int)Mathf.Round(Random.Range(0, allMaps.Length));
        Instantiate(allMaps[ranNum], new Vector3(0, -6, 0), Quaternion.Euler(0, 0, 0), placeToSpawn);
    }

    void DestroyActualMap()
    {
        GameObject map = GameObject.FindGameObjectWithTag("Wall");
        Destroy(map.transform.parent.gameObject);
    }
}
