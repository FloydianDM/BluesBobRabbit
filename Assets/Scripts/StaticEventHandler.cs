using System;
using UnityEngine;

public static class StaticEventHandler
{
    public static event Action<PickupPickedEventArgs> OnPickupPicked;

    public static void CallPickupPickedEvent(PickupType pickupType)
    {
        OnPickupPicked?.Invoke(new PickupPickedEventArgs
            {
                PickupType = pickupType
            });
    }
}

public class PickupPickedEventArgs : EventArgs
{
    public PickupType PickupType;
}
