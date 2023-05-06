using UnityEngine;

public static class GameplayManager
{
    public static void SendPlayerBackInTime(GameObject player,int seconds)
    {
        PlayerTray playerTray = player.GetComponent<PlayerTray>();
        if(playerTray is null)
        {
            Debug.LogError("Player target is null");
            return;
        }
        playerTray.GoBackInTime(seconds);
    }
}
