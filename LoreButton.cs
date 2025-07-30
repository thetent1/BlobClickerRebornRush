using UnityEngine;

public class LoreButton : MonoBehaviour
{
    public GameObject lorePanel;

    public void ToggleLore()
    {
        lorePanel.SetActive(!lorePanel.activeSelf);
    }
}
