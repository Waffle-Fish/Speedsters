using TMPro;
using UnityEngine;

public class playerCountSelect : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropdown;

    [SerializeField]
    GameObject[] scenes;
    int originalLevelIndex = 0;

    public void changeSelect(CharacterSelectMenu menu)
    {
        originalLevelIndex = CharacterSelectMenu.Instance.sceneIndex;
        CharacterSelectMenu.Instance.SetIndex(originalLevelIndex);
        if (dropdown.value== 1) 
        {
            scenes[0].SetActive(true);
            scenes[1].SetActive(true);
            scenes[2].SetActive(false);
            menu.setIsPlayer2(false);
            CharacterSelectMenu.Instance.SetIndex(originalLevelIndex+2);
        }
        else if (dropdown.value == 2)
        {
            scenes[0].SetActive(true);
            scenes[1].SetActive(false);
            scenes[2].SetActive(true);
            menu.setIsPlayer2(true);
        }
        else if (dropdown.value == 0)
        {
            scenes[0].SetActive(false);
            scenes[1].SetActive(false);
            scenes[2].SetActive(false);
        }
        
    }
}
