using DinkumEvolved.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace DinkumEvolved
{
    public class HackLogic : MonoBehaviour
    {
        public AnimalManager animalManager;
        public CharLevelManager characterLevelManager;
        public CharMovement charMovement;
        public NPCManager npcManager;
        public BankMenu bank;
        public PermitPointsManager permitPointsManager;
        public WorldManager worldManager;
        public StatusManager statusManager;
        public HuntingChallengeManager huntingChallengeManager;
        public DeedManager deedManager;
        public LicenceManager licenceManager;
        public CheatScript cheatScript;
        public List<AnimalAI> animals;
        public List<NPCAI> npcs;
        public List<CharMovement> players;
        public Camera camera;
        public List<TileObject> tileObjects;

        
        public bool espEnabled = false;
        public int hackSpeed = 35;
        public float verticalFlySpeed = 6;
        public bool speedHacking = false;
        public bool GravityEnabled = true;
        private bool b_infiniteHealth = false;
        private bool b_infiniteStamina = false;
        private bool b_isCheatMenuActive = false;
        private bool b_superMine = false;
        private bool b_TimeHacking = false;

        

        

        public void SpeedHackLogic()
        {
            if (charMovement == null)
            {
                charMovement = FindObjectOfType<CharMovement>();
            }
            else
            {
                if (speedHacking)
                {
                    charMovement.setSpeedDif(hackSpeed);
                }
                else
                {
                    charMovement.setSpeedDif(1);
                }

            }
        }

        public void FlyHackLogic()
        {
            Rigidbody rigidbody = charMovement.GetComponent<Rigidbody>();
            if (!GravityEnabled)
            {
                charMovement.fallSpeed = 0;

                rigidbody.useGravity = false;
                charMovement.grounded = true;
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    charMovement.transform.position =
                        new Vector3(charMovement.transform.position.x,
                        charMovement.transform.position.y - verticalFlySpeed * Time.deltaTime,
                        charMovement.transform.position.z);
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    charMovement.transform.position =
                        new Vector3(charMovement.transform.position.x,
                        charMovement.transform.position.y + verticalFlySpeed * Time.deltaTime,
                        charMovement.transform.position.z);
                }
            }
            else
            {
                charMovement.fallSpeed = 1;
                rigidbody.useGravity = true;

            }
        }

        public void CallOfTheWild()
        {
            
            ConsoleBase.WriteLine("Calling all the good boys");

            foreach (var animal in animals)
            {
                ConsoleBase.WriteLine($"luring animal: {animal.animalName} ");
                StartCoroutine(animal.callAnimalToPos(charMovement.transform.position));
            }
        }

        public void SpawnRandomAnimal()
        {

            int randomAnimalId = UnityEngine.Random.Range(0, animalManager.allAnimals.Length - 1);
            NetworkNavMesh.nav.SpawnAnAnimalOnTile(randomAnimalId * 10, (int)(charMovement.transform.position.x / 2f), (int)(charMovement.transform.position.z / 2f), null, 0, 0U);
            ConsoleBase.WriteLine($"Animal Spawned {animalManager.allAnimals[randomAnimalId].animalName}");

        }

        public void KillAllAnimals()
        {
            
            ConsoleBase.WriteLine("Killing all animals");
            foreach (var animal in animals)
            {
                
                Damageable damageable = animal.GetComponent<Damageable>();
                if (damageable != null && !animal.isAPet())
                {
                   //ConsoleBase.WriteLine($"Killing: {animal.animalName}");
                    damageable.attackAndDoDamage(1000, charMovement.transform, 0);
                }
                else
                {
                    //ConsoleBase.WriteLine($"{animal.animalName} is not damageable");
                }
                
            }
        }


        public void AddMoney(int moneyToAdd)
        {
            if (Inventory.inv != null || moneyToAdd < 3)
            {
                ConsoleBase.WriteLine("Adding money");
                Inventory.inv.changeWallet(moneyToAdd, true);
            }
                
        }

        public void AddPermitPoints(int pointsToAdd)
        {
            if (permitPointsManager == null)
            {
                permitPointsManager = FindObjectOfType<PermitPointsManager>();
            }
            else
            {
                ConsoleBase.WriteLine("Adding Permit Points");
                permitPointsManager.addPoints(pointsToAdd);
            }
        }

        public void ToggleSpeedHack()
        {

            speedHacking = !speedHacking;
            ConsoleBase.WriteLine($"Speed hack enabled: {speedHacking}");

        }

        public void LevelUpAllSkills()
        {
            if (characterLevelManager == null)
            {
                characterLevelManager = FindObjectOfType<CharLevelManager>();
            }
            else
            {
                ConsoleBase.WriteLine($"Levelling all skills Once");
                foreach (CharLevelManager.SkillTypes skillType in Enum.GetValues(typeof(CharLevelManager.SkillTypes)))
                {
                    characterLevelManager.addXp(skillType, characterLevelManager.getLevelRequiredXP((int)skillType));
                }



            }
        }

        public void PayTownDebt()
        {
            if (charMovement == null)
            {
                charMovement = FindObjectOfType<CharMovement>();
            }
            else
            {
                ConsoleBase.WriteLine($"Paying Town Debt");
                charMovement.CmdPayTownDebt(1000000);
                if (NetworkMapSharer.share.townDebt == 0)
                {
                    ConversationManager.manage.talkToNPC(NPCManager.manage.sign, TownManager.manage.debtComplete, false, false);
                }
            }
        }

        public void ToggleEsp()
        { 
            espEnabled = !espEnabled;
            ConsoleBase.WriteLine($"ESP enabled {espEnabled}");
        }

        public void UnlockAllDeeds()
        {
            if (deedManager == null)
            {
                deedManager = FindObjectOfType<DeedManager>();
            }
            else
            {
                ConsoleBase.WriteLine($"Unlocking all deeds");
                foreach (var deed in deedManager.deedDetails)
                {
                    deed.unlocked = true;

                }
            }
        }
        public void ToggleSuperMine()
        {
            b_superMine = !b_superMine;
            ConsoleBase.WriteLine($"Super Mine enabled {b_superMine}");
        }

        public void ToggleFlyHack()
        {
            GravityEnabled = !GravityEnabled;
            ConsoleBase.WriteLine($"Fly Hack enabled {!GravityEnabled}");

        }
        public void ToggleTimeHack()
        {
            b_TimeHacking = !b_TimeHacking;
            ConsoleBase.WriteLine($"Time Hack enabled {b_TimeHacking}");
            if (b_TimeHacking)
            {
                Time.timeScale = 3f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        public void TeleportToPos(int[] pos)
        {
            if (charMovement != null && worldManager != null)
            {
                ConsoleBase.WriteLine($"Teleporting to {pos[0]}, {pos[1]}");
                StartCoroutine(charMovement.teleportCharToPos(pos));
            }
        }

        public void UnlockAllLicenses()
        {
            if (licenceManager != null)
            {
                ConsoleBase.WriteLine($"Unlocking all licenses");
                foreach (var licence in licenceManager.allLicences)
                {

                    licence.isUnlocked = true;
                    licence.hasBeenSeenBefore = true;
                    licence.currentLevel = licence.getMaxLevel();
                    LicenceManager.manage.checkForUnlocksOnLevelUp(licence, false);

                }
            }
        }
        internal void ToggleInfiniteHealth()
        {
            b_infiniteHealth = !b_infiniteHealth;
        }
        internal void ToggleInfiniteStamina()
        {
            b_infiniteStamina = !b_infiniteStamina;
        }

        internal void InfiniteHealth()
        {
            if (b_infiniteHealth)
            {

                statusManager.changeHealthTo(100);
            }
        }
      

        internal void InfiniteStamina()
        {
            if (b_infiniteStamina)
            {
                statusManager.changeStamina(statusManager.getStaminaMax());
            }
            
        }
        internal void ToggleCheatMenu()
        {
            b_isCheatMenuActive = !b_isCheatMenuActive;
            if (b_isCheatMenuActive)
            {
                ConsoleBase.WriteLine(cheatScript.name);
                if (cheatScript is null)
                {
                    cheatScript = FindObjectOfType<CheatScript>();
                }
                if (cheatScript != null)
                {
                    cheatScript.cheatsOn = true;
                    cheatScript.showAll();
                }
              
                
            }
            else
            {
                cheatScript.cheatsOn = false;
                
            }
        }
        internal void SuperMineLogic()
        {
            if (b_superMine && Input.GetMouseButton(0))
            {

                if (Inventory.inv.invSlots[Inventory.inv.selectedSlot].itemInSlot.hasFuel)
                {
                    Inventory.inv.invSlots[Inventory.inv.selectedSlot].updateSlotContentsAndRefresh(Inventory.inv.invSlots[Inventory.inv.selectedSlot].itemNo, charMovement.myEquip.currentlyHolding.fuelMax);
                }
                
                charMovement.myInteract.doDamage(false);
                
            }
        }

        internal void GetItemIds()
        {
            var orderedList = Inventory.inv.allItems.ToList().OrderBy(item => item.itemName);
            foreach (var item in orderedList)
            {
                ConsoleBase.WriteLine($"{item.itemName}, {item.getItemId()}");
            }
          
        }
        internal void DestroyAllGrass(int radius = 5)
        {
            

            LawnMower lawnMower = FindObjectOfType<LawnMower>();
            
            if (lawnMower == null)
            {
                SpawnItemOnFloor(798);
                lawnMower = FindObjectOfType<LawnMower>();
            }

            WorldManager worldManager = WorldManager.manageWorld;
            for (int xPos = 0; xPos < radius; xPos++)
            {
                for (int yPos = 0; yPos < radius; yPos++)
                {
                    ConsoleBase.WriteLine($"Attempting to find Grass at {500 +xPos}, {500 +yPos}");

                    var tileObjectSettings = worldManager.getTileObjectSettings(500 + xPos,500 + yPos);

                    if (tileObjectSettings.isGrass)
                    {
                        ConsoleBase.WriteLine($"Destroying grass at {xPos + 500},{yPos + 500}");

                        lawnMower.CmdCutTheGrass(xPos + 500, yPos + 500);
                        PickUpAllItems();
                    }
                }
                
               
            }
            
        }

        internal void SpawnItemOnFloor(int itemId)
        {
            charMovement.myInteract.CmdSpawnPlaceable(charMovement.transform.position, itemId);
        }
        internal void PickUpAllItems()
        {
            List<DroppedItem> droppedItems = FindObjectsOfType<DroppedItem>().ToList();
            foreach (var item in droppedItems)
            {
                if (Inventory.inv.addItemToInventory(item.myItemId, item.stackAmount, true))
                {
                    SoundManager.manage.play2DSound(SoundManager.manage.pickUpItem);
                    charMovement.myPickUp.CmdPickUp(item.netId);
                    item.pickUpLocal();
                }
                else
                {
                    NotificationManager.manage.turnOnPocketsFullNotification(charMovement.myPickUp.holdingPickUp);
                }
            }
        }

        internal void EspLogic()
        {

            
            
        }

      
    }

}
