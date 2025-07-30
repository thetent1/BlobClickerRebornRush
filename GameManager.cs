using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Title Screen")]
    public GameObject titleScreen;

    [Header("Gameplay UI & Objects")]
    public GameObject backgroundImage;
    public GameObject blobImage;             
    public GameObject blobHelpersContainer;  
    public GameObject energyText;
    public GameObject rebirthText;
    public GameObject clickButton;
    public GameObject rebirthButton;
    public GameObject upgradeButton;
    public GameObject statsButton;
    public GameObject lorebookButton;
    public GameObject blob1;
    public GameObject blob2;
    public GameObject blob3;
    public GameObject blob4;

    public LoreManager loreManager;

    void Start()
    {
        blobImage.SetActive(true);
        blobHelpersContainer.SetActive(true);
        blob1.SetActive(true);
        blob2.SetActive(true);
        blob3.SetActive(true);
        blob4.SetActive(true);
        SetGameplayActive(false);
    }

    public void StartGame()
    {
        if (titleScreen != null)
            titleScreen.SetActive(false);

        if (blob1 != null)
            blob1.SetActive(false);

        if (blob2 != null)
            blob2.SetActive(false);

        if (blob3 != null)
            blob3.SetActive(false);

        if (blob4 != null)
            blob4.SetActive(false);

        SetGameplayActive(true);

        if (loreManager != null)
            loreManager.ShowLore(0);
    }

    private void SetGameplayActive(bool active)
    {
        backgroundImage.SetActive(active);
        energyText.SetActive(active);
        rebirthText.SetActive(active);
        //clickButton.SetActive(active);
        rebirthButton.SetActive(active);
        upgradeButton.SetActive(active);
        statsButton.SetActive(active);
        lorebookButton.SetActive(active);
    }
}
