using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [Header("References")]
    public Transform pivotToRestart;
    public GameObject ship;

    public GameObject startButton;
    public GameObject elementsUI;
    public GameObject gameControls;
    public GameObject restartGuide;
    public GameObject shipHealthBar;

    [Header("Behaviour")]
    [HideInInspector] public float gameTime = 1;
    [HideInInspector] public bool endGame;


    void Awake()
    {
        instance = this;
        gameTime = 0;
        HideUI();
        HideRestartGuide();
    }

    void Update()
    {
        if (Input.GetButton("Cancel") && endGame)
            RestatGame();
    }

    private void HideUI()
    {
        elementsUI.SetActive(false);
    }
    private void ShowUI()
    {
        elementsUI.SetActive(true);
    }

    private void ShowRestartGuide()
    {
        restartGuide.SetActive(true);
    }
    private void HideRestartGuide()
    {
        restartGuide.SetActive(false);
    }
    public void StartGame()
    {
        startButton.SetActive(false);
        gameControls.SetActive(false);
        ShowUI();
        gameTime = 1;
    }

    public void EndGame()
    {
        ShowRestartGuide();
        endGame = true;
        SpawnManager.Instance.spawnAble = false;
        gameTime = 0;
        ShowRestartGuide();
    }


    void RestatGame()
    {
        endGame = false;
        ship.transform.position = new Vector3(pivotToRestart.position.x, ship.transform.position.y, pivotToRestart.position.z);
        ship.GetComponent<ShipController>().allStatus[ship.GetComponent<ShipController>().healthLevel - 1].health = 100;
        ship.GetComponent<ShipController>().EnebleMesh(true);
        SpawnManager.Instance.DestroyerAllEnemy();
        SpawnManager.Instance.spawnAble = true;
        HideRestartGuide();
        shipHealthBar.gameObject.GetComponent<Slider>().value = 100;
        gameTime = 1;
    }
}
