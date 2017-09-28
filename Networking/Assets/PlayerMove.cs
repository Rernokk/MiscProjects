using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {
  public GameObject bulletPrefab;

	void Update () {
    if (!isLocalPlayer)
      return;

    var x = Input.GetAxis("Horizontal") * .1f;
    var z = Input.GetAxis("Vertical") * .1f;
    transform.Translate(x, 0, z);

    if (Input.GetKeyDown(KeyCode.Space)){
      CmdFire();
    }
	}

  [Command]
  void CmdFire(){
    var bullet = (GameObject)Instantiate(bulletPrefab, transform.position - transform.forward, Quaternion.identity);
    bullet.GetComponent<Rigidbody>().velocity = -transform.forward * 4;
    NetworkServer.Spawn(bullet);
    Destroy(bullet, 2f);
  }

  public override void OnStartLocalPlayer()
  {
    base.OnStartLocalPlayer();
    GetComponent<MeshRenderer>().material.color = Color.cyan;
  }
}
