using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour{

  public int index;
  public string levelName;

  private bool nextLevel;

  private void Update(){
      if(nextLevel){
        if(Input.GetKeyDown(KeyCode.E)){
          nextLevel = false;
          SceneManager.LoadScene(levelName);

          // Load via levelName
          // SceneManager.LoadScene(levelName);

          // Restart LevelControl
          // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
      }
    }

  private void OnTriggerEnter2D(Collider2D other){
    if(other.CompareTag("Player")){
      nextLevel = true;
    }
  }

  private void OnTriggerExit2D(Collider2D other){
    if(other.CompareTag("Player")){
      nextLevel = false;
    }
  }
}
