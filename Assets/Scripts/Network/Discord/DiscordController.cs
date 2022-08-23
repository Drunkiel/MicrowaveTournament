using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;
    private float timeInGame;

    // Use this for initialization
    void Start()
    {
        discord = new Discord.Discord(1011193121507377162, (System.UInt64)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = "Playing",
            Details = "",
            Assets =
            {
                LargeImage = "icon",
                LargeText = "MicrowaveTournament"
            },
            Secrets =
            {
                Spectate = "text"
            },
            Timestamps =
            {
                Start = System.DateTimeOffset.Now.ToUnixTimeMilliseconds()
            }
        };

        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
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