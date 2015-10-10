public class Buff
{
    private int duration;
    public string name;

    public Buff(string name, int duration)
    {
        this.name = name;
        //plus 1 cause immediately reduced
        this.duration = duration+1;
    }

    public void ReduceDuration()
    {
        duration--;
    }

    public int GetDuration()
    {
        return duration;
    }
}
