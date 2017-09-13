using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_Script : MonoBehaviour {
    Entity_Script myEntity;
    int ChainLightningTargets = 3;
    [SerializeField]
    List<GameObject> Targets;
    // Use this for initialization
    void Start () {
        myEntity = GetComponent<Entity_Script>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit info;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info) && info.transform.gameObject.GetComponent<Entity_Script>())
            {
                Entity_Script hit = info.transform.gameObject.GetComponent<Entity_Script>();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ChainLightning(hit);
                }

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    hit.Heal(10);
                }
            }
        }
    }

    void ChainLightning(Entity_Script hitTarget)
    {
        Targets = new List<GameObject>();
        Targets.Add(hitTarget.gameObject);
        hitTarget.GetComponent<Entity_Script>().SetTagged(true);
        ChainLightningExpand(Targets);

    }

    void ChainLightningExpand(List<GameObject> myObjects)
    {
    }
}
