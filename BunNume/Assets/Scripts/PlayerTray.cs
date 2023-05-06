using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTray : MonoBehaviour
{
    [SerializeField] private float _updateRateInSeconds = 0.5f;

    private List<Vector3> _pastPlayerPositions;
    private Vector3 lastPostion;


    void Start()
    {
        Initialize();
        StartCoroutine(nameof(SavePlayerTray));
    }

    private void Initialize()
    {
        _pastPlayerPositions = new List<Vector3>();
        lastPostion = transform.position;
    }

    private void OnDestroy()
    {
        _pastPlayerPositions = null;
        StopAllCoroutines();
    }

    private IEnumerable SavePlayerTray()
    {
        StorePlayerPosition();

        yield return new WaitForSeconds(_updateRateInSeconds);
        StartCoroutine(nameof(SavePlayerTray));
    }

    
    private void StorePlayerPosition()
    {
        if(lastPostion != transform.position)
        {
            _pastPlayerPositions.Add(transform.position);
        }
        lastPostion = transform.position;

    }
    
}
