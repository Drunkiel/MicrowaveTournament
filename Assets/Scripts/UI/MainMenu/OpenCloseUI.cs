using UnityEngine;

public class OpenCloseUI : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject[] UIToClose;
    public bool isOpen;

    public void Button()
    {
        if (!isOpen)
        {
            OpenClose(mainUI, true);
            isOpen = true;
        }
        else
        {
            OpenClose(mainUI, false);
            isOpen = false;
        }
    }

    public void CloseRestUI()
    {
        foreach (GameObject UI in UIToClose)
        {
            OpenCloseUI UIObject = UI.GetComponent<OpenCloseUI>();
            UIObject.isOpen = false;
            UIObject.OpenClose(UIObject.mainUI, false);
        }
    }

    void OpenClose(GameObject UI, bool OpenClose)
    {
        UI.SetActive(OpenClose);
    }
}
