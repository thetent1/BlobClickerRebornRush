using UnityEngine;
using TMPro;

public class LorePanel : MonoBehaviour
{
    public TMP_Text loreText;
    public GameObject panel;

    public void ShowLore(string lore)
    {
        loreText.text = lore;
        panel.SetActive(true);
    }

    public void CloseLore()
    {
        panel.SetActive(false);
    }
}
