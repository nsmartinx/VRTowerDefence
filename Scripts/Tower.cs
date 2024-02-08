using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //all statistic of the tower
    //not passed through to enemy
    public float attackSpeed = 2f;//attack speed of unit 10/x seconds, 0=10 seconds, 10=1second
    public float range = 5;//range of tower
    public float areaOfEffect = 0f;//explosion radius
    public int chain = 1;//number of enemies it can hit in a chain

    //passed through to enemy
    public float damage = 1f;//damage delt by tower before modifiers
    public float magic = 0f;//reduces effectiveness of armour
    public float slowMovement = 0f;//slows enemy movement
    public float slowTime = 0f;//time to slow enemy for
    public float fireDamage = 0f;//damage enemy takes per second if on fire
    public float fireTime = 0f;//length of time to set enemy on fire for

    GameObject target;
    float attackTimer;

    public LayerMask units;

    

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = 10/attackSpeed;//sets the attack timer to the attack speed
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer <= 0)//if the imter has finished
        {
            attackTimer = 10/attackSpeed;//restarts timer
            Attack();//makes the tower attack
        }
        else
        {
            attackTimer -= Time.deltaTime;//timer counts down by the frametime
        }
    }

    GameObject FindTarget()//finds a target to hit
    {
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, range, units);//find the colliders of all targets in range of the tower
        if (targetsInRange.Length != 0)//if the tower has units in range
        {
            return targetsInRange[targetsInRange.Length - 1].gameObject;//returns the last one in the array
        }
        else
        {
            return null;//tells the tower not to attack
        }
    }

    void Attack()
    {
        target = FindTarget();//finds a target for the tower to hit

        if (target != null)
        {
            if (areaOfEffect > 0)//if the tower has an area of effect
            {
                Collider[] targetsToHit = Physics.OverlapSphere(target.transform.position, areaOfEffect, units);//find all units within a certain range of the first target
                foreach (var target in targetsToHit)
                {
                    try
                    {
                        target.GetComponent<Units>().Hit(damage, magic, slowMovement, slowTime, fireDamage, fireTime);//tells the unit it was hit and passes through all of the stats.
                    } catch { };
                    try
                    {
                        target.GetComponent<Units1>().Hit(damage, magic, slowMovement, slowTime, fireDamage, fireTime);//tells the unit it was hit and passes through all of the stats.
                    }
                    catch { };
                }
            }
            else if (chain > 1)//if the tower has a chain attack
            {
                List<GameObject> targetsToHitChain = new List<GameObject>();
                GameObject nextTarget = target;
                targetsToHitChain.Add(nextTarget);
                nextTarget.layer = 9;
                for (int i = 0; i < chain-1; i++)//will hit howevery many enemies the chain variable is (0 and 1 will both hit one unit)
                {
                    Collider[] targetsInRange = Physics.OverlapSphere(nextTarget.transform.position, range, units);//find all units within a certain range of the first target
                    if (targetsInRange.Length >= 1)
                    {
                        nextTarget = targetsInRange[0].gameObject;
                        targetsToHitChain.Add(nextTarget);
                        
                        nextTarget.layer = 9;
                    }
                }

                if (targetsToHitChain.Count != 0)
                {
                    foreach (var target in targetsToHitChain)
                    {

                        try
                        {
                            target.GetComponent<Units>().Hit(damage, magic, slowMovement, slowTime, fireDamage, fireTime);//tells the unit it was hit and passes through all of the stats.
                        }
                        catch { };
                        try
                        {
                            target.GetComponent<Units1>().Hit(damage, magic, slowMovement, slowTime, fireDamage, fireTime);//tells the unit it was hit and passes through all of the stats.
                        }
                        catch { };
                        target.layer = 8;
                    }
                }
            }
            else
            {
                try
                {
                    target.GetComponent<Units>().Hit(damage, magic, slowMovement, slowTime, fireDamage, fireTime);//tells the unit it was hit and passes through all of the stats.
                }
                catch { };
                try
                {
                    target.GetComponent<Units1>().Hit(damage, magic, slowMovement, slowTime, fireDamage, fireTime);//tells the unit it was hit and passes through all of the stats.
                }
                catch { };
            }
        }
    }
}
