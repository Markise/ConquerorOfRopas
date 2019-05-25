using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperButton : MonoBehaviour {

    public GameObject player;
    public void PaperClicked()
    {
        if (player.GetComponent<PlayerController>().inBattle)
        {
            player.GetComponent<PlayerController>().paperPressed = true;
        }
    }
}
