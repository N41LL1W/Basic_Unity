using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DestructiveBase : MonoBehaviour
{
    public float currentLife;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife <= 0)
        {
            OnDestroyed();
        }
    }

    public virtual void OnDestroyed()
    {
        
    }
}
