using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour {

    public GameObject[] LevelGO;

    private static List<Level> Levels = new List<Level>();
    
    public int mapId;
    public Text StarsAmount;
    Text starsAmount;

    int starsPlayer;
    int starsMax;

    // Use this for initialization
    void Start () {
        InitLevelsList();
        SetHighscoresAndStars();
        PrintLevels();

        starsAmount = StarsAmount.GetComponent<Text>();
        starsPlayer = 0;
        starsMax = 3 * Levels.Count;
        foreach (Level level in Levels)
        {
            starsPlayer += level.stars;
        }
        starsAmount.text = starsPlayer.ToString() + " / " + starsMax.ToString();

        int i = mapId * 8;
        foreach (GameObject GO in LevelGO)
        {
            GameObject GoUnlocked = GameObject.Find(("Unlocked-" + (i + 1)).ToString());
            GameObject GoLocked = GameObject.Find(("Locked-" + (i + 1)).ToString());
            GameObject GoFirstStarYellow = GameObject.Find(("YellowStarLeft-" + (i + 1)).ToString());
            GameObject GoSecondStarYellow = GameObject.Find(("YellowStarMiddle-" + (i + 1)).ToString());
            GameObject GoThirdStarYellow = GameObject.Find(("YellowStarRight-" + (i + 1)).ToString());
            GameObject GoFirstStarGrey = GameObject.Find(("GreyStarLeft-" + (i + 1)).ToString());
            GameObject GoSecondStarGrey = GameObject.Find(("GreyStarMiddle-" + (i + 1)).ToString());
            GameObject GoThirdStarGrey = GameObject.Find(("GreyStarRight-" + (i + 1)).ToString());

            // Czy ten poziom nie jest zaliczony
            if (Levels[i].stars == 0)
            {
                // Nie jest zaliczony
                GoFirstStarYellow.SetActive(false);
                GoFirstStarGrey.SetActive(true);
                GoSecondStarYellow.SetActive(false);
                GoSecondStarGrey.SetActive(true);
                GoThirdStarYellow.SetActive(false);
                GoThirdStarGrey.SetActive(true);

                // Czy ten poziom to 1 poziom
                if (i == 0)
                {
                    GoUnlocked.SetActive(true);
                    GoLocked.SetActive(false);
                }
                else
                {
                    // To nie jest pierwszy poziom

                    // Czy poprzedni NIE jest zaliczony
                    if (Levels[i - 1].stars == 0)
                    {
                        // Nie jest zaliczony, więc ten jest zablokowany
                        GoUnlocked.SetActive(false);
                        GoLocked.SetActive(true);
                    }
                    else
                    {
                        // Jest zaliczony, więc ten jest odblokowany
                        GoUnlocked.SetActive(true);
                        GoLocked.SetActive(false);
                    }
                }
            }
            else
            {
                // Ten poziom jest zaliczony
                GoUnlocked.SetActive(true);
                GoLocked.SetActive(false);

                // Czy ten poziom ma jedną gwiazdkę
                if (Levels[i].stars == 1)
                {
                    GoFirstStarYellow.SetActive(true);
                    GoFirstStarGrey.SetActive(false);
                    GoSecondStarYellow.SetActive(false);
                    GoSecondStarGrey.SetActive(true);
                    GoThirdStarYellow.SetActive(false);
                    GoThirdStarGrey.SetActive(true);
                }
                // Czy ten poziom ma dwie gwiazdki
                else if (Levels[i].stars == 2)
                {
                    // Ten poziom ma dwie gwiazdki
                    GoFirstStarYellow.SetActive(true);
                    GoFirstStarGrey.SetActive(false);
                    GoSecondStarYellow.SetActive(true);
                    GoSecondStarGrey.SetActive(false);
                    GoThirdStarYellow.SetActive(false);
                    GoThirdStarGrey.SetActive(true);
                }
                else
                {
                    // Ten poziom ma trzy gwiazdki
                    GoFirstStarYellow.SetActive(true);
                    GoFirstStarGrey.SetActive(false);
                    GoSecondStarYellow.SetActive(true);
                    GoSecondStarGrey.SetActive(false);
                    GoThirdStarYellow.SetActive(true);
                    GoThirdStarGrey.SetActive(false);
                }
            }
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Level ArrayList Init
    void InitLevelsList()
    {
        Levels.Clear();

        // Jungle
        Levels.Add(new Level(1, "Level 01", Level.LevelType.BANANA, 50, 100, 150, "HS-L01"));
        Levels.Add(new Level(2, "Level 02", Level.LevelType.TIME, 30, 60, 100, "HS-L02"));
        Levels.Add(new Level(3, "Level 03", Level.LevelType.DISTANCE, 180, 240, 300, "HS-L03"));
        Levels.Add(new Level(4, "Level 04", Level.LevelType.JUMP, 25, 35, 50, "HS-L04"));
        Levels.Add(new Level(5, "Level 05", Level.LevelType.BANANA, 150, 225, 300, "HS-L05"));
        Levels.Add(new Level(6, "Level 06", Level.LevelType.POWER_UP, 2, 4, 6, "HS-L06"));
        Levels.Add(new Level(7, "Level 07", Level.LevelType.ROLL, 20, 30, 40, "HS-L07"));
        Levels.Add(new Level(8, "Level 08", Level.LevelType.POINTS, 8000, 16000, 25000, "HS-L08"));

        // Village
        Levels.Add(new Level(9, "Level 09", Level.LevelType.DISTANCE, 300, 450, 600, "HS-L09"));
        Levels.Add(new Level(10, "Level 10", Level.LevelType.BANANA, 250, 450, 650, "HS-L10"));
        Levels.Add(new Level(11, "Level 11", Level.LevelType.JUMP, 40, 55, 70, "HS-L11"));
        Levels.Add(new Level(12, "Level 12", Level.LevelType.TIME, 90, 135, 180, "HS-L12"));
        Levels.Add(new Level(13, "Level 13", Level.LevelType.POINTS, 18000, 25000, 32000, "HS-L13"));
        Levels.Add(new Level(14, "Level 14", Level.LevelType.BANANA, 300, 500, 800, "HS-L14"));
        Levels.Add(new Level(15, "Level 15", Level.LevelType.ROLL, 35, 50, 65, "HS-L15"));
        Levels.Add(new Level(16, "Level 16", Level.LevelType.POWER_UP, 4, 6, 8, "HS-L16"));
    }

    // Set Highscores and stars
    void SetHighscoresAndStars()
    {
        foreach (Level level in Levels)
        {
            level.highScore = PlayerPrefs.GetInt(level.highScoreKey);
            if (level.highScore >= level.firstStar)
            {
                level.stars++;
                if (level.highScore >= level.secondStar)
                {
                    level.stars++;
                    if (level.highScore >= level.thirdStar) level.stars++;
                }
            }
        }
    }

    // Print Levels to Console
    void PrintLevels()
    {
        foreach (Level level in Levels)
        {
            Debug.Log(level.id + "\t" + level.name + "\t" + level.levelType + "\t" + level.firstStar + "\t" + level.secondStar + "\t" + level.thirdStar + "\t" + level.highScore + "\t" + level.stars + "\t" + level.highScoreKey);
        }
    }

    // Back To Main Menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToMapSelection()
    {
        SceneManager.LoadScene("MapSelection");
    }

    // Play Levels Methods

    // Jungle
    public void PlayLevelOne()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void PlayLevelTwo()
    {
        if (Levels[0].stars > 0) SceneManager.LoadScene("LevelTwo");
    }

    public void PlayLevelThree()
    {
        if (Levels[1].stars > 0) SceneManager.LoadScene("LevelThree");
    }

    public void PlayLevelFour()
    {
        if (Levels[2].stars > 0) SceneManager.LoadScene("LevelFour");
    }

    public void PlayLevelFive()
    {
        if (Levels[3].stars > 0) SceneManager.LoadScene("LevelFive");
    }

    public void PlayLevelSix()
    {
        if (Levels[4].stars > 0) SceneManager.LoadScene("LevelSix");
    }

    public void PlayLevelSeven()
    {
        if (Levels[5].stars > 0) SceneManager.LoadScene("LevelSeven");
    }

    public void PlayLevelEight()
    {
        if (Levels[6].stars > 0) SceneManager.LoadScene("LevelEight");
    }
    
    // Village
    public void PlayLevelNine()
    {
        if (Levels[6].stars > 0) SceneManager.LoadScene("LevelNine");
    }

    public void PlayLevelTen()
    {
        if (Levels[8].stars > 0) SceneManager.LoadScene("LevelTen");
    }

    public void PlayLevelEleven()
    {
        if (Levels[9].stars > 0) SceneManager.LoadScene("LevelEleven");
    }

    public void PlayLevelTwelve()
    {
        if (Levels[10].stars > 0) SceneManager.LoadScene("LevelTwelve");
    }

    public void PlayLevelThirteen()
    {
        if (Levels[11].stars > 0) SceneManager.LoadScene("LevelThirteen");
    }

    public void PlayLevelFourteen()
    {
        if (Levels[12].stars > 0) SceneManager.LoadScene("LevelFourteen");
    }

    public void PlayLevelFifteen()
    {
        if (Levels[13].stars > 0) SceneManager.LoadScene("LevelFifteen");
    }

    public void PlayLevelSixteen()
    {
        if (Levels[14].stars > 0) SceneManager.LoadScene("LevelSixteen");
    }
}
