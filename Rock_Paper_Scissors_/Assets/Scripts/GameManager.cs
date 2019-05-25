using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Image healthUI;
    public Text Kills;
    public int trueKills;
    public Text levelAnnounce;
    public Canvas canvas;
    public GameObject rockEnemy;
    public GameObject scissorEnemy;
    public GameObject paperEnemy;
    public int enemyNum;
    public int maxHealth;
    public int health;
    public int level;
    public int enemiesKilled;
    public int enemyToLevelMult;
    public bool okToSpawn;

    private GameObject[] uiHearts;
    private int spawnAmount;
    private int maxEnemies;

    // Use this for initialization
    void Start () {
        Vector3 pos = new Vector3(12.5f, 590f, 0f);
        for (int i = 1; i <= maxHealth; i++)
        {
            Image HP = Instantiate(healthUI, pos, Quaternion.identity);         
            HP.transform.SetParent(canvas.transform);
            pos = new Vector3(pos.x + 25f, pos.y, 0f);
        }
    
        health = maxHealth;
        enemyNum = 0;
        okToSpawn = true;
        uiHearts = GameObject.FindGameObjectsWithTag("Heart");
        UpdateHealthUI();
        level = 0;
        trueKills = 0;
        spawnAmount = 0;
        LevelUp();
	}
	
	// Update is called once per frame
	void Update () {

        maxEnemies = level * enemyToLevelMult;
        Debug.Log("Maxenemies  " + maxEnemies);
        Kills.text = " True  Kills:   " + trueKills;
        EnemyBehavior[] Enemies = FindObjectsOfType<EnemyBehavior>();

        if(okToSpawn && spawnAmount<maxEnemies)
        {
            SpawnEnemies();
        }
        else if(enemiesKilled == maxEnemies)
        {
            spawnAmount = 0;
            enemiesKilled = 0;
            LevelUp();
        }
        
        if(health <= 0)
        {
            SceneManager.LoadScene("Lose");
        }
  

	}

    void SpawnEnemies()
    {        
        okToSpawn = false;
        int enemySelection = Random.Range(0, 3);
        int xOrY = Random.Range(0, 2);
        int xPos, yPos;

        if(xOrY == 1)
        {
            yPos = -4;
            xPos = Random.Range(-7,7);
        }
        else
        {
            int negOrPos = Random.Range(0, 2);
            if(negOrPos == 1)
            {
                xPos = -7;
            }
            else
            {
                xPos = 7;
            }

            yPos = Random.Range(-4,2);

        }


        Vector2 spawnPoint = new Vector2(xPos,yPos);


        switch(enemySelection)
        {
            case 0:
             GameObject Rock =  Instantiate(rockEnemy, spawnPoint, Quaternion.identity);
                Rock.GetComponent<EnemyBehavior>().myNum = enemyNum + 1;
                Rock.GetComponent<EnemyBehavior>().rpsType = "Rock";
                break;
            case 1:
               GameObject Scissor = Instantiate(scissorEnemy, spawnPoint, Quaternion.identity);
                Scissor.GetComponent<EnemyBehavior>().myNum = enemyNum + 1;
                Scissor.GetComponent<EnemyBehavior>().rpsType = "Scissor";
                break;
            case 2:
                GameObject Paper = Instantiate(paperEnemy, spawnPoint, Quaternion.identity);
                Paper.GetComponent<EnemyBehavior>().myNum = enemyNum + 1;
                Paper.GetComponent<EnemyBehavior>().rpsType = "Paper";
                break;
        }

        spawnAmount++;
        StartCoroutine(SpawnWait());

    }

    public IEnumerator SpawnWait()
    {
        float spawnTime = Random.Range(2.75f,3.5f);
        yield return new WaitForSeconds(spawnTime);
        okToSpawn = true;
    }


    public void UpdateHealth(int hpUpate)
    {
        if (Mathf.Sign(hpUpate) == 1)
        {
            health++;
        }
        else if (Mathf.Sign(hpUpate) == -1)
        {
            health--;
        }
        UpdateHealthUI();
    }
    public void UpdateHealthUI()
    {
        for (int x = maxHealth; x > maxHealth - health; x--)
        {
            uiHearts[x-1].GetComponent<Image>().color = Color.red;

            for (int i = 0; i < maxHealth - health; i++)
            {
                uiHearts[i].GetComponent<Image>().color = Color.gray;          
            }
        }

    }

    public void LevelUp()
    {
        Debug.Log("Level  "+level);
        okToSpawn = false;
        level++;        
        if (level >5)
        {
            SceneManager.LoadScene("Win");
        }
        Vector2 pos = new Vector2(0, 0);
        levelAnnounce.text = "Level " + level;
        Color temp = levelAnnounce.color;
        temp.a = 1f;
        levelAnnounce.color = temp;
        StartCoroutine(NextLevel());       
    }

    public IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        okToSpawn = true;
        Color temp = levelAnnounce.color;
        temp.a = 0f;
        levelAnnounce.color = temp;
    }

}
