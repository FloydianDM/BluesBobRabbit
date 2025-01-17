using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class SpeedHandler : NetworkBehaviour
{
    [SerializeField] private float _speedTimer = 10f;
    
    private Coroutine _addSpeedCoroutine;
    public bool IsAllowSpeedIncrease { get; private set; } = false;
    
    public void AddSpeed()
    {
        if (!IsOwner)
        {
            return;
        }

        if (_addSpeedCoroutine != null)
        {
            return;
        }
        
        _addSpeedCoroutine = StartCoroutine(AddSpeedRoutine());
    }

    private IEnumerator AddSpeedRoutine()
    {
        IsAllowSpeedIncrease = true;
        
        yield return new WaitForSecondsRealtime(_speedTimer);
        
        IsAllowSpeedIncrease = false;
        _addSpeedCoroutine = null;
    }
}
