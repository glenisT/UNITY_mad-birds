using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LevelController : MonoBehaviour
{

    private static int _nextLevelIndex = 1;     //saves next level id

    private Enemy[] _enemies;   //creates array of enemies

    [SerializeField] private float _delayBeforeLoading = 5f;    //delay before loading next level

    private float timeElapsed;  

    void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();   //finds collection of enemies to later check when they are all destroyed

    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;      //Time.deltaTime is the time elapsed between 2 frames A.K.A 2 Update() methods

        
        if(timeElapsed > _delayBeforeLoading)       //only then start checking...necessary order of coding events for this to work(before foreach check)
        {

            
        foreach (Enemy enemy in _enemies)    //loops over every enemy in the level to run the code below
        {
            if (enemy != null)   //if enemy is not dead exit out of Update
            {
                return;
            }
            
        }

            Debug.Log("You killed all the enemies!");

            _nextLevelIndex++;
            string nextLevelName = "Level0" + _nextLevelIndex;     //the next level in a string variable
            SceneManager.LoadScene(nextLevelName);
        }
    }
}