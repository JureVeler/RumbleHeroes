﻿using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour 
{
    // Entity static variables
    public string entityName;
    public string entityClass;
    public string entityType;
    public string entityInformation;
    public float speedBase;
    public float entityJumpPower; // Relates to the strength of an entities Jump.


    public GameObject Ability;
    public GameObject Ability2;
    // Represent the string that will be used for calling the animation for each ability in the animator
    public string ability_animator;
    public string ability2_animator;
    public int maxHP;
    // Entity active variables
    public int currentHP;
    public float speed;
    public bool ability_ready = true;
    public bool ability2_ready = true;
    // Entity global status
    protected bool isEntityAlive = true;
    // Entity Description Image
    public Sprite entityDescriptionImage;


    // This class will be expanded when we know more what we can move here. Preparations maybe for some simple AI :X
}
