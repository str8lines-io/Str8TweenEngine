using UnityEngine;

//Loads the Canvas and EventSystem to use for testing
public class EndUserTestsSetup : MonoBehaviour
{
    void Start()
    {
        GameObject canvas = Resources.Load<GameObject>("TestCanvas");
        UnityEngine.Object.Instantiate(canvas);
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        UnityEngine.Object.Instantiate(eventSystem);
    }
}
