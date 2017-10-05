using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CommandParticles))]
public class Player : MonoBehaviour {
  CommandParticles hit;
	// Use this for initialization
	void Start () {
    hit = GetComponent<CommandParticles>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F)){
      hit.SummonParticles();
    }
	}
}
