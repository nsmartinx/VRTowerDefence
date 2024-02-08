using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class SocketTower : MonoBehaviour
{
    XRBaseInteractable modelInteractable;
    int modelInteractableID;
    GameObject model;
    GameObject tower;
    GameObject towerPlace;
    public GameObject towerManager;

    public PhotonView photonView;

    int socketID;

    List<GameObject> models = new List<GameObject>();
    List<GameObject> towers = new List<GameObject>();
    public void towerPlaced(GameObject socket)
    {
        socketID = socket.GetComponent<PhotonView>().ViewID;//gets the photonviewID of the coket
        modelInteractableID = PhotonView.Find(socketID).gameObject.GetComponent<XRSocketInteractor>().selectTarget.gameObject.GetComponent<PhotonView>().ViewID;//gets the phton view ID of the XRBaseInteractable that is currently in the socket (the model)
        photonView.RPC("SetID", RpcTarget.All, socketID, modelInteractableID);//calls the remote procedure call to set thge viewID of both the sokcet and the model
        photonView.RPC("CreateTower", RpcTarget.All, socketID);//calls the RPC to tell all clients to activate the tower and move it to the right spot

    }

    public void towerRemoved(GameObject socket)
    {
        socketID = socket.GetComponent<PhotonView>().ViewID;//gets the viewID of the socket
        
        photonView.RPC("RemoveTower", RpcTarget.All, socketID);//tells all clients to remove the tower
    }

    [PunRPC]
    void SetID(int socketIDPass, int modelInteractableIDPass)
    {
        socketID = socketIDPass;//sets the IDs for the socket and model interactable for all clients
        modelInteractableID = modelInteractableIDPass;
    }

    [PunRPC]
    void RemoveTower(int socketViewID)
    {
        GameObject socket = PhotonView.Find(socketViewID).gameObject;
        
        models = towerManager.GetComponent<TowerManagerNew>().models;//gets the list of models from TowerManager
        towers = towerManager.GetComponent<TowerManagerNew>().towers;//gets the list of towers from TowerManager

        Debug.Log(modelInteractableID);
        model = PhotonView.Find(modelInteractableID).gameObject;//sets model to the gameobject the Interactor is attached to 
        tower = towers[models.IndexOf(model)]; //finds the tower prefab at the same index as the model

        if (tower != null)
            tower.SetActive(false);
    }

    [PunRPC]
    void CreateTower(int socketViewID)
    {
        GameObject socket = PhotonView.Find(socketViewID).gameObject;
        models = towerManager.GetComponent<TowerManagerNew>().models;//gets the list of models from TowerManager
        towers = towerManager.GetComponent<TowerManagerNew>().towers;//gets the list of towers from TowerManager

        model = PhotonView.Find(modelInteractableID).gameObject;//sets model to the gameobject the Interactor is attached to

        tower = towers[models.IndexOf(model)]; //finds the tower prefab at the same index as the model


        tower.SetActive(true);//activates the tower
        towerPlace = PhotonView.Find(socketViewID).gameObject.transform.parent.gameObject;//the location to place the tower at
        tower.transform.position = towerPlace.transform.position;//moves the tower to that location
        tower.transform.rotation = towerPlace.transform.rotation; //rotates the tower to the rotation of towerplace
    }
}
