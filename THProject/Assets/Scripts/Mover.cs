using System;

public class Mover:Task
{
    public float X, Y, Angle;
    public float Scale, Alpha;
    public bool Alive;
    public int Color;

    public Mover(TaskList list, int shape_id, float x, float y, float angle):base(list)
    {
        this.X = x;
        this.Y = y;
        this.Angle = angle;
        Scale = 1;
        Alpha = 1;
        Alive = true;
        Color = -1;
    }

    public virtual void Move(){ Console.WriteLine("Move");}
}
