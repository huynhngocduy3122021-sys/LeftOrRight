using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLevel", menuName = "Scriptable Objects/GameLevel")]
public class GameLevel : ScriptableObject
{
    public List<GameData> listLevel;
}
