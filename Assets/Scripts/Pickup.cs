using Unity.Netcode;
using UnityEngine;

public abstract class Pickup : NetworkBehaviour
{
    protected abstract void OnTriggerEnter2D(Collider2D other);
}
