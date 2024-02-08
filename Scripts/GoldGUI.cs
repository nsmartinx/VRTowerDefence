using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldGUI : MonoBehaviour
{
    public int goldCount;
    public TextMeshProUGUI goldCounter;
    float i = 0;
    public GameObject goldManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        i += Time.deltaTime;

        if (i >= 1)
        {
            GetGold();
            goldCounter.text = goldCount + " Gold";
            i = 0;
        }
    }
    void GetGold()
    {
        if (gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<PlayerIdentifier>().playerNum == 1)
            goldCount = goldManager.GetComponent<GoldManager>().P1Gold;
        else if (gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<PlayerIdentifier>().playerNum == 2)
            goldCount = goldManager.GetComponent<GoldManager>().P2Gold;
    }
}
