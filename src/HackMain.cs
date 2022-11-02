using DinkumEvolved.UI;
using DinkumEvolved.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

namespace DinkumEvolved
{
    [SuppressMessage("ReSharper", "Unity.RedundantEventFunction")]
    partial class HackMain : MonoBehaviour
    {
        private GuiHandler _guiHandler;
        private HackLogic hackLogic;

        /* - Initializing Methods - */
        public void Awake()
        {
            // This function is called when the class is loaded by the game (prior to attachment)
           
        }

        public void OnEnable()
        {
            
        }

        public void Start()
        {
            // This function is called once for each instance of this class,
            // when it starts execution (in this case, attached to an object)
            ConsoleBase.WriteLine("HackMain Start()");
            EntityUpdate = EntityUpdateFunct(2);
            StartCoroutine(EntityUpdate);
            hackLogic = gameObject.AddComponent<HackLogic>();
            GetHackLogicStartUp();
            _guiHandler = gameObject.AddComponent<GuiHandler>();
            _guiHandler.InitHacks(hackLogic);
         
        }

        private void GetHackLogicStartUp()
        {
            hackLogic.characterLevelManager = FindObjectOfType<CharLevelManager>();
            hackLogic.charMovement = FindObjectOfType<CharMovement>();
            hackLogic.bank = FindObjectOfType<BankMenu>();
            hackLogic.permitPointsManager = FindObjectOfType<PermitPointsManager>();
            hackLogic.worldManager = FindObjectOfType<WorldManager>();
            hackLogic.statusManager = FindObjectOfType<StatusManager>();
            hackLogic.huntingChallengeManager = FindObjectOfType<HuntingChallengeManager>();
            hackLogic.deedManager = FindObjectOfType<DeedManager>();
            hackLogic.licenceManager = FindObjectOfType<LicenceManager>();
            hackLogic.animalManager = FindObjectOfType<AnimalManager>();
            hackLogic.animals = new List<AnimalAI>();
            hackLogic.tileObjects = new List<TileObject>();
            
            hackLogic.camera = Camera.main;
            hackLogic.cheatScript =FindObjectOfType<CheatScript>();
            if (hackLogic.cheatScript == null)
            {
                ConsoleBase.WriteLine("No CheatScript Found");
            }
            else
            {
                ConsoleBase.WriteLine("CheatScript Found");
            }
        }

        /* - Game Loop Methods - */
        public void Update()
        {
            if (hackLogic.camera == null)
                hackLogic.camera = Camera.main;
            
           
           
            if (Input.GetKeyDown(KeyCode.Y))
            {
                hackLogic.ToggleSpeedHack();
                

            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                hackLogic.ToggleFlyHack();
                

            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                hackLogic.CallOfTheWild();
            }

            hackLogic.InfiniteHealth();
            hackLogic.InfiniteStamina();
            hackLogic.FlyHackLogic();
            hackLogic.SpeedHackLogic();
            hackLogic.SuperMineLogic();
            
            
        }

      

        public void LateUpdate()
        {   // This function is called once per frame, it's frequency depends on the frame rate.
            // This is at the end of the game logic cycle.
        }
        public void OnGUI()
        {   // This function is called at the end of the frame, after all game logic.

            if (hackLogic.espEnabled)
            {

               
              
                /*
                foreach (var npc in hackLogic.npcs)
                {   
                    Basic_ESP(npc.transform.position, NPCManager.manage.NPCDetails[npc.myId.NPCNo].NPCName);
                }
                */
                
                foreach (var player in hackLogic.players)
                {
                    Basic_ESP(player.transform.position, $"{player.myEquip.playerName}");
                }
                


            }
        }

        /* - Physics Method - */
        public void FixedUpdate()
        {   // This function is called at a fixed frequency (Typically 100hz) and is independent of the frame rate.
            // For physics operations.
        }

        /* - Closing Methods - */
        public void OnDisable()
        {   // This function is called when the instance of the class is disabled by it's parent.
            // The component remains attached, but disabled (Component.ENABLED = false)
        }
        public void OnDestroy()
        {   // This function is called when the instance of the class is destroyed by it's parent.
            // The component and all it's data are destroyed and must be created again.
        }

    }
}