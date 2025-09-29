namespace App.Ecs
{
    public enum CollisionLayers
    {
        Default = 0,
        TransparentFX = 1 << 1,
        IgnoreRaycast = 1 << 2,
        Ground = 1 << 3,
        Water = 1 << 4,
        UI = 1 << 5,
        Enemy = 1 << 6,
        Player = 1 << 7,
        PlayerPerk = 1 << 8,
    }
}