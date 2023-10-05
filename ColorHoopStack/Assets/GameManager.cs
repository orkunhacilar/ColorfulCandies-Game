using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject SeciliObje;
    GameObject SeciliStand;
    Cember _Cember;
    public bool HaraketVar;

    //bunlar sonra gelecegiz
    public int HedefStandSayisi;
    int TamamlananStandSayisi;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //sol click atildigi zaman
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100)) //kameradan isin yolladik. hit carptigi objeyi temsil ediyor.
            {
                if(hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    

                    if(SeciliObje!=null && SeciliStand != hit.collider.gameObject) //SeciliObje en usteki cember . SeciliStand anastandim. Else kismini incele 49
                    { //bir cemberi gonderme

                        Stand _Stand = hit.collider.GetComponent<Stand>(); // Yeni secmis oldugum standi aliyorum.
                        SeciliStand.GetComponent<Stand>().SoketDegistirmeIslemleri(SeciliObje); //Bir cemberi baska yere yollarken mevcut standdan sil dedik.  *(ESKISINDEN SIL)*

                        _Cember.HaraketEt("PozisyonDegistir", hit.collider.gameObject,_Stand.MusaitSoketiVer(),_Stand.HaraketPozisyonu);

                        

                        _Stand.BosOlanSoket++; //Cunku yeni bir tane cember koyduk ya yeniye. Simdi onun BosOlanSoket numarasini 1 artiriyorum ki. Yeni bisi gelirse o numaranin yerine gelicek (List icindeki siralanislarla alakali.(Stande tiklayip inspectordan incele daha rahat anlarsin.))
                        _Stand._Cemberler.Add(SeciliObje);  //  *(YENISINE EKLE)*

                        //Sonra bu ikisini null yapiyorum ki asagidaki else kismi tekrardan calissin hani stand hangi cember gene o degerleri alip islemlere basliyalim diye.
                        SeciliObje = null;    
                        SeciliStand = null;
                    }
                    else //KODLARDA ILK ONCE BU ASAMA CALISIYOR.
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>(); //Carptigi objenin stand scriptini al _Stande at dedik
                        SeciliObje = _Stand.EnUsttekiCemberiVer(); // en usteki cemberi secili objeye atiyoruz. ARTIK SECILIOBJE BIR CEMBER
                        _Cember = SeciliObje.GetComponent<Cember>(); //en usteki cemberin sciptini alip _Cembere attik.
                        HaraketVar = true;

                        if (_Cember.HaraketEdebilirmi)
                        {
                            _Cember.HaraketEt("Secim", null, null, _Cember._AitOlduguStand.GetComponent<Stand>().HaraketPozisyonu);

                            SeciliStand = _Cember._AitOlduguStand;
                        }
                    }
                }
            }
        }

    }
}
