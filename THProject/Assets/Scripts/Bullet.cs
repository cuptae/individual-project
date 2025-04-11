

public class Bullet:Mover
{
    public 	float AngleRate;
	public float Speed, SpeedRate;
    public Bullet(int shape_id, float x, float y,float angle, float angle_rate,float speed, float speed_rate):base(GameManager.Instance.BulletList,shape_id,x,y,angle)
    {
        this.AngleRate = angle_rate;
        this.Speed = speed;
        this.SpeedRate = speed_rate;
    }
    public TaskList New(int n)
    {
        return (TaskList)GameManager.Instance.BulletList.New(n);
    }

    public void Delete(object p)
    {
        GameManager.Instance.BulletList.Delete(p);
    }

    public override void Move()
    {

    }
}
