using Godot;
using Microsoft.VisualBasic;
using System;

public partial class pro_gen_lvl_1 : Node2D
{
    [Signal] public delegate Vector2 MapsPositionsEventHandler(Vector2 position1, Vector2 position2, Vector2 position3);
    public Vector2 map1;
    public Vector2 map2;
    public Vector2 map3;
    Vector2[] maps = {new Vector2(0,0),new Vector2(1152,0), new Vector2(2304,0) };
    RandomNumberGenerator rng = new RandomNumberGenerator();
    public override void _Ready()
    {
        base._Ready();
        int a = rng.RandiRange(0, 2);
        map1 = maps[a];
        int b = rng.RandiRange(0, 2);
        while (a == b)
        {
            b = rng.RandiRange(0, 2);
        }
        map2 = maps[b];
        int c = rng.RandiRange(0, 2);
        while ((c == a) | (c == b))
        {
            c = rng.RandiRange(0, 2);
        }
        map3 = maps[c];
        EmitSignal("MapsPositions", map1,map2,map3);
    }
}
