using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private string stateName = "PlayerSpriteAnimation";
    [SerializeField] private float duration = 1f; // seconds for one way

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0f;
    }

    private void Update()
    {
        float t = Mathf.PingPong(Time.time / duration, 1f);
        anim.Play(stateName, 0, t);
    }
}
