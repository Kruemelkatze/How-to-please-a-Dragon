using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Dragon : SceneSingleton<Dragon>
{
    public int Amount = 2000;
    public float AvgPeriod = 12.5F;
    public float RandomPeriodDeviation = 2.5F;

    public Dimmer Dimmer;
    public float DimmerTimeValue = 0.4f;

    public bool DragonIsWorking = false;
    public float NextArrival;

    [Header("Timing")] public float FlapWaitTime = 3;
    public float FlyOverWaitTime = 2;
    public float ShakeDuration = 1.5f;
    public float ShakeWaitDuration = 1.5f;

    // Use this for initialization
    void Start()
    {
        Invoke(nameof(BringBackLootPeriodic), NextArrival = GetRandomDelay());
        GameManager.Instance.OnGameEnd += StopInvoking;
    }

    private void StopInvoking(GameEndReason reason)
    {
        CancelInvoke(nameof(BringBackLootPeriodic));
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameEnd -= StopInvoking;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.DragonCanCarry)
        {
            NextArrival -= Time.deltaTime;
        }
    }

    private float GetRandomDelay()
    {
        var delay = AvgPeriod + UnityEngine.Random.Range(-RandomPeriodDeviation, RandomPeriodDeviation);
        Debug.Log("\tDragon Delay:" + delay);

        return delay;
    }

    private void BringBackLootPeriodic()
    {
        Debug.Log("Periodic Dragon *rawr*");
        if (!GameManager.Instance.DragonCanCarry)
            return;

        StartCoroutine(BringBackLootWaiting());
    }

    IEnumerator BringBackLootWaiting(int? amount = null)
    {
        if (GameManager.Instance.DragonCanCarry && !DragonIsWorking)
            yield return StartCoroutine(DoDragonStuff(amount));

        // Invoke next
        Invoke(nameof(BringBackLootPeriodic), NextArrival = GetRandomDelay());
    }

    //For External use
    public void BringBackLoot(int? amount = null)
    {
        if (!GameManager.Instance.DragonCanCarry || DragonIsWorking)
            return;

        StartCoroutine(DoDragonStuff(amount));
    }

    private IEnumerator DoDragonStuff(int? amount)
    {
        DragonIsWorking = true;

        var value = amount ?? Amount;
        // Flap
        Debug.Log("\tFlap Wings");
        AudioControl.Instance.PlaySound("flap");
        yield return new WaitForSeconds(FlapWaitTime);

        //Shadow
        Debug.Log("\tShadow");
        AudioControl.Instance.PlaySound("flyover");
        Dimmer.SmoothTime = DimmerTimeValue;
        Dimmer.Dim();
        yield return new WaitForSeconds(FlyOverWaitTime);

        //Screenshake
        Debug.Log("\tScreen Shake");
        CameraShake.Instance.Shake(ShakeDuration);
        yield return new WaitForSeconds(ShakeWaitDuration);

        //Drop
        Debug.Log("\tDrop Loot");
        AudioControl.Instance.PlaySound("coins");
        LootDropper.Instance.Drop(value);

        //Show Icon
        DragonsPersonality.Instance.ShowDragonText();

        DragonIsWorking = false;
    }
}