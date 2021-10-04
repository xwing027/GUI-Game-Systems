using System.Collections;
using System;
using UnityEngine;

public class Stats : Attributes
{
    #region Structs
    [Serializable]
    public struct StatBlock
    {
        public string name;
        public int value;
        public int tempValue;
        public int levelTempValue;
    }
    #endregion

    #region Variables
    public StatBlock[] characterStats = new StatBlock[6];
    public CharacterClass characterClass = CharacterClass.Barbarian;
    public CharacterRace characterRace = CharacterRace.Human;
    #endregion
}
public enum CharacterClass
{
    Barbarian,
    Bard,
    Cleric,
    Druid,
    Monk,
    Paladin,
    Ranger,
    Rogue,
    Sorcerer,
    Warlock,
    Wizard,
}

public enum CharacterRace
{
    Dragonborn,
    Dwarf,
    Elf,
    Gnome,
    HalfElf,
    Halfling,
    HalfOrc,
    Human,
    Tiefling,
}