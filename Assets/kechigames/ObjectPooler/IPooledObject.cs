namespace kechigames.ObjectPooler
{
    public interface IPooledObject 
    {
        void OnObjectSpawn();
        void OnObjectReturn();
    }
}
