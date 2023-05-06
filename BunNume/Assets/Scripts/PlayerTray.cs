using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTray : MonoBehaviour
{
    [SerializeField] private float _updateRateInSeconds = 0.5f;
    [SerializeField] private int _positionsBufferLength = 10;
    [SerializeField] private GameObject _toSpawnPastPosition;
    [SerializeField] private GameObject _parentOfSpawns;
    private List<GameObject> _pastPlayerData;
    private Vector3 _lastPostion;


    void Start()
    {
        Debug.Log("in start");
        Initialize();
        StartCoroutine(nameof(SavePlayerTray));
    }

    private void Initialize()
    {
        _pastPlayerData = new List<GameObject>();
        _lastPostion = transform.localPosition;
    }

    private void OnDestroy()
    {
        _pastPlayerData = null;
        StopAllCoroutines();
    }

    private IEnumerator SavePlayerTray()
    {
        StorePlayerPosition();

        yield return new WaitForSeconds(_updateRateInSeconds);
        StartCoroutine(nameof(SavePlayerTray));
    }

    
    private void StorePlayerPosition()
    {
        Vector3 currentPos = transform.position;

        if (_lastPostion != currentPos)
        {
            _pastPlayerData.Add(SpawnEmptyGO(currentPos));
            if (_pastPlayerData.Count > 1)
            {
                Debug.Log("Adding line renderer");
                SetLineRendererPositions(_pastPlayerData[_pastPlayerData.Count - 2], _pastPlayerData[_pastPlayerData.Count - 1]);
            }
        }
        
       
        
        _lastPostion = currentPos;

    }
    
    private GameObject SpawnEmptyGO(Vector3 pos)
    {
        Debug.Log("Spawning empty GO in:"+pos);
        GameObject go =  Instantiate(_toSpawnPastPosition,_parentOfSpawns.transform);
        go.transform.position = pos;
        Debug.Log("GO pos:" + go.transform);
        return go;
        
    }
    private void SetLineRendererPositions(GameObject first, GameObject second)
    {
        LineRenderer lineRenderer = first.GetComponent<LineRenderer>();
        
        lineRenderer.SetPosition(0, first.transform.position);
        lineRenderer.SetPosition(1, second.transform.position);
    }
}
