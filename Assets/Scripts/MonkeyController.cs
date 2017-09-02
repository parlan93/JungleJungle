using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MonkeyController : MonoBehaviour {

    public enum PlayerState
    {
        READY,
        PLAYING,
        GAMEOVER
    }

    public static PlayerState playerState { get; set; }
    Rigidbody2D rb2d;
    Animator animator;
    float maxSpeed = 4.75f;
    float speed = 3.0f;
    float jumpSpeed = 6.0f;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 1000.0f;
    bool roll = false;

    BoxCollider2D bc2d;

    bool isDead = false;
    bool timeReaded = false;
    bool speedChanger = false;
    bool speedIncreased = false;

    int points = 0;
    int score = 0;
    int highScore = 0;
    int bananas = 0;
    int obstacleJumped = 0;
    int obstacleRolled = 0;
    int totalGameTime = 0;
    int powerUps = 0;
    float gameTimeStart = 0;
    float gameTimeEnd = 0;
    float gameTimeCurrentSpeed = 0;
    float currentTime = 0;
    float previousTime = 0;
    float distanceFull = 0;
    float distanceCurrentSpeed = 0;
    float totalGameTimeNow = 0;
    float timeToShow = 0f;

    PowerUp shield = new PowerUp(1, PowerUp.PowerUpType.SHIELD);
    PowerUp doubleBananas = new PowerUp(2, PowerUp.PowerUpType.DOUBLE_BANANAS);
    PowerUp doubleTask = new PowerUp(3, PowerUp.PowerUpType.DOUBLE_TASK);
    int shieldTime = 0;
    int doubleBananasTime = 0;
    int doubleTaskTime = 0;
    float currentPowerUpTime = 0f;
    float currentPowerUpStartTime = 0f;
    int currentPowerUpMaxTime = 0;
    bool isPowerUp = false;
    bool hasShield = false;
    bool hasDoubleBananas = false;
    bool hasDoubleTask = false;
    int doublePoints = 0;
    int pointsUntilDouble = 0;
    float doubleTime = 0f;
    float timeUntilDouble = 0f;
    float doubleDistance = 0f;
    float distanceUntilDouble = 0f;
    float bonusTime = 0f;
    float totalBonus = 0f;

    /**
     *  TO CHANGE
     **/
    public int firstStar;
    public int secondStar;
    public int thirdStar;
    public string levelHighScore;
    public string task;
    public string currentLevelTitle;
    public string currentLevelScene;
    /** **/

    GameObject Playing;
    GameObject Ready;
    GameObject GameOver;
    GameObject BottomInfo;

    GameObject YellowStarFirst;
    GameObject YellowStarSecond;
    GameObject YellowStarThird;
    GameObject GreyStarFirst;
    GameObject GreyStarSecond;
    GameObject GreyStarThird;

    GameObject DoubleBananasPowerUp;
    GameObject DoubleTaskPowerUp;
    GameObject ShieldPowerUp;

    public Text BananaAmount;
    public Text PointsAmount;
    public Text LevelValue;
    public Text HighestValue;
    public Text LevelTitle;
    public Text TaskGet;
    public Text TaskFirstStar;
    public Text TaskSecondStar;
    public Text TaskThirdStar;
    public Text PowerUpRemainTime;
    Text bananaAmount;
    Text pointsAmount;
    Text levelValue;
    Text highestValue;
    Text levelTitle;
    Text taskGet;
    Text taskFirstStar;
    Text taskSecondStar;
    Text taskThirdStar;
    Text powerUpRemainTime;

    GameObject FGStar;
    GameObject SGStar;
    GameObject TGStar;
    GameObject FYStar;
    GameObject SYStar;
    GameObject TYStar;

    int playerBananas;
    bool playerBananasSaved;

    // Start
    void Start()
    {
        speed = 3.0f;

        isDead = false;
        timeReaded = false;
        speedChanger = false;
        speedIncreased = false;

        highScore = PlayerPrefs.GetInt(levelHighScore);
        points = 0;
        score = 0;
        bananas = 0;
        obstacleJumped = 0;
        obstacleRolled = 0;
        totalGameTime = 0;
        powerUps = 0;
        gameTimeStart = 0;
        gameTimeEnd = 0;
        gameTimeCurrentSpeed = 0;
        currentTime = 0;
        previousTime = 0;
        distanceFull = 0;
        distanceCurrentSpeed = 0;
        totalGameTimeNow = 0f;
        timeToShow = 0f;

        playerState = PlayerState.READY;

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();

        gameTimeStart = Time.realtimeSinceStartup;
        currentTime = Time.realtimeSinceStartup;
        previousTime = currentTime;

        Playing = GameObject.Find("GameUIPlaying");
        Ready = GameObject.Find("Ready");
        GameOver = GameObject.Find("GameOver");
        BottomInfo = GameObject.Find("BottomInfo");

        YellowStarFirst = GameObject.Find("YellowStarFirst");
        YellowStarSecond = GameObject.Find("YellowStarSecond");
        YellowStarThird = GameObject.Find("YellowStarThird");
        GreyStarFirst = GameObject.Find("GreyStarFirst");
        GreyStarSecond = GameObject.Find("GreyStarSecond");
        GreyStarThird = GameObject.Find("GreyStarThird");

        DoubleBananasPowerUp = GameObject.Find("PowerUpDoubleBananas");
        DoubleTaskPowerUp = GameObject.Find("PowerUpDoubleTask");
        ShieldPowerUp = GameObject.Find("PowerUpShield");

        bananaAmount = BananaAmount.GetComponent<Text>();
        pointsAmount = PointsAmount.GetComponent<Text>();
        levelValue = LevelValue.GetComponent<Text>();
        highestValue = HighestValue.GetComponent<Text>();
        levelTitle = LevelTitle.GetComponent<Text>();
        taskGet = TaskGet.GetComponent<Text>();
        taskFirstStar = TaskFirstStar.GetComponent<Text>();
        taskSecondStar = TaskSecondStar.GetComponent<Text>();
        taskThirdStar = TaskThirdStar.GetComponent<Text>();
        powerUpRemainTime = PowerUpRemainTime.GetComponent<Text>();
        powerUpRemainTime.text = "";

        taskFirstStar.text = firstStar.ToString();
        taskSecondStar.text = secondStar.ToString();
        taskThirdStar.text = thirdStar.ToString();

        // Power Ups GET Info From PlayerPrefs
        // PowerUp Level
        if (PlayerPrefs.GetInt("PU-Shield-Level") > 1)
        {
            shield.powerUpLevel = PlayerPrefs.GetInt("PU-Shield-Level");
            shield.powerUpTime = shield.powerUpLevel * 3 + 1;
        }
        if (PlayerPrefs.GetInt("PU-Bananas-Level") > 1)
        {
            doubleBananas.powerUpLevel = PlayerPrefs.GetInt("PU-Bananas-Level");
            doubleBananas.powerUpTime = doubleBananas.powerUpLevel * 3 + 1;
        }
        if (PlayerPrefs.GetInt("PU-Double-Level") > 1)
        {
            doubleTask.powerUpLevel = PlayerPrefs.GetInt("PU-Double-Level");
            doubleTask.powerUpTime = doubleTask.powerUpLevel * 3 + 1;
        }
        // Power Up Time
        shieldTime = shield.powerUpTime;
        doubleBananasTime = doubleBananas.powerUpTime;
        doubleTaskTime = doubleTask.powerUpTime;
        isPowerUp = false;
        hasShield = false;
        hasDoubleBananas = false;
        hasDoubleTask = false;
        currentPowerUpTime = 0f;
        currentPowerUpStartTime = 0f;
        currentPowerUpMaxTime = 0;
        doublePoints = 0;
        pointsUntilDouble = 0;
        doubleTime = 0f;
        timeUntilDouble = 0f;
        doubleDistance = 0f;
        distanceUntilDouble = 0f;
        bonusTime = 0f;
        totalBonus = 0f;

        FGStar = GameObject.Find("FGStar");
        SGStar = GameObject.Find("SGStar");
        TGStar = GameObject.Find("TGStar");
        FYStar = GameObject.Find("FYStar");
        SYStar = GameObject.Find("SYStar");
        TYStar = GameObject.Find("TYStar");

        DoubleBananasPowerUp.SetActive(false);
        DoubleTaskPowerUp.SetActive(false);
        ShieldPowerUp.SetActive(false);

        // Get Bananas amount
        if (PlayerPrefs.GetInt("Bananas") == null || PlayerPrefs.GetInt("Bananas") < 1)
        {
            playerBananas = 0;
        }
        else
        {
            playerBananas = PlayerPrefs.GetInt("Bananas");
        }
        playerBananasSaved = false;
    }

    public void SetPlayerStatePlaying()
    {
        playerState = PlayerState.PLAYING;
    }

    public void GoBackToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void GoBackToLevelSelectionVillage()
    {
        SceneManager.LoadScene("LevelSelectionVillage");
    }

    public void Retry()
    {
        SceneManager.LoadScene(currentLevelScene);
    }
	
    // Fixed Update
    void FixedUpdate()
    {
        if (playerState == PlayerState.READY)
        {
            levelTitle.text = currentLevelTitle;
            Playing.SetActive(false);
            Ready.SetActive(true);
            GameOver.SetActive(false);
            pointsAmount.text = "";
            bananaAmount.text = "";
            BottomInfo.SetActive(false);
        }

        if (playerState == PlayerState.GAMEOVER)
        {
            /**
             *  TO CHANGE
             **/
            if (task == "Banana") score = bananas;
            else if (task == "Jump") score = obstacleJumped;
            else if (task == "Roll") score = obstacleRolled;
            else if (task == "Time") score = totalGameTime;
            else if (task == "Distance") score = (int)distanceFull;
            else if (task == "PowerUp") score = powerUps;
            else if (task == "Point") score = points;
            
            if (score >= highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt(levelHighScore, highScore); 
            }
            highestValue.text = highScore.ToString();
            /** **/

            Playing.SetActive(false);
            Ready.SetActive(false);
            BottomInfo.SetActive(false);
            GameOver.SetActive(true);

            if (highScore >= firstStar)
            {
                YellowStarFirst.SetActive(true);
                GreyStarFirst.SetActive(false);
            }
            else
            {
                YellowStarFirst.SetActive(false);
                GreyStarFirst.SetActive(true);
            }
            if (highScore >= secondStar)
            {
                YellowStarSecond.SetActive(true);
                GreyStarSecond.SetActive(false);
            }
            else
            {
                YellowStarSecond.SetActive(false);
                GreyStarSecond.SetActive(true);
            }
            if (highScore >= thirdStar)
            {
                YellowStarThird.SetActive(true);
                GreyStarThird.SetActive(false);
            }
            else
            {
                YellowStarThird.SetActive(false);
                GreyStarThird.SetActive(true);
            }
            bananaAmount.text = "";
            pointsAmount.text = "";

            if (!playerBananasSaved)
            {
                playerBananasSaved = true;
                playerBananas += bananas;
                PlayerPrefs.SetInt("Bananas", playerBananas);
            }

            /**
             *  TO CHANGE
             **/
            levelValue.text = score.ToString();
            /** **/
        }

        if (playerState == PlayerState.PLAYING)
        {

            if (task == "Banana") score = bananas;
            else if (task == "Jump") score = obstacleJumped;
            else if (task == "Roll") score = obstacleRolled;
            else if (task == "Time") score = totalGameTime;
            else if (task == "Distance") score = (int)distanceFull + (int)distanceCurrentSpeed;
            else if (task == "PowerUp") score = powerUps;
            else if (task == "Point") score = points;

            Playing.SetActive(true);
            Ready.SetActive(false);
            GameOver.SetActive(false);
            BottomInfo.SetActive(true);
            bananaAmount.text = bananas.ToString();
            taskGet.text = score.ToString();
            pointsAmount.text = points.ToString();
            totalGameTimeNow = Time.realtimeSinceStartup - gameTimeStart;

            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            animator.SetBool("Ground", grounded);
            animator.SetFloat("vSpeed", rb2d.velocity.y * 5);

            // Check player life
            if (!isDead) rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                animator.enabled = false;
            }
            
            if (isDead && !timeReaded)
            {
                gameTimeEnd = Time.realtimeSinceStartup;
                timeReaded = true;
                //Debug.Log("GAME TIME: " + (int)(gameTimeEnd - gameTimeStart));
            }

            if (hasDoubleTask && task == "Time")
            {
                bonusTime = totalGameTimeNow - timeUntilDouble;
            }

            timeToShow = Time.realtimeSinceStartup - gameTimeStart + totalBonus;

            if (isDead && timeReaded)
            {
                playerState = PlayerState.GAMEOVER;
                totalGameTime = (int)(gameTimeEnd - gameTimeStart + totalBonus);
            }
            totalGameTime = (int)(Time.realtimeSinceStartup - gameTimeStart + totalBonus);
            // Speed increase
            if ((int)(Time.realtimeSinceStartup - gameTimeStart) % 15 == 0) {
                if (!speedIncreased && speed < maxSpeed)
                {
                    speed += 0.12f;
                    speedChanger = true;
                    speedIncreased = true;
                }
            }
            else
            {
                speedIncreased = false;
            }
            
            // Power Ups
            if (isPowerUp)
            {
                if (Time.realtimeSinceStartup - currentPowerUpStartTime > currentPowerUpMaxTime)
                {
                    if (hasDoubleTask && task == "Time") totalBonus += bonusTime;
                    isPowerUp = false;
                    hasShield = false;
                    hasDoubleBananas = false;
                    hasDoubleTask = false;
                }
                powerUpRemainTime.text = ((int)(currentPowerUpMaxTime - (Time.realtimeSinceStartup - currentPowerUpStartTime))).ToString();
                if (hasDoubleBananas)
                {
                    DoubleBananasPowerUp.SetActive(true);
                    DoubleTaskPowerUp.SetActive(false);
                    ShieldPowerUp.SetActive(false);
                }
                else if (hasDoubleTask)
                {
                    DoubleBananasPowerUp.SetActive(false);
                    DoubleTaskPowerUp.SetActive(true);
                    ShieldPowerUp.SetActive(false);
                } 
                else if (hasShield)
                {
                    DoubleBananasPowerUp.SetActive(false);
                    DoubleTaskPowerUp.SetActive(false);
                    ShieldPowerUp.SetActive(true);
                }
            }
            else
            {
                powerUpRemainTime.text = "";
                DoubleBananasPowerUp.SetActive(false);
                DoubleTaskPowerUp.SetActive(false);
                ShieldPowerUp.SetActive(false);
            }

            // Bottom Stars
            FGStar.SetActive(true);
            SGStar.SetActive(true);
            TGStar.SetActive(true);
            FYStar.SetActive(false);
            SYStar.SetActive(false);
            TYStar.SetActive(false);

            if (score >= firstStar)
            {
                FGStar.SetActive(false);
                FYStar.SetActive(true);
            }
            if (score >= secondStar)
            {
                SGStar.SetActive(false);
                SYStar.SetActive(true);
            }
            if (score >= thirdStar)
            {
                TGStar.SetActive(false);
                TYStar.SetActive(true);
            }

            points = (bananas * 15 + (int)distanceFull * 10 + (int)distanceCurrentSpeed * 10 + obstacleJumped * 50 + obstacleRolled * 50);
            if (hasDoubleTask && task == "Points")
                points += (points - pointsUntilDouble) * 2;
        }
    }

    // Update
	void Update ()
    {
        if (playerState == PlayerState.PLAYING)
        {
            // Jump
            if (grounded && Input.GetButtonDown("Jump"))
            //if (grounded && SwipeManager.Instance.IsSwiping(SwipeDirection.UP))
            {
                animator.SetBool("Ground", false);
                rb2d.AddForce(new Vector2(0, jumpForce));
            }

            // Roll
            if(Input.GetButtonDown("Roll"))
            //if (SwipeManager.Instance.IsSwiping(SwipeDirection.DOWN))
            {
                animator.SetTrigger("Roll");
                bc2d.size = new Vector2(0.32f, 0.32f);
                bc2d.offset = new Vector2(-0.17f, 0.16f);
            }

            // Distance measure
            if (!isDead)
            {
                currentTime = Time.realtimeSinceStartup;
                distanceCurrentSpeed = (currentTime - previousTime) * speed;
                if (hasDoubleTask && task == "Distance") distanceCurrentSpeed += ((distanceCurrentSpeed + distanceFull) - distanceUntilDouble) * 2;
                if (speedChanger)
                {
                    distanceFull += distanceCurrentSpeed;
                    speedChanger = false;
                    distanceCurrentSpeed = 0;
                    previousTime = currentTime;
                }
            }

            if (isDead)
            {
                distanceFull += distanceCurrentSpeed;
                distanceCurrentSpeed = 0;
            }
        }
    }

    // Collision enter
    void OnCollisionEnter2D(Collision2D col)
    {
        if (playerState == PlayerState.PLAYING)
        {
            if (col.gameObject.tag == "Obstacle")
            {
                if (hasShield)
                {
                    Destroy(col.gameObject);
                }
                else
                {
                    //Debug.Log("GAME OVER");
                    isDead = true;
                    if (col.gameObject.GetComponent<Animator>())
                    {
                        col.gameObject.GetComponent<Animator>().Stop();
                    }
                }
            }

            if (col.gameObject.tag == "ObstacleUp" || col.gameObject.tag == "ObstacleDown")
            {
                if (hasShield)
                {
                    Destroy(col.gameObject);
                }
                else
                {
                    //Debug.Log("GAME OVER");
                    isDead = true;
                    if (col.gameObject.GetComponent<Animator>())
                    {
                        col.gameObject.GetComponent<Animator>().Stop();
                    }
                }
            }
        }
        
    }

    // Trigger enter
    void OnTriggerEnter2D(Collider2D col)
    {
        if (playerState == PlayerState.PLAYING)
        {
            // Bananas counting
            if (col.gameObject.tag == "Banana" && !isDead)
            {
                if (hasDoubleBananas) bananas++;
                if (hasDoubleTask && task == "Banana") bananas++;
                bananas++;
                //Debug.Log("BANANAS: " + bananas);
                Destroy(col.gameObject);
            }

            // Init obstacle
            if (col.gameObject.CompareTag("Init") && !isDead)
            {
                col.gameObject.GetComponentInChildren<Animator>().SetBool("Init", true);
                if (col.gameObject.GetComponentInChildren<Animator>().GetBool("Init"))
                {
                    //Debug.Log("INITED!!!");
                }
                else
                {
                    //Debug.Log("Error in INITED!");
                }
                //Destroy(col.gameObject.GetComponent<BoxCollider2D>());
            }

            // Jump over obstacle
            if (col.gameObject.tag == "ObstacleUp" && !isDead)
            {
                obstacleJumped++;
                if (hasDoubleTask && task == "Jump") obstacleJumped++;
                //Debug.Log("OBSTACLES JUMPED: " + obstacleJumped);
            }

            // Roll under obstacle
            if (col.gameObject.tag == "ObstacleDown" && !isDead)
            {
                obstacleRolled++;
                if (hasDoubleTask && task == "Roll") obstacleRolled++;
                //Debug.Log("OBSTACLES ROLLED: " + obstacleRolled);
            }

            // Power Up - Shield 
            if (col.gameObject.tag == "PowerUpShield")
            {
                currentPowerUpStartTime = Time.realtimeSinceStartup;
                powerUps++;
                if (task == "PowerUps" && hasDoubleTask) powerUps++;
                hasDoubleTask = false;
                hasDoubleBananas = false;
                isPowerUp = true;
                hasShield = true;
                currentPowerUpTime = 0;
                currentPowerUpMaxTime = shieldTime;
                Destroy(col.gameObject);
            }

            // Power Up - Double Task
            if (col.gameObject.tag == "PowerUpDoubleTask")
            {
                currentPowerUpStartTime = Time.realtimeSinceStartup;
                powerUps++;
                if (task == "PowerUps" && hasDoubleTask) powerUps++;
                hasShield = false;
                hasDoubleBananas = false;
                isPowerUp = true;
                hasDoubleTask = true;
                currentPowerUpTime = 0;
                currentPowerUpMaxTime = doubleTaskTime;
                if (task == "Distance") distanceUntilDouble = distanceFull + distanceCurrentSpeed;
                if (task == "Time") timeUntilDouble = Time.realtimeSinceStartup - gameTimeStart;
                if (task == "Points") pointsUntilDouble = points;
                Destroy(col.gameObject);
            }

            // Power Up - Double Bananas
            if (col.gameObject.tag == "PowerUpDoubleBanana")
            {
                currentPowerUpStartTime = Time.realtimeSinceStartup;
                powerUps++;
                if (task == "PowerUps" && hasDoubleTask) powerUps++;
                hasShield = false;
                hasDoubleTask = false;
                isPowerUp = true;
                hasDoubleBananas = true;
                currentPowerUpTime = 0;
                currentPowerUpMaxTime = doubleBananasTime;
                Destroy(col.gameObject);
            }
        }
    }

    // Change box collider
    void ChangeBoxCollider2DWalk()
    {
        bc2d.size = new Vector2(0.54f, 0.6f);
        bc2d.offset = new Vector2(-0.32f, 0.3f);
    }

}
