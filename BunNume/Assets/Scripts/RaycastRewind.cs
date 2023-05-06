using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastRewind : MonoBehaviour
{
    [SerializeField] private float _velocity = 10f;
    [SerializeField] private GameObject _rewindBullet;
   
    private Player player;
    private void Start()
    {
        player = gameObject.GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireRewind();
        }
    }
    private void FireRewind()
    {
        GameObject rewindBullet = Instantiate(_rewindBullet, transform.parent);
        Rigidbody2D rigidbody = rewindBullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = player.GetLastInputDirection()* _velocity;
    }

}
