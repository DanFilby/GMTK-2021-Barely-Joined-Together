using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text timeLimitText;
    public GameObject welder;
    public Building builder;
    public Car car;

    public float buildingTimeLimit;
    public float weldingTimeLimit;

    private bool once;

    public GameObject Player;
    public Transform playerSpawn;

    void Start()
    {
        Player.GetComponent<Collider>().enabled = false;
        welder.SetActive(false);
    }

    void Update()
    {
        if(buildingTimeLimit > 0)
        {
            buildingTimeLimit -= Time.deltaTime;
            timeLimitText.text = "Build: " + Mathf.Round(buildingTimeLimit);
        }
        else if(weldingTimeLimit > 0)
        {
            builder.ChangePieceUI(PartType.none);
            builder.canBuild = false;
            Destroy(GameObject.Find("Point Parent"));

            welder.SetActive(true);
            weldingTimeLimit -= Time.deltaTime;
            timeLimitText.text = "Weld: " + Mathf.Round(weldingTimeLimit);
        }
        else
        {
            if (once == false)
            {
                StartRace();
                once = true;
            }
        }
    }

    private void StartRace()
    {
        GameObject.Find("Weld Parent").transform.parent = car.transform;


        Player.transform.parent = car.gameObject.transform;
        Player.transform.position = playerSpawn.position;
        Player.GetComponent<Collider>().enabled = false;

        car.breaking = true;
        car.calculateBreakRate();
        car.racing = true;
        car.CalculateForces();

    }

}
