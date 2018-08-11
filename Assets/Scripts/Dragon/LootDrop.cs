using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    public float DropSpeed = 5;
    public int Amount = 2000;

    private Rigidbody2D _rigid2D;

    // Use this for initialization
    void Start()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
        _rigid2D.velocity = Vector2.down * DropSpeed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Pile"))
            return;

        Pile.Instance.Add(Amount);
        Destroy(this.gameObject);
    }
}