using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class HPHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnHPChanged))]
    byte HP { get; set; }

    [Networked(OnChanged = nameof(OnStateChanged))]
    public bool isDead { get; set; }

    bool isInitialized = false;

    const byte startingHP = 5;

    public Color uiOnHitColor;

    public Image uiOnHitImage;

    public MeshRenderer bodyMeshRenderer;
    Color defaultMeshBodyColor;

    public GameObject playerModel;
    public GameObject deathGameObjectPrefab;

    public bool skipSettingStartValues = false;

    HitboxRoot hitboxRoot;
    CharacterMovementHandler characterMovementHandler;
    NetworkInGameMessages networkInGameMessages;
    NetworkPlayer networkPlayer;
    private void Awake()
    {
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
        hitboxRoot = GetComponentInChildren<HitboxRoot>();
        networkInGameMessages = GetComponent<NetworkInGameMessages>();
        networkPlayer = GetComponent<NetworkPlayer>();
    }

    void Start()
    {
        if (!skipSettingStartValues)
        {
            HP = startingHP;
            isDead = false;
        }
        defaultMeshBodyColor = bodyMeshRenderer.material.color;

        isInitialized = true;
    }

    IEnumerator OnHitEffect()
    {
        bodyMeshRenderer.material.color = Color.white;

        if (Object.HasInputAuthority)
        {
            uiOnHitImage.color = uiOnHitColor;
        }

        yield return new WaitForSeconds(0.2f);

        bodyMeshRenderer.material.color = defaultMeshBodyColor;

        if (Object.HasInputAuthority && !isDead)
        {
            uiOnHitImage.material.color = new Color(0,0,0,0);
        }
    }

    IEnumerator ServerRevive()
    {
        yield return new WaitForSeconds(2.0f);

        characterMovementHandler.RequestRespawn();
    }

    public void OnTakeDamage(string AttackerNickName, byte damageAmount)
    {
        if (isDead)
        {
            return;
        }

        if (damageAmount > HP)
            damageAmount = HP;

        HP -= damageAmount;

        if (HP <= 0)
        {
            networkInGameMessages.SendInGameRPCMessage(AttackerNickName,$"Killed <b>{networkPlayer.nickName}</b");
            StartCoroutine(ServerRevive());
            isDead = true;
        }
    }

    static void OnHPChanged(Changed<HPHandler> changed)
    {
        byte newHP = changed.Behaviour.HP;

        changed.LoadOld();

        byte oldHP = changed.Behaviour.HP;

        if (newHP < oldHP)
        {
            changed.Behaviour.OnHPReduced();
        }
    }

    void OnDeath()
    {
        playerModel.gameObject.SetActive(false);
        hitboxRoot.HitboxRootActive = false;
        characterMovementHandler.SetCharacterControllerEnabled(false);

        Instantiate(deathGameObjectPrefab,transform.position,Quaternion.identity);
        
    }

    void OnRevive()
    {
        if (Object.HasInputAuthority)
        {
            uiOnHitImage.color = new Color(0, 0, 0, 0);
        }

        playerModel.gameObject.SetActive(true);
        hitboxRoot.HitboxRootActive = true;
        characterMovementHandler.SetCharacterControllerEnabled(true);
    }

    void OnHPReduced()
    {
        if (!isInitialized)
        {
            return;
        }

        StartCoroutine(OnHitEffect());
    }

    static void OnStateChanged(Changed<HPHandler> changed)
    {
        bool isDeadCurrent = changed.Behaviour.isDead;

        changed.LoadOld();

        bool isDeadOld = changed.Behaviour.isDead;

        if (isDeadCurrent)
        {
            changed.Behaviour.OnDeath();
        }
        else if (!isDeadCurrent && isDeadOld)
        {
            changed.Behaviour.OnRevive();
        }
    }

    public void OnRespawned()
    {
        HP = startingHP;
        isDead = false;
    }
}
