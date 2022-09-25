using UnityEngine;
using UnityEngine.UI;

public class EffectVolumeController : MonoBehaviour
{
    public Slider effectSlider;

    void Start()
    {
        effectSlider = GameObject.FindGameObjectWithTag("EffectSlider").GetComponent<Slider>();
        SetEffectSource();
    }

    public void SetEffectSource()
    {
        GameObject[] effectSounds = GameObject.FindGameObjectsWithTag("EffectSoundSorce");

        foreach (GameObject effect in effectSounds)
        {
            effect.GetComponent<AudioSource>().volume = effectSlider.value;
        }
    }
}
