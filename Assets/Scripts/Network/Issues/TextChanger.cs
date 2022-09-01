using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour
{
    public TMP_Text myText;
    private string parentText;

    // Start is called before the first frame update
    void Start()
    {
        ChangeText();
    }

    void ChangeText()
    {
        parentText = transform.parent.GetComponent<IssuesController>().text;

        myText.text = parentText;
    }
}
