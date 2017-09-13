using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour {
    float durationRemaining;
    Entity_Script entity;

    private void Start()
    {
        entity = gameObject.GetComponent<Entity_Script>();
    }
}
