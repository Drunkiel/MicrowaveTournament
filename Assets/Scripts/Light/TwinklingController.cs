using System.Collections;
using UnityEngine;

public class TwinklingController : MonoBehaviour
{
    private Color32 starterColor;
    public Color32 blinkColor;

    public float speedOfTwinkling;
    public float initialSpeedOfTwinkling;
    public bool isTwinkling;

    Renderer objectRenderer;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        initialSpeedOfTwinkling = speedOfTwinkling;
        starterColor = objectRenderer.material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTwinkling)
        {
            StartCoroutine(Twinkling());
        }
    }

    private IEnumerator Twinkling()
    {
        isTwinkling = true;
        objectRenderer.material.SetColor("_Color", blinkColor);
        yield return new WaitForSeconds(speedOfTwinkling);
        objectRenderer.material.SetColor("_Color", starterColor);
        yield return new WaitForSeconds(speedOfTwinkling);
        isTwinkling = false;
    }
}
