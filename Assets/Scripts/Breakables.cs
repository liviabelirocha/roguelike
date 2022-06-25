using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [SerializeField]
    private GameObject[] brokenPieces;

    [SerializeField]
    private int maxPieces;

    void Start()
    {
        maxPieces = brokenPieces.Length - 1;
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerController.instance.IsDashing())
            {
                SmashAnimation();

                Destroy (gameObject);
            }
        }
    }

    private int GetRandom(int length, int min = 0)
    {
        return Random.Range(min, length);
    }

    private void SmashAnimation()
    {
        int piecesToDrop = GetRandom(maxPieces, 1);

        for (int i = 0; i < piecesToDrop; i++)
        {
            int randomPiece = GetRandom(brokenPieces.Length);
            Instantiate(brokenPieces[randomPiece],
            transform.position,
            transform.rotation);
        }
    }
}
