using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class History : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("SkinSelection");
    }
}
