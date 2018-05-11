using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] Transform target;
    [SerializeField] float firingSpeed = 1f;
    [SerializeField] float maxRange = 2f;
    ParticleSystem particleSystem;
    bool isFiring = false;
	// Use this for initialization
	void Start () {
        objectToPan = transform.Find("Tower_A_Top");
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if(target)
        {
            objectToPan.LookAt(target);
        }
        
        if(isFiring == false)
        {
            StartCoroutine(FireAtTarget());
            isFiring = true;
        }
	}

    private IEnumerator FireAtTarget()
    {
        while (target)
        {
            float distance = Vector3.Distance(target.position / 10, gameObject.transform.position / 10);
            if (distance <= maxRange)
            {
                particleSystem.Play();
                
            }

            yield return new WaitForSeconds(firingSpeed);
        }
    }
}
