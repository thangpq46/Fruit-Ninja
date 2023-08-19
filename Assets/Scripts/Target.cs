using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;
    public int pointValue;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manger").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * Random.Range(12, 16), ForceMode.Impulse);
        rb.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10), ForceMode.Impulse);
        transform.position = new Vector3(Random.Range(-4, 4), 0);
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            DestroyTarget();
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.diseaseLive();
            }
            gameManager.updateScore(pointValue);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.diseaseLive();
        }
        Destroy(gameObject);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); gameManager.updateScore(pointValue);
        }
    }
}
