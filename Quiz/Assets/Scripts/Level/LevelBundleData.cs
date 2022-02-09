using UnityEngine;

namespace Quiz.Level
{
    [CreateAssetMenu(fileName = "New LevelBundleData", menuName = "Level Bundle Data", order = 52)]
    public class LevelBundleData : ScriptableObject
    {
        [SerializeField]
        private LevelData[] levelDatas;

        public LevelData[] LevelDatas => levelDatas;
    }
}
