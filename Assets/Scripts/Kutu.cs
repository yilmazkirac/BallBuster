using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kutu : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;

    public void EfektOynat()
    {
        _GameManager.KutuParcalanmaEfekt(transform.position);
        gameObject.SetActive(false);
    }
}
