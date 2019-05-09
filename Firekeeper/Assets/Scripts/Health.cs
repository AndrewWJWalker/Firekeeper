using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] private int _healthPoints;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Transform _healthBarSpawnPoint;
    public int maxHealth;
    private Slider healthSlider;
    private GameObject healthBar;

    private void Start()
    {
        maxHealth = _healthPoints;

        healthBar = Instantiate(_healthBarPrefab, _canvas.transform);
        SetHealthBarPosition();


        healthSlider = healthBar.GetComponent<Slider>();
        UpdateHealthBar();
    }

    public void DealDamage(int damage)
    {
        _healthPoints -= damage;

        if (_healthPoints <= 0)
        {
            Die();
        }
        UpdateHealthBar();
    }

    void Die()
    {
        if (gameObject.GetComponent<Fence>() != null)
        {
            gameObject.GetComponent<Fence>().OpenBase();
        }

        this.gameObject.SetActive(false);
        healthBar.SetActive (false);
    }

    public void Heal(int heal)
    {
        _healthPoints += heal;
        if (_healthPoints > maxHealth)
        {
            _healthPoints = maxHealth;
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthSlider.value = GetHealthPercentage();
    }

    public int GetCurrentHealthPoints()
    {
        return _healthPoints;
    }

    public float GetHealthPercentage()
    {
        float p = (float)_healthPoints / (float)maxHealth;
        Debug.Log(this.gameObject.name + " health : " + p);

        return p;
    }
    
    void SetHealthBarPosition()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(_healthBarSpawnPoint.position);
        RectTransform rect = healthBar.GetComponent<RectTransform>();
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.GetComponent<RectTransform>(), screenPosition, Camera.main, out localPosition);
        healthBar.transform.localPosition = localPosition;
    }

    private void Update()
    {
        SetHealthBarPosition();
    }
}
