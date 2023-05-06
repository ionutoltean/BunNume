using UnityEngine;

public class RewindBulletCollision : MonoBehaviour
{
    [HideInInspector]public GameObject currentPlayer;
    [HideInInspector] public int secondsToRewind;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ReferenceEquals(currentPlayer, collision.gameObject)) return;
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            GameplayManager.SendPlayerBackInTime(player.gameObject,secondsToRewind);
            Destroy(gameObject);
        }
    }
}
