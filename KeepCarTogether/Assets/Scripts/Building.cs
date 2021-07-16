using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    MouseRaycaster mouseDetect;
    public GameObject centrePiece;
    private Car car;

    public Pieces pieces;
    public UIStuff UIPieces;

    public GameObject selectedType;

    public bool canBuild = true;

    void Start()
    {
        mouseDetect = GetComponent<MouseRaycaster>();
        mouseDetect.PickedPart += NewPart;
        mouseDetect.PlacePart += AddPart;

        car = centrePiece.GetComponent<Car>();
    }

    void Update()
    {
        
    }

    private void NewPart(PartType type)
    {
        if (canBuild)
        {
            ChangePieceUI(type);
        }
    }

    private void AddPart(Point point)
    {
        Quaternion rot = Quaternion.identity;
        if(selectedType.GetComponent<CarPart>().partType == PartType.wheel
            && centrePiece.transform.position.x - point.pos.x < 0)
        {
            rot = Quaternion.Euler(0, 180, 0);
        }

        Instantiate(selectedType, point.pos, rot, centrePiece.transform);
        ChangeCarStats(selectedType.GetComponent<CarPart>().partType);

        Destroy(point.gameObject);
    }

    private void ChangeCarStats(PartType type)
    {
        car.numParts++;
        if(type == PartType.floor){
            car.Sturdiness += 10;
            UIPieces.sturdinessText.text = "Sturdiness: " + car.Sturdiness;
            car.Weight += 10;
        }
        if (type == PartType.engine)
        {
            car.Power += 50;
            UIPieces.powerText.text = "Power: " + car.Power;
            car.Weight += 50;
        }
        if (type == PartType.wheel)
        {
            car.Sturdiness += 50;
            UIPieces.sturdinessText.text = "Sturdiness: " + car.Sturdiness;
            car.Weight += 20;
        }
        UIPieces.weightText.text = "Weight: " + car.Weight;
    }

    public void ChangePieceUI(PartType type)
    {
        UIPieces.smallEngine.SetActive(false);
        UIPieces.smallFloor.SetActive(false);
        UIPieces.smallWheel.SetActive(false);
        if (type == PartType.floor)
        {
            UIPieces.smallFloor.SetActive(true);
            selectedType = pieces.FloorPiece;
        }
        else if (type == PartType.wheel)
        {
            UIPieces.smallWheel.SetActive(true);
            selectedType = pieces.WheelPiece;
        }
        else if (type == PartType.engine)
        {
            UIPieces.smallEngine.SetActive(true);
            selectedType = pieces.EnginePiece;
        }
    }

}

[System.Serializable]
public struct Pieces
{
    public GameObject FloorPiece;
    public GameObject EnginePiece;
    public GameObject WheelPiece;
}

[System.Serializable]
public struct UIStuff
{
    public GameObject smallFloor;
    public GameObject smallEngine;
    public GameObject smallWheel;

    public Text powerText;
    public Text weightText;
    public Text sturdinessText;

}