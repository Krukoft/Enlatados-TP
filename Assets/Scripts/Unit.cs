using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enlatados.Player.Units
{
    [CreateAssetMenu(fileName = "New player unit", menuName = "New unit")]

    public class Unit : ScriptableObject
    {
        public enum unitType
        {
            Soldier,
            SoldierHero,
            ZombieAlly,

        }

        public unitType Type;

        public bool isPlayerUnits;

        public string unitName;

        public GameObject unitPrefab;

        public int attack;
        public int health;

    }

}