using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Units1 : MonoBehaviour
{
    //all statistics of the unit
    public float health = 10f;//health of unit, 0=dead
    public float damage = 10f;//damge delt by unit, 0=no damage                                                       ////////////////
    public float armour = 0f;//resistance of unit, 0=no protection -1% of damage per level, compounded
    public float speed = 1f;//movemenet speed of unit, 0=no movement

    public PhotonView photonView;
    public GameObject healthManager;

    float effectiveArmour;
    float damageReduction;
    float damageTaken;
    float currentHealth;
    float slowTime;
    float slowMovement;
    float fireTimer;
    float fireTime;
    float fireDamage;


    private Transform target;
    private int waypointIndex = 0;

    public GameObject healthBar;

    private void Awake()
    {
        healthManager = GameObject.Find("HealthManager");
    }
    void Start()
    {
        target = Waypoints1.waypoints[0];//sets the target to the first waypoint
        currentHealth = health;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }

        if (waypointIndex <= Waypoints1.waypoints.Length)//if it has not reached the final waypoint
        {
            Vector3 direction = target.position - transform.position;//creates a vector3 towards the next waypoint
            transform.Translate(direction.normalized * speed * slowMovement * Time.deltaTime);//moves the unit towards the waypoint at the set speed

            if (Vector3.Distance(transform.position, target.position) < 0.2)//if the unit has reached the waypoint
            {
                GetNextWaypoint();
            }
        }
        else
        {
            PhotonNetwork.Destroy(gameObject);
            healthManager.GetComponent<HealthManager>().damage(damage, 2);
        }

        Slow();
        Fire();
    }

    void GetNextWaypoint()//sets target to the next waypoint in the list
    {
        waypointIndex++;//incremenets the waypoint count by 1
        if (waypointIndex < Waypoints1.waypoints.Length)//if it has not reached the final waypoint
            target = Waypoints1.waypoints[waypointIndex];//finds the next waypoint and sets target to that
    }

    public void Hit(float damageHit, float magicHit, float slowMovementHit, float slowTimeHit, float fireDamageHit, float fireTimeHit)
    {
        photonView.RPC("HitRPC", RpcTarget.All, damageHit, magicHit, slowMovementHit, slowTimeHit, fireDamageHit, fireTimeHit);
    }
    [PunRPC]
    void HitRPC(float damageHit, float magicHit, float slowMovementHit, float slowTimeHit, float fireDamageHit, float fireTimeHit)//is called from a tower when the tower hits the unit
    {
        effectiveArmour = armour * Mathf.Pow(.99f, magicHit);//the effective protection after taking into account magic damage (-1% of armour per magic (exponential))
        damageReduction = Mathf.Pow(.99f, effectiveArmour);//The damage reduction of the armour (-1% of damage per level (expoential))
        damageTaken = damageHit * damageReduction;//calculates the amount of damage the unit will take

        currentHealth -= damageTaken;//reduces the units health by the amount of damage they take;

        //photonView.RPC("ReduceHealth", RpcTarget.All, damageTaken);//reduces the health by the amount of damage taken

        slowTime = slowTimeHit;//starts a timer for the amount of time they are slowed for
        slowMovement = (100 - slowMovementHit) / 100;//the percentage that they are slowed

        fireTime = fireTimeHit;//starts a timer for the amount of time they are on fire for
        fireDamage = fireDamageHit;

        UpdateHealthBar();//updates the health bar of the unit
    }
    void Slow()
    {
        if (slowTime > 0)
        {
            slowTime -= Time.deltaTime;
        }
        else
        {
            slowMovement = 1;
        }
    }
    void Fire()
    {
        if (fireTimer >= 0)
        {
            fireTimer -= Time.deltaTime;
            fireTime -= Time.deltaTime;

            if (fireTimer <= 0)
            {
                fireTimer = 1;
                currentHealth -= fireDamage;
                UpdateHealthBar();
            }
        }
    }
    void UpdateHealthBar()
    {
        healthBar.GetComponent<HealthBar>().SetHealth(currentHealth / health);//tells the healthbar to update and passes through the current health of the unit (out of 1)
    }

    //[PunRPC]
    //void ReduceHealth(float damageTaken)
    //{
    //    currentHealth -= damageTaken;
    //}
}
