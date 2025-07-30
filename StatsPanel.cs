using UnityEngine;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    public RebirthData rebirthData;

    [Header("Text Fields")]
    public TMP_Text clickPowerText;
    public TMP_Text critChanceText;
    public TMP_Text clickMultiplierText;
    public TMP_Text blobHelpersText;
    public TMP_Text autoClickText;
    public TMP_Text doubleTapText;

    void OnEnable()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        // Effective click power includes rebirth multiplier
        int effectiveClick = rebirthData.baseClickPower * rebirthData.GetCurrentMultiplier();

        clickPowerText.text = $"Click Power: {effectiveClick} (Base {rebirthData.baseClickPower} × Rebirth {rebirthData.GetCurrentMultiplier()}x)";

        critChanceText.text = $"Crit Chance: {(rebirthData.critClickChance * 100f):F0}%";

        clickMultiplierText.text = $"Click Multiplier: {rebirthData.clickMultiplier:F1}x";

        blobHelpersText.text = rebirthData.blobHelperUnlocked
            ? $"Blob Helpers: {rebirthData.blobHelpersOwned}/{4}"
            : "Blob Helpers: Locked";

        autoClickText.text = rebirthData.blobHelperUnlocked
            ? $"Auto Click Speed: Every {rebirthData.autoclickInterval:F2}s"
            : "Auto Click Speed: Locked";

        doubleTapText.text = rebirthData.currentRebirth >= 4
            ? $"Double Tap Bonus: {rebirthData.doubleTapMultiplier}x every 10th click"
            : "Double Tap Bonus: Locked";
    }
}
