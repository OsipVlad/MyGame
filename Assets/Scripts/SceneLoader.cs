using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
