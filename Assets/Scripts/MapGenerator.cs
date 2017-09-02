using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    //Transform deltaX;
    public GameObject[] mapPrefabs;
    float spawnTime = 5.0f;
    MonkeyController.PlayerState playerState = MonkeyController.playerState;
    float time = 0;
    bool wasGenerated = false;
    int i = 1;

	// Use this for initialization
	void Start () {
        Invoke("GenerateMap", spawnTime / 10);
        //InvokeRepeating("GenerateMap", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update () {
        playerState = MonkeyController.playerState;
    }

    void FixedUpdate()
    {
        if (playerState == MonkeyController.PlayerState.PLAYING)
        {
            spawnTime = 5.0f;
            time = Time.realtimeSinceStartup;
            if ((int)time % (int)spawnTime == 0)
            {
                if (!wasGenerated)
                {
                    Invoke("GenerateMap", 1.0f);
                    wasGenerated = true;
                }
            }
            else
            {
                wasGenerated = false;
            }
        }
        if (playerState == MonkeyController.PlayerState.GAMEOVER || playerState == MonkeyController.PlayerState.READY)
        {

        }
    }

    void GenerateMap()
    {
        int randomMapFragment = Random.Range(0, mapPrefabs.Length);
        var pos = new Vector3(i * 24.0f, transform.position.y, transform.position.z);
        Instantiate(mapPrefabs[randomMapFragment], pos, Quaternion.Euler(Vector3.zero));
        i++;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monkey")
        {
            Destroy(gameObject);
        }
    }
}
