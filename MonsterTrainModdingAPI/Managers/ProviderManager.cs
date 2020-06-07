using System;
using System.Collections.Generic;
using System.Text;
using ShinyShoe;
using ShinyShoe.Audio;

namespace MonsterTrainModdingAPI.Managers
{

    public class ProviderManager : IClient
    {

        /*
         * Potential Updates:
         * Add of Fully Initialized AddListener Method
         * Remove Listener
         * 
         * That's all, im real tired after copy and pasting a whole bunch of stuff
         * This class is very menial, but it allows quick and easy static access of stuff
         */
        #region IProviders

        #region AchievementManager
        private static AchievementManager _achievementManager;
        /// <summary>
        /// Attempts to get the Manager for Achievements
        /// </summary>
        /// <param name="achievementManager"></param>
        /// <returns></returns>
        public static bool TryGetAchievementManager(out AchievementManager achievementManager)
        {
            if (_achievementManager == null)
            {
                achievementManager = null;
                return false;
            }
            achievementManager = _achievementManager;
            return true;
        }
        private static Signal<AchievementManager> NewAchievementManagerSignal = new Signal<AchievementManager>();
        public static void AddNewAchievementManagerListener(Action<AchievementManager> callback) => NewAchievementManagerSignal.AddListener(callback);


        #endregion

        #region AdaptiveCombatMusicDriver
        private static AdaptiveCombatMusicDriver _adaptiveCombatMusicDriver;
        /// <summary>
        /// Attempts to get the Manager for Combat Music
        /// </summary>
        /// <param name="adaptiveCombatMusicDriver"></param>
        /// <returns></returns>
        public static bool TryGetAdaptiveCombatMusicDriver(out AdaptiveCombatMusicDriver adaptiveCombatMusicDriver)
        {
            if (_adaptiveCombatMusicDriver == null)
            {
                adaptiveCombatMusicDriver = null;
                return false;
            }
            adaptiveCombatMusicDriver = _adaptiveCombatMusicDriver;
            return true;
        }
        private static Signal<AdaptiveCombatMusicDriver> NewAdaptiveCombatMusicDriverSignal = new Signal<AdaptiveCombatMusicDriver>();
        public static void AddNewAdaptiveCombatMusicDriverListener(Action<AdaptiveCombatMusicDriver> callback) => NewAdaptiveCombatMusicDriverSignal.AddListener(callback);
        #endregion

        #region AssetLoadingManager
        private static AssetLoadingManager _assetLoadingManager;
        /// <summary>
        /// Attempts to get the Manager for Loading Ingame Assets
        /// </summary>
        /// <param name="assetLoadingManager"></param>
        /// <returns></returns>
        public static bool TryGetAssetLoadingManager(out AssetLoadingManager assetLoadingManager)
        {
            if (_assetLoadingManager == null)
            {
                assetLoadingManager = null;
                return false;
            }
            assetLoadingManager = _assetLoadingManager;
            return true;
        }
        private static Signal<AssetLoadingManager> NewAssetLoadingManagerSignal = new Signal<AssetLoadingManager>();
        public static void AddNewAssetLoadingManagerListener(Action<AssetLoadingManager> callback) => NewAssetLoadingManagerSignal.AddListener(callback);
        #endregion

        #region BattleModeManager
        private static BattleModeManager _battleModeManager;
        /// <summary>
        /// Attempts to get the Manager for Battle Mode
        /// </summary>
        /// <param name="battleModeManager"></param>
        /// <returns></returns>
        public static bool TryGetBattleModeManager(out BattleModeManager battleModeManager)
        {
            if (_battleModeManager == null)
            {
                battleModeManager = null;
                return false;
            }
            battleModeManager = _battleModeManager;
            return true;
        }
        private static Signal<BattleModeManager> NewBattleModeManagerSignal = new Signal<BattleModeManager>();
        public static void AddBattleModeManagerListener(Action<BattleModeManager> callback) => NewBattleModeManagerSignal.AddListener(callback);
        #endregion

        #region CameraControls
        private static CameraControls _cameraControls;
        /// <summary>
        /// Attempts to get the Manager of the Camera
        /// </summary>
        /// <param name="cameraControls"></param>
        /// <returns></returns>
        public static bool TryGetCameraControls(out CameraControls cameraControls)
        {
            if (_cameraControls == null)
            {
                cameraControls = null;
                return false;
            }
            cameraControls = _cameraControls;
            return true;
        }
        private static Signal<CameraControls> NewCameraControlsSignal = new Signal<CameraControls>();
        public static void AddCameraControlsListener(Action<CameraControls> callback) => NewCameraControlsSignal.AddListener(callback);
        #endregion

        #region CardManager
        private static CardManager _cardManager;
        /// <summary>
        /// Attempts to get the Manager of Cards
        /// </summary>
        /// <param name="cardManager"></param>
        /// <returns></returns>
        public static bool TryGetCardManager(out CardManager cardManager)
        {
            if (_cardManager == null)
            {
                cardManager = null;
                return false;
            }
            cardManager = _cardManager;
            return true;
        }
        private static Signal<CardManager> NewCardManagerSignal = new Signal<CardManager>();
        public static void AddNewCardManagerListener(Action<CardManager> callback) => NewCardManagerSignal.AddListener(callback);
        #endregion

        #region CardSelectionBehaviour
        private static CardSelectionBehaviour _cardSelectionBehaviour;
        /// <summary>
        /// Attempts to get the Manager of how cards should behave when selected
        /// </summary>
        /// <param name="cardManager"></param>
        /// <returns></returns>
        public static bool TryGetCardSelectionBehaviour(out CardSelectionBehaviour cardSelectionBehaviour)
        {
            if (_cardSelectionBehaviour == null)
            {
                cardSelectionBehaviour = null;
                return false;
            }
            cardSelectionBehaviour = _cardSelectionBehaviour;
            return true;
        }
        private static Signal<CardSelectionBehaviour> NewCardSelectionBehaviourSignal = new Signal<CardSelectionBehaviour>();
        public static void AddCardSelectionBehaviourListener(Action<CardSelectionBehaviour> callback) => NewCardSelectionBehaviourSignal.AddListener(callback);
        #endregion

        #region CardStatistics
        private static CardStatistics _cardStatistics;
        /// <summary>
        /// Attempts to get the Manager for Card Statistics
        /// </summary>
        /// <param name="achievementManager"></param>
        /// <returns></returns>
        public static bool TryGetCardStatistics(out CardStatistics cardStatistics)
        {
            if (_cardStatistics == null)
            {
                cardStatistics = null;
                return false;
            }
            cardStatistics = _cardStatistics;
            return true;
        }
        private static Signal<CardStatistics> NewCardStatisticsSignal = new Signal<CardStatistics>();
        public static void AddNewCardStatisticsListener(Action<CardStatistics> callback) => NewCardStatisticsSignal.AddListener(callback);
        #endregion

        #region ChallengeDetailsScreen
        private static ChallengeDetailsScreen _challengeDetailsScreen;
        /// <summary>
        /// Attempts to get the Screen for Challenge Details
        /// </summary>
        /// <param name="achievementManager"></param>
        /// <returns></returns>
        public static bool TryGetChallengeDetailsScreen(out ChallengeDetailsScreen challengeDetailsScreen)
        {
            if (_challengeDetailsScreen == null)
            {
                challengeDetailsScreen = null;
                return false;
            }
            challengeDetailsScreen = _challengeDetailsScreen;
            return true;
        }
        private static Signal<ChallengeDetailsScreen> NewChallengeDetailsScreenSignal = new Signal<ChallengeDetailsScreen>();
        public static void AddNewChallengeDetailsScreenListener(Action<ChallengeDetailsScreen> callback) => NewChallengeDetailsScreenSignal.AddListener(callback);
        #endregion

        #region ChampionUpgradeScreen
        private static ChampionUpgradeScreen _championUpgradeScreen;
        /// <summary>
        /// Attempts to get the Screen for Upgrading Champions
        /// </summary>
        /// <param name="cardManager"></param>
        /// <returns></returns>
        public static bool TryGetChampionUpgradeScreen(out ChampionUpgradeScreen championUpgradeScreen)
        {
            if (_championUpgradeScreen == null)
            {
                championUpgradeScreen = null;
                return false;
            }
            championUpgradeScreen = _championUpgradeScreen;
            return true;
        }
        private static Signal<ChampionUpgradeScreen> NewChampionUpgradeScreenSignal = new Signal<ChampionUpgradeScreen>();
        public static void AddNewChampionUpgradeScreenListener(Action<ChampionUpgradeScreen> callback) => NewChampionUpgradeScreenSignal.AddListener(callback);
        #endregion

        #region CheatScreen
        //Cheat Screen does not ship with built game, add this in if it ever becomes availible
        #endregion

        #region CombatManager
        private static CombatManager _combatManager;
        /// <summary>
        /// Attempts to get the Manager of Cards
        /// </summary>
        /// <param name="cardManager"></param>
        /// <returns></returns>
        public static bool TryGetCombatManager(out CombatManager combatManager)
        {
            if (_combatManager == null)
            {
                combatManager = null;
                return false;
            }
            combatManager = _combatManager;
            return true;
        }
        private static Signal<CombatManager> NewCombatManagerSignal = new Signal<CombatManager>();
        public static void AddNewCombatManagerListener(Action<CombatManager> callback) => NewCombatManagerSignal.AddListener(callback);
        #endregion

        #region CreditsScreen
        private static CreditsScreen _creditsScreen;
        public static bool TryGetCreditsScreen(out CreditsScreen creditsScreen)
        {
            if (_creditsScreen == null)
            {
                creditsScreen = null;
                return false;
            }
            creditsScreen = _creditsScreen;
            return true;
        }
        private static Signal<CreditsScreen> NewCreditsScreenSignal = new Signal<CreditsScreen>();
        public static void AddNewCreditsScreenListener(Action<CreditsScreen> callback) => NewCreditsScreenSignal.AddListener(callback);
        #endregion

        #region DiscordManager
        private static DiscordManager _discordManager;
        public static bool TryGetDiscordManager(out DiscordManager discordManager)
        {
            if (_discordManager == null)
            {
                discordManager = null;
                return false;
            }
            discordManager = _discordManager;
            return true;
        }
        private static Signal<DiscordManager> NewDiscordManagerSignal = new Signal<DiscordManager>();
        public static void AddNewDiscordManagerListener(Action<DiscordManager> callback) => NewDiscordManagerSignal.AddListener(callback);
        #endregion

        #region FileCacheManager
        private static FileCacheManager _fileCacheManager;
        public static bool TryGetFileCacheManager(out FileCacheManager fileCacheManager)
        {
            if (_fileCacheManager == null)
            {
                fileCacheManager = null;
                return false;
            }
            fileCacheManager = _fileCacheManager;
            return true;
        }
        private static Signal<FileCacheManager> NewFileCacheManagerSignal = new Signal<FileCacheManager>();
        public static void AddNewFileCacheManagerListener(Action<FileCacheManager> callback) => NewFileCacheManagerSignal.AddListener(callback);
        #endregion

        #region FloatingRewardManager
        private static FloatingRewardManager _floatingRewardManager;
        public static bool TryGetFloatingRewardManager(out FloatingRewardManager floatingRewardManager)
        {
            if (_floatingRewardManager == null)
            {
                floatingRewardManager = null;
                return false;
            }
            floatingRewardManager = _floatingRewardManager;
            return true;
        }
        private static Signal<FloatingRewardManager> NewFloatingRewardManagerSignal = new Signal<FloatingRewardManager>();
        public static void AddNewFloatingRewardManagerListener(Action<FloatingRewardManager> callback) => NewFloatingRewardManagerSignal.AddListener(callback);
        #endregion

        #region GameScreen
        private static GameScreen _gameScreen;
        public static bool TryGetGameScreen(out GameScreen gameScreen)
        {
            if (_gameScreen == null)
            {
                gameScreen = null;
                return false;
            }
            gameScreen = _gameScreen;
            return true;
        }
        private static Signal<GameScreen> NewGameScreenSignal = new Signal<GameScreen>();
        public static void AddNewGameScreenListener(Action<GameScreen> callback) => NewGameScreenSignal.AddListener(callback);
        #endregion

        #region GameStateManager
        private static GameStateManager _gameStateManager;
        public static bool TryGetGameStateManager(out GameStateManager gameStateManager)
        {
            if (_gameStateManager == null)
            {
                gameStateManager = null;
                return false;
            }
            gameStateManager = _gameStateManager;
            return true;
        }
        private static Signal<GameStateManager> NewGameStateManagerSignal = new Signal<GameStateManager>();
        public static void AddNewGameStateManagerListener(Action<GameStateManager> callback) => NewGameStateManagerSignal.AddListener(callback);
        #endregion

        #region GameVFXManager
        private static GameVFXManager _gameVFXManager;
        public static bool TryGetGameVFXManager(out GameVFXManager gameVFXManager)
        {
            if (_gameVFXManager == null)
            {
                gameVFXManager = null;
                return false;
            }
            gameVFXManager = _gameVFXManager;
            return true;
        }
        private static Signal<GameVFXManager> NewGameVFXManagerSignal = new Signal<GameVFXManager>();
        public static void AddNewGameVFXManagerListener(Action<GameVFXManager> callback) => NewGameVFXManagerSignal.AddListener(callback);
        #endregion

        #region GraphicsSettingsManager
        private static GraphicsSettingsManager _graphicsSettingsManager;
        public static bool TryGetGraphicsSettingsManager(out GraphicsSettingsManager graphicsSettingsManager)
        {
            if (_graphicsSettingsManager == null)
            {
                graphicsSettingsManager = null;
                return false;
            }
            graphicsSettingsManager = _graphicsSettingsManager;
            return true;
        }
        private static Signal<GraphicsSettingsManager> NewGraphicsSettingsManagerSignal = new Signal<GraphicsSettingsManager>();
        public static void AddNewGraphicsSettingsManagerListener(Action<GraphicsSettingsManager> callback) => NewGraphicsSettingsManagerSignal.AddListener(callback);
        #endregion

        #region HandUI
        private static HandUI _handUI;
        public static bool TryGetHandUI(out HandUI handUI)
        {
            if (_handUI == null)
            {
                handUI = null;
                return false;
            }
            handUI = _handUI;
            return true;
        }
        private static Signal<HandUI> NewHandUISignal = new Signal<HandUI>();
        public static void AddNewHandUIListener(Action<HandUI> callback) => NewHandUISignal.AddListener(callback);
        #endregion

        #region HeroManager
        private static HeroManager _heroManager;
        public static bool TryGetHeroManager(out HeroManager heroManager)
        {
            if (_heroManager == null)
            {
                heroManager = null;
                return false;
            }
            heroManager = _heroManager;
            return true;
        }
        private static Signal<HeroManager> NewHeroManagerSignal = new Signal<HeroManager>();
        public static void AddNewHeroManagerListener(Action<HeroManager> callback) => NewHeroManagerSignal.AddListener(callback);
        #endregion

        #region InkWriter
        private static InkWriter _inkWriter;
        public static bool TryGetInkWriter(out InkWriter inkWriter)
        {
            if (_inkWriter == null)
            {
                inkWriter = null;
                return false;
            }
            inkWriter = _inkWriter;
            return true;
        }
        private static Signal<InkWriter> NewInkWriterSignal = new Signal<InkWriter>();
        public static void AddNewInkWriterListener(Action<InkWriter> callback) => NewInkWriterSignal.AddListener(callback);
        #endregion

        #region InputManager
        private static InputManager _inputManager;
        public static bool TryGetInputManager(out InputManager inputManager)
        {
            if (_inputManager == null)
            {
                inputManager = null;
                return false;
            }
            inputManager = _inputManager;
            return true;
        }
        private static Signal<InputManager> NewInputManagerSignal = new Signal<InputManager>();
        public static void AddNewInputManagerListener(Action<InputManager> callback) => NewInputManagerSignal.AddListener(callback);
        #endregion

        #region LanguageManager
        private static LanguageManager _languageManager;
        public static bool TryGetLanguageManager(out LanguageManager languageManager)
        {
            if (_languageManager == null)
            {
                languageManager = null;
                return false;
            }
            languageManager = _languageManager;
            return true;
        }
        private static Signal<LanguageManager> NewLanguageManagerSignal = new Signal<LanguageManager>();
        public static void AddNewLanguageManagerListener(Action<LanguageManager> callback) => NewLanguageManagerSignal.AddListener(callback);
        #endregion

        #region MerchantCharacterUI
        private static MerchantCharacterUI _merchantCharacterUI;
        public static bool TryGetMerchantCharacterUI(out MerchantCharacterUI merchantCharacterUI)
        {
            if (_merchantCharacterUI == null)
            {
                merchantCharacterUI = null;
                return false;
            }
            merchantCharacterUI = _merchantCharacterUI;
            return true;
        }
        private static Signal<MerchantCharacterUI> NewMerchantCharacterUISignal = new Signal<MerchantCharacterUI>();
        public static void AddNewMerchantCharacterUIListener(Action<MerchantCharacterUI> callback) => NewMerchantCharacterUISignal.AddListener(callback);
        #endregion

        #region MonsterManager
        private static MonsterManager _monsterManager;
        public static bool TryGetMonsterManager(out MonsterManager monsterManager)
        {
            if (_monsterManager == null)
            {
                monsterManager = null;
                return false;
            }
            monsterManager = _monsterManager;
            return true;
        }
        private static Signal<MonsterManager> NewMonsterManagerSignal = new Signal<MonsterManager>();
        public static void AddNewMonsterManagerListener(Action<MonsterManager> callback) => NewMonsterManagerSignal.AddListener(callback);
        #endregion

        #region PlayerManager
        private static PlayerManager _playerManager;
        public static bool TryGetPlayerManager(out PlayerManager playerManager)
        {
            if (_playerManager == null)
            {
                playerManager = null;
                return false;
            }
            playerManager = _playerManager;
            return true;
        }
        private static Signal<PlayerManager> NewPlayerManagerSignal = new Signal<PlayerManager>();
        public static void AddNewPlayerManagerListener(Action<PlayerManager> callback) => NewPlayerManagerSignal.AddListener(callback);
        #endregion

        #region PopupNotificationManager
        private static PopupNotificationManager _popupNotificationManager;
        public static bool TryGetPopupNotificationManager(out PopupNotificationManager popupNotificationManager)
        {
            if (_popupNotificationManager == null)
            {
                popupNotificationManager = null;
                return false;
            }
            popupNotificationManager = _popupNotificationManager;
            return true;
        }
        private static Signal<PopupNotificationManager> NewPopupNotificationManagerSignal = new Signal<PopupNotificationManager>();
        public static void AddNewPopupNotificationManagerListener(Action<PopupNotificationManager> callback) => NewPopupNotificationManagerSignal.AddListener(callback);
        #endregion

        #region PreferencesManager
        private static PreferencesManager _preferencesManager;
        public static bool TryGetPreferencesManager(out PreferencesManager preferencesManager)
        {
            if (_preferencesManager == null)
            {
                preferencesManager = null;
                return false;
            }
            preferencesManager = _preferencesManager;
            return true;
        }
        private static Signal<PreferencesManager> NewPreferencesManagerSignal = new Signal<PreferencesManager>();
        public static void AddNewPreferencesManagerListener(Action<PreferencesManager> callback) => NewPreferencesManagerSignal.AddListener(callback);
        #endregion

        #region PubSubManager
        private static PubSubManager _pubSubManager;
        public static bool TryGetPubSubManager(out PubSubManager pubSubManager)
        {
            if (_pubSubManager == null)
            {
                pubSubManager = null;
                return false;
            }
            pubSubManager = _pubSubManager;
            return true;
        }
        private static Signal<PubSubManager> NewPubSubManagerSignal = new Signal<PubSubManager>();
        public static void AddNewPubSubManagerListener(Action<PubSubManager> callback) => NewPubSubManagerSignal.AddListener(callback);
        #endregion

        #region RelicManager
        private static RelicManager _relicManager;
        public static bool TryGetRelicManager(out RelicManager relicManager)
        {
            if (_relicManager == null)
            {
                relicManager = null;
                return false;
            }
            relicManager = _relicManager;
            return true;
        }
        private static Signal<RelicManager> NewRelicManagerSignal = new Signal<RelicManager>();
        public static void AddNewRelicManagerListener(Action<RelicManager> callback) => NewRelicManagerSignal.AddListener(callback);
        #endregion

        #region RoomManager
        private static RoomManager _roomManager;
        public static bool TryGetRoomManager(out RoomManager roomManager)
        {
            if (_roomManager == null)
            {
                roomManager = null;
                return false;
            }
            roomManager = _roomManager;
            return true;
        }
        private static Signal<RoomManager> NewRoomManagerSignal = new Signal<RoomManager>();
        public static void AddNewRoomManagerListener(Action<RoomManager> callback) => NewRoomManagerSignal.AddListener(callback);
        #endregion

        #region RoomUI
        private static RoomUI _roomUI;
        public static bool TryGetRoomUI(out RoomUI roomUI)
        {
            if (_roomUI == null)
            {
                roomUI = null;
                return false;
            }
            roomUI = _roomUI;
            return true;
        }
        private static Signal<RoomUI> NewRoomUISignal = new Signal<RoomUI>();
        public static void AddNewRoomUIListener(Action<RoomUI> callback) => NewRoomUISignal.AddListener(callback);
        #endregion

        #region SaveManager
        private static SaveManager _saveManager;
        public static bool TryGetSaveManager(out SaveManager saveManager)
        {
            if (_saveManager == null)
            {
                saveManager = null;
                return false;
            }
            saveManager = _saveManager;
            return true;
        }
        private static Signal<SaveManager> NewSaveManagerSignal = new Signal<SaveManager>();
        public static void AddNewSaveManagerListener(Action<SaveManager> callback) => NewSaveManagerSignal.AddListener(callback);
        #endregion

        #region ScenarioManager
        private static ScenarioManager _scenarioManager;
        public static bool TryGetScenarioManager(out ScenarioManager scenarioManager)
        {
            if (_scenarioManager == null)
            {
                scenarioManager = null;
                return false;
            }
            scenarioManager = _scenarioManager;
            return true;
        }
        private static Signal<ScenarioManager> NewScenarioManagerSignal = new Signal<ScenarioManager>();
        public static void AddNewScenarioManagerListener(Action<ScenarioManager> callback) => NewScenarioManagerSignal.AddListener(callback);
        #endregion

        #region SceneVFXManager
        private static SceneVFXManager _sceneVFXManager;
        public static bool TryGetSceneVFXManager(out SceneVFXManager sceneVFXManager)
        {
            if (_sceneVFXManager == null)
            {
                sceneVFXManager = null;
                return false;
            }
            sceneVFXManager = _sceneVFXManager;
            return true;
        }
        private static Signal<SceneVFXManager> NewSceneVFXManagerSignal = new Signal<SceneVFXManager>();
        public static void AddNewSceneVFXManagerListener(Action<SceneVFXManager> callback) => NewSceneVFXManagerSignal.AddListener(callback);
        #endregion

        #region ScreenManager
        private static ScreenManager _screenManager;
        public static bool TryGetScreenManager(out ScreenManager screenManager)
        {
            if (_screenManager == null)
            {
                screenManager = null;
                return false;
            }
            screenManager = _screenManager;
            return true;
        }
        private static Signal<ScreenManager> NewScreenManagerSignal = new Signal<ScreenManager>();
        public static void AddNewScreenManagerListener(Action<ScreenManager> callback) => NewScreenManagerSignal.AddListener(callback);
        #endregion

        #region SoundManager
        private static SoundManager _soundManager;
        public static bool TryGetSoundManager(out SoundManager soundManager)
        {
            if (_soundManager == null)
            {
                soundManager = null;
                return false;
            }
            soundManager = _soundManager;
            return true;
        }
        private static Signal<SoundManager> NewSoundManagerSignal = new Signal<SoundManager>();
        public static void AddNewSoundManagerListener(Action<SoundManager> callback) => NewSoundManagerSignal.AddListener(callback);
        #endregion

        #region StatusEffectManager
        private static StatusEffectManager _statusEffectManager;
        public static bool TryGetStatusEffectManager(out StatusEffectManager statusEffectManager)
        {
            if (_statusEffectManager == null)
            {
                statusEffectManager = null;
                return false;
            }
            statusEffectManager = _statusEffectManager;
            return true;
        }
        private static Signal<StatusEffectManager> NewStatusEffectManagerSignal = new Signal<StatusEffectManager>();
        public static void AddNewStatusEffectManagerListener(Action<StatusEffectManager> callback) => NewStatusEffectManagerSignal.AddListener(callback);
        #endregion

        #region SteamClientHades
        private static SteamClientHades _steamClientHades;
        public static bool TryGetSteamClientHades(out SteamClientHades steamClientHades)
        {
            if (_steamClientHades == null)
            {
                steamClientHades = null;
                return false;
            }
            steamClientHades = _steamClientHades;
            return true;
        }
        private static Signal<SteamClientHades> NewSteamClientHadesSignal = new Signal<SteamClientHades>();
        public static void AddNewSteamClientHadesListener(Action<SteamClientHades> callback) => NewSteamClientHadesSignal.AddListener(callback);
        #endregion

        #region StoryManager
        private static StoryManager _storyManager;
        public static bool TryGetStoryManager(out StoryManager storyManager)
        {
            if (_storyManager == null)
            {
                storyManager = null;
                return false;
            }
            storyManager = _storyManager;
            return true;
        }
        private static Signal<StoryManager> NewStoryManagerSignal = new Signal<StoryManager>();
        public static void AddNewStoryManagerListener(Action<StoryManager> callback) => NewStoryManagerSignal.AddListener(callback);
        #endregion

        #endregion

        #region Singleton
        /// <summary>
        /// Singleton Structure to Allow implementation of IClient 
        /// </summary>
        private static ProviderManager _instance;
        public static ProviderManager Instance
        {
            get
            {
                if (_instance == null) _instance = new ProviderManager();
                return _instance;
            }
        }
        #endregion

        #region IClient Methods
        public void NewProviderAvailable(IProvider newProvider)
        {
            if (DepInjector.MapProvider<AchievementManager>(newProvider, ref _achievementManager))
            {
                NewAchievementManagerSignal.Dispatch(_achievementManager);
                return;
            }
            if (DepInjector.MapProvider<AdaptiveCombatMusicDriver>(newProvider, ref _adaptiveCombatMusicDriver))
            {
                NewAdaptiveCombatMusicDriverSignal.Dispatch(_adaptiveCombatMusicDriver);
                return;
            }
            if (DepInjector.MapProvider<AssetLoadingManager>(newProvider, ref _assetLoadingManager))
            {
                NewAssetLoadingManagerSignal.Dispatch(_assetLoadingManager);
                return;
            }
            if (DepInjector.MapProvider<BattleModeManager>(newProvider, ref _battleModeManager))
            {
                NewBattleModeManagerSignal.Dispatch(_battleModeManager);
                return;
            }
            if (DepInjector.MapProvider<CameraControls>(newProvider, ref _cameraControls))
            {
                NewCameraControlsSignal.Dispatch(_cameraControls);
                return;
            }
            if (DepInjector.MapProvider<CardManager>(newProvider, ref _cardManager))
            {
                NewCardManagerSignal.Dispatch(_cardManager);
                return;
            }
            if (DepInjector.MapProvider<CardSelectionBehaviour>(newProvider, ref _cardSelectionBehaviour))
            {
                NewCardSelectionBehaviourSignal.Dispatch(_cardSelectionBehaviour);
                return;
            }
            if (DepInjector.MapProvider<CardStatistics>(newProvider, ref _cardStatistics))
            {
                NewCardStatisticsSignal.Dispatch(_cardStatistics);
                return;
            }
            if (DepInjector.MapProvider<ChallengeDetailsScreen>(newProvider, ref _challengeDetailsScreen))
            {
                NewChallengeDetailsScreenSignal.Dispatch(_challengeDetailsScreen);
                return;
            }
            if (DepInjector.MapProvider<ChampionUpgradeScreen>(newProvider, ref _championUpgradeScreen))
            {
                NewChampionUpgradeScreenSignal.Dispatch(_championUpgradeScreen);
                return;
            }
            if (DepInjector.MapProvider<CombatManager>(newProvider, ref _combatManager))
            {
                NewCombatManagerSignal.Dispatch(_combatManager);
                return;
            }
            if (DepInjector.MapProvider<CreditsScreen>(newProvider, ref _creditsScreen))
            {
                NewCreditsScreenSignal.Dispatch(_creditsScreen);
                return;
            }
            if (DepInjector.MapProvider<DiscordManager>(newProvider, ref _discordManager))
            {
                NewDiscordManagerSignal.Dispatch(_discordManager);
                return;
            }
            if (DepInjector.MapProvider<FileCacheManager>(newProvider, ref _fileCacheManager))
            {
                NewFileCacheManagerSignal.Dispatch(_fileCacheManager);
                return;
            }
            if (DepInjector.MapProvider<FloatingRewardManager>(newProvider, ref _floatingRewardManager))
            {
                NewFloatingRewardManagerSignal.Dispatch(_floatingRewardManager);
                return;
            }
            if (DepInjector.MapProvider<GameScreen>(newProvider, ref _gameScreen))
            {
                NewGameScreenSignal.Dispatch(_gameScreen);
                return;
            }
            if (DepInjector.MapProvider<GameStateManager>(newProvider, ref _gameStateManager))
            {
                NewGameStateManagerSignal.Dispatch(_gameStateManager);
                return;
            }
            if (DepInjector.MapProvider<GameVFXManager>(newProvider, ref _gameVFXManager))
            {
                NewGameVFXManagerSignal.Dispatch(_gameVFXManager);
                return;
            }
            if (DepInjector.MapProvider<GraphicsSettingsManager>(newProvider, ref _graphicsSettingsManager))
            {
                NewGraphicsSettingsManagerSignal.Dispatch(_graphicsSettingsManager);
                return;
            }
            if (DepInjector.MapProvider<HandUI>(newProvider, ref _handUI))
            {
                NewHandUISignal.Dispatch(_handUI);
                return;
            }
            if (DepInjector.MapProvider<HeroManager>(newProvider, ref _heroManager))
            {
                NewHeroManagerSignal.Dispatch(_heroManager);
                return;
            }
            if (DepInjector.MapProvider<InkWriter>(newProvider, ref _inkWriter))
            {
                NewInkWriterSignal.Dispatch(_inkWriter);
                return;
            }
            if (DepInjector.MapProvider<InputManager>(newProvider, ref _inputManager))
            {
                NewInputManagerSignal.Dispatch(_inputManager);
                return;
            }
            if (DepInjector.MapProvider<LanguageManager>(newProvider, ref _languageManager))
            {
                NewLanguageManagerSignal.Dispatch(_languageManager);
                return;
            }
            if (DepInjector.MapProvider<MerchantCharacterUI>(newProvider, ref _merchantCharacterUI))
            {
                NewMerchantCharacterUISignal.Dispatch(_merchantCharacterUI);
                return;
            }
            if (DepInjector.MapProvider<MonsterManager>(newProvider, ref _monsterManager))
            {
                NewMonsterManagerSignal.Dispatch(_monsterManager);
                return;
            }
            if (DepInjector.MapProvider<PlayerManager>(newProvider, ref _playerManager))
            {
                NewPlayerManagerSignal.Dispatch(_playerManager);
                return;
            }
            if (DepInjector.MapProvider<PopupNotificationManager>(newProvider, ref _popupNotificationManager))
            {
                NewPopupNotificationManagerSignal.Dispatch(_popupNotificationManager);
                return;
            }
            if (DepInjector.MapProvider<PreferencesManager>(newProvider, ref _preferencesManager))
            {
                NewPreferencesManagerSignal.Dispatch(_preferencesManager);
                return;
            }
            if (DepInjector.MapProvider<PubSubManager>(newProvider, ref _pubSubManager))
            {
                NewPubSubManagerSignal.Dispatch(_pubSubManager);
                return;
            }
            if (DepInjector.MapProvider<RelicManager>(newProvider, ref _relicManager))
            {
                NewRelicManagerSignal.Dispatch(_relicManager);
                return;
            }
            if (DepInjector.MapProvider<RoomManager>(newProvider, ref _roomManager))
            {
                NewRoomManagerSignal.Dispatch(_roomManager);
                return;
            }
            if (DepInjector.MapProvider<SaveManager>(newProvider, ref _saveManager))
            {
                NewSaveManagerSignal.Dispatch(_saveManager);
                return;
            }
            if (DepInjector.MapProvider<ScenarioManager>(newProvider, ref _scenarioManager))
            {
                NewScenarioManagerSignal.Dispatch(_scenarioManager);
                return;
            }
            if (DepInjector.MapProvider<SceneVFXManager>(newProvider, ref _sceneVFXManager))
            {
                NewSceneVFXManagerSignal.Dispatch(_sceneVFXManager);
                return;
            }
            if (DepInjector.MapProvider<SoundManager>(newProvider, ref _soundManager))
            {
                NewSoundManagerSignal.Dispatch(_soundManager);
                return;
            }
            if (DepInjector.MapProvider<StatusEffectManager>(newProvider, ref _statusEffectManager))
            {
                NewStatusEffectManagerSignal.Dispatch(_statusEffectManager);
                return;
            }
            if (DepInjector.MapProvider<SteamClientHades>(newProvider, ref _steamClientHades))
            {
                NewSteamClientHadesSignal.Dispatch(_steamClientHades);
                return;
            }
            if (DepInjector.MapProvider<StoryManager>(newProvider, ref _storyManager))
            {
                NewStoryManagerSignal.Dispatch(_storyManager);
                return;
            }
        }

        public void NewProviderFullyInstalled(IProvider newProvider)
        {

        }

        public void ProviderRemoved(IProvider removeProvider)
        {
            DepInjector.UnmapProvider<AchievementManager>(removeProvider, ref _achievementManager);
            DepInjector.UnmapProvider<AdaptiveCombatMusicDriver>(removeProvider, ref _adaptiveCombatMusicDriver);
            DepInjector.UnmapProvider<AssetLoadingManager>(removeProvider, ref _assetLoadingManager);
            DepInjector.UnmapProvider<BattleModeManager>(removeProvider, ref _battleModeManager);
            DepInjector.UnmapProvider<CameraControls>(removeProvider, ref _cameraControls);
            DepInjector.UnmapProvider<CardManager>(removeProvider, ref _cardManager);
            DepInjector.UnmapProvider<CardSelectionBehaviour>(removeProvider, ref _cardSelectionBehaviour);
            DepInjector.UnmapProvider<CardStatistics>(removeProvider, ref _cardStatistics);
            DepInjector.UnmapProvider<ChallengeDetailsScreen>(removeProvider, ref _challengeDetailsScreen);
            DepInjector.UnmapProvider<ChampionUpgradeScreen>(removeProvider, ref _championUpgradeScreen);
            DepInjector.UnmapProvider<CombatManager>(removeProvider, ref _combatManager);
            DepInjector.UnmapProvider<CreditsScreen>(removeProvider, ref _creditsScreen);
            DepInjector.UnmapProvider<DiscordManager>(removeProvider, ref _discordManager);
            DepInjector.UnmapProvider<FileCacheManager>(removeProvider, ref _fileCacheManager);
            DepInjector.UnmapProvider<FloatingRewardManager>(removeProvider, ref _floatingRewardManager);
            DepInjector.UnmapProvider<GameScreen>(removeProvider, ref _gameScreen);
            DepInjector.UnmapProvider<GameStateManager>(removeProvider, ref _gameStateManager);
            DepInjector.UnmapProvider<GameVFXManager>(removeProvider, ref _gameVFXManager);
            DepInjector.UnmapProvider<GraphicsSettingsManager>(removeProvider, ref _graphicsSettingsManager);
            DepInjector.UnmapProvider<HandUI>(removeProvider, ref _handUI);
            DepInjector.UnmapProvider<HeroManager>(removeProvider, ref _heroManager);
            DepInjector.UnmapProvider<InkWriter>(removeProvider, ref _inkWriter);
            DepInjector.UnmapProvider<InputManager>(removeProvider, ref _inputManager);
            DepInjector.UnmapProvider<LanguageManager>(removeProvider, ref _languageManager);
            DepInjector.UnmapProvider<MerchantCharacterUI>(removeProvider, ref _merchantCharacterUI);
            DepInjector.UnmapProvider<MonsterManager>(removeProvider, ref _monsterManager);
            DepInjector.UnmapProvider<PlayerManager>(removeProvider, ref _playerManager);
            DepInjector.UnmapProvider<PopupNotificationManager>(removeProvider, ref _popupNotificationManager);
            DepInjector.UnmapProvider<PreferencesManager>(removeProvider, ref _preferencesManager);
            DepInjector.UnmapProvider<PubSubManager>(removeProvider, ref _pubSubManager);
            DepInjector.UnmapProvider<RelicManager>(removeProvider, ref _relicManager);
            DepInjector.UnmapProvider<RoomManager>(removeProvider, ref _roomManager);
            DepInjector.UnmapProvider<SaveManager>(removeProvider, ref _saveManager);
            DepInjector.UnmapProvider<ScenarioManager>(removeProvider, ref _scenarioManager);
            DepInjector.UnmapProvider<SceneVFXManager>(removeProvider, ref _sceneVFXManager);
            DepInjector.UnmapProvider<SoundManager>(removeProvider, ref _soundManager);
            DepInjector.UnmapProvider<StatusEffectManager>(removeProvider, ref _statusEffectManager);
            DepInjector.UnmapProvider<SteamClientHades>(removeProvider, ref _steamClientHades);
            DepInjector.UnmapProvider<StoryManager>(removeProvider, ref _storyManager);
           
        }
        #endregion
    }
}
