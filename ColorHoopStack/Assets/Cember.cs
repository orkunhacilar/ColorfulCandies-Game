using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cember : MonoBehaviour
{

    public GameObject _AitOlduguStand;
    public GameObject _AitOlduguCemberSoketi; 
    public bool HaraketEdebilirmi;
    public string Renk;
    public GameManager _GameManager;

    GameObject HaraketPozisyonu;
    GameObject GidecegiStand;

    bool Secildi, PosDegistir, SoketOtur, SoketeGeriGit;

                                             //GIDECEGI STAND //GIDECEGI STANDIN MUSAIT SOKETI
    public void HaraketEt(string islem, GameObject Stand = null, GameObject Soket = null, GameObject GidilecekObje = null)
    {
        switch (islem)
        {
            case "Secim":
                HaraketPozisyonu = GidilecekObje; // GidilecekObje kendi haraket pozisyonum aslinda. GameManager satir 58
                Secildi = true;
                break;
            case "PozisyonDegistir":
                GidecegiStand = Stand;
                _AitOlduguCemberSoketi = Soket;     // _AitOlduguCemberSoketi  --->  Gidecegi yerdeki bos olan cember soketi diyebiliriz.
                HaraketPozisyonu = GidilecekObje;
                PosDegistir = true;
                break;
            case "SoketeGeriGit":
                SoketeGeriGit = true;
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Secildi)
        {                                                     
            transform.position = Vector3.Lerp(transform.position, HaraketPozisyonu.transform.position, .2f); // cemberi al ve kendi haraket pozisyonuna kaldir. 25'i oku.
            if(Vector3.Distance(transform.position, HaraketPozisyonu.transform.position) < .10) // aralarindaki mesafe bu kadar kaldiysa secildiyi false yap
            {
                Secildi = false;
            }
        }
        if (PosDegistir)
        {
            transform.position = Vector3.Lerp(transform.position, HaraketPozisyonu.transform.position, .2f);
            if (Vector3.Distance(transform.position, HaraketPozisyonu.transform.position) < .10) // aralarindaki mesafe bu kadar kaldiysa secildiyi false yap
            {
                PosDegistir = false;
                SoketOtur = true;
            }
        }
        if (SoketOtur)
        {
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .2f);
            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10) // aralarindaki mesafe bu kadar kaldiysa secildiyi false yap
            {
                _GameManager.sesler[1].Play();
                transform.position = _AitOlduguCemberSoketi.transform.position;
                
                SoketOtur = false;

                _AitOlduguStand = GidecegiStand;

                if (_AitOlduguStand.GetComponent<Stand>()._Cemberler.Count > 1) // 2 taneden fazla cember varsa kisaca
                {
                    _AitOlduguStand.GetComponent<Stand>()._Cemberler[^2].GetComponent<Cember>().HaraketEdebilirmi = false;  // sondan bir oncekini yani 4 varsa 3cunun haraket edebilirmisini false yap diyoruz. Cunku yeni bir tane sonuncumuz var
                }
                _GameManager.HaraketVar = false;
            }
        }

        if (SoketeGeriGit)
        {
            _GameManager.sesler[0].Play();

            //_AitOlduguCemberSoketi zaten kendi icinde bulundugu obje
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .2f);
            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10) // aralarindaki mesafe bu kadar kaldiysa secildiyi false yap
            {
                transform.position = _AitOlduguCemberSoketi.transform.position;
                SoketeGeriGit = false;
                _GameManager.HaraketVar = false;
            }
        }
    }
}
