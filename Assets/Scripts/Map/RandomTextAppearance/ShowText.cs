using UnityEngine;
using TMPro;

public class ShowText : MonoBehaviour
{
    public bool isRedTeam;
    private TMP_Text showingText;

    public Texts texts;

    void Start()
    {
        showingText = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        DisplayRandomText();
    }

    public void DisplayRandomText()
    {
        isRedTeam = transform.parent.GetComponent<RandomAppearanceController>().isBlueTeam;

        string textToShow = texts.PickText(isRedTeam);

        if (isRedTeam) showingText.color = new Color32(144, 15, 16, 255);
        else showingText.color = new Color32(15, 45, 144, 255);

        showingText.text = textToShow;
    }
}
