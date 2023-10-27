using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreCanonsItem : MonoBehaviour
{
    [SerializeField] ItemManager _item;

    void Start()
    {
        _item = FindObjectOfType<GameManager>().GetComponent<ItemManager>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _item.isDobleCanonItem = true;
        }

    }
}
