[System.Serializable]
public class LevelData
{
    public int[] level;
    public int levelCount;

    public LevelData(Level levelConfig) {
        level = levelConfig.level;
        levelCount = levelConfig.levelCount;
    }
}
