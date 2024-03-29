using UnityEngine;

[System.Serializable]
public class Texts
{
    public string[] blueTeamTexts;
    public string[] redTeamTexts;

    public string PickText(bool team)
    {
        if (team)
        {
            string redTeamText = redTeamTexts[Random.Range(0, redTeamTexts.Length)];
            return redTeamText;
        }
        else
        {
            string blueTeamText = blueTeamTexts[Random.Range(0, blueTeamTexts.Length)];
            return blueTeamText;
        }
    }
}
