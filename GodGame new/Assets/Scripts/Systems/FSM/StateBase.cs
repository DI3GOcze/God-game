// States of Finite state machine
public class StateBase
{
    virtual public void EnterState() { } 
    virtual public void UpdateState() { }
    virtual public void ExitState() { }
}