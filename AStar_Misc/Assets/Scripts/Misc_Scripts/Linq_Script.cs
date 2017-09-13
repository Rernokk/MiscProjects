using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linq_Script : MonoBehaviour {
    [SerializeField]
    List<GameObject> Valid;
    [SerializeField]
    List<GameObject> scene_Objects;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Valid.Clear();
        scene_Objects.Clear();
        foreach (GameObject ctx in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            scene_Objects.Add(ctx);
        }
        Valid = scene_Objects.FindAll((GameObject obj) => Vector3.Distance(obj.transform.position, transform.position) < 8);
	}
}
