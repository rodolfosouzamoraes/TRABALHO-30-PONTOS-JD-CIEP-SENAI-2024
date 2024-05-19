using Godot;
using System;

public partial class Nave : CharacterBody2D
{
    int direction = 0;
    public override void _Ready()
    {
        
    }

    public override void _Process(double delta)
    {
        if(direction == 1){
            RotationDegrees += 250 * (float)delta;
        }
        else if(direction == 2)
        {
            RotationDegrees -= 250 * (float)delta;
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed && eventKey.Keycode == Key.Right)
            {
                direction = 1;
            }
            else if (eventKey.Pressed && eventKey.Keycode == Key.Left)
            {
                direction = 2;
            }
            else
            {
                direction = 0;
            }
        }
    }
}
