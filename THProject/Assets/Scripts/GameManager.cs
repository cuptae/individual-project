
public class GameManager
{
    public TaskList MyShipList;
    public TaskList EnemyList;
    public TaskList BulletList;
    public MyShip myShip;

    public static GameManager Instance;

    public GameManager()
    {
        MyShipList = new TaskList(100,5);
        EnemyList = new TaskList(100,50);
        BulletList = new TaskList(100,50000);
    }
    public void MoveTask(TaskList list)
    {
        TaskIter iter = new TaskIter(list);
        while (iter.HasNext())
        {
            Mover mover = iter.Next() as Mover;
            if (mover == null) continue;

            mover.Move();
            if (!mover.Alive)
                iter.Remove();
        }
    }

    public void DeleteTask(TaskList list)
    {
        TaskIter iter = new TaskIter(list);
        while (iter.HasNext())
        {
            iter.Next(); // Advance
            iter.Remove();
        }
    }

    public void Move()
    {
        MoveTask(MyShipList);
        MoveTask(EnemyList);
        MoveTask(BulletList);
    }

    public void Die()
    {
        DeleteTask(MyShipList);
        DeleteTask(EnemyList);
        DeleteTask(BulletList);
    }

    public static void Main()
    {
        GameManager bulletGame = new GameManager();
        MyShip ship = new MyShip(5); // 내부에서 GameManager.Instance.MyShip = this;

        bulletGame.Move();
        bulletGame.Die();

        // C#에서는 delete 필요 없음
    }
}
