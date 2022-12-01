using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public string stateTextToShow;
    public string detailsTextToShow;

    public Discord.Discord discord;
    private float timeInGame;

    // Use this for initialization
    void Start()
    {
        discord = new Discord.Discord(1011193121507377162, (System.UInt64)CreateFlags.Default);
        var activityManager = discord.GetActivityManager();
        var activity = new Activity
        {
            State = stateTextToShow + ": " + Application.version,
            Details = detailsTextToShow,
            Assets =
            {
                LargeImage = "icon",
                LargeText = "Microwave Tournament"
            },
            Timestamps =
            {
                Start = System.DateTimeOffset.Now.ToUnixTimeMilliseconds()
            }
        };

        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Result.Ok)
            {
                Debug.Log("Everything is fine!");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }

    void OnApplicationQuit()
    {
        discord.Dispose();
    }
}