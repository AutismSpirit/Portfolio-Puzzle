using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The scene manager that changes the current scene. Lives in the Persistent Scene so it never gets unloaded.
/// </summary>
public class SceneController : MonoBehaviour
{
    private Button[] mainMenuButtons;

    public SceneSetup sceneSetup;

    public void Awake()
    {
        ButtonSetup();

    }

    public void ButtonSetup()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        mainMenuButtons = canvas.GetComponent<CanvasReferences>().buttons;

        mainMenuButtons[0].onClick.AddListener(OnStart);
        mainMenuButtons[1].onClick.AddListener(OnContinue);
        mainMenuButtons[2].onClick.AddListener(OnOptions);
        mainMenuButtons[3].onClick.AddListener(OnQuit);
    }

    public void OnStart()
    {
        //Get the load screen going
        SceneManager.LoadScene(sceneSetup.LoadScreenScene, LoadSceneMode.Additive);
        //Unload the main menu
        SceneManager.UnloadSceneAsync(sceneSetup.MainMenuScene);
        //Start loading the game
        SceneManager.LoadSceneAsync(sceneSetup.StartScene, LoadSceneMode.Additive);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == SceneManager.GetSceneByBuildIndex(sceneSetup.StartScene))
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneSetup.StartScene));
            SceneManager.UnloadSceneAsync(sceneSetup.LoadScreenScene);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void OnContinue()
    {

    }

    public void OnOptions()
    {

    }

    public void OnQuit()
    {

        Application.Quit();
    }

}

[System.Serializable]
public class SceneSetup
{
    [Header("Menus")]
    public int MainMenuScene;
    public int OptionsScene;

    [Header("Gameplay Scenes")]
    public int StartScene;
    public int ContinueScene;

    [Header("Transition Scenes")]
    public int LoadScreenScene;
}

