using UnityEngine;
using TMPro;

public class GameVersion : MonoBehaviour
{
    public string version;
    public TMP_Text gameVersionText;

    // Start is called before the first frame update
    void Awake()
    {
        CheckVersion();
        gameVersionText.text = "Alpha: " + version;
    }

    void CheckVersion()
    {
        version = Application.version;
    }
}
