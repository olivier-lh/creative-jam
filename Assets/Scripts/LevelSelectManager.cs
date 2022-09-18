using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{

    //Level completed bools
    public bool PlantTutorialCompleted = false;
    
    //Level buttons objects
    [Header("Buttons")]
    [SerializeField] private GameObject plantTutorialBtn;
    
    // Update is called once per frame
    void Update()
    {
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadPlantTutorial()
    {
        SceneManager.LoadScene("PlantTutorial");
    }
}
