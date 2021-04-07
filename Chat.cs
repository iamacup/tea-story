using Godot;
using System;
using System.Collections.Generic;

public class Chat : Control
{
    Choice choice;
    Dialog dialog;

    List<Thing> things = new List<Thing>();

    [Signal]
    public delegate void Next();

    public override void _Ready()
    {
        PackedScene scene1 = ResourceLoader.Load("res://Choice.tscn") as PackedScene;
        this.choice = scene1.Instance() as Choice;
        this.choice.Visible = false;
        this.AddChild(this.choice);

        PackedScene scene2 = ResourceLoader.Load("res://Dialog.tscn") as PackedScene;
        this.dialog = scene2.Instance() as Dialog;      
        this.dialog.Visible = false;
        this.AddChild(this.dialog);

        this.dialog.Connect("TextDisplayed", this, "onTextDisplayed");
        this.choice.Connect("ChoicePicked", this, "onChoicePicked");        
    }

    public void start(List<Thing> things)
    {   
        this.things = things;
        this.showDialog(this.things[0].Text, this.things[0].ID);
    }

    public async void onTextDisplayed(string ID) 
    {
        Timer t = new Timer();
        t.OneShot = true;
        t.WaitTime = 1.5f;
        this.AddChild(t);
        t.Start();
        await ToSignal(t, "timeout");
        t.QueueFree();

        this.doNextItem(ID, null);
    }

    public void onChoicePicked(string ID, string choiceID) 
    {
        this.doNextItem(ID, choiceID);
    }

    public void showDialog(string text, string ID) 
    {
        this.dialog.ShowDialog(text, ID);

        this.choice.Visible = false;
        this.dialog.Visible = true;
    }

    public void showChoices(string text, ChoiceStruct[] choices, string ID) 
    {
        this.choice.showChoices(text, choices, ID);

        this.choice.Visible = true;
        this.dialog.Visible = false;  
    }

    public void doNextItem(string ID, string choiceID) 
    {
        bool next = false;
        string nextID = "";

        for(int a=0; a<this.things.Count; a++) 
        {
            if(this.things[a].ID == ID) 
            {
                if(choiceID != null) 
                {
                    for(int b=0; b<this.things[a].choices.Length; b++) 
                    {
                        if(this.things[a].choices[b].ID == choiceID)
                        {
                            nextID = this.things[a].choices[b].NextID;
                        }
                    }
                }
                else
                {
                    nextID = this.things[a].nextID;
                }
            }
        }

        for(int a=0; a<this.things.Count; a++) 
        {
            if(this.things[a].ID == nextID) 
            {
                if(this.things[a].Type == ThingType.Choice)
                {
                    next = true;
                    this.showChoices(this.things[a].Text, this.things[a].choices, this.things[a].ID);
                }
                else if(this.things[a].Type == ThingType.Dialog)
                {
                    next = true;
                    this.showDialog(this.things[a].Text, this.things[a].ID);
                }
            }
        }        

        if(next == false) 
        {
            EmitSignal(nameof(Next));
        }
    }
}

public enum ThingType  
{
    Choice,
    Dialog,
}

public class Thing 
{
    public ThingType Type {get;set;}
    public string Text {get;set;}
    public string ID {get;set;}
    public ChoiceStruct[] choices {get;set;}
    public string nextID {get;set;}
}

