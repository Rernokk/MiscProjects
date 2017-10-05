using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandParticles : MonoBehaviour {
  ParticleSystem system;

  [SerializeField]
  Material mat;

  [SerializeField]
  RenderTexture renderTar;

  [SerializeField]
  GameObject prefab;
  GameObject myPrefab;
	// Use this for initialization
	public void SummonParticles () {
    myPrefab = Instantiate(prefab, transform, false);
    system = myPrefab.transform.Find("Particle System").GetComponent<ParticleSystem>();
    mat = new Material(mat);
    renderTar = new RenderTexture(renderTar);
    ParticleSystemRenderer systemRenderer = (ParticleSystemRenderer) system.GetComponent<Renderer>();
    systemRenderer.material = mat;
    myPrefab.transform.Find("TextCam").GetComponent<Camera>().targetTexture = renderTar;
    mat.SetTexture("_MainTex", renderTar);
    myPrefab.transform.Find("Canvas/Panel/Text").GetComponent<Text>().text = Random.Range(0, 100).ToString();
    system.Emit(1);
    Destroy(myPrefab, 5f);
    myPrefab = null;
  }
}
