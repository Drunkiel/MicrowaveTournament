using UnityEngine;

public class IssuesController : MonoBehaviour
{
    public GameObject prefab;
    public string text;

    public void ShowIssue(string textToShow)
    {
        Instantiate(prefab, transform);
        text = textToShow;
    }
}
