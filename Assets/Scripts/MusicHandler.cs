using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Code recycled from last month's game 
// If I don't have the time to finish this game, I don't have the time to write another script
// So there's unused code in comments, don't pay attention to it (lines 28-30 and 43-60 mainly)
public class MusicHandler : MonoBehaviour
{
    public static MusicHandler instance;
    [SerializeField] AudioSource EditModeMusic, PlayModeMusic;
    float maxEditValue, maxPlayValue;
    WaitForEndOfFrame _wait = new WaitForEndOfFrame();

    void Awake()
    {
        // Debug.Log("SCRIPT NUMBER  " + this.GetInstanceID()  + "   LAUNCHES AWAKE");
        if (instance != null && instance != this)
        {
            // SceneManager.activeSceneChanged -= SceneChanged;
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Apparently there is no "OnSceneChanged()" so I need to do it like that
            // I just used the same syntax as when doing an Input System Event, looks like it works
            // SceneManager.activeSceneChanged += SceneChanged;
        }
    }

    void Start()
    {
        // Get the maximum sound values since it's not 1
        maxEditValue = EditModeMusic.volume;
        maxPlayValue = PlayModeMusic.volume;

        // Both sounds are Played On Awake, but we only want the edit one to be audible
        PlayModeMusic.volume = 0;
    }

    // void SceneChanged(Scene current, Scene next)
    // {
    //     // Debug.Log("THIS OBJECT'S SCRIPT ID:  " + this.GetInstanceID());
    //     // Debug.Log("INSTANCE's ID:  " + instance.GetInstanceID());

    //     // For the case where you were in-game and you return to the main menu
    //     // Destroy even if DontDestroyOnLoad, and remove the instance so it can spawn again if you re-launch the game
    //     if (next.buildIndex == 0)
    //     {
    //         // Prevent memory leaks!!!
    //         SceneManager.activeSceneChanged -= SceneChanged;

    //         Destroy(gameObject);

    //         instance = null;
    //     }
    // }

    public void SwitchMusic(bool isEnteringPlayMode) // Called when switching from build to play mode to fade in/out the musics
    {
        // To avoid overrunning coroutines if the button to play is spammed 

        StopAllCoroutines();
        if (isEnteringPlayMode)
        {
            StartCoroutine(CrossfadePlayMode());
        }
        else
        {
            StartCoroutine(CrossfadeEditMode());
        }
    }

    IEnumerator CrossfadePlayMode()
    {
        float timer = 0;

        while (timer < 1)
        {
            // Increase the timer 
            timer += Time.deltaTime;

            // Increase one value, and decrease the other
            PlayModeMusic.volume = timer * maxPlayValue;
            EditModeMusic.volume = (1-timer) * maxEditValue;
            
            // Why not do a "new WaitForEndOfFrame()"? Because apparently that creates the function each frame since we do "new"
            // According to samyam (godess of Unity) this method is better
            yield return _wait;
        }

        // Stop itself
        StopCoroutine(CrossfadePlayMode());
    }

    IEnumerator CrossfadeEditMode()
    {
        
        float timer = 0;

        while (timer < 1)
        {
            // Increase the timer 
            timer += Time.deltaTime;

            // Increase one value, and decrease the other
            PlayModeMusic.volume = (1-timer) * maxPlayValue;
            EditModeMusic.volume = (timer) * maxEditValue;
            
            // Why not do a "new WaitForEndOfFrame()"? Because apparently that creates the function each frame since we do "new"
            // According to samyam (godess of Unity) this method is better
            yield return _wait;
        }

        // Stop itself
        StopCoroutine(CrossfadeEditMode());
    }

}