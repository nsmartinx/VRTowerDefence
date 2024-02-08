using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthScript : MonoBehaviour
{
    float health;
    public TextMeshProUGUI healthNumber;
    float i = 0;
    public GameObject healthBar;
    public GameObject healthManager;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        i += Time.deltaTime;

        if (i >= 1)
        {
            GetHealth();
            healthNumber.text = health + " Health";
            healthBar.GetComponent<HealthBar>().SetHealth(health/100);
            i = 0;
        }
    }
    void GetHealth()
    {
        if (gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<PlayerIdentifier>().playerNum == 1)
            health = healthManager.GetComponent<HealthManager>().P1health;
        else if (gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<PlayerIdentifier>().playerNum == 2)
            health = healthManager.GetComponent<HealthManager>().P2health;
    }
}
