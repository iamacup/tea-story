using Godot;
using System;

public class Chat : Control
{
    Choice choice;
    Dialog dialog;

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

        this.showDialog("Some text here youo crasy little popop opasd jasdh ahsdj ksahdkj hajskdhk ajd haskjd text here youo crasy little popop opasd jasdh ahsdj ksahdkj hajskdhk ajd haskjd text here youo crasy little popop opasd jasdh ahsdj ksahdkj hajskdhk ajd haskjd");
    }

    public void showDialog(string text) {
        this.choice.Visible = false;
        this.dialog.Visible = true;

        this.dialog.ShowDialog(text);
    }

    public void showChoices(string text, string[] choices) {
        this.choice.Visible = true;
        this.dialog.Visible = false;
    }
}
