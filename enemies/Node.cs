using Godot;
using System;

public class Node : Godot.Node
{
  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	  //GD.Print("test");
	  GD.Var2Str(delta);
  }
}
