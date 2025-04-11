public class Enemy:Mover
{
    public Enemy(int shape_id):base(GameManager.Instance.EnemyList, shape_id, 0, -0.7f, 0)
    {

    }

    public TaskList New(int n)
    {
        return GameManager.Instance.EnemyList;
    }

    public void Delete(object p)
    {
        GameManager.Instance.EnemyList.Delete(p);
    }

}
