using Godot;
using Microsoft.VisualBasic;
using System;

public partial class pro_gen_lvl_1 : Node2D
{
    [Signal] public delegate Vector2 MapsPositionsEventHandler(Vector2 position1, Vector2 position2);
    public Vector2 map1;
    public Vector2 map2;
    Vector2[] maps = {new Vector2(0,0),new Vector2(1152,0)};
    RandomNumberGenerator rng = new RandomNumberGenerator();
    public override void _Ready()
    {
        base._Ready();
        int a = rng.RandiRange(0, 1);
        map1 = maps[a];
        int b = rng.RandiRange(0, 1);
        while (a == b)
        {
            b = rng.RandiRange(0, 1);
        }
        map2 = maps[b];
        EmitSignal("MapsPositions", map1,map2);
    }
}
