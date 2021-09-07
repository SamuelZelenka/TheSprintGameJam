using System.Net;
using System.Collections;
using System.Collections.Specialized;
using UnityEngine;

public class DiscordMessageSender : MonoBehaviour
{
    public static void SendWebHook(string url, string message, string username)
    {
        Http.Post(url, new NameValueCollection()
        {
            {
                "username",
                username
            },
            {
                "content",
                message
            }
        }
        );
    }
    public static void UploadMessage(string highscore, string name)
    {
        SendWebHook("https://discordapp.com/api/webhooks/884806111025307699/0aPVEHWolg0neE4wLwPynm4CDxUoJq5gEFWRoavVJW34HIkLKjfg02aWs-BDIHOLOj8H", highscore, name);
    }
}

public class Http
{
    public static byte[] Post(string url, NameValueCollection pairs)
    {
        using (WebClient webClient = new WebClient())
        {
           return webClient.UploadValues(url, pairs);
        }
    }
}



