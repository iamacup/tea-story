using Godot;
using System;

public class Entrypoint : Control
{
    Control intro;
    Control chat;

    public override void _Ready()
    {
        PackedScene scene = ResourceLoader.Load("res://Intro.tscn") as PackedScene;
        this.intro = scene.Instance() as Control;

        this.AddChild(this.intro);

        this.intro.Connect("Next", this, "onIntroNext");
    }
    
    public void onIntroNext() {
        this.intro.Connect("tree_exited", this, "addChat");
        this.intro.QueueFree();   
    }

    public void addChat() {
        PackedScene scene = ResourceLoader.Load("res://Chat.tscn") as PackedScene;
        this.chat = scene.Instance() as Control;

        this.AddChild(this.chat);
    }
}
