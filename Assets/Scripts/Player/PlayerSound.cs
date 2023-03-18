using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSound : MonoBehaviour
{
    [SerializeField] private float _volume = 1f;

    private Player _player;
    private float _footStepTimer;
    private float _footStepTimerMax = 0.1f;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _footStepTimer -= Time.deltaTime;

        if (_footStepTimer <= 0f)
        {
            _footStepTimer = _footStepTimerMax;

            if (_player.IsWalking())
                KitchenSounds.Instanse.PlayStepSound(_player.transform.position, _volume);
        }
    }
}
