using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{

    private bool isSoundOn = true;
    private bool isMusicOn = true;

    public void SelectSound() {
        isSoundOn = !isSoundOn;
        AudioManager.Instance.PlaySound(isSoundOn);
    }

    public void SelectMusic() {
        isMusicOn = !isMusicOn;
        AudioManager.Instance.PlayMusic(isMusicOn);
    }
}
