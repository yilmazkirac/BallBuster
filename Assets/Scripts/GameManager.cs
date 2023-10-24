using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class Hedefler_UI
{
    public GameObject Hedef;
    public Image HedefGorsel;
    public TextMeshProUGUI HedefTextDeger;
    public GameObject GorevTamam;
}
[Serializable]
public class Hedefler
{

    public Sprite HedefGorsel;
    public int TopDegeri;
    public GameObject GorevTamam;
    public string HedefTuru;
}
public class GameManager : MonoBehaviour
{
    [Header("-----LEVEL AYARLARI")]
    [SerializeField] private GameObject[] Toplar;
    public Sprite[] SpriteObjeleri;
    [SerializeField] private TextMeshProUGUI KalanTopText;
    int KalanTopSayisi;
    int HavuzIndex;


    [Header("-----DIGER OBJELER")]
    [SerializeField] private ParticleSystem PatlamaEfekt;
    [SerializeField] private ParticleSystem[] KutuKirilmaEfekt;
    int KutuKirilmaEfektIndex;
    [SerializeField] private GameObject[] Paneller;

    [Header("-----TOP ATIS SISTEMI")]
    [SerializeField] private GameObject TopAtici;
    [SerializeField] private GameObject TopSoketi;
    [SerializeField] private GameObject GelecekTop;
    GameObject SeciliTop;

    [Header("-----GOREV ISLEMLERI")]
    [SerializeField] private List<Hedefler_UI> Hedefler_UI;
    [SerializeField] private List<Hedefler> Hedefler;
    int TopDegeri, KutuDegeri, ToplamGorevSayisi;
    public bool TopHedefi;
    bool KutuHedefi;
    bool KontrolTop, KontrolKutu;
    private void Start()
    {

        KalanTopSayisi = Toplar.Length;
        TopGetir(true);
        ToplamGorevSayisi = Hedefler.Count;
        for (int i = 0; i < Hedefler.Count; i++)
        {
            Hedefler_UI[i].Hedef.SetActive(true);
            Hedefler_UI[i].HedefGorsel.sprite = Hedefler[i].HedefGorsel;
            Hedefler_UI[i].HedefTextDeger.text = Hedefler[i].TopDegeri.ToString();
            if (Hedefler[i].HedefTuru == "Top")
            {
                TopHedefi = true;
                TopDegeri = Hedefler[i].TopDegeri;
            }
            else if (Hedefler[i].HedefTuru == "Kutu")
            {
                KutuHedefi = true;
                KutuDegeri = Hedefler[i].TopDegeri;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("OyunZemini"))
                {
                    Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    TopAtici.transform.position = Vector2.MoveTowards(TopAtici.transform.position, new Vector2(MousePosition.x, TopAtici.transform.position.y), 30 * Time.deltaTime);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            SeciliTop.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            SeciliTop.transform.parent = null;
            SeciliTop.GetComponent<Top>().DurumuDegis();
            TopGetir(false);
        }
    }
    public void TopGetir(bool IlkKurulum)
    {
        if (IlkKurulum)
        {
            Toplar[HavuzIndex].transform.SetParent(TopAtici.transform);
            Toplar[HavuzIndex].transform.position = TopSoketi.transform.position;
            Toplar[HavuzIndex].SetActive(true);
            SeciliTop = Toplar[HavuzIndex];

            HavuzIndex++;

            Toplar[HavuzIndex].transform.position = GelecekTop.transform.position;
            Toplar[HavuzIndex].SetActive(true);
            KalanTopText.text = KalanTopSayisi.ToString();
        }
        else
        {
            if (KalanTopSayisi != 0)
            {
                Toplar[HavuzIndex].transform.SetParent(TopAtici.transform);
                Toplar[HavuzIndex].transform.position = TopSoketi.transform.position;
                Toplar[HavuzIndex].SetActive(true);
                SeciliTop = Toplar[HavuzIndex];
                KalanTopSayisi--;
                KalanTopText.text = KalanTopSayisi.ToString();

                if (HavuzIndex != Toplar.Length - 1)
                {
                    HavuzIndex++;
                    Toplar[HavuzIndex].transform.position = GelecekTop.transform.position;
                    Toplar[HavuzIndex].SetActive(true);
                }
            }
            if (KalanTopSayisi == 0)
            {
                Invoke("Kontrol", 2f);
            }
        }
    }
    public void Kontrol()
    {
        if (ToplamGorevSayisi == 0)
            Kazandin();
        else
            Kaybettin();
    }
    public void PatlamaEfekti(Vector2 Pos)
    {
        PatlamaEfekt.transform.position = Pos;
        PatlamaEfekt.gameObject.SetActive(true);
        PatlamaEfekt.Play();
    }
    public void KutuParcalanmaEfekt(Vector2 Pos)
    {
        KutuKirilmaEfekt[KutuKirilmaEfektIndex].transform.position = Pos;
        KutuKirilmaEfekt[KutuKirilmaEfektIndex].gameObject.SetActive(true);
        KutuKirilmaEfekt[KutuKirilmaEfektIndex].Play();
        if (KutuHedefi)
        {
            KutuDegeri--;
            if (KutuDegeri == 0)
            {
                Hedefler_UI[1].GorevTamam.SetActive(true);
            
                if (!KontrolKutu)
                {
                    ToplamGorevSayisi--;
                    if (ToplamGorevSayisi == 0)
                    {
                        Kazandin();
                    }
                    KontrolKutu=true;
                }
               
             
            }
     }
        if (KutuKirilmaEfektIndex == KutuKirilmaEfekt.Length - 1)
            KutuKirilmaEfektIndex = 0;
        else
            KutuKirilmaEfektIndex++;
    }
  
    public void GorevSayiKontrol(int Sayi)
    {
        if (Sayi == TopDegeri)
        {
            Hedefler_UI[0].GorevTamam.SetActive(true);
            if (!KontrolTop)
            {
                ToplamGorevSayisi--;
                if (ToplamGorevSayisi == 0)
                {
                    Kazandin();
                }
                KontrolTop = true;
            } 
        }
    

    }
    public void Kazandin()
    {
        Paneller[0].SetActive(true);
    }
    public void Kaybettin()
    {
        Paneller[1].SetActive(true);
    }

    public void LevelTekrari()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SonrakiLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}