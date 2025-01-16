using System;
using UnityEngine;

public static class StaticEventHandler
{
    public static event Action<PickupPickedEventArgs> OnPickupPicked;
    public static event Action<PlayerDeathEventArgs> OnPlayerDeath;

    public static void CallPickupPickedEvent(PickupType pickupType)
    {
        OnPickupPicked?.Invoke(new PickupPickedEventArgs
            {
                PickupType = pickupType
            });
    }

    public static void CallPlayerDeathEvent(ulong playerId)
    {
        OnPlayerDeath?.Invoke(new PlayerDeathEventArgs
        {
            PlayerId = playerId
        });
    }
}

public class PickupPickedEventArgs : EventArgs
{
    public PickupType PickupType;
}

public class PlayerDeathEventArgs : EventArgs
{
    public ulong PlayerId;
}
