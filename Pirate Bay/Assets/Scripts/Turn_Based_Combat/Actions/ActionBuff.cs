public class ActionBuff : Action
{
    protected Combatant target;
    protected string name;
    protected int duration;

    public ActionBuff(Combatant me, string name, int duration)
    {
        this.target = me;
        this.name = name;
        this.duration = duration;
        
    }
    public override void Work(float deltaTime)
    {
        target.buffs.Add(new Buff(name, duration));
        done = true;
    }
}
