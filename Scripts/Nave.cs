using Godot;
using System;

public partial class Nave : CharacterBody2D
{
    [Export] PackedScene laser = ResourceLoader.Load<PackedScene>("res://Prefabs/laser_nave.tscn");
    [Export] PackedScene explosion = ResourceLoader.Load<PackedScene>("res://Prefabs/explosion.tscn");
    int direction = 0;

    Timer timerFire;
    bool isFire;
    public override void _Ready()
    {
        isFire = false;
        Callable callable = Callable.From(() => EnableFire());

        timerFire = new Timer();
        timerFire.OneShot = true;
        timerFire.WaitTime = 0.05;
        timerFire.Autostart = true;
        timerFire.Connect("timeout", callable);
        AddChild(timerFire);
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

        if (isFire == true)
        {
            Fire();
            isFire = false;
            timerFire.Start();
        }
    }

    private void EnableFire()
    {
        isFire = true;
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

    private void Fire()
    {
        Node laserNode = laser.Instantiate();
        GetParent().GetParent().AddChild(laserNode);
        laserNode.GetNode<Node2D>(laserNode.GetPath()).Position = new Vector2(GlobalPosition.X, GlobalPosition.Y);
        laserNode.GetNode<Node2D>(laserNode.GetPath()).RotationDegrees = RotationDegrees;
    }
    public void Explosion()
    {
        Node explosionNode = explosion.Instantiate();
        GetParent().GetParent().AddChild(explosionNode);
        explosionNode.GetNode<Node2D>(explosionNode.GetPath()).Position = new Vector2(GlobalPosition.X, GlobalPosition.Y);
        QueueFree();
    }
}
