using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip swing;
    [SerializeField] private AudioClip step;
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip town;

    private void Awake()
    {
        TryGetComponent(out audioSource);
    }

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
    public void PlayHit()
    {
        audioSource.PlayOneShot(hit);
    } 
    public void PlayTown()
    {
        audioSource.clip = town;
        audioSource.Play();
    }
}
