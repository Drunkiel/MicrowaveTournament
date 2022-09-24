using UnityEngine;
using TMPro;

public class GameGraphicsController : MonoBehaviour
{
    public bool fullscreen;
    public bool vsync;

    public TMP_Dropdown resolutionDropdown;

    public void ChangeFullscreen()
    {
        fullscreen = !fullscreen;
        ChangeResolution(resolutionDropdown.value);
    }

    public void ChangeVsync()
    {
        vsync = !vsync;

        if (vsync)
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
                Screen.SetResolution(7680, 4320, fullscreen);
                break;

            case 1:
                Screen.SetResolution(3840, 2160, fullscreen);
                break;

            case 2:
                Screen.SetResolution(2560, 1440, fullscreen);
                break;

            case 3:
                Screen.SetResolution(1920, 1080, fullscreen);
                break;

            case 4:
                Screen.SetResolution(1280, 720, fullscreen);
                break;
        }
    }
}
