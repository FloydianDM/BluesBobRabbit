using Unity.Netcode;
using UnityEngine;

public class SpeedPickup : Pickup
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StaticEventHandler.CallPickupPickedEvent(PickupType.SpeedPickup);

            if (IsClient)
            {
                DestroyServerRpc();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    
    [Rpc(SendTo.Server)]
    private void DestroyServerRpc()
    {
        Destroy(gameObject);
    }
}
