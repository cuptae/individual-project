using System;
using System.Diagnostics;
using System.Drawing;


public class MyShip:Mover
{


    public bool PrevButon;
    // void* operator new(size_t n)
    // {

    // }
    //void operator delete(void* p);
    
    public MyShip(int shape_id):base(GameManager.Instance.MyShipList,shape_id,0,0,0)
    {
        PrevButon = true;
        Console.WriteLine($"{shape_id} MyShip");
        GameManager.Instance.myShip = this;
    }

    public TaskList New(int n)
    {
        return (TaskList)GameManager.Instance.MyShipList.New(n);
    }

    public void Delete(object p)
    {
        GameManager.Instance.MyShipList.Delete(p);
    }

    public override void Move()
    {

    }
}
