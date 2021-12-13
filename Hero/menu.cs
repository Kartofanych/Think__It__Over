using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] cvet;
    public GameObject[] necvet;
    public TMPro.TextMeshPro[] texts;
    public GameObject training_map;
    void Start()
    {
        if (!PlayerPrefs.HasKey("level")) PlayerPrefs.SetInt("level", 0);

        if(PlayerPrefs.GetInt("level") == 0)
        {
            necvet[0].SetActive(false);
            necvet[1].SetActive(true);
            necvet[2].SetActive(true);
            cvet[0].SetActive(true);
            cvet[1].SetActive(false);
            cvet[2].SetActive(false);
            training_map.SetActive(false);

        } 

        if (PlayerPrefs.GetInt("level") == 1)
        {
            necvet[0].SetActive(false);
            necvet[1].SetActive(false);
            necvet[2].SetActive(true);
            cvet[0].SetActive(true);
            cvet[1].SetActive(true);
            cvet[2].SetActive(false);
            if (PlayerPrefs.GetInt("time1") < 60)
            {
                texts[0].text = "0 min " + PlayerPrefs.GetInt("time1") + " sec";
            }
            else
            {
                texts[0].text = PlayerPrefs.GetInt("time1")/60 + " min " + PlayerPrefs.GetInt("time1")%60 + " sec";
            }
        }
        else if (PlayerPrefs.GetInt("level") == 2)
        {
            necvet[0].SetActive(false);
            necvet[1].SetActive(false);
            necvet[2].SetActive(false);
            cvet[0].SetActive(true);
            cvet[1].SetActive(true);
            cvet[2].SetActive(true);
            if (PlayerPrefs.GetInt("time1") < 60)
            {
                texts[0].text = "0 min " + PlayerPrefs.GetInt("time1") + " sec";
            }
            else
            {
                texts[0].text = PlayerPrefs.GetInt("time1") / 60 + " min " + PlayerPrefs.GetInt("time1") % 60 + " sec";
            }
            if (PlayerPrefs.GetInt("time2") < 60)
            {
                texts[1].text = "0 min " + PlayerPrefs.GetInt("time2") + " sec";
            }
            else
            {
                texts[1].text = PlayerPrefs.GetInt("time2") / 60 + " min " + PlayerPrefs.GetInt("time2") % 60 + " sec";
            }
        }
        else
        if (PlayerPrefs.GetInt("level") == 3)
        {

            necvet[0].SetActive(false);
            necvet[1].SetActive(false);
            necvet[2].SetActive(false);

            cvet[0].SetActive(true);
            cvet[1].SetActive(true);
            cvet[2].SetActive(true);

            if (PlayerPrefs.GetInt("time1") < 60)
            {
                texts[0].text = "0 min " + PlayerPrefs.GetInt("time1") + " sec";
            }
            else
            {
                texts[0].text = PlayerPrefs.GetInt("time1") / 60 + " min " + PlayerPrefs.GetInt("time1") % 60 + " sec";
            }
            if (PlayerPrefs.GetInt("time2") < 60)
            {
                texts[1].text = "0 min " + PlayerPrefs.GetInt("time2") + " sec";
            }
            else
            {
                texts[1].text = PlayerPrefs.GetInt("time2") / 60 + " min " + PlayerPrefs.GetInt("time2") % 60 + " sec";
            }
            if (PlayerPrefs.GetInt("time3") < 60)
            {
                texts[2].text = "0 min " + PlayerPrefs.GetInt("time3") + " sec";
            }
            else
            {
                texts[2].text = PlayerPrefs.GetInt("time3") / 60 + " min " + PlayerPrefs.GetInt("time3") % 60 + " sec";
            }
        }
    }
}
