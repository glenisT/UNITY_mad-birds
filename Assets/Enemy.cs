using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject _cloudParticlePrefab; //reference to the prefab - serialized to inspect
    public AudioSource source;              //reference to the audio source component for enemy
    public AudioClip clip;              //reference to the audio CLIP "poof"
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        bool didHitBird = collision.collider.GetComponent<Bird>() != null;  

        if(didHitBird)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);   //spawns clouds when enemy is destroyed, at its position, with [identity] rotation
            Destroy(gameObject);        //destroys object - object disappears
            return;         //we need to leave the chunk of code so we dont waste time checking for birds again after first contact
        }

        Enemy enemy = collision.collider.GetComponent<Enemy>();     
        if(enemy != null)
        {
            return;             //if nothing hits
        }

        if(collision.contacts[0].normal.y < - 0.5)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }      //contacts is an array that saves points of contact with the game element/character --- [0] means just any first contact --- .normal gives the position of impact which is any impact
    
    }


}