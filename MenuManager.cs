using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject panelOptions;
    public GameObject panelMenu;
    public GameObject panelLevels;
    public GameObject panelCredits;
    public Button[] levels;

    public static int level;
    
    ///Iniciamos el juego mostrando el menú y ocultando las opciones
    ///Guardamos en una variable el último nivel que se ha completado con PlayerPrefs

    private void Awake()
    {
        Time.timeScale = 1f;
        level = PlayerPrefs.GetInt("Level", 0);
    }

    ///Comprobar los niveles desbloqueados y desbloquear los botones en consecuencia
    private void Start()
    {     
        
        for (int x = 0; x < level; x++)
        {
            levels[x].interactable = true;
        }
        panelOptions.SetActive(false);
        panelLevels.SetActive(false);
        panelCredits.SetActive(false);
        panelMenu.SetActive(true);
    }

    ///Salir del juego
    public void Exit()
    {
        Application.Quit();
    }

    ///Nivel conseguido
    public static void UnlockLevel(int index)
    {
        level = index;
        PlayerPrefs.SetInt("Level", index);
    }

    ///Comprobar si es la primera vez que se juega e iniciar el primer nivel directamente
    public void FirstTime()
    {
        if (level == 0) Load(1);
        else
        {
            panelMenu.SetActive(false);
            panelLevels.SetActive(true);
        }
    }

    ///Cargar escena
    public void Load(int index)
    {
        SceneManager.LoadScene(index);
    }

    ///Cambiar entre panel de menú y de opciones
    public void Options(bool value)
    {
        panelOptions.SetActive(value);
        panelMenu.SetActive(!value);
    }

    ///Cambiar entre panel de menú y de créditos
    public void Credits(bool value)
    {
        panelCredits.SetActive(value);
        panelMenu.SetActive(!value);
    }

    ///Función para botón Back
    public void Back()
    {
        StartCoroutine("PanelOut");
    }

    ///Controlar el tiempo de animación de salida del panel
    IEnumerator PanelOut()
    {
        yield return new WaitForSeconds(1f);
        panelLevels.SetActive(false);
        panelOptions.SetActive(false);
        panelCredits.SetActive(false);
        panelMenu.SetActive(true);
    }

}
