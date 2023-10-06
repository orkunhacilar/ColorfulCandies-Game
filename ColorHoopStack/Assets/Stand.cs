using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject HaraketPozisyonu;
    public GameObject[] Soketler;
    public int BosOlanSoket;
    public List<GameObject> _Cemberler = new();
    [SerializeField] private GameManager _GameManager;

    int CemberTamamlanmaSayisi;


    public GameObject EnUsttekiCemberiVer()
    {
        return _Cemberler[^1];  //_Cemberler[_Cemberler.Count - 1]; ayini sey demek
    }

    public GameObject MusaitSoketiVer()
    {
        return Soketler[BosOlanSoket];
    }

    public void SoketDegistirmeIslemleri(GameObject SilinecekObje)
    {
        _Cemberler.Remove(SilinecekObje);

        if (_Cemberler.Count != 0) //Eger sildigim yerdeki cember sayisi 0 degilse
        {
            BosOlanSoket--; //sildigim icin bos olan soket sayim mantiken dusmus oldu (bir azalt)
            _Cemberler[^1].GetComponent<Cember>().HaraketEdebilirmi = true; // ve en sonda bulunan (yani en ustte) yeni olan o cemberin haraket edebilirmisini true yap 
        }
        else // eger cember zaten yoksa 
        {
            BosOlanSoket = 0; // yani 0 cemberli bisise bu  BosOlanSoketi 0 yapiyoruz cunku yeni gelicek olan sey ilk yere eklenicek. 0 en alttaki soket 1 ondan sonrakisoket yeri 2 bla bla array gibi.
        }
    }


    public void CemberleriKontrolEt()
    {
        if (_Cemberler.Count == 4)
        {
            string Renk = _Cemberler[0].GetComponent<Cember>().Renk;

            foreach(var item in _Cemberler)
            {
                if (Renk == item.GetComponent<Cember>().Renk)
                    CemberTamamlanmaSayisi++;
            }

            if (CemberTamamlanmaSayisi == 4)
            {
                _GameManager.StandTamamlandi();
                TamamlanmisStandIslemleri();
            }
            else
            {
               
                CemberTamamlanmaSayisi = 0;
            }
        }
    }

    void TamamlanmisStandIslemleri() //biten standlardaki cemberlerin opaklastirilmasi haraketlerin durdurulmasi ve tamamlanmis stand tagi verme
    {
        foreach(var item in _Cemberler)
        {
            item.GetComponent<Cember>().HaraketEdebilirmi = false;

            MeshRenderer renderer = item.GetComponent<MeshRenderer>();
            Material material = renderer.material;
            Color color = material.color;
            color.a = 0.5f;
            material.color = color;

            gameObject.tag = "TamamlanmisStand";
        }
    }

}
