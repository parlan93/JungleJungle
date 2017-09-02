using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    private List<Level> levels;

	// Use this for initialization
	void Start () {
        InitLevels();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitLevels()
    {
        /*levels.Add(new Level(1, "Level 1", Level.LevelType.BANANA, 50, 100, 150));
        levels.Add(new Level(2, "Level 2", Level.LevelType.TIME, 30, 60, 100));
        levels.Add(new Level(3, "Level 3", Level.LevelType.DISTANCE, 180, 240, 300));
        levels.Add(new Level(4, "Level 4", Level.LevelType.JUMP, 25, 35, 50));
        levels.Add(new Level(5, "Level 5", Level.LevelType.BANANA, 150, 225, 300));
        levels.Add(new Level(6, "Level 6", Level.LevelType.POWER_UP, 2, 4, 6));
        levels.Add(new Level(7, "Level 7", Level.LevelType.ROLL, 25, 35, 45));
        levels.Add(new Level(8, "Level 8", Level.LevelType.POINTS, 12000, 18000, 25000));*/
    }
}
