using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject elementalButtons;
    public Button initialDialogue;

    public Decisions playerDecisions;

    public bool initialDone = false;

    public GameObject winScreen;

    public GameObject[] enemies;
    string sceneName;

    // For Decision consequences
    public Player player;
    public Movement movement;
    public Image mist;

    public Button earth;
    public Button water;
    public Button wind;
    public Button fire;

    public List<int> specialAnswers = new List<int>{ 104, 106, 107, 109, 211, 214, 215, 304, 306, 307, 313, 407, 410, 413 };

    // Start is called before the first frame update
    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        sceneName = currentScene.name;

        if (sceneName == "R1")
        {
            //initialDialogue.onClick.Invoke();
        }
        else if (sceneName == "R2")
        {
            LoadDecisions();
            if (enemies == null)
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
            //winScreen = GameObject.Find("Win Screen");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (sceneName == "R2")
        {
            if (CheckEnemiesWiped())
            {
                Time.timeScale = 0f;
                winScreen.SetActive(true);
            }
        }
    }

    public bool CheckEnemiesWiped()
    {
        foreach (GameObject enemy in enemies)
        {
            if (!enemy.GetComponent<EnemyAi>().isDead)
            {
                return false;          
            }
                
        }
        return true;
    }

    public bool CheckDoneTrees()
    {
        foreach (Button dialogueTree in elementalButtons.GetComponentsInChildren<Button>()) 
        {
            if (dialogueTree.IsInteractable()) 
            {
                return false;
            }
        }
        ManageDecisions();
        return true;
    }

    public void LoadDecisions()
    {
        if(PlayerPrefs.HasKey("104"))
        {
            player.currentHealth = 45;
        }
        if (PlayerPrefs.HasKey("106"))
        {
            fire.interactable = true;
        }
        if (PlayerPrefs.HasKey("107"))
        {
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyAi>().fireballDamage = 15;
            }
        }
        if (PlayerPrefs.HasKey("109"))
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyAi>().fireballDamage = 45;
            }
        }
        if (PlayerPrefs.HasKey("211"))
        {
            water.interactable = true;
        }
        if (PlayerPrefs.HasKey("214"))
        {
            var tempColor = mist.color;
            tempColor.a = 0.9f;
            mist.color = tempColor;
            mist.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("215"))
        {
            var tempColor = mist.color;
            tempColor.a = 0.8f;
            mist.color = tempColor;
            mist.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("304"))
        {
            movement.speed = 10;
        }
        if (PlayerPrefs.HasKey("306"))
        {
            player.maxHealth += 10;
        }
        if (PlayerPrefs.HasKey("307"))
        {
            movement.speed = 13;
        }
        if (PlayerPrefs.HasKey("313"))
        {
            earth.interactable = true;
        }
        if (PlayerPrefs.HasKey("407"))
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyAi>().speed = 17;
            }
        }
        if (PlayerPrefs.HasKey("410"))
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyAi>().speed = 15;
            }
        }
        if (PlayerPrefs.HasKey("413"))
        {
            wind.interactable = true;
        }
    }

    public void ManageDecisions()
    {
        var intersection = specialAnswers.Intersect(playerDecisions.playerDecisions);
        foreach(int choice in  intersection)
        {
            PlayerPrefs.SetInt(choice.ToString(), choice);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResetLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToTitle()
    {
        PlayerPrefs.DeleteAll();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
