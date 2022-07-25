using UnityEngine;

public class GameWinMenuController : MonoBehaviour
{
    public GameObject UI;

    public void OpenMenu()
    {
        UI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        UI.SetActive(false);
        Time.timeScale = 1f;
    }
}
