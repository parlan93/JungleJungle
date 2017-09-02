using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp {

    public enum PowerUpType
    {
        SHIELD,
        DOUBLE_BANANAS,
        DOUBLE_TASK
    }

    public int id { get; set; }
    public PowerUpType powerUpType { get; set; }
    public int powerUpLevel { get; set; }
    public int powerUpTime { get; set; }

    public PowerUp(int id, PowerUpType powerUpType)
    {
        this.id = id;
        this.powerUpType = powerUpType;
        this.powerUpLevel = 1;
        this.powerUpTime = this.powerUpLevel * 3 + 1;
    }
    
}
