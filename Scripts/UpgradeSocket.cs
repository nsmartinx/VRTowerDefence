using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using Photon.Pun;

public class UpgradeSocket : MonoBehaviour
{
    public Material white, black;
    public GameObject towerManager;

    public PhotonView photonView;

    Material used;
    GameObject model, tower;
    GameObject modelBase, modelBody, modelWeapon, towerBase, towerBody, towerWeapon;

    List<GameObject> models = new List<GameObject>();
    List<GameObject> towers = new List<GameObject>();

    public TextMeshProUGUI damage, range, attackSpeed, AoE, chain, magic, slowMovement, slowMovementTime, fireDamage, fireDamageTime;
    public GameObject emptyStatPage;

    public void FindTower()
    {
        models = towerManager.GetComponent<TowerManagerNew>().models;//gets the list of models from TowerManager
        towers = towerManager.GetComponent<TowerManagerNew>().towers;//gets the list of towers from TowerManager
        model = GetComponent<XRSocketInteractor>().selectTarget.gameObject;
        tower = towers[models.IndexOf(model)]; //finds the tower prefab in at the same index as the model

        modelBase = model.transform.Find("ModelBase").gameObject;//finds all the 3 components of both the model and the colour
        modelBody = model.transform.Find("ModelBody").gameObject;
        modelWeapon = model.transform.Find("ModelWeapon").gameObject;
        towerBase = tower.transform.Find("TowerBase").gameObject;
        towerBody = tower.transform.Find("TowerBody").gameObject;
        towerWeapon = tower.transform.Find("TowerWeapon").gameObject;
    }
    public void Upgrade(int colourPart)//change this to the value of upgrades (i.e. seperate values for damge and range upgrades)
    {
        photonView.RPC("UpgradeRPC", RpcTarget.All, colourPart);
        /*
        if (colourPart <= 3)
            used = black;
        if (colourPart >= 4)
            used = white;

        FindTower();


        if(colourPart == 1|| colourPart == 4)
        {
            modelBase.GetComponent<MeshRenderer>().material = used;
            towerBase.GetComponent<MeshRenderer>().material = used;
        }
        if (colourPart == 2 || colourPart == 5)
        {
            modelBody.GetComponent<MeshRenderer>().material = used;
            towerBody.GetComponent<MeshRenderer>().material = used;
        }
        if (colourPart == 3 || colourPart == 6)
        {
            modelWeapon.GetComponent<MeshRenderer>().material = used;
            towerWeapon.GetComponent<MeshRenderer>().material = used;
        } 
        */
    }
    [PunRPC]
    void UpgradeRPC(int colourPart)
    {
        if (colourPart <= 3)
            used = black;
        if (colourPart >= 4)
            used = white;

        FindTower();


        if (colourPart == 1 || colourPart == 4)
        {
            modelBase.GetComponent<MeshRenderer>().material = used;
            towerBase.GetComponent<MeshRenderer>().material = used;
        }
        if (colourPart == 2 || colourPart == 5)
        {
            modelBody.GetComponent<MeshRenderer>().material = used;
            towerBody.GetComponent<MeshRenderer>().material = used;
        }
        if (colourPart == 3 || colourPart == 6)
        {
            modelWeapon.GetComponent<MeshRenderer>().material = used;
            towerWeapon.GetComponent<MeshRenderer>().material = used;
        }
    }


    public void SetStatText(bool towerPresent)//updates the text that displays the stats of the tower currently in the upgrade socket. (bool true means a tower is in the socket, false means a tower is not in the socket)
    {
        if (!towerPresent)//when the tower is removed
        {
            emptyStatPage.SetActive(false);//disables all of the stat text
        }
        else
        {
            FindTower();
            emptyStatPage.SetActive(true);//enables all of the stat text
            range.text = "Range: " + tower.GetComponent<Tower>().range;//updates each of the stats
            attackSpeed.text = "AttackSpeed: " + tower.GetComponent<Tower>().attackSpeed.ToString();
            damage.text = "Damage: " + tower.GetComponent<Tower>().damage.ToString();
            AoE.text = "AoE: " + tower.GetComponent<Tower>().areaOfEffect.ToString();
            chain.text = "Chain: " + tower.GetComponent<Tower>().chain.ToString();
            magic.text = "Magic: " + tower.GetComponent<Tower>().magic.ToString();
            slowMovement.text = "SlowMovement: " + tower.GetComponent<Tower>().slowMovement.ToString();
            slowMovementTime.text = "SlowTime: " + tower.GetComponent<Tower>().slowTime.ToString();
            fireDamage.text = "FireDamage: " + tower.GetComponent<Tower>().fireDamage.ToString();
            fireDamageTime.text = "FireTime: " + tower.GetComponent<Tower>().fireTime.ToString();
        }
    }

    public void UpgradeAttackSpeed(float upgradeAmount)//increase the specific stat by the ammount specified (negative for decreasing)
    {
        FindTower();
        tower.GetComponent<Tower>().attackSpeed += upgradeAmount;//increases the stats of the tower in the upgrade socket. (or decreases)
        SetStatText(true);//updates teh stat text
    }
    public void UpgradeRange(float upgradeAmount)
    {
        FindTower();
        tower.GetComponent<Tower>().range += upgradeAmount;
        SetStatText(true);
    }
    public void UpgradeAreaOfEffect(float upgradeAmount)
    {
        FindTower();
        tower.GetComponent<Tower>().areaOfEffect += upgradeAmount;
        SetStatText(true);
    }
    public void UpgradeChain(int upgradeAmount)
    {
        FindTower();
        tower.GetComponent<Tower>().chain += upgradeAmount;
        SetStatText(true);
    }
    public void UpgradeDamage(float upgradeAmount)
    {
        FindTower();
        tower.GetComponent<Tower>().damage += upgradeAmount;
        SetStatText(true);
    }
    public void UpgradeMagic(float upgradeAmount)
    {
        FindTower();
        tower.GetComponent<Tower>().magic += upgradeAmount;
        SetStatText(true);
    }
    public void UpgradeSlowMovement(float upgradeAmount)
    {
        FindTower();
        tower.GetComponent<Tower>().slowMovement += upgradeAmount;
        SetStatText(true);
    }
    public void UpgradeSlowTime(float upgradeAmount)
    {
        FindTower();
        tower.GetComponent<Tower>().slowTime += upgradeAmount;
        SetStatText(true);
    }
    public void UpgradeFireDamage(float upgradeAmount)
    {
        FindTower();
        tower.GetComponent<Tower>().fireDamage += upgradeAmount;
        SetStatText(true);
    }
    public void UpgradeFireTime(float upgradeAmount)
    {
        FindTower();
        tower.GetComponent<Tower>().fireTime += upgradeAmount;
        SetStatText(true);
    }
}
