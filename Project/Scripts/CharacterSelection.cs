using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
	public GameObject characterSkins;
	public int selectedCharacter = 0;

    void Start()
    {
        foreach (Transform skin in characterSkins.transform) 
        {
            skin.gameObject.SetActive(false);
        }
        characterSkins.transform.GetChild(selectedCharacter).gameObject.SetActive(true);
    }

	public void NextCharacter()
	{
        characterSkins.transform.GetChild(selectedCharacter).gameObject.SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characterSkins.transform.childCount;
        characterSkins.transform.GetChild(selectedCharacter).gameObject.SetActive(true);
    }

    public void PreviousCharacter()
	{
        characterSkins.transform.GetChild(selectedCharacter).gameObject.SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characterSkins.transform.childCount;
        }
        characterSkins.transform.GetChild(selectedCharacter).gameObject.SetActive(true);
    }

    public void StartGame()
	{
        if (GameObject.FindGameObjectsWithTag("TensionMusic").Length > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("TensionMusic"));
        }
        PlayerPrefs.SetInt("selectedSkin", selectedCharacter);
        SceneManager.LoadScene("Game");
    }
}
