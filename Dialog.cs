using Godot;
using System;

public class Dialog : Control
{
    RichTextLabel label;
    float lapsed = 0;
    bool running = false;

    [Signal]
    public delegate void TextDisplayed();

    public override void _Ready()
    {
       this.label = this.FindNode("Text") as RichTextLabel;
    }

    public void ShowDialog(string text)
    {
        this.label.BbcodeText = text;
        this.label.VisibleCharacters = 0;
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
                EmitSignal(nameof(TextDisplayed));
                this.running = false;
            } 
        }
    }
}