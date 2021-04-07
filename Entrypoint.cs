using Godot;
using System;
using System.Collections.Generic;

public class Entrypoint : Control
{
    BlackText intro;
    Chat chat;

    public override void _Ready()
    {
        PackedScene scene = ResourceLoader.Load("res://BlackText.tscn") as PackedScene;
        this.intro = scene.Instance() as BlackText;
        this.intro.Connect("ready", this, "onBlackTextReadyIntro");
        this.AddChild(this.intro);
    }
    
    public void onBlackTextReadyIntro()
    {
        this.intro.label.BbcodeText = @"[center]Your name is [b]YouTuber Hair Max[/b]

You just walked into a coffee shop to meet with [b]Vampire Skin Robin[/b][/center]";

        this.intro.button.Text = "Let's Go!";

        this.intro.Connect("Next", this, "onIntroNext");
    }

    public void onIntroNext() {
        this.intro.Connect("tree_exited", this, "addChat");
        this.intro.QueueFree();   
    }

    public void addChat() {
        PackedScene scene = ResourceLoader.Load("res://Chat.tscn") as PackedScene;
        this.chat = scene.Instance() as Chat;
        this.chat.Connect("Next", this, "onChatNext");

        this.AddChild(this.chat);

        List<Thing> things = new List<Thing>();

        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[b]Max:[/b] Hello there!", ID = "1", nextID = "2"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[b]Robin:[/b] Hey!", ID = "2", nextID = "3"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[b]Max:[/b] Do you want some Tea?", ID = "3", nextID = "4"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[b]Robin:[/b] Sure. that would be great", ID = "4", nextID = "5"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[i]Max orders some tea for the both of them[/i]", ID = "5", nextID = "6"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[i]Robin and Max make small talk[/i]", ID = "6", nextID = "7"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[i]The tea arrives[/i]", ID = "7", nextID = "8"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[b]Robin:[/b] Hey Max, I don't think i want this tea anymore :(", ID = "8", nextID = "9"});

        ChoiceStruct[] arr1 = { 
            new ChoiceStruct() { Text = "Ok that's cool <No Logic>", ID = "1", NextID = "9-1" },
            new ChoiceStruct() { Text = "That's a shame <No Logic>", ID = "2", NextID = "9-1" },
            new ChoiceStruct() { Text = "You have to drink it! I ordered it for you", ID = "3", NextID = "9-1" }
        };
        
        things.Add(new Thing() {Type = ThingType.Choice, Text = "[b]Robin:[/b] Hey Max, I don't think i want this tea anymore :(", ID = "9", choices = arr1 });
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[b]Robin:[/b] I really don't want to", ID = "9-1", nextID = "10"});

        this.chat.start(things);
    }

    public void onChatNext()
    {
        this.chat.Connect("tree_exited", this, "addMidpoint");
        this.chat.QueueFree();
    }

    public void addMidpoint()
    {
        PackedScene scene = ResourceLoader.Load("res://BlackText.tscn") as PackedScene;
        this.intro = scene.Instance() as BlackText;
        this.intro.Connect("ready", this, "onBlackTextReadyMidpoint");
        this.AddChild(this.intro);
    }

    public void onBlackTextReadyMidpoint()
    {
        this.intro.label.BbcodeText = @"[center]You just lived this experience as [b]YouTuber Hair Max[/b]

You now get to experience it as [b]Vampire Skin Robin[/b][/center]";

        this.intro.button.Text = "Cool, Let's see it!";

        // this.intro.Connect("Next", this, "onIntroNext");
    }
}
