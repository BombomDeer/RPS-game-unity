using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPSGame : MonoBehaviour
{
    public Animator CharacterAnimator;
    enum handType
    {
        ROCK, PAPER, SCISSORS
    }

    enum outcome
    {
        VICTORY, DEFEAT, DRAW
    }

    class hand
    {
        handType me;
        handType loses;
        handType wins;
        string name;

        public void init(handType myHand, handType lossHand, handType winHand, string handName)
        {
            me = myHand;
            loses = lossHand;
            wins = winHand;
            name = handName;
        }

        public handType getMyHand()
        {
            return me;
        }

        public string getName()
        {
            return name;
        }

        public handType getLosesToHand()
        {
            return loses;
        }

        public handType getWinsHand()
        {
            return wins;
        }
    }

    Dictionary<handType, hand> hands = new Dictionary<handType, hand>();
    //handType myHand;

    [SerializeField] private Text _textUI;
    [SerializeField] private Text _resultTextUI;

    // Start is called before the first frame update
    void Start()
    {
        hand rock = new hand();
        rock.init(handType.ROCK, handType.PAPER, handType.SCISSORS, "Rock");
        hands.Add(handType.ROCK, rock);

        hand paper = new hand();
        paper.init(handType.PAPER, handType.SCISSORS, handType.ROCK, "Paper");
        hands.Add(handType.PAPER, paper);

        hand scissors = new hand();
        scissors.init(handType.SCISSORS, handType.ROCK, handType.PAPER, "Scissors");
        hands.Add(handType.SCISSORS, scissors);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GamePlay(handType myHand)
    {
        handType enemyHand = GenerateEnemyHand();
        ShowEnemyHand(enemyHand);

        outcome wld = determineOutcome(enemyHand, myHand);
        ShowOutcome(wld);
    }

    void ShowOutcome(outcome result)
    {
        string resultText;
        switch (result)
        {
            case outcome.VICTORY:
                resultText = "Victory";
                break;
            case outcome.DEFEAT:
                resultText = "Defeat";
                break;
            default:
                resultText = "Draw";
                break;
        }
        _resultTextUI.text = resultText;
    }

    handType GenerateEnemyHand()
    {
        int limit = hands.Count;
        int randomValue = Random.Range(0, limit);
        handType key = (handType)randomValue;
        return hands[key].getMyHand();
    }

    void ShowEnemyHand(handType enemyHand)
    {
        _textUI.text = hands[enemyHand].getName();
        //가위:23  바위:30 보:12
        CharacterAnimator.SetTrigger(hands[enemyHand].getName());
    }

    outcome determineOutcome(handType enemyHand, handType myHand)
    {
        hand me = hands[myHand];
        if (enemyHand == me.getLosesToHand())
            return outcome.DEFEAT;
        else if (enemyHand == me.getWinsHand())
            return outcome.VICTORY;
        else
            return outcome.DRAW;

    }


    public void onRockPress()
    {
        GamePlay(handType.ROCK);
    }
    public void onPaperPress()
    {
        GamePlay(handType.PAPER);
    }
    public void onScissorsPress()
    {
        GamePlay(handType.SCISSORS);
    }
}
