using Godot;
using System;

public class Intro : Control
{
    float lapsed = 0;

    RichTextLabel label;
    Button button;

    [Signal]
    public delegate void Next();
    
    public override void _Ready()
    {
        this.label = this.FindNode("Text") as RichTextLabel;
        this.button = this.FindNode("Button") as Button;

        this.button.Connect("pressed", this, "onButtonPressed");
    }

    public void onButtonPressed() {
        EmitSignal(nameof(Next));
    }
    
     // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(this.label.VisibleCharacters <= this.label.Text.Length) {
            this.lapsed = this.lapsed + delta;
            this.label.VisibleCharacters = (int)Math.Ceiling(lapsed / 0.05);
        }

        if(this.label.VisibleCharacters >= this.label.Text.Length && this.button.Visible == false) {
            this.showButton();
        } 
    }

    public void showButton() {
        this.button.Visible = true;
    }
}
