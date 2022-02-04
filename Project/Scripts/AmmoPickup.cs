using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public float startTimeA, startTimeB;
    public float repeatRateA, repeatRateB;
    GameObject ammoBoxA, ammoBoxB;

    // Start is called before the first frame update
    void Start()
    {
        ammoBoxA = transform.GetChild(0).gameObject;
        ammoBoxB = transform.GetChild(1).gameObject;
        ammoBoxA.SetActive(false);
        ammoBoxB.SetActive(false);
        InvokeRepeating("AmmoBoxA", startTimeA, repeatRateA);
        InvokeRepeating("AmmoBoxB", startTimeB, repeatRateB);
    }

    void AmmoBoxA()
    {
        if( !ammoBoxA.activeSelf )
        {
            ammoBoxA.SetActive(true);
        }
    }

    void AmmoBoxB()
    {
        if (!ammoBoxB.activeSelf)
        {
            ammoBoxB.SetActive(true);
        }
    }
}
