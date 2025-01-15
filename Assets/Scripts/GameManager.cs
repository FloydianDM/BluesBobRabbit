using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Space(5)] [Header ("REFERENCES")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _powerPrefab;
    
    [Space(5)] [Header ("SETTINGS")]
    [SerializeField] private float _pickupSpawnTimer = 5f;
}
