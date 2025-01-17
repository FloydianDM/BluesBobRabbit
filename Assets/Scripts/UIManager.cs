using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _joinMenuUI;
    [SerializeField] private GameObject _scoreUI;

    public static UIManager Instance;
    public ScoreUI ScoreUI { private set; get; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        ScoreUI = _scoreUI.GetComponent<ScoreUI>();
        _joinMenuUI.SetActive(true);
        _scoreUI.SetActive(false);
    }

    public void DisableJoinMenu()
    {
        _joinMenuUI.SetActive(false);
        _scoreUI.SetActive(true);
    }
}
