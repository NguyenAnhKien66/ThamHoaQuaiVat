[System.Serializable]
public class DuLieuGame
{
    public DuLieuMap[] maps;

    public DuLieuGame(int soMap)
    {
        maps = new DuLieuMap[soMap];
        for (int i = 0; i < soMap; i++)
        {
            maps[i] = new DuLieuMap();
        }
    }
}
