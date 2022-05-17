using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] Button Button;
    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        SceneLoader.LoadScene(1);
    }
}
