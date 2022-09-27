using UnityEngine;
using TMPro;

public class ShowText : MonoBehaviour
{
    public bool isBlueTeam;
    private TMP_Text showingText;

    public Texts texts;

    void Start()
    {
        showingText = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        DisplayRandomText();
    }

    public void DisplayRandomText()
    {
        isBlueTeam = transform.parent.GetComponent<RandomAppearanceController>().isBlueTeam;

        string textToShow = texts.PickText(isBlueTeam);
        showingText.text = textToShow;
    }
}
