using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastRewind : MonoBehaviour
{
    [SerializeField] private float _velocity = 10f;
    [SerializeField] private GameObject _rewindBullet;
    [SerializeField] private float _coolDown = 5f;
    [SerializeField] private float _selfDestroy = 15f;
    [SerializeField] private Image _cdImage;
   
    private Player player;
    private Vector3 lastPos;
    private bool _canShoot = true;
    private void Start()
    {
        lastPos = Vector3.zero;
        player = gameObject.GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireRewind();
        }
        if (_canShoot == false)
        {
            _cdImage.fillAmount += 1.0f / _coolDown * Time.deltaTime;
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private IEnumerator WaitCooldownDMG()
    {
        _canShoot = false;
        _cdImage.fillAmount = 0f;
        yield return new WaitForSeconds(_coolDown);
        _canShoot = true;
    }
    private void FireRewind()
    {
       
        if (_canShoot == false) return;
        if (player.inputDirection == Vector3.zero)
        {
            return;
        }
        GameObject rewindBullet = Instantiate(_rewindBullet, transform.parent);
        rewindBullet.transform.position = player.transform.position;

        RewindBulletCollision rewindBulletCollision = rewindBullet.gameObject.GetComponent<RewindBulletCollision>();
        rewindBulletCollision.currentPlayer = player.gameObject;
       // rewindBulletCollision.secondsToRewind

        Rigidbody2D rigidbody = rewindBullet.GetComponent<Rigidbody2D>();
        rewindBullet.SetActive(true);
        //if(player.inputDirection == Vector3.zero)
        //{
        //    if(lastPos!= Vector3.zero)
        //        rigidbody.velocity = lastPos * _velocity;
        //}
        if (player.inputDirection != Vector3.zero)
        {
            rigidbody.velocity = player.inputDirection * _velocity;
            lastPos = player.inputDirection;
        }

        StartCoroutine(DestroyBullet(rewindBullet));

        StartCoroutine(nameof(WaitCooldownDMG));
    }
    private IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(_selfDestroy);
        if (bullet)
            Destroy(bullet);
    }

}
