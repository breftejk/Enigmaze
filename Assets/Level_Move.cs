using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Move : MonoBehaviour
{
    public int NextSceneIndex; //Zmienna musi byæ public, aby kod by³ uniwersalny i abysmy mogli go u¿yæ do wszystkich scen z pozycji Unity
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");
        if (other.tag == "Player")
        {
            print("Switching Scene to " + NextSceneIndex);
            SceneManager.LoadScene(NextSceneIndex, LoadSceneMode.Single);
        }
    }
}