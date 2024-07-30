using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using System.Data;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MainMenu : MonoBehaviour
{
    GameObject panelInputUsername;
    GameObject panelLeaders;
    private void Awake()
    {
        panelLeaders = GameObject.Find("PanelLeaders");
        panelInputUsername = GameObject.Find("PanelInputUsername");

        panelLeaders.SetActive(false);
        panelInputUsername.SetActive(false);
    }

    public void ClickNewGame()
    {
        if (!panelInputUsername.activeSelf)
        {
            panelInputUsername.SetActive(true);
        }
        else
        {
            try
            {
                panelInputUsername.transform.localPosition = new Vector3(0, 0, 0);
                panelInputUsername.SetActive(false);
            }
            catch (Exception buggPanel)
            {
                Debug.LogException(buggPanel, this);
            }
        }
    }
    public void ClickEnterInputUsername()
    {
        TMP_InputField inputFieldUsername = TMP_InputField.FindObjectOfType<TMP_InputField>();

        inputFieldUsername.text = inputFieldUsername.text.Trim();
        Debug.Log(inputFieldUsername.text);
        if (inputFieldUsername.text != "")
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
        
    }
    public void ClickLeaders()
    {
        if (!panelLeaders.activeSelf)
        {
            panelLeaders.SetActive(true);
        }
        else
        {
            try
            {
                panelLeaders.transform.localPosition = new Vector3(0, 0, 0);
                panelLeaders.SetActive(false);
            }
            catch (Exception buggPanel)
            {
                Debug.LogException(buggPanel, this);
            }
        }
    }
    public void ClickClosePanel()
    {
        GameObject buttonClose = GameObject.Find("ButtonClose");
        buttonClose.transform.parent.gameObject.SetActive(false);
    }
}
