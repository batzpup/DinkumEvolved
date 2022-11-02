using DinkumEvolved.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace DinkumEvolved.UI
{
    public class GuiHandler : MonoBehaviour
    {
        public GuiWindow Window;
        public float someFloat1 = 0;
        public float someFloat2 = 0;
        HackLogic hackLogic;
        private List<GameObject> someObjects;

        public void InitHacks(HackLogic hackLogic)
        {
            this.hackLogic = hackLogic;
        }
        public void Start()
        {
            Window = gameObject.AddComponent<GuiWindow>();
            Window.SetWindowTitle("Hack Menu");
            Window.SetParameters(20, 200);
            Window.AddNumField("Money To add", (int money) => hackLogic.AddMoney(money));
            Window.AddNumField("Permit Points To add", (int amount) => hackLogic.AddPermitPoints(amount));
            Window.AddNumField("Spawn Item", (int itemId) => hackLogic.SpawnItemOnFloor(itemId));
            Window.AddVector2NumField("Teleport to", (int[] pos) =>hackLogic.TeleportToPos(pos));
            Window.AddButton("Pay Debt", () => hackLogic.PayTownDebt());
            Window.AddButton("Call Of the Wild", () => hackLogic.CallOfTheWild());
            Window.AddButton("Unlock all Deeds", () => hackLogic.UnlockAllDeeds());
            Window.AddButton("Spawn Random Animal", () => hackLogic.SpawnRandomAnimal());
            Window.AddButton("Kill all Animals", () => hackLogic.KillAllAnimals());
            Window.AddButton("Level all Skills + 1", () => hackLogic.LevelUpAllSkills());
            Window.AddButton("Remove Grass", () => hackLogic.DestroyAllGrass());
            Window.AddButton("Super Vac", () => hackLogic.PickUpAllItems());
            Window.AddToggle("Fly Hack", () => hackLogic.ToggleFlyHack());
            Window.AddToggle("Speed Hack", () => hackLogic.ToggleSpeedHack());
            Window.AddToggle("Infinite Health", () => hackLogic.ToggleInfiniteHealth());
            Window.AddToggle("Infinite Stamina", () => hackLogic.ToggleInfiniteStamina());
            Window.AddToggle("Super Mine", () => hackLogic.ToggleSuperMine());
            Window.AddToggle("Enable Cheat Menu (sp only)", () => hackLogic.ToggleCheatMenu());
            Window.AddToggle("ESP Hack", () => hackLogic.ToggleEsp());
            Window.AddToggle("Time Hack", () => hackLogic.ToggleTimeHack());
            Window.AddButton("Show all Ids (DEBUG ONLY)", () => hackLogic.GetItemIds());

        }

        public void OnGUI()
        {
         
        }
    }
}