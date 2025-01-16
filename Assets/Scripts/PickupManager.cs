using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PickupManager : NetworkBehaviour
{
    [Space(5)] [Header ("REFERENCES")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _pickupPrefabs;
    [SerializeField] private GameObject _pickupPocket;
    
    [Space(5)] [Header ("SETTINGS")]
    [SerializeField] private float _pickupSpawnTimer = 5f;
    [SerializeField] private int _maxPickupCountInScene = 3;
    
    private int _currentPickupCount = 0;
    
    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            StaticEventHandler.OnPickupPicked += StaticEventHandler_OnPickupPicked;
            
            StartCoroutine(SpawnPickupRoutine());
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsHost)
        {
            StaticEventHandler.OnPickupPicked -= StaticEventHandler_OnPickupPicked;
            
            StopAllCoroutines();
        }
    }
    
    private void StaticEventHandler_OnPickupPicked(PickupPickedEventArgs obj)
    {
        Debug.Log("PICKED");
        
        _currentPickupCount--;
    }

    private IEnumerator SpawnPickupRoutine()
    {
        if (_pickupPrefabs.Length == 0)
        {
            yield return null;
        }

        while (true)
        {
            if (_currentPickupCount < _maxPickupCountInScene)
            {
                yield return new WaitForSecondsRealtime(_pickupSpawnTimer);
            
                // get random spawn point
                int randomSpawnPointIndex = Random.Range(0, _spawnPoints.Length);
                Transform spawnPoint = _spawnPoints[randomSpawnPointIndex];
            
                // get random pickup
                int randomPickupIndex = Random.Range(0, _pickupPrefabs.Length);
                GameObject pickupPrefab = _pickupPrefabs[randomPickupIndex];
                
                // spawn the pickup
                GameObject pickup = Instantiate(pickupPrefab, spawnPoint.position, spawnPoint.rotation, _pickupPocket.transform);
                NetworkObject pickupNetworkObject = pickup.GetComponent<NetworkObject>();
            
                pickupNetworkObject.Spawn();
            
                _currentPickupCount++;
            }
            else
            {
                yield return null;
            }
        }
        
    }
}
