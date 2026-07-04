using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;

    void OnEnable()
    {
        Player.OnAmmoChanged += UpdateAmmoDisplay;
    }
    void OnDisable()
    {
        Player.OnAmmoChanged -= UpdateAmmoDisplay;
    }

    private void UpdateAmmoDisplay(int newAmmo)
    {
        ammoText.text = "Ammo: " + newAmmo;
    }
}
