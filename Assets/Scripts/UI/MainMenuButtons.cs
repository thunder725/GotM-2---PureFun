using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    static bool hasBeenSetup;
    [SerializeField] GameObject[] TutorialUsefulPieces;
    [SerializeField] Button PlayButton, ReturnButton;


    void Start()
    {
        // Check the current scene so it doesn't do it in the Main Game scene which has no tutorial panel
        if (!hasBeenSetup && SceneManager.GetActiveScene().buildIndex == 0)
        {ExitTutorial(); hasBeenSetup = true;}
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowTutorial()
    {
        Debug.Log("e");
        MainMenuPanel.SetActive(false);
        foreach (GameObject g in TutorialUsefulPieces)
        {
            g.SetActive(true);
        }
        ReturnButton.Select();
    }

    public void ExitTutorial()
    {
        MainMenuPanel.SetActive(true);
        foreach (GameObject g in TutorialUsefulPieces)
        {
            g.SetActive(false);
        }
        PlayButton.Select();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

}