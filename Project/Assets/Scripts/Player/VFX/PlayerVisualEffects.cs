using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualEffects
{
    public PlayerDashAfterImage dashAfterImage { get; private set; }
    private PlayerController player;

    public PlayerVisualEffects(PlayerController player)
    {
        this.player = player;
        dashAfterImage = new PlayerDashAfterImage(player);
    }
}
