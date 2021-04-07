using Godot;
using System;

public class Dialog : Control
{
    RichTextLabel label;
    float lapsed = 0;
    bool running = false;
    string ID;

    [Signal]
    public delegate void TextDisplayed(string ID);

    public override void _Ready()
    {
       this.label = this.FindNode("Text") as RichTextLabel;
    }

    public void ShowDialog(string text, string ID)
    {
        this.lapsed = 0;
        this.label.VisibleCharacters = 0;
        this.ID = ID;
        this.label.BbcodeText = text;
        this.running = true;
    }

    public override void _Process(float delta)
    {
        if(running == true) 
        {
            if(this.label.VisibleCharacters <= this.label.Text.Length) {
                this.lapsed = this.lapsed + delta;
                this.label.VisibleCharacters = (int)Math.Ceiling(lapsed / 0.05);
            }

            if(this.label.VisibleCharacters >= this.label.Text.Length) {
                EmitSignal(nameof(TextDisplayed), this.ID);
                this.running = false;
            } 
        }
    }
}