using UnityEngine;

public class AcidPool : MonoBehaviour
{
    public int damage;
    public float damageTime;

    private float _DamageTimer;
    private bool _Enemy;
    private GameObject _EnemyGObj;
    private bool _player;
    private GameObject _playerGObj;

    private void Update()
    {
        if (_DamageTimer <= 0)
        {
            if (_player && _playerGObj != null)
            {
                _playerGObj.GetComponent<PlayerHealthController>().DamageFromEnemy(damage);
                _DamageTimer = damageTime;
            }

            if(_Enemy && _EnemyGObj != null)
            {
                _EnemyGObj.GetComponent<EnemyHealthController>().DamageEnemy(damage, null);
                _DamageTimer = damageTime;
            }
        }
        else
        {
            _DamageTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = true;
            _playerGObj = collision.gameObject;
            _DamageTimer = damageTime;
        }
        else if (collision.CompareTag("Enemy"))
        {
            _Enemy = true;
            _EnemyGObj = collision.gameObject;
            _DamageTimer = damageTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = false;
            _playerGObj = null;
            _DamageTimer = damageTime;
        }
        else if (collision.CompareTag("Enemy"))
        {
            _Enemy = false;
            _EnemyGObj = null;
            _DamageTimer = damageTime;
        }
    }
}
