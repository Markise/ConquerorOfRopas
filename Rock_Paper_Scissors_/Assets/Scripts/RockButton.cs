using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockButton : MonoBehaviour {

    public GameObject player;
	public void RockClicked()
    {
        if (player.GetComponent<PlayerController>().inBattle)
        {
            player.GetComponent<PlayerController>().rockPressed = true;
        }
    }
}
