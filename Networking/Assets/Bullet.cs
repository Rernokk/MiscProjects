using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
  private void OnCollisionEnter(Collision collision)
  {
    var hit = collision.gameObject;
    var hitTarget = hit.GetComponent<Combat>();
    if (hitTarget != null){
      hitTarget.TakeDamage(10);
      Destroy(gameObject);
    }
  }
}
