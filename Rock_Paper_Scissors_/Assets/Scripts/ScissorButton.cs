using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorButton : MonoBehaviour {

    public GameObject player;
    public void ScissorClicked()
    {
        if (player.GetComponent<PlayerController>().inBattle)
        {
            player.GetComponent<PlayerController>().scissorPressed = true;
        }
    }
}
