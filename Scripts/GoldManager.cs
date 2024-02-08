using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GoldManager : MonoBehaviour
{
    public int P1Gold = 1000, P2Gold = 1000;
    public PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addGoldP1(int gold)
    {
        photonView.RPC("addGoldRPC", RpcTarget.All, gold, 1);
    }
    public void addGoldP2(int gold)
    {
        photonView.RPC("addGoldRPC", RpcTarget.All, gold, 2);
    }
    [PunRPC]
    void addGoldRPC(int gold, int player)
    {
        if (player == 1)
            P1Gold += gold;
        else
            P2Gold += gold;
    }
}
