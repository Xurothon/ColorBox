using UnityEngine;

public class MainUIHelper : MonoBehaviour
{
    public void DeactivePanel (GameObject panel)
    {
        panel.SetActive (false);
    }

    public void ActivePanel (GameObject panel)
    {
        panel.SetActive (true);
    }

    public void LoadScene (int idScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene (idScene);
    }

}