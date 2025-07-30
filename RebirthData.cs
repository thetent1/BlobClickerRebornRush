using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RebirthData : MonoBehaviour
{
    public int currentRebirth = 0;
    public int currentEnergy = 0;
    public int baseClickPower = 1;
    private int clickCount = 0;

    public int maxRebirths = 5;
    public int[] energyRequiredPerRebirth = { 500, 5000, 50000, 500000, 5000000 };
    public Image blobImage;

    public TMP_Text energyText;
    public TMP_Text rebirthText;
    public FloatingTextSpawner floatingTextSpawner;

    private float autoclickTimer = 0f;
    public float autoclickInterval = 999f; // Start locked
    public bool blobHelperUnlocked = false;

    [Header("Blob Helpers")]
    public GameObject[] blobHelpers;
    public int blobHelpersOwned = 0;

    [Header("Helper Anchors")]
    public Transform[] helperAnchors;

    [Header("Sounds")]
    public AudioClip rebirthSound;
    public AudioClip winSound;
    private AudioSource audioSource;

    private Color[] helperColors = new Color[]
{
    Color.blue,    // Helper 1
    Color.magenta, // Helper 2 (pink)
    new Color(0.6f, 0f, 0.8f), // Helper 3 (purple)
    new Color(1f, 0.5f, 0f)    // Helper 4 (orange)
    };

    [Header("Blob & Background Forms")]
    public GameObject[] blobForms; 
    public GameObject[] backgroundForms; 

    //for upgrade panel
    public float clickMultiplier = 1f;
    public float critClickChance = 0f;
    public int doubleTapMultiplier = 1;

    public int[] upgradeLevels = new int[6]; // Resets every rebirth
    public int GetCurrentMultiplier() => Mathf.Clamp(currentRebirth + 1, 1, 6); // Rebirth 0 = x1, Rebirth 5 = x6
    public int GetEffectiveClickPower() => baseClickPower * GetCurrentMultiplier();

    public bool CanRebirth() => currentRebirth < maxRebirths && currentEnergy >= energyRequiredPerRebirth[currentRebirth];

    public Button exitButton;

    void Start()
    {
        UpdateBlobHelpers(); // make sure they're off at start
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        autoclickTimer += Time.deltaTime;
        if (autoclickTimer >= autoclickInterval && blobHelperUnlocked)
        {
            autoclickTimer = 0f;
            for (int i = 0; i < blobHelpersOwned; i++)
            {
                int gained = GainEnergy(false); 

                if (blobHelpers[i] != null && floatingTextSpawner != null)
                {
                    string displayText = $"+{gained} Energy";
                    Color textColor = helperColors[i % helperColors.Length];
                    floatingTextSpawner.SpawnFloatingText(displayText, textColor, helperAnchors[i].position, blobHelpers[i].transform);
                }
            }
        }
    }

    public int GainEnergy(bool showFloatingText = true)
    {
        int finalClick = Mathf.RoundToInt(baseClickPower * clickMultiplier * GetCurrentMultiplier());

        bool isCrit = false;
        bool isDoubleTap = false;

        if (Random.value < critClickChance)
        {
            finalClick *= 2;
            isCrit = true;
        }

        clickCount++;
        if (doubleTapMultiplier > 1 && clickCount % 10 == 0)
        {
            finalClick *= doubleTapMultiplier;
            isDoubleTap = true;
        }

        currentEnergy += finalClick;
        UpdateUI();

        if (showFloatingText && floatingTextSpawner != null)
        {
            string displayText = $"+{finalClick} Energy";

            Color textColor = Color.green;
            if (isDoubleTap)
                textColor = Color.red;     
            else if (isCrit)
                textColor = Color.yellow; 

            floatingTextSpawner.SpawnFloatingText(displayText, textColor);
        }

        return finalClick;
    }


    public void DoRebirth()
    {
        currentRebirth++;

        if (audioSource != null)
        {
            if (currentRebirth < maxRebirths)
            {
                if (rebirthSound != null)
                    audioSource.PlayOneShot(rebirthSound);
            }
            else if (currentRebirth == maxRebirths)
            {
                if (winSound != null)
                    audioSource.PlayOneShot(winSound);
            }
        }

        currentEnergy = 0;

        if (currentRebirth >= 3)
        {
            blobHelperUnlocked = true;
            autoclickInterval = 4f - (currentRebirth - 3);
            autoclickInterval = Mathf.Max(0.2f, autoclickInterval);
        }

        // reset upgrades
        for (int i = 0; i < upgradeLevels.Length; i++)
            upgradeLevels[i] = 0;

        baseClickPower = 1;
        clickMultiplier = 1f;
        critClickChance = 0f;
        doubleTapMultiplier = 1;
        blobHelpersOwned = 0;
        blobHelperUnlocked = false;
        autoclickInterval = 999f;

        UpdateBlobHelpers();
        UpdateUI();

        // clamp index so it never goes out of bounds
        int formIndex = Mathf.Min(currentRebirth, blobForms.Length - 1);
        ActivateBlobAndBG(formIndex);

        var loreManager = FindObjectOfType<LoreManager>();
        if (loreManager != null)
        {
            loreManager.ShowLore(currentRebirth);
        }

        if (currentRebirth == maxRebirths)
            WinGame();
    }

    public void UpdateUI()
    {
        energyText.text = $"Energy: {currentEnergy} / {energyRequiredPerRebirth[Mathf.Min(currentRebirth, energyRequiredPerRebirth.Length - 1)]}";
        rebirthText.text = $"Rebirths: {currentRebirth}";
    }

    public void UpdateBlobHelpers()
    {
        for (int i = 0; i < blobHelpers.Length; i++)
        {
            // Active if i < owned
            blobHelpers[i].SetActive(i < blobHelpersOwned);
        }
    }

    void ActivateBlobAndBG(int index)
    {
        for (int i = 0; i < blobForms.Length; i++)
            blobForms[i].SetActive(i == index);

        for (int i = 0; i < backgroundForms.Length; i++)
            backgroundForms[i].SetActive(i == index);
    }

    void WinGame()
    {
        energyText.text = "FINAL FORM UNLOCKED";
        rebirthText.text = "You Win!";
        //FindObjectOfType<ClickButton>().enabled = false;
        //FindObjectOfType<RebirthButton>().enabled = false;

        if (MusicManager.Instance != null && MusicManager.Instance.audioSource != null)
        {
            MusicManager.Instance.audioSource.Stop();
        }

        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            if (gm.blob1 != null)
            {
                gm.blob1.SetActive(true);
                gm.blob1.GetComponent<BlobJump>().enabled = true;
            }
            if (gm.blob2 != null)
            {
                gm.blob2.SetActive(true);
                gm.blob2.GetComponent<BlobJump>().enabled = true;
            }
            if (gm.blob3 != null)
            {
                gm.blob3.SetActive(true);
                gm.blob3.GetComponent<BlobJump>().enabled = true;
            }
            if (gm.blob4 != null)
            {
                gm.blob4.SetActive(true);
                gm.blob4.GetComponent<BlobJump>().enabled = true;
            }
        }


        if (exitButton != null)
            exitButton.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // stops play mode in editor
        #else
            Application.Quit(); // works in a build
        #endif
    }

}
