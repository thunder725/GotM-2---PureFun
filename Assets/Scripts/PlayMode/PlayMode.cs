using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayMode : MonoBehaviour
{
    // Is the game in PlayMode or not
    public static bool isPlayMode;

    // There probably are a thousand options that are better than this one, but I have no idea what I'm doing so that's good enough
    [SerializeField] GameObject SpawnButtonPanel, InteractionButtonPanel, EnterPlaymodeButton, ExitPlaymodeButton;
    [SerializeField] GameObject GridVisualizer;
    [SerializeField] GameObject MarblePrefab;
    public GameObject MarbleReference;
    public MusicHandler _music;

    public static UnityAction<bool> PlayModeEvent;

    // ================ [GENERAL UNITY METHODS] ===================

    void Start()
    {
        // Spawn the marble once at the start of the game and reference it
        // It'll spawn inside of a box made to contain it
        // When entering the play mode I'll just teleport it, and when exiting the play mode i'll teleport it back up there
        MarbleReference = Instantiate(MarblePrefab, Vector3.up * 400, Quaternion.identity);

        // Reference the MusicHandler
        _music = MusicHandler.instance;

        // ExitPlayMode();
    }



    // =============== [PLAY MODE METHODS] =================

    public void EnterPlayMode()
    {
        // Call the event
        PlayModeEvent.Invoke(true);


        isPlayMode = true;

        // Disable all of the UI
        SpawnButtonPanel.SetActive(false);
        InteractionButtonPanel.SetActive(false);
        EnterPlaymodeButton.SetActive(false);
        GridVisualizer.SetActive(false);

        // Enable the Exit Button UI
        ExitPlaymodeButton.SetActive(true);

        // "Spawn" the marble
        MarbleReference.transform.position = Vector3.up * 35;

        // Switch music
        _music.SwitchMusic(true);
    }

    public void ExitPlayMode()
    {
        // Call the event
        PlayModeEvent.Invoke(false);
        
        isPlayMode = false;

        // Disable all of the UI
        SpawnButtonPanel.SetActive(true);
        InteractionButtonPanel.SetActive(true);
        EnterPlaymodeButton.SetActive(true);
        GridVisualizer.SetActive(true);

        // Enable the Exit Button UI
        ExitPlaymodeButton.SetActive(false);

        // "Remove" the marble
        MarbleReference.transform.position = Vector3.up * 400;

        // Switch music
        _music.SwitchMusic(false);
    }
}
