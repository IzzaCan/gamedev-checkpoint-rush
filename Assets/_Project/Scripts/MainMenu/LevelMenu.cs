using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform levelButtonsParent;

    private Button[] buttons;

    private void Awake()
    {

        buttons = levelButtonsParent.GetComponentsInChildren<Button>();

        int unlockedLevel = LevelManager.GetUnlockedLevel();

        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        for (int i = 0; i < unlockedLevel && i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        SceneManager.LoadScene($"Level_{levelId}");
    }
}