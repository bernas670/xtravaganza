using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public Slider musicSlider, sfxSlider, sensSlider;


    FMOD.Studio.Bus _music, _sfx;

    float _musicVol = 0.5f;
    float _sfxVol = 0.5f;
    float _mouseSens = 5f;

    // TODO: find out what are the buses' names so we can control sound independently

    void Awake() {
        _musicVol = PlayerPrefs.GetFloat("music_volume", 0.5f);
        _sfxVol = PlayerPrefs.GetFloat("sfx_volume", 0.5f);
        _mouseSens = PlayerPrefs.GetFloat("mouse_sensitivity", 5f);

        _music = FMODUnity.RuntimeManager.GetBus("bus:/GSFX");
        _sfx = FMODUnity.RuntimeManager.GetBus("bus:/Music");
    }

    void Start() {
        // update sliders
        musicSlider.value = _musicVol;
        sfxSlider.value = _sfxVol;
        sensSlider.value = _mouseSens;

        // set initial values
        _music.setVolume(_musicVol);
        _sfx.setVolume(_sfxVol);
    }

    public void SetMusicVolume(float newVol) {
        _music.setVolume(newVol);

        Debug.LogFormat("Changed music volume from {0} to {1}", _musicVol, newVol);
        _musicVol = newVol;
    }

    public void SetSFXVolume(float newVol) {
        _sfx.setVolume(newVol);

        Debug.LogFormat("Changed sound effects volume from {0} to {1}", _sfxVol, newVol);
        _sfxVol = newVol;
    }

    public void ChangeSensitivity(float newSens) {
        GameObject player = GameObject.Find("Player");

        if (player != null) {
            CameraController camController = player.GetComponent<CameraController>();
            camController.ChangeSensitivity(newSens);
        }

        _mouseSens = newSens;
    }

    void OnDisable() {
        Debug.Log("disabled");
        PlayerPrefs.SetFloat("music_volume", _musicVol);
        PlayerPrefs.SetFloat("sfx_volume", _sfxVol);
        PlayerPrefs.SetFloat("mouse_sensitivity", _mouseSens);
    }

}
