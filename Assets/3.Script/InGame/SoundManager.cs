using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip swing;
    [SerializeField] private AudioClip step;
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip jump;

    public void PlaySwing()
    {
        audioSource.PlayOneShot(swing);
    }

    public void PlayStep()
    {
        audioSource.PlayOneShot(step);
    }
    public void PlayDash()
    {
        audioSource.PlayOneShot(dash);
    }
    public void PlayJump()
    {
        audioSource.PlayOneShot(jump);
    }
}
