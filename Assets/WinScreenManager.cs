using UnityEngine;
using UnityEngine.SceneManagement;
public class WinScreenManager : MonoBehaviour
{
    public GameObject WinScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WinGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnCollisionEnter(Collision collision)
    {
        WinScreen.SetActive(true);
    }
}
