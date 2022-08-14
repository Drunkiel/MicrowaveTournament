using UnityEngine;

public class InfoUI : MonoBehaviour
{
    public GameObject infoUI;
    public bool isOpen;

    public void InfoButton()
    {
        if (!isOpen)
        {
            OpenCloseUI(infoUI, true);
            isOpen = true;
        }
        else
        {
            OpenCloseUI(infoUI, false);
            isOpen = false;
        }
    }

    void OpenCloseUI(GameObject UI, bool OpenClose)
    {
        UI.SetActive(OpenClose);
    }
}
