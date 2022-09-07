using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the logic for the menu UI

public class MenuController : MonoBehaviour
{
    // UI Variables Under the Menu
    public GameObject acknowledgementsBackground;
    public GameObject returnButton;
    public GameObject acknowledgementsHeaderText;
    public GameObject acknowledgementsText;
    public GameObject popUpMenu;
    public GameObject menuButton;

    // Hide and show the proper UI elements when menu UI first shown
    void Start() {
        acknowledgementsBackground.SetActive(false);
        returnButton.SetActive(false);
        acknowledgementsHeaderText.SetActive(false);
        acknowledgementsText.SetActive(false);
        popUpMenu.SetActive(false);
        menuButton.SetActive(true);
    }

    // Show the Menu Pop Up and Hide Menu Button
    public void ShowMenuPopUp() {
        popUpMenu.SetActive(true);
        menuButton.SetActive(false);
    }

    // Hide Menu Pop Up and Show Menu Button
    public void HideMenuPopUp() {
        popUpMenu.SetActive(false);
        menuButton.SetActive(true);
    }

    // Exit the app when you press the exit button
    public void PressExitButton() {
        Application.Quit();
    }

    // Show Acknowlegments When Pressing Our Team Button
    public void PressOurTeamButton() {
        popUpMenu.SetActive(false);
        acknowledgementsBackground.SetActive(true);
        returnButton.SetActive(true);
        acknowledgementsHeaderText.SetActive(true);
        acknowledgementsText.SetActive(true);
    }

    
    // Hide Acknowlegments When Pressing Return Button
    public void PressReturnButton() {
        popUpMenu.SetActive(true);
        acknowledgementsBackground.SetActive(false);
        returnButton.SetActive(false);
        acknowledgementsHeaderText.SetActive(false);
        acknowledgementsText.SetActive(false);
    }

}
