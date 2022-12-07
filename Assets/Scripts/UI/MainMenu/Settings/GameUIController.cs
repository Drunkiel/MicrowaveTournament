using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Toggle menuInfoToggle;
#nullable enable
    public GameObject? menuInfoObject;
#nullable disable

    public void ChangeVisibility()
    {
        if (menuInfoObject != null)
        {
            if (menuInfoToggle.isOn)
            {
                menuInfoObject.SetActive(true);
            }
            else
            {
                menuInfoObject.SetActive(false);
            }
        }
    }
}
