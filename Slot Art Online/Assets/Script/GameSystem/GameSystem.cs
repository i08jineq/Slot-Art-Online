using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkLordGame;

public class GameSystem : MonoBehaviour {
    [SerializeField]private Transform slotNode;
    [SerializeField]private SlotMachine slotMachinePrefab;
    [SerializeField]private Button startSpinningButton;
    [SerializeField]private Text buttonText;

    private const string StartSpinningText = "Start Megumin";
    private const string StopSpinningText = "Stop Megumin";

    [SerializeField]private float spinningPeriod = 1.0f;

    private SlotMachine createdMachine;

    private bool isSpinning = false;
	// Use this for initialization
	void Start () {
        this.createdMachine = Instantiate<SlotMachine>(this.slotMachinePrefab, this.slotNode);
        this.createdMachine.Init();
        this.AddStartSpinningAction();
	}
	
    private void AddStartSpinningAction() {
        this.buttonText.text = StartSpinningText;
        this.startSpinningButton.onClick.AddListener(this.StartSpinning);
    }

    private void AddStopSpinningAction() {
        this.buttonText.text = StopSpinningText;
        this.startSpinningButton.onClick.AddListener(this.StopSpinning);
    }

    private void StartSpinning() {
        if(this.isSpinning) {
            return;
        }
        this.createdMachine.StartSpinningAll();
        this.startSpinningButton.interactable = false;
        this.isSpinning = true;
        StartCoroutine(this.stopSpinningCoroutine());
    }

    private IEnumerator stopSpinningCoroutine() {
        yield return new WaitForSeconds(spinningPeriod);
        this.startSpinningButton.interactable = true;
        this.startSpinningButton.onClick.RemoveAllListeners();
        this.AddStopSpinningAction();

    }

    private void StopSpinning() {
        this.isSpinning = false;
        this.createdMachine.StopSpinningAll();
        this.startSpinningButton.onClick.RemoveAllListeners();
        this.AddStartSpinningAction();
    }
	
}
