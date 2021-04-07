using Godot;
using System;
using System.Collections.Generic;

public class Entrypoint : Control
{
    BlackText intro;
    Chat chat;

    public override void _Ready()
    {
        this.start();
    }

    public void start()
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

    public void onIntroNext() 
    {
        this.intro.Connect("tree_exited", this, "addChat");
        this.intro.QueueFree();   
    }

    public void addChat() 
    {
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
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[color=red][b]A MINIGAME COULD GO HERE?  MAKING TEA? CHECKERS? OR A CARD GAME? etc. etc.[/b][/color]", ID = "6", nextID = "7"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[i]The tea arrives[/i]", ID = "7", nextID = "8"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[b]Robin:[/b] Hey Max, I don't think i want this tea anymore :(", ID = "8", nextID = "9"});

        ChoiceStruct[] arr1 = { 
            new ChoiceStruct() { Text = "Why did you let me order it then?", ID = "1", NextID = "9-1" },
            new ChoiceStruct() { Text = "That's a shame, I paid for this", ID = "2", NextID = "9-2" },
        };
        things.Add(new Thing() {Type = ThingType.Choice, Text = "[b]Robin:[/b] Hey Max, I don't think i want this tea anymore :(", ID = "9", choices = arr1 });
        
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[b]Robin:[/b] Sorry Max, now it's infront of me I just don't want it", ID = "9-1", nextID = "10-1"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[b]Robin:[/b] Yeha sorry Max, I just don't want it anymore now i see it", ID = "9-2", nextID = "10-2"});

        ChoiceStruct[] arr2 = { 
            new ChoiceStruct() { Text = "You have to drink it! I ordered it for you", ID = "1", NextID = "11" }
        };
        things.Add(new Thing() {Type = ThingType.Choice, Text = "[b]Robin:[/b] Sorry Max, now it's infront of me I just don't want it", ID = "10-1", choices = arr2 });
        
        ChoiceStruct[] arr3 = { 
            new ChoiceStruct() { Text = "You have to drink it! I ordered it for you", ID = "1", NextID = "11" }
        };
        things.Add(new Thing() {Type = ThingType.Choice, Text = "[b]Robin:[/b] Yeha sorry Max, I just don't want it anymore now i see it", ID = "10-2", choices = arr3 });
                
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[b]Robin:[/b] But Max....", ID = "11", nextID = "12"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[i]Max stares at Robin[/i]", ID = "11", nextID = "12"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = "[b]Robin:[/b] Fine.... I'll drink it....", ID = "11", nextID = "12"});

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

        this.intro.Connect("Next", this, "onMidpointNext");
    }

    public void onMidpointNext() 
    {
        this.intro.Connect("tree_exited", this, "addReplay");
        this.intro.QueueFree();   
    }

     public void addReplay() {
        PackedScene scene = ResourceLoader.Load("res://Chat.tscn") as PackedScene;
        this.chat = scene.Instance() as Chat;
        this.chat.Connect("Next", this, "onReplayNext");

        this.AddChild(this.chat);

        List<Thing> things = new List<Thing>();

        things.Add(new Thing() {Type = ThingType.Dialog, Text = @"[b]Max:[/b] Hello there!", ID = "1", nextID = "2"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = @"[b]Robin:[/b] Hey!", ID = "2", nextID = "3"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = @"[i]Pleasantries are exchanged, the teas ordered and they arrive[/i]", ID = "3", nextID = "4"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = @"[b]Robin:[/b] Hey Max, I don't think i want this tea anymore :(", ID = "4", nextID = "5"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = @"[b]Max:[/b] Why did you let me order it then?", ID = "5", nextID = "6"});
        things.Add(new Thing() {Type = ThingType.Dialog, Text = @"[b]Robin:[/b] Sorry Max, now it's infront of me I just don't want it", ID = "6", nextID = "7"});

        ChoiceStruct[] arr1 = { 
            new ChoiceStruct() { Text = "Robin want's to leave the coffee shop...", ID = "1", NextID = "8" }
        };
        things.Add(new Thing() {Type = ThingType.Choice, Text = "[b]Robin:[/b] Sorry Max, now it's infront of me I just don't want it", ID = "7", choices = arr1 });

        things.Add(new Thing() {Type = ThingType.Dialog, Text = @"[b]Max:[/b] You have to drink it! I ordered it for you", ID = "8", nextID = "9"});    
        things.Add(new Thing() {Type = ThingType.Dialog, Text = @"[b]Robin:[/b] But Max....", ID = "9", nextID = "10"});

        ChoiceStruct[] arr2 = { 
            new ChoiceStruct() { Text = "Max's response to Robin no longer wanting the tea has scared her...", ID = "1", NextID = "11" }
        };
        things.Add(new Thing() {Type = ThingType.Choice, Text = "[b]Robin:[/b] But Max....", ID = "10", choices = arr2 });

        things.Add(new Thing() {Type = ThingType.Dialog, Text = @"[i]Max stares at Robin[/i]", ID = "11", nextID = "12"});

        ChoiceStruct[] arr3 = { 
            new ChoiceStruct() { Text = "Robin does not enjoy the intensity of Max at this moment...", ID = "1", NextID = "13" }
        };
        things.Add(new Thing() {Type = ThingType.Choice, Text = "[i]Max stares at Robin[/i]", ID = "12", choices = arr3 });

        things.Add(new Thing() {Type = ThingType.Dialog, Text = @"[b]Robin:[/b] Fine.... I'll drink it....", ID = "13", nextID = "14"});

        ChoiceStruct[] arr4 = { 
            new ChoiceStruct() { Text = "Robin has been forced to do something she does not want...", ID = "1", NextID = "15" }
        };
        things.Add(new Thing() {Type = ThingType.Choice, Text = "[b]Robin:[/b] Fine.... I'll drink it....", ID = "14", choices = arr4 });

        this.chat.start(things);
    }

    public void onReplayNext()
    {
        this.chat.Connect("tree_exited", this, "addEnd");
        this.chat.QueueFree();
    }

    public void addEnd()
    {
        PackedScene scene = ResourceLoader.Load("res://BlackText.tscn") as PackedScene;
        this.intro = scene.Instance() as BlackText;
        this.intro.Connect("ready", this, "onBlackTextReadyEnd");
        this.AddChild(this.intro);
    }

    public void onBlackTextReadyEnd()
    {
        this.intro.label.BbcodeText = @"[center]While this conversation is about Tea, its easy to understand how this could be about something more serious, like [b]Sexual Intercourse[/b]
        
        Robin clearly did not want to drink the tea, and no one should force her, it put her in an uncomfortable situation.
        
        [b]Remember![/b] Consent can be withdrawn at any time![/center]";

        this.intro.button.Text = "Start Again!";

        this.intro.Connect("Next", this, "restart");
    }

    public void restart() 
    {
        this.intro.Connect("tree_exited", this, "start");
        this.intro.QueueFree();   
    }
}
