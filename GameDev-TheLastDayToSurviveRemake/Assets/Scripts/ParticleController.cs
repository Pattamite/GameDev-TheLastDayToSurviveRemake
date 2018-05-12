using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    public GameObject playerCharacter;
    private ParticleSystem particle;
    private ParticleSystem.MainModule particleModule;
    // Use this for initialization
    void Start () {
        particle = GetComponent<ParticleSystem>();
        particleModule = particle.main;
    }
	
	// Update is called once per frame
	void Update () {
        particleModule.startRotation = (360f - playerCharacter.transform.eulerAngles.z) / 180 * Mathf.PI;
    }

    public void EmitCount(int count) {
        particle.Emit(count);
    }
}
