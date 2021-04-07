using Godot;
using System;

public class Choice : Control
{
    HBoxContainer choiceContainer;
    PackedScene ChoiceButton;
    RichTextLabel text;
    string ID;

    [Signal]
    public delegate void ChoicePicked(string ID, string choiceID);

    public override void _Ready()
    {
        this.choiceContainer = this.FindNode("ChoiceContainer") as HBoxContainer;
        this.text = this.FindNode("Text") as RichTextLabel;
        
        this.ChoiceButton = ResourceLoader.Load("res://ChoiceButton.tscn") as PackedScene;
    }

    public void showChoices(string text, ChoiceStruct[] choices, string ID) 
    {   
        this.ID = ID;
        this.text.BbcodeText = text;

        Godot.Collections.Array children = this.choiceContainer.GetChildren();

        for(int a=0; a<children.Count; a++) 
        {
            this.choiceContainer.RemoveChild(children[a] as Node);
            (children[a] as Node).QueueFree();
        }

        for(int a=0; a<choices.Length; a++) 
        {
            Button button = this.ChoiceButton.Instance() as Button;
            button.Text = choices[a].Text;
            button.Connect("pressed", this, "onButtonPressed", new Godot.Collections.Array() {choices[a].ID});
            this.choiceContainer.AddChild(button);
        }
    }

    public void onButtonPressed(string ID) 
    {
        EmitSignal(nameof(ChoicePicked), this.ID, ID);
    }
}
