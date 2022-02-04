using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text healthAmount, killedAmount, roundNumber, currentWeapon, ammoAmount;
    public GameObject playerUIObj;

    void Start()
    {
        playerUIObj.SetActive(true);
    }

    public void DisableUI()
    {
        playerUIObj.SetActive(false);
    }
}
