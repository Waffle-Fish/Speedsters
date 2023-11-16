using TMPro;
using UnityEngine;

public class playerCountSelect : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropdown;

    [SerializeField]
    GameObject[] scenes;

    public void changeSelect(MainMenu menu)
    {
        if (dropdown.value== 1) 
        {
            scenes[0].SetActive(true);
            scenes[1].SetActive(true);
            scenes[2].SetActive(false);
            menu.setIsPlayer2(false);
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
