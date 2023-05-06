using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTray : MonoBehaviour
{
    [SerializeField] private int _updateTrayRatePerSecond;
    [SerializeField] private float _removePastTrayDelay;
    [SerializeField] private GameObject _toSpawnPastPosition;
    
    [SerializeField] private int _goSecondsInPast;
    
    private List<GameObject> _pastPlayerData;
    private Vector3 _lastPostion;

    private PlayerHealth playerHealth;
    private GameObject _parentOfSpawns;

    void Start()
    {
        Initialize();
        StartCoroutine(nameof(SavePlayerTray));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GoBackInTime(_goSecondsInPast);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            BurnPastTray();
        }
    }
    private void Initialize()
    {
        _parentOfSpawns = Instantiate(new GameObject(), transform.parent);
        _pastPlayerData = new List<GameObject>();
        _lastPostion = transform.localPosition;
        playerHealth = gameObject.GetComponent<PlayerHealth>();
    }

    private void OnDestroy()
    {
        _pastPlayerData = null;
        StopAllCoroutines();
    }

    public void GoBackInTime(int secondsInPast)
    {
        try
        {
            int pastPositionIndex = _pastPlayerData.Count - (secondsInPast * _updateTrayRatePerSecond);

            for (int i = pastPositionIndex; i < _pastPlayerData.Count; i++)
                Destroy(_pastPlayerData[i].gameObject);

            transform.position = _pastPlayerData[pastPositionIndex].transform.position;

            playerHealth.SetCurrentHealth(_pastPlayerData[pastPositionIndex].transform.gameObject.GetComponent<PlayerHealth>().GetCurrentHealth());

            _pastPlayerData.RemoveRange(pastPositionIndex, secondsInPast * _updateTrayRatePerSecond);
        }catch(ArgumentOutOfRangeException e)
        {
            if(_pastPlayerData.Count>0)
                transform.position = _pastPlayerData[0].transform.position;
            ClearSpawnedStuf();
        }
    }

    public void BurnPastTray()
    {
        for (int i = 0; i < _parentOfSpawns.transform.childCount; i++)
        {
            GameObject tray = _parentOfSpawns.transform.GetChild(i).gameObject;
            if(tray.GetComponent<ParticleSystem>() == null)
            {
                ParticleSystem particle = tray.GetComponentInChildren<ParticleSystem>();

                if (particle != null)
                    particle.Play();

                CircleCollider2D collider = tray.GetComponent<CircleCollider2D>();
                if (collider)
                {
                    collider.enabled = true;
                }
            }
               
            
        }
    }
    private void ClearSpawnedStuf()
    {
        _pastPlayerData.Clear();
        for(int i = 0; i < _parentOfSpawns.transform.childCount; i++)
        {
            Destroy(_parentOfSpawns.transform.GetChild(i).gameObject);
        }
    }

    private IEnumerator SavePlayerTray()
    {
        StorePlayerPosition();
        yield return new WaitForSeconds(1f/Convert.ToSingle(_updateTrayRatePerSecond));
        StartCoroutine(nameof(SavePlayerTray));
    }

    
    private void StorePlayerPosition()
    {
        Vector3 currentPos = transform.position;

        if (_lastPostion != currentPos)
        {
            var go = SpawnEmptyGO(currentPos);

            _pastPlayerData.Add(go);

            StartCoroutine(DestroyOldTray(go));

            if (_pastPlayerData.Count > 1)
            {
                SetLineRendererPositions(_pastPlayerData[_pastPlayerData.Count - 2], _pastPlayerData[_pastPlayerData.Count-1]);
            }
        }
        _lastPostion = currentPos;

    }

    private IEnumerator DestroyOldTray(GameObject gameObject)
    {
        yield return new WaitForSeconds(_removePastTrayDelay);
        _pastPlayerData.Remove(gameObject);
        Destroy(gameObject);
    }
    
    
    private GameObject SpawnEmptyGO(Vector3 pos)
    {
        GameObject go =  Instantiate(_toSpawnPastPosition,_parentOfSpawns.transform);
        SavePlayerHealthInPast(go);

        go.transform.position = pos;
        go.SetActive(true);
        return go;
        
    }
    private void SavePlayerHealthInPast(GameObject go)
    {
        PlayerHealth playerHealthSave = go.GetComponent<PlayerHealth>();
        playerHealthSave.SetCurrentHealth(playerHealth.GetCurrentHealth());
    }
    private void SetLineRendererPositions(GameObject first, GameObject second)
    {
        if(first && second)
        {
            LineRenderer lineRenderer = first.GetComponent<LineRenderer>();
            if (lineRenderer == null) return;
            lineRenderer.SetPosition(0, first.transform.position);
            lineRenderer.SetPosition(1, second.transform.position);
        }
       
    }
}
