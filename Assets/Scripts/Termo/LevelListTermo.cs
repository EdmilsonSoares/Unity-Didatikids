using System.Collections.Generic;
using UnityEngine;

namespace Termo
{
    [CreateAssetMenu(fileName = "Levels", menuName = "SO/AllLevels")]
    public class LevelListTermo : ScriptableObject
    {
        public List<LevelDataTermo> Levels;
    }
}
