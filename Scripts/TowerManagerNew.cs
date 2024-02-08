using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class TowerManagerNew : MonoBehaviour
{
    public Transform towerPrefab;
    public Transform modelPrefabSpawn;
    public PhotonView photonView;
    public Transform modelPrefabSpawn1;

    GameObject towerModel;

    int towerModelPhotonViewID;

    public List<GameObject> prefabs = new List<GameObject>();
    public List<GameObject> models = new List<GameObject>();
    public List<GameObject> towers = new List<GameObject>();

    public void SpawnTowerModel(int pos)//called when a new tower is being created. Adds it to a list of models
    {
        if (pos == 0)
            towerModel = PhotonNetwork.Instantiate("TowerModel", modelPrefabSpawn.position, modelPrefabSpawn.rotation);//creates a TowerModel
        else if (pos == 1)
            towerModel = PhotonNetwork.Instantiate("TowerModel", modelPrefabSpawn1.position, modelPrefabSpawn1.rotation);

        towerModelPhotonViewID = towerModel.GetComponent<PhotonView>().ViewID;

        photonView.RPC("AddToList", RpcTarget.All, towerModelPhotonViewID);

    }

    [PunRPC]
    void AddToList(int towerModelPhotonViewID)
    {
        towerModel = PhotonView.Find(towerModelPhotonViewID).gameObject;
        prefabs.Add(towerModel);//adds the TowerModel to the list of prefabs
        models.Add(towerModel.transform.Find("Model").gameObject);//gets the child, model of it and adds it to a list
        towers.Add(towerModel.transform.Find("Tower").gameObject);//gets the child, tower of it and adds it to a list
    }
}
