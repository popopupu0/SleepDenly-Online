using System.Collections;
using System.Collections.Generic;
using SleepDenly;
using UnityEngine;


namespace SleepDenly
{
    public class SpellEffectApplier
    {
        public static void ApplySpell(CardData hourCard, CharacterStats targetStats)
        {
            if (targetStats.age >= hourCard.ageRequired)
            {
                targetStats.currentSleepHours += hourCard.hourEffect;
            }
        }
    }
}