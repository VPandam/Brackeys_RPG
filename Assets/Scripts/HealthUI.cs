using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform uiTarget;

    float coolDown = 5;
    float lastTimeShowed;

    Transform ui;
    Image healthSlider;

    Camera cam;

    CharacterStats _stats;

    // Start is called before the first frame update
    void Start()
    {
        _stats = GetComponent<CharacterStats>();
        cam = Camera.main;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.tag == "HealthUICanvas")
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        _stats.OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            lastTimeShowed = Time.time;

            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }

        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = uiTarget.position;
            ui.forward = -cam.transform.forward;
            if (Time.time - lastTimeShowed >= coolDown)
            {
                ui.gameObject.SetActive(false);
            }
        }
    }
}
