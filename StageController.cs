using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public GameObject factoryButton;
    public GameObject mausoleumButton;
    public GameObject palaceButton;
    public GameObject uknButton;
    public GameObject stageSelectionMenu;
    public GameObject skillTree;
    // Start is called before the first frame update
    void Start()
    {
        if (!Data.beatenLevel1){
            // hide all buttons except factory
            mausoleumButton.SetActive(false);
            palaceButton.SetActive(false);
            uknButton.SetActive(false);
        }
        if (!Data.beatenLevel2){
            // hide all buttons except factory and mausoleum
            palaceButton.SetActive(false);
            uknButton.SetActive(false);
        }
        if (!Data.beatenLevel3){
            // hide all buttons except factory, mausoleum, and palace
            uknButton.SetActive(false);
        }
        
        
    }
    public void StageSelectionMenu(){
        // show the stage selection menu and hide the skill tree
        stageSelectionMenu.SetActive(true);
        skillTree.SetActive(false);
    }

    public void SkillTreeMenu(){
        // show the skill tree and hide the stage selection menu
        stageSelectionMenu.SetActive(false);
        skillTree.SetActive(true);
    }

    public void GoToMainMenu(){
        SceneController.loadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
