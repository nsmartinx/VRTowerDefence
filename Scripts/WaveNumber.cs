using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveNumber : MonoBehaviour
{
    public int waveNumber;
    public TextMeshProUGUI waveCounter;
    float i = 0;
    public GameObject gameManager;

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
            waveNumber = gameManager.GetComponent<WaveSpawner>().waveIndex;
            waveCounter.text = "Wave " + waveNumber;
            i = 0;
        }
    }
}
