using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Script : MonoBehaviour {
    [SerializeField]
    int MaxHealth = 100, CurrentHealth, Armor = 0, Power = 0;
    bool tagged = false;
    //List<Buff> buffs;

    #region MonoBehaviour
    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {   
        //Checking if dead.
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    //Gameplay mechanics like damage reduction functions, etc.
    #region Protected Methods (All entities should experience combat under the same conditions.)
    public void Damage(int val)
    {
        int temp = val - Armor;
        if (temp < 0)
        {
            temp = 0;
        }
        CurrentHealth -= temp;
    }

    public void Heal(int val)
    {
        if (val + CurrentHealth > MaxHealth)
        {
            val = MaxHealth-CurrentHealth;
        }
        CurrentHealth += val;
    }
    #endregion

    //Used to read necessary variables for data calculations.
    #region Accessors
    public int GetMaxHP()
    {
        return MaxHealth;
    }
    
    public int GetCurrentHP()
    {
        return MaxHealth;
    }

    public int GetArmor()
    {
        return Armor;
    }

    public int GetDamage()
    {
        return Power;
    }

    public bool GetTagged()
    {
        return tagged;
    }
    #endregion

    //Used to modify appropriate variables. These should only be used for setup calculations as necessary. (Buffs/Pre-Combat stuff)
    #region Mutators
    public void SetMaxHP(int val)
    {
        MaxHealth = val;
    }
    
    public void SetCurrentHP(int val)
    {
        CurrentHealth = val;
    }

    public void SetArmor(int val)
    {

    }

    public void SetDamage(int val)
    {

    }

    public void SetTagged(bool val)
    {
        tagged = val;
    }
    #endregion
}
