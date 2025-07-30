using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public RebirthData rebirthData;
    public TMP_Text[] upgradeCostTexts;
    public TMP_Text[] upgradeLevelTexts;

    private int[] upgradeBaseCosts = { 50, 200, 5000, 10000, 20000, 50000 };

    private float[] critChances = { 0.15f, 0.30f, 0.45f, 0.60f, 0.75f, 0.90f };
    private int maxHelpers = 4;
    private float[] intervals = { 0.9f, 0.8f, 0.7f, 0.6f, 0.5f };

    [SerializeField] private Button[] upgradeButtons;

    private readonly int[] maxLevels = { 15, 6, 5, 4, 5, 4 };
    private readonly int[] baseCosts = { 50, 200, 5000, 10000, 20000, 50000 };
    private readonly float[] costScales = { 2f, 3f, 2.5f, 2f, 2.5f, 3f };

    [Header("Sounds")]
    public AudioClip upgradeSound;
    private AudioSource audioSource;


    void OnEnable()
    {
        audioSource = FindObjectOfType<ButtonSoundManager>().GetComponent<AudioSource>();
        UpdateAllUpgradeText();
    }

    public void BuyUpgrade(int index)
    {
        if (!IsUpgradeUnlocked(index)) return;

        int cost = upgradeBaseCosts[index] * (int)Mathf.Pow(2, rebirthData.upgradeLevels[index]);

        if (rebirthData.currentEnergy < cost) return;

        rebirthData.currentEnergy -= cost;
        rebirthData.upgradeLevels[index]++;

        switch (index)
        {
            case 0:
                rebirthData.baseClickPower += 1;
                break;
            case 1:
                rebirthData.critClickChance = critChances[Mathf.Min(rebirthData.upgradeLevels[index] - 1, critChances.Length - 1)];
                break;
            case 2:
                rebirthData.clickMultiplier *= 1.5f;
                break;
            case 3:
                if (!rebirthData.blobHelperUnlocked)
                {
                    rebirthData.blobHelperUnlocked = true;
                    rebirthData.autoclickInterval = 1f; // 1 click per second
                }

                if (rebirthData.blobHelpersOwned < maxHelpers)
                {
                    rebirthData.blobHelpersOwned++;
                    rebirthData.UpdateBlobHelpers();
                }
                break;

            case 4:
                int level = rebirthData.upgradeLevels[index];
                rebirthData.autoclickInterval = intervals[Mathf.Clamp(level - 1, 0, intervals.Length - 1)];
                break;

            case 5:
                rebirthData.doubleTapMultiplier = 2 * (int)Mathf.Pow(2, rebirthData.upgradeLevels[index] - 1); // x2, x4, x8, x16 
                break;
        }


        rebirthData.UpdateUI();
        UpdateUpgradeText(index);

        if (upgradeSound != null && audioSource != null)
        {
            audioSource.pitch = 1f; 
            audioSource.PlayOneShot(upgradeSound);
        }

    }

    bool IsUpgradeUnlocked(int index)
    {
        switch (index)
        {
            case 2: return rebirthData.currentRebirth >= 1;  // Crit Clicks
            case 3: return rebirthData.currentRebirth >= 2;  // Blob Helper
            case 4: return rebirthData.currentRebirth >= 3;  // Autoclick Speed
            case 5: return rebirthData.currentRebirth >= 4;  // Double Tap
            default: return true;
        }
    }

    int GetRequiredRebirth(int index)
    {
        switch (index)
        {
            case 2: return 1;
            case 3: return 2;
            case 4: return 3;
            case 5: return 4;
            default: return 0;
        }
    }


    void UpdateUpgradeText(int index)
    {
        int level = rebirthData.upgradeLevels[index];
        bool isMax = level >= maxLevels[index];

        if (!IsUpgradeUnlocked(index))
        {
            upgradeCostTexts[index].text = $"Unlocks at Rebirth {GetRequiredRebirth(index)}";
            upgradeLevelTexts[index].text = "Locked";
            upgradeButtons[index].interactable = false;
            return;
        }

        if (isMax)
        {
            upgradeCostTexts[index].text = "MAX";
            upgradeLevelTexts[index].text = $"Level: {level}";
            upgradeButtons[index].interactable = false;
            return;
        }

        int cost = Mathf.RoundToInt(baseCosts[index] * Mathf.Pow(costScales[index], level));
        upgradeCostTexts[index].text = $"Cost: {cost}";
        upgradeLevelTexts[index].text = $"Level: {level}";

        upgradeButtons[index].interactable = rebirthData.currentEnergy >= cost;
    }



    public void UpdateAllUpgradeText()
    {
        for (int i = 0; i < rebirthData.upgradeLevels.Length; i++)
            UpdateUpgradeText(i);
    }
}
