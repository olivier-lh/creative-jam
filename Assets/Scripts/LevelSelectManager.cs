using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
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

    public void LoadDoom()
    {
        SceneManager.LoadScene("teh_d00m_str1kes_b4ck");
    }

    public void LoadPage1()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    
    public void LoadPage2()
    {
        SceneManager.LoadScene("LevelSelect2");
    }
}
