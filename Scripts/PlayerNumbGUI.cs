using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNumbGUI : MonoBehaviour
{
    public int playerNumSet;
    public TextMeshProUGUI playerNumDisplay;
    float i = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerNumSet = 0;
    }

    // Update is called once per frame
    void Update()
    {
        i += Time.deltaTime;

        if (i >= 1)
        {
            playerNumSet = gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<PlayerIdentifier>().playerNum;
            playerNumDisplay.text = "Player " + playerNumSet;
            i = 0;
        }
    }
}
