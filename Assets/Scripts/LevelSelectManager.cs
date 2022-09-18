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
    
    public void LoadBackAndForth()
    {
        SceneManager.LoadScene("BackAndForth");
    }
    
    public void LoadHop()
    {
        SceneManager.LoadScene("Hop");
    }
    
    public void LoadSuper()
    {
        SceneManager.LoadScene("Teh_sooper_levul_of_d00m");
    }
    
    public void LoadWallGap()
    {
        SceneManager.LoadScene("WallGap");
    }
    
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
