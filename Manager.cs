 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject canvas;
    public Text infoText;
    public Text ammoText;
    public Slider healthBar;
    public Slider powerBar;
    public GameObject panelDeath;
    public GameObject panelWin;
    public GameObject panelPause;
    public GameObject panelOptions;
    public GameObject key;
    public GameObject notePot;
    public GameObject[] potenciadores;

    [Header("Player")]
    public int power = 0;
    public int maxPower;
    public int ammo;
    public int startAmmo;
    public bool hasKey = false;
    public Vector2 savedPos;

    [Header("PowerUp")]
    public int percentHeal;
    public float powerTimer;
    public bool isPowered = false;
    public bool tripleArrow = false;
    public bool infiniteAmmo = false;
    public bool extraDamage = false;

    public bool canPower = true;
    public bool canPause = true;
    public bool isDead = false;

    private GameObject[] startEnemies;
    private string[] currentEnemies;
    private GameObject currentPlayer;
    private Player playerScript;
    private int savedPower;
    private int savedAmmo;

    public static bool isPaused = false;

    ///Script Game Manager

    private void Awake()
    {
        Time.timeScale = 1f;
        startEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        CheckEnemies(startEnemies);

        if (GameObject.FindGameObjectsWithTag("Manager").Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);
        if (savedPos.Equals(Vector2.zero)) savedPos = GameObject.FindGameObjectWithTag("Start").transform.position;
    }

    ///Comprobar los potenciadores ya adquiridos mediante PlayerPrefs: 0 = false, 1 = true.
    void Start()
    {
        Restart();
        IncreaseAmmo(startAmmo);
        //IncreasePower(maxPower);
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        playerScript = currentPlayer.GetComponent<Player>();
        healthBar.maxValue = playerScript.maxHealth;
        powerBar.maxValue = maxPower;
        savedPower = 0;
        savedAmmo = startAmmo;       

        if (PlayerPrefs.GetInt("Pot1") == 1) potenciadores[0].SetActive(true);        
        if (PlayerPrefs.GetInt("Pot2") == 1) potenciadores[1].SetActive(true);
        if (PlayerPrefs.GetInt("Pot3") == 1) potenciadores[2].SetActive(true);
        if (PlayerPrefs.GetInt("Pot4") == 1) potenciadores[3].SetActive(true);
    }
    
    void Update()
    {       
        
        ///Potenciadores jugador
        if(isPowered)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) && PlayerPrefs.GetInt("Pot1") == 1) ///Heal
            {
                isPowered = false;
                currentPlayer.GetComponent<Player>().Heal(percentHeal);
                IncreasePower(-maxPower);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2) && PlayerPrefs.GetInt("Pot2") == 1)///Extra daño
            {
                isPowered = false;
                extraDamage = true;
                StartCoroutine("DepletePower");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && PlayerPrefs.GetInt("Pot3") == 1)///No pierde munición
            {
                isPowered = false;
                infiniteAmmo = true;
                StartCoroutine("DepletePower");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && PlayerPrefs.GetInt("Pot4") == 1)///3x Flechas
            {
                isPowered = false;
                tripleArrow = true;
                StartCoroutine("DepletePower");
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && canPause) ///Pausar el juego
        {
            Pause();
        }        
    }

    ///Comprueba si hay una posición guardada cada vez que carga un nivel y los enemigos que siguen vivos
    private void OnLevelWasLoaded(int level)
    {
        if (level != 0)
        {
            if (savedPos.Equals(Vector2.zero)) savedPos = GameObject.FindGameObjectWithTag("Start").transform.position;
            Restart();
            currentPlayer = GameObject.FindGameObjectWithTag("Player");
            startEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (currentEnemies.Length == 0) CheckEnemies(startEnemies);

            foreach (GameObject startEnemy in startEnemies)
            {
                bool delete = true;

                foreach (string enemy in currentEnemies)
                {
                    if (enemy.Equals(startEnemy.name))
                    {
                        Debug.Log(enemy);
                        delete = false;
                        break;
                    }
                }

                if (delete) Destroy(startEnemy.gameObject);
            }            
        }
    }

    ///Desbloquea potenciadores y muestra info en HUD.
    public void UnlockPot(int id, Sprite sprite)
    {
        Time.timeScale = 0f;
        canPause = false;
        Weapon.canMove = false;
        potenciadores[id].SetActive(true);
        notePot.GetComponent<Image>().sprite = sprite;
        //notePot.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        notePot.SetActive(true);
    }

    ///Return de la nota del potenciador
    public void ReturnPot()
    {
        Weapon.canMove = true;
        Time.timeScale = 1f;
        canPause = true;        
        notePot.SetActive(false);
    }

    ///Muestra un texto en pantalla
    public void DisplayInfo(string info)
    {
        infoText.text = info;
    }

    ///Establecer vida
    public void SetHealth(int health)
    {
        healthBar.value = health;
    }

    ///Aumentar munición
    public void IncreaseAmmo(int value)
    {
        ammo += value;
        ammoText.text = ammo.ToString();
    }

    ///Aumentar poder
    public void IncreasePower(int power)
    {
        this.power += power;
        if (this.power >= maxPower)
        {
            this.power = maxPower;
            isPowered = true;
        }
        powerBar.value = this.power;
    }

    ///Corrutina para vaciar barra de poder
    IEnumerator DepletePower()
    {
        canPower = false;
        int decrease = (int)(-maxPower / powerTimer);
        while (powerTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            powerTimer--;
            IncreasePower(decrease);
        }
        canPower = true;
        tripleArrow = false;
        infiniteAmmo = false;
        extraDamage = false;
    }

    ///Mostrar llave en HUD
    public void Key(bool value)
    {
        key.SetActive(value);
        hasKey = true;
    }

    ///Guardar posición y enemigos actuales
    public void Checkpoint(Vector2 pos)
    {
        savedPos = pos;
        //savedAmmo = ammo;
        //savedPower = power;
        CheckEnemies(GameObject.FindGameObjectsWithTag("Enemy"));        
    }

    ///Check enemigos vivos
    private void CheckEnemies(GameObject[] enemies)
    {
        currentEnemies = new string[enemies.Length];
        int count = 0;
        foreach (GameObject enemy in enemies)
        {
            currentEnemies[count] = enemy.name;
            count++;
        }
    }

    ///Reset a condiciones iniciales
    public void Restart()
    {
        canPower = true;
        tripleArrow = false;
        infiniteAmmo = false;
        extraDamage = false;
        hasKey = false;
        isPaused = false;
        isDead = false;
        canPause = true;
        DisplayInfo("");
        Weapon.canMove = true;
        BossCucua.invulnerable = false;
        panelDeath.SetActive(false);
        panelWin.SetActive(false);
        panelPause.SetActive(false);
    }

    ///Respawn de jugador, reset de stats y recargar escena
    public void Respawn()
    {
        //IncreaseAmmo(savedAmmo - ammo);
        //IncreasePower(savedPower - power);
        IncreaseAmmo(startAmmo - ammo);
        IncreasePower(- power);
        SetHealth((int)healthBar.maxValue);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    ///Muerte de jugador
    public void Death()
    {
        Time.timeScale = 0f;
        canPause = false;
        isPaused = true;
        isDead = true;
        StopCoroutine("DepletePower");
        panelDeath.SetActive(true);
    }

    ///Victoria de jugador
    public void Win()
    {
        FindObjectOfType<SoundManager>().StopSound("BossMusic");
        FindObjectOfType<SoundManager>().PlaySound("Win");
        Time.timeScale = 0f;
        canPause = false;
        isPaused = true;        
        MenuManager.UnlockLevel(SceneManager.GetActiveScene().buildIndex + 1);
        panelWin.SetActive(true);
    }
    
    ///Pausa
    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f:1f;
        if (!isPaused) StartCoroutine(PanelOut(panelPause));
        else
        {
            panelPause.SetActive(true);
            panelOptions.SetActive(false);
        }
    }

    ///Ocultar panel opciones
    public void OptionsBack()
    {
        canPause = true;
        StartCoroutine(PanelOut(panelOptions));
        if (!isDead) panelPause.SetActive(true);
        else panelDeath.SetActive(true);
    }

    ///Reset de stats y cargar siguiente nivel
    public void NextLevel()
    {
        IncreaseAmmo(startAmmo - ammo);
        IncreasePower(-power);
        SetHealth((int)healthBar.maxValue);
        savedPos = Vector2.zero;
        savedAmmo = startAmmo;
        savedPower = 0;
        currentEnemies = new string[0];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    ///Cargar menú principal
    public void LoadMenu()
    {
        savedPos = Vector2.zero;
        Restart();
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(canvas, SceneManager.GetActiveScene());
        SceneManager.LoadScene(0);
    }

    ///Controlar animación para ocultar paneles
    IEnumerator PanelOut(GameObject panel)
    {
        panel.GetComponentInChildren<Animator>().SetTrigger("Out");
        yield return new WaitForSeconds(0.5f);
    }
}
