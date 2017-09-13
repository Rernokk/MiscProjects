using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_of_Sight_Script : MonoBehaviour {
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.forward);
        Debug.Log((Vector3.Dot(transform.forward, player.transform.position - transform.position)));
        Debug.Log(Mathf.Acos(Vector3.Dot(transform.forward, player.transform.position - transform.position)));
        /*if (Vector3.Dot(transform.forward, player.transform.position - transform.position) < 0)
        {
            Debug.Log("Behind");
        } else
        {
            Debug.Log("In Front");
        }*/
    }
}
