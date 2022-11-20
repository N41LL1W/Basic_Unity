using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : DestructiveBase
{
    // Basics Attibutes
    public int currentLevel;
    public BasicStats basicStats;
    
    [System.Serializable]
    public class BasicStats
    {
        public float startLife;
        public float startMana;
        public int strength;
        public int magic;
        public int agillity;
        public int baseDefense;
        public int baseAttack;
        public float baseRange;
    }
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentLife = basicStats.startLife;
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    public override void OnDestroyed()
    {
        
    }
}
