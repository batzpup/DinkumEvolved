using DinkumEvolved.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

namespace DinkumEvolved
{
    partial class HackMain : MonoBehaviour
    {
        private IEnumerator EntityUpdate;
        private IEnumerator EntityUpdateFunct(float time)
        {
            yield return new WaitForSeconds(time);
            // Update Entities here //

            AssignCamera();
            hackLogic.animals = FindObjectsOfType<AnimalAI>().ToList();
            hackLogic.npcs = FindObjectsOfType<NPCAI>().ToList();
            hackLogic.players = FindObjectsOfType<CharMovement>().ToList();
            hackLogic.tileObjects = FindObjectsOfType<TileObject>().ToList();
            ///////////////////////////
            EntityUpdate = EntityUpdateFunct(5);
            StartCoroutine(EntityUpdate);

        }
    }
}