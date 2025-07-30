using TMPro;
using UnityEngine;

public class LoreManager : MonoBehaviour
{
    public TMP_Text loreText;
    public GameObject lorePanel;
    public int LoreCount => loreMessages.Length;

    private string[] loreMessages = {
        "Wow, you were just born! You are now a blob and you have no idea what's going on. Also you have an itch on your back and you can't reach it!",
        "Wow you've just been reborn! And now you have arms?!?! This is great because you can finally get that itch on your back. It's been ages...",
        "You are reborn once again — this time with legs! The problem is, you have no clue how to use them. You can barely even stand up, but hey, it's progress.",
        "BOOM! Reborn yet again, and your body has taken form. You look like something out of Mr. Bloblympia. Buff. Slimy. Shredded. Your new nickname: Blobbie Coleman.",
        "Hmmmmm… another rebirth, and now you’re rocking armor. You don’t know who gave it to you, but it fits perfectly and makes you look real cool. At this point, you have no idea why you keep getting reborn, but you know what, you're not mad.",
        "Wow. Just wow. You are officially reborn into the omega blob. All the male blobs want to be you and all the lady blobs want to marry you. You are truly the blobest of blobs out there."
    };

    [SerializeField]
    private string[] loreTitles =
    {
        "The Blob is Born. A Slimy Legend Awakens.",
        "Arms of Adventure. Ready to Grab the World.",
        "Legs of Destiny. Marching Toward Greatness.",
        "Blob Gains a Body. Behold the True Form.",
        "Blob and Shining Armor. Defender of All Blobs.",
        "The Omega Blob. The Blobest Blob of them all."
    };


    public void ShowLore(int rebirth)
    {
        lorePanel.SetActive(true);
        loreText.text = loreMessages[Mathf.Clamp(rebirth, 0, loreMessages.Length - 1)];
    }

    public string GetLore(int rebirth)
    {
        if (rebirth < 0 || rebirth >= loreMessages.Length)
            return "???";
        return loreMessages[rebirth];
    }

    public string GetTitle(int rebirth)
    {
        if (rebirth < 0 || rebirth >= loreTitles.Length)
            return "???";
        return loreTitles[rebirth];
    }

}
