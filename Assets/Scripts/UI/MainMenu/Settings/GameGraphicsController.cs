using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameGraphicsController : MonoBehaviour
{
    public bool fullScreen;
    public bool vSync;
    public int resolutionValue;

    public Toggle fullScreenToggle;
    public Toggle vSyncToggle;
    public TMP_Dropdown resolutionDropdown;

    public void UpdateGraphics()
    {
        fullScreenToggle.isOn = fullScreen;
        vSyncToggle.isOn = vSync;
        resolutionDropdown.value = resolutionValue;
    }

    public void ChangeFullscreen()
    {
        fullScreen = !fullScreen;
        ChangeResolution(resolutionDropdown.value);
    }

    public void ChangeVsync()
    {
        vSync = !vSync;

        if (vSync)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    public void ChangeResolution(int value)
    {
        switch (value)
        {
            case 0:
                Screen.SetResolution(7680, 4320, fullScreen);
                break;

            case 1:
                Screen.SetResolution(3840, 2160, fullScreen);
                break;

            case 2:
                Screen.SetResolution(2560, 1440, fullScreen);
                break;

            case 3:
                Screen.SetResolution(1920, 1080, fullScreen);
                break;

            case 4:
                Screen.SetResolution(1280, 720, fullScreen);
                break;
        }
    }
}
