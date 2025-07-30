using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoreBookManager : MonoBehaviour
{
    public LoreManager loreManager;
    public GameObject loreBookPanel;
    public TMP_Text loreText;
    public TMP_Text loreTitleText; 
    public Button leftArrow;
    public Button rightArrow;

    private int currentPage = 0;
    private RebirthData rebirthData;

    void Start()
    {
        rebirthData = FindObjectOfType<RebirthData>();
        rightArrow.onClick.AddListener(PrevPage);
        leftArrow.onClick.AddListener(NextPage);
        UpdatePage();
    }

    void UpdatePage()
    {
        int maxUnlocked = rebirthData.currentRebirth;

        if (currentPage > maxUnlocked)
        {
            loreText.text = "???";
            loreTitleText.text = "???";
        }
        else
        {
            loreText.text = loreManager.GetLore(currentPage);
            loreTitleText.text = loreManager.GetTitle(currentPage);
        }


        rightArrow.interactable = currentPage > 0;
        leftArrow.interactable = currentPage < loreManager.LoreCount - 1;
    }


    public void CloseLoreBook()
    {
        loreBookPanel.SetActive(false);
    }

    void NextPage()
    {
        if (currentPage < loreManager.LoreCount - 1)
        {
            currentPage++;
            UpdatePage();
        }
    }

    void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }
}

