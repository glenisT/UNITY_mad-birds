using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{



    private Vector3 _initialPosition;       //underscore eshte njelloj si te thuash qe variabli eshte private

    private bool _birdWasLaunched;
    private float _timeSittingAround;

    public AudioSource source;
    public AudioClip launchScream;
    public AudioClip slingshot;

    [SerializeField] private float _launchPower = 500;  //do jape vleren e launch power ne varesi te terheqjes te zogut, [SerializeField] ben te mundur manipulimin e vlerave te variablit nga editori i Unity

    private void Awake()        //metode qe thirret kur fillojme lojen/ndryshojme stadin e dickaje (psh: zogu fluturon)
    {
        _initialPosition = transform.position;      //transform eshte built-in nga Unity qe mer komponentin.atributNgaKomponenti

    }

    private void Update()    //metode qe therritet 1 here per frame
    {

        GetComponent<LineRenderer>().SetPosition(0, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(1, transform.position);       //mer line renderin dhe ben expand/retractsipas pozicionit te zogut per ti vene me vone texture si shigjetaper direction of launch

        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {     //nese zogu eshte hedhur dhe ka ndaluar be reset
            _timeSittingAround += Time.deltaTime;   //deltaTime eshte 1 pjesetuar per frames per second, timesittingaround inkrementohet
        }

        if (transform.position.y > 10 ||
         transform.position.x > 30 ||
         transform.position.y < -10 ||
         transform.position.x < -20 ||
         _timeSittingAround > 2)   //ben reset scene nese zogu del jashte kufijve te nivelit ose nese rri pa levizur per me shume se 3 njesi kohe
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);     //mer si parameter numer nga scene manager per cilen skene do besh load, ose thjesht current scene ne rastin ketu
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;    //ndryshon ngjyren e zogut kur klikohet, dhe ngel
        GetComponent<LineRenderer>().enabled = true;        //ben enable lineRenderer kur terheqim zogun
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;  //kthen ngjyren e zogut ne default kur stopon klikimin

        Vector2 directionToInitialPosition = _initialPosition - transform.position;     //marim diferencen e pozicionit ku jemi me pozicionin ku terheqim zogun per te mare vektorin e drejtimit te LAUNCH
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);         //mer RigidBody dhe i jep force ne drejtimin e vektorit qe ruajtem (AddForce mer si parameter nje vektor qe shumezohet me shpejtesine/forcen)
        GetComponent<Rigidbody2D>().gravityScale = 1;                                   //i jep gravitet trupit AFTER-LAUNCH
        _birdWasLaunched = true;
        source.PlayOneShot(launchScream);

        GetComponent<LineRenderer>().enabled = false;       //i ben disable lineRenderer ne momentin qe e leshojme zogun dhe fluturon
    }

    private void OnMouseDrag()
    {
        //Vector3 eshte tip qe kap 3 koordinata (X, Y, Z)
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//mer pozicionin e mouse dhe e konverton ne pozicion te vertete ne Scene per cameran qe mos te zhduket zogu
        //Marim vetem X dhe Y nga newPosition sepse kur perdorim ScreenToWorldPoint Z e zogut konvertohet ne Z e Camera (Z eshte koordinate 3D)
        transform.position = new Vector3(newPosition.x, newPosition.y);
        source.PlayOneShot(slingshot);
    }
}