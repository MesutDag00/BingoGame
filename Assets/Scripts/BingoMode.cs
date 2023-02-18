using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BingoMode")]
public class BingoMode : ScriptableObject
{
    public int Id;
    public Color BackGroundImage;
    public Modes BingoType;
    public int BingoNext;
    public bool ActiveBingo;
    public int Gold;
}

public enum Modes
{
    BronzBingo,
    SilverBingo,
    GoldBingo
}