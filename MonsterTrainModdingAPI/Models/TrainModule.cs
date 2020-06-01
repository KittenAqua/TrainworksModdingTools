using System.Collections.Generic;
using BepInEx;
using MonsterTrainModdingAPI.Builder;

namespace MonsterTrainModdingAPI.Models
{
    public interface TrainModule
    {
        List<CardDataBuilder> RegisterCustomCards();
        List<RelicData> RegisterCustomArtifacts();
        List<CharacterData> RegisterCustomCharacters();
        BaseUnityPlugin RegisterPlugin();
    }
}