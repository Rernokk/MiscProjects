using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Script : MonoBehaviour {
    Node anchor;
    [SerializeField]
    bool Pathable = true;
	// Use this for initialization
	void Start () {
        anchor = transform.Find("Anchor_Node").gameObject.GetComponent<Node>();
        transform.parent = GameObject.Find("Tile_Parent").transform;
        if (Random.Range(0, 100) > 75f)
        {
            Pathable = false;
            anchor.SetPathable(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
