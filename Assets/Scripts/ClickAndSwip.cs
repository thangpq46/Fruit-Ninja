using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwip : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trailRenderer;
    private BoxCollider boxCollider;
    private bool swiping = false;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        gameManager = GameObject.Find("Game Manger").GetComponent<GameManager>();
        trailRenderer = GetComponent<TrailRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        trailRenderer.enabled = false; boxCollider.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();

            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }
            if (swiping)
            {
                UpdateMousePosition();
            }

        }
    }

    void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        transform.position = mousePos;
    }

    void UpdateComponents()
    {
        trailRenderer.enabled = swiping;
        boxCollider.enabled = swiping;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget();
            if (collision.gameObject.CompareTag("Bad"))
            {
                gameManager.diseaseLive();
            }
        }
    }
}
