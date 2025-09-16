using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// In MatchUI 
public class ButtonActions : MonoBehaviour
{

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GamemodeSelect(int mode)
    {
        GameData.Instance.GameMode = mode;
    }

    public void PowerSelect(int power)
    {
        GameData.Instance.PPowerSelected = power;
    }
    public void DifficultSelect(float value)
    {
        GameData.Instance.Difficult = value;
    }

}
