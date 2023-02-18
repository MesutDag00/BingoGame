using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using JetBrains.Annotations;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class BingoControllerManger : MonoBehaviour
{
    public List<BingoBox> BingoBoxes;

    public int BallCount;
    public int _bingoCounter;
    private int _repatingDesk;

    public bool RepatingInfo = true;

    public TMP_Text BallCountText;
    public TMP_Text BallText;
    public TMP_Text GoldText;

    public List<BingoMode> BingoMode;
    public GameObject[] SelectPanel;
    public GameObject Winner;

    public Button CollectButton;
    public Button BingoCardButton;

    private void Start()
    {
        SelectPanel[BingoMode.First(m => m.ActiveBingo).Id].SetActive(false);
        AssingBingo();
        StartCoroutine(BingoInfo());
    }

    private void AssingBingo()
    {
        for (int i = 0; i < BingoBoxes.Count; i++)
            BingoBoxes[i].BingoText.text = Random.Range(0, 100).ToString();
        BingoBoxes[12].CheckImage.GetComponent<Image>().color = BingoMode.First(m => m.ActiveBingo).BackGroundImage;
    }

    private void Conclusion()
    {
        for (int k = 0; k < BingoPayTableManger.PayTableEvent.Length; k++)
        {
            int a = 0;
            for (int i = 0; i < BingoPayTableManger.PayTableEvent[k].Line.Length; i++)
                if (BingoBoxes[BingoPayTableManger.PayTableEvent[k].Line[i]].Check &&
                    !BingoPayTableManger.PayTableEvent[k].Check)
                {
                    a++;
                    if (a == 5)
                    {
                        BingoPayTableManger.PayTableEvent[k].Check = true;
                        _bingoCounter++;
                        if (BingoMode.First(m => m.ActiveBingo).BingoNext <= _bingoCounter)
                        {
                            BingoCardButton.enabled = false;
                            Invoke(nameof(WinnerPanel), 0.3f);
                        }
                    }
                }
        }
    }

    private void WinnerPanel()
    {
        Winner.SetActive(true);
        GoldText.text = BingoMode.First(n => n.ActiveBingo).Gold.ToString();
        CollectButton.onClick.AddListener(PrizePanel);
    }

    private void PrizePanel()
    {
        var a = BingoMode.First(n => n.ActiveBingo);
        Debug.Log("Toplam Kazanç " + a.Gold.ToString());
        Invoke(nameof(NextGameScreen), 0.1f);
        CollectButton.enabled = false;
    }

    private IEnumerator BingoInfo()
    {
        int a = 0;
        while (RepatingInfo)
        {
            if (_repatingDesk >= BingoPayTableManger.PayTableEvent.Length )
            {
                a = a >= BingoPayTableManger.PayTableEvent.Length  ? a = 0 : a;
                _repatingDesk = 0;
                yield return new WaitForSeconds(3f);
            }

            BingoRepatingBox(true, a);
            yield return new WaitForSeconds(0.8f);
            BingoRepatingBox(false, a);
            a++;
        }
    }

    private void BingoRepatingBox(bool active, int desk)
    {
        for (int i = 0; i < BingoPayTableManger.PayTableEvent[desk].Line.Length; i++)
            BingoBoxes[BingoPayTableManger.PayTableEvent[desk].Line[i]].InfoImage.SetActive(active);
        if (!active)
            _repatingDesk++;
    }

    private void DefaultValue()
    {
        for (int i = 0; i < BingoBoxes.Count; i++)
        {
            BingoPayTableManger
                .PayTableEvent[
                    i < BingoPayTableManger.PayTableEvent.Length ? i : BingoPayTableManger.PayTableEvent.Length - 1]
                .Check = false;
            BingoBoxes[i].BingoText.color = Color.black;
            BingoBoxes[i].Check = false;
            BingoBoxes[i].CheckImage.SetActive(i == 12);
        }
        AssingBingo();
        _bingoCounter = 0;
    }

    private void NextGameScreen()
    {
        int mode = BingoMode.First(m => m.ActiveBingo).Id;
        Winner.SetActive(false);
        Debug.Log("NextGameScene");
        BingoMode[mode].ActiveBingo = false;
        SelectPanel[mode].SetActive(true);
        mode = mode == 2 ? mode : mode + 1;
        BingoMode[mode].ActiveBingo = true;
        SelectPanel[mode].SetActive(false);
        CollectButton.enabled = true;
        BingoCardButton.enabled = true;

        DefaultValue();
    }

    public void BringBall()
    {
        if (BallCount <= 0) return;
        int a = Random.Range(0, 100);
        BallText.text = a.ToString();
        BallCountText.text = (BallCount -= 1).ToString();

        foreach (var t in BingoBoxes)
        {
            if (a == int.Parse(t.BingoText.text))
            {
                t.BingoText.color = Color.white;
                t.CheckImage.SetActive(t.Check = true);
            }
        }

        Conclusion();
    }
}

[Serializable]
public class BingoBox
{
    public Text BingoText;

    public bool Check;

    public GameObject CheckImage;

    public GameObject InfoImage;
}