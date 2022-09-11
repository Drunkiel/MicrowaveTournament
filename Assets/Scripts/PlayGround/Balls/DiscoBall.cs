using UnityEngine;

public class DiscoBall : MonoBehaviour
{
    public float speedOfColorChange;
    public int maxNumberToReach; //Between 0 and 255
    public Light lightEmitter;
    public TrailRenderer trailColor;

    public byte intensityRed;
    public byte intensityGreen;
    public byte intensityBlue;

    private int nextStep;

    void Start()
    {
        if (maxNumberToReach < 0 || maxNumberToReach > 255) Debug.LogError("The maximum or minimum possible number was exceeded");
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        switch (nextStep)
        {
            case 0: //Yellow
                if (intensityGreen != maxNumberToReach) intensityGreen += 1;
                else nextStep++;

                break;

            case 1: //Green
                if (intensityRed != 0) intensityRed -= 1;
                else nextStep++;

                break;

            case 2: //LightBlue
                if (intensityBlue != maxNumberToReach) intensityBlue += 1;
                else nextStep++;

                break;

            case 3: //Blue
                if (intensityGreen != 0) intensityGreen -= 1;
                else nextStep++;

                break;

            case 4: //Pink
                if (intensityRed != maxNumberToReach) intensityRed += 1;
                else nextStep++;

                break;

            case 5: //Red
                if (intensityBlue != 0) intensityBlue -= 1;
                else nextStep = 0;

                break;
        }

        Color32 newColor = new Color32(intensityRed, intensityGreen, intensityBlue, 255);
        lightEmitter.color = newColor;
        trailColor.endColor = newColor;
    }
}
