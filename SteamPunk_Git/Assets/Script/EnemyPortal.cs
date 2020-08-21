﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            Vector3 currentposition = col.gameObject.transform.position; 
            col.gameObject.transform.position = new Vector3(currentposition.x, currentposition.y, -110f);
        }
    }
}
