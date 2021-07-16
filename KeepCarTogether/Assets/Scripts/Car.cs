using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public int numParts;

    public int Power;
    public int Weight;
    public int Sturdiness;

    public Text sturdinessText;
    public Text Score;

    public List<GameObject> WeldSpots;
    public GameObject weldDeathPS;

    public bool breaking = false;
    public bool racing = false;
    public float breakRateCoef;
    public float forceCoef;
    private int breakRate;

    private Rigidbody rb;
    private Vector3 moveForce;

    private float prevTime;
    private bool yoi = true;


    private void Awake()
    {
        WeldSpots = new List<GameObject>();
    }

    public void calculateBreakRate()
    {
        breakRate = (int)(Power * breakRateCoef) + 1;
    }

    public void CalculateForces()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        moveForce = transform.forward * (Power * forceCoef);
    }

    public void Update()
    {
        if (breaking)
        {
            if (WeldSpots.Count > 0)
            {
                for (int i = 0; i < breakRate; i++)
                {

                    int index = Random.Range(0, WeldSpots.Count);
                    GameObject spot = WeldSpots[index];
                    Instantiate(weldDeathPS, spot.transform.position, Quaternion.identity);
                    Destroy(spot);
                    WeldSpots.RemoveAt(index);
                    Sturdiness--;
                    UpdateText();

                }
            }
            else if(Time.time - prevTime >= 0.05 && WeldSpots.Count <= 5)
            {
                Sturdiness -= 5;
                UpdateText();
                prevTime = Time.time;
            }

        }

        if (racing && Sturdiness > 0 && yoi)
        {
            rb.AddForce(moveForce);
        }

        if(racing && Sturdiness <= 0)
        {
            rb.mass += 1000;
            yoi = false;
            StartCoroutine(boi());
        }

    }

    public void UpdateText()
    {
        sturdinessText.text = "Sturdiness: " + Sturdiness;
    }

    private IEnumerator boi()
    {
        float score = Vector3.Distance(new Vector3(0, 0, 0), transform.position);
        Score.text = "Score: " + (int)score;
        yield return new WaitForSeconds(10);
        Application.Quit();
    }

}
