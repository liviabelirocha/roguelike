using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [SerializeField]
    private GameObject[] brokenPieces;

    [SerializeField]
    private int maxPieces;

    [SerializeField]
    private bool shouldDropItem;

    [SerializeField]
    private GameObject[] itemsToDrop;

    [SerializeField]
    private float itemDropPercent;

    [SerializeField]
    private int sfxIndex;

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
            if (PlayerController.instance.IsDashing()) DestroyBreakable();
    }

    public void DestroyBreakable()
    {
        SmashAnimation();
        DropItem();
        Destroy (gameObject);
        AudioManager.instance.PlaySFX (sfxIndex);
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

    private void DropItem()
    {
        if (shouldDropItem)
        {
            float dropChance = Random.Range(0f, 100f);

            if (dropChance < itemDropPercent)
            {
                int randomItem = GetRandom(itemsToDrop.Length);
                Instantiate(itemsToDrop[randomItem],
                transform.position,
                transform.rotation);
            }
        }
    }
}
