using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Top : MonoBehaviour
{
    [SerializeField] private int Sayi;
    [SerializeField] private TextMeshProUGUI SayiText;
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private ParticleSystem BirlesmeEfect;
    [SerializeField] private SpriteRenderer _Renderer;

    bool Birincil;
    [SerializeField] private bool VarsayilanTop;
    void DurumuAyarla()
    {
        Birincil = true;
    }
   public void DurumuDegis()
    {
        Invoke("DurumuAyarla",2f);
    }
    private void Start()
    {
        SayiText.text=Sayi.ToString();
        if (VarsayilanTop)
        {
            Birincil = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Sayi.ToString()))
        {
            BirlesmeEfect.Play();     
            Sayi += Sayi;
            gameObject.tag= Sayi.ToString();
            SayiText.text = Sayi.ToString();
            collision.gameObject.SetActive(false);

            switch (Sayi)
            {
                case 4:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[1];
                    break;

                case 8:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[2];
                    break;
                case 16:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[3];
                    break;
                case 32:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[4];
                    break;
                case 64:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[5];
                    break;

                case 128:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[6];
                    break;

                case 256:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[7];
                    break;

                case 512:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[8];
                    break;

                case 1024:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[9];
                    break;
                case 2048:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[9];
                    break;

            }

            if (_GameManager.TopHedefi==true)
            {
                _GameManager.GorevSayiKontrol(Sayi);
            }
          
            Birincil = false;
            Invoke("DurumuAyarla", 2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Sayi.ToString()))
        {
            BirlesmeEfect.Play();
            Sayi += Sayi;
            gameObject.tag = Sayi.ToString();
            SayiText.text = Sayi.ToString();
            collision.gameObject.SetActive(false);

            switch (Sayi)
            {
                case 4:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[1];
                    break;

                case 8:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[2];
                    break;
                case 16:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[3];
                    break;
                case 32:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[4];
                    break;
                case 64:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[5];
                    break;

                case 128:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[6];
                    break;

                case 256:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[7];
                    break;

                case 512:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[8];
                    break;

                case 1024:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[9];
                    break;
                case 2048:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[9];
                    break;

            }
            if (_GameManager.TopHedefi == true)
            {
                _GameManager.GorevSayiKontrol(Sayi);
            }
            Birincil = false;
            Invoke("DurumuAyarla", 2f);
        }
    }
}
