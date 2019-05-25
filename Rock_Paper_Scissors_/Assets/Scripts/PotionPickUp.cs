using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickUp : MonoBehaviour {

    public GameManager GM;
    public void PotionClicked()
    {
        GM = FindObjectOfType<GameManager>();
        GM.UpdateHealth(1);
        Destroy(gameObject);
    }
}
