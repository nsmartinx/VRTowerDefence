using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Gradient gradient;
    public Image fill;

    public PhotonView photonView;

    private void Start()
    {
        SetHealth(1);//sets the health bar to be full
    }
    public void SetHealth(float health)
    {
        healthBar.value = health;//updates the health bar value
        fill.color = gradient.Evaluate(health);//upudates the health bar colour
        //photonView.RPC("UpdateHealthBar", RpcTarget.All, health);
    }

    //[PunRPC]
    //void UpdateHealthBar(float healthToReduce)
    //{
    //    healthBar.value = healthToReduce;//updates the health bar value
    //    fill.color = gradient.Evaluate(healthToReduce);//updates the health bar colour
    //}
}
