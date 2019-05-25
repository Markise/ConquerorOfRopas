using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public PlayerController player;
    public GameManager GM;
    public float speed;
    public int myNum;
    public string rpsType;
    private bool okToMove;
	// Use this for initialization
	void Start () {
        
        player = FindObjectOfType<PlayerController>();
        GM = FindObjectOfType<GameManager>();
        okToMove = true;
        speed = speed * Vector2.Distance(transform.position, player.transform.position)*.10f;
        speed += GM.level * .05f;
        Debug.Log("Speed: " + speed);
	}
	
	// Update is called once per frame
	void Update () {

        if (okToMove)
        {
            if (player.curEnemy == null)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed *.50f * Time.deltaTime);
            }
        }
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player_Area")
        {
            okToMove = false;
        }
        if(collision.tag == "Enemy")
        {
            if (myNum > collision.GetComponent<EnemyBehavior>().myNum)
            {
               transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, -speed * Time.deltaTime);
            }

        }
    }
    
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (myNum > collision.GetComponent<EnemyBehavior>().myNum)
            {
                transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, -speed * Time.deltaTime);
            }

        }
    }

}
