using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float timeToPress;
    public Canvas canvas;
    public GameManager GM;
    public GameObject curEnemy;
    public GameObject PlayerParticles;
    public GameObject RockParticles;
    public GameObject PaperParticles;
    public GameObject ScissorParticles;
    public Button hpPot;
    public bool rockPressed, paperPressed, scissorPressed;
    public bool inBattle;
    public Camera cam;
    private float startTime;

    private float timeToPressConst;
    // Use this for initialization
    void Start () {
        timeToPressConst = timeToPress;
        inBattle = false;
        GM = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //    rockPressed = false;
        //    paperPressed = false;
        //     scissorPressed = false;

        if (inBattle)
        {
            if (Time.time - startTime<=timeToPress)
            {
                if (rockPressed == true)
                {                    
                    if (curEnemy.GetComponent<EnemyBehavior>().rpsType == "Scissor")
                    {
                        Win();
                    }
                   else
                    {
                        Lose();
                    }
                }
                else if(scissorPressed == true)
                {                   
                    if (curEnemy.GetComponent<EnemyBehavior>().rpsType == "Paper")
                    {
                        Win();
                    }
                    else
                    {
                        Lose();
                    }
                }
                else if(paperPressed == true)
                {
                    if (curEnemy.GetComponent<EnemyBehavior>().rpsType == "Rockr")
                    {
                        Win();
                    }
                    else
                    {
                        Lose();
                    }
                }
            }
            else
            {

                Lose();
            }

        }
	}

    public void BattlePhase()
    {
       inBattle = true;
       timeToPress = timeToPressConst / GM.level + .25f;
       startTime = Time.time;
       curEnemy.GetComponent<Animator>().SetBool("Battle", true);
       curEnemy.GetComponent<Animator>().speed = timeToPress / (timeToPress * timeToPress);
        Debug.Log("TTP: " + timeToPress);

    }

    void Win()
    {
        GM.trueKills++;
        GM.enemiesKilled++;
        curEnemy.GetComponent<Animator>().SetBool("Battle", false);
        rockPressed = false;
        paperPressed = false;
        scissorPressed = false;
        inBattle = false;
        int dropRate = Random.Range(0,10);
        if(dropRate <= 4)
        {
            Vector2 pos = cam.WorldToScreenPoint(curEnemy.transform.position);
            Button pot = Instantiate(hpPot, pos, Quaternion.identity);
            pot.transform.SetParent(canvas.transform);
        }
        Destroy(curEnemy);
      

        if (curEnemy.GetComponent<EnemyBehavior>().rpsType == "Rock")
        {
            Instantiate(RockParticles, curEnemy.transform.position, Quaternion.identity);
        }
        else if (curEnemy.GetComponent<EnemyBehavior>().rpsType == "Paper")
        {
            Instantiate(PaperParticles, curEnemy.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(ScissorParticles, curEnemy.transform.position, Quaternion.identity);
        }

    }
    void Lose()
    {
        GM.enemiesKilled++;
        curEnemy.GetComponent<Animator>().SetBool("Battle", false);
        GM.UpdateHealth(-1);
        rockPressed = false;
        paperPressed = false;
        scissorPressed = false;
        inBattle = false;
        Destroy(curEnemy);
       

        if (curEnemy.GetComponent<EnemyBehavior>().rpsType == "Rock")
        {
            Instantiate(RockParticles, curEnemy.transform.position, Quaternion.identity);
            Instantiate(PlayerParticles, transform.position, Quaternion.identity);
        }
        else if (curEnemy.GetComponent<EnemyBehavior>().rpsType == "Paper")
        {
            Instantiate(PaperParticles, curEnemy.transform.position, Quaternion.identity);
            Instantiate(PlayerParticles, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(ScissorParticles, curEnemy.transform.position, Quaternion.identity);
            Instantiate(PlayerParticles, transform.position, Quaternion.identity);
        }
      
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {         
            curEnemy = other.gameObject;
            BattlePhase();
        }
    }

}
