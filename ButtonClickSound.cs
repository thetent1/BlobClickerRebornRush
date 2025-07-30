using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    private Button button;
    private AudioSource audioSource;

    void Awake()
    {
        button = GetComponent<Button>();

        audioSource = FindObjectOfType<ButtonSoundManager>().GetComponent<AudioSource>();

        button.onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        if (ButtonSoundManager.Instance != null && ButtonSoundManager.Instance.clickClip != null)
        {
            var audioSource = ButtonSoundManager.Instance.GetAudioSource();
            audioSource.PlayOneShot(ButtonSoundManager.Instance.clickClip);
        }
    }

}
