using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {

    public enum LevelType
    {
        BANANA,
        TIME,
        DISTANCE,
        POWER_UP,
        JUMP,
        ROLL,
        POINTS
    }

    public int id { get; set; }
    public string name { get; set; }
    public LevelType levelType { get; set; }
    public int firstStar { get; set; }
    public int secondStar { get; set; }
    public int thirdStar { get; set; }
    public int highScore { get; set; }
    public int stars { get; set; }
    public string highScoreKey { get; set; }

    public Level(int id, string name, LevelType levelType, int firstStar, int secondStar, int thirdStar, string highscoreKey) {
        this.id = id;
        this.name = name;
        this.levelType = levelType;
        this.firstStar = firstStar;
        this.secondStar = secondStar;
        this.thirdStar = thirdStar;
        this.highScoreKey = highscoreKey;
        this.highScore = 0;
        this.stars = 0;
    }
}
