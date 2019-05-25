using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOvTime : MonoBehaviour {

    public float timeToDelete;
	// Use this for initialization
	void Start () {
		
	}
	
	IEnumerator Delete()
    {
        yield return new WaitForSeconds(timeToDelete);
        Destroy(gameObject);
    }
}
