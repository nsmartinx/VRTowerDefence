using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthManager : MonoBehaviour
{
    public float P1health = 100, P2health = 100;
    public PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(float damage, int player)
    {
        photonView.RPC("damageRPC", RpcTarget.All, damage, player);
    }
    [PunRPC]
    void damageRPC(float damage, int player)
    {
        if (player == 1)
            P1health -= damage;
        else
            P2health -= damage;
    }
}
