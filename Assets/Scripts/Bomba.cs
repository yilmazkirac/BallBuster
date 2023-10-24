using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    [SerializeField] private int Sayi;
    [SerializeField] private TextMeshProUGUI SayiText;
    [SerializeField] private GameManager _GameManager;

    List<Collider2D> colliders=new List<Collider2D>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Sayi.ToString()))
        {
            GucUygula();
        }
    }
    void GucUygula()
    {
        var contactFilter2D = new ContactFilter2D
        {
            useTriggers = true
        };
         Physics2D.OverlapBox(transform.position, transform.localScale * 3, 20f, contactFilter2D, colliders);
        _GameManager.PatlamaEfekti(transform.position);
        gameObject.SetActive(false);
        foreach (var item in colliders)
        {
            if (item.gameObject.CompareTag("Kutu"))
            {
                item.GetComponent<Kutu>().EfektOynat();
            }
            else
            {
                item.gameObject.GetComponent<Rigidbody2D>().AddForce(50 * new Vector2(0, 6), ForceMode2D.Force);
            }
          
        }
    }
}
