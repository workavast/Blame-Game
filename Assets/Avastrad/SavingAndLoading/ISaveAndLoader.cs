namespace Avastrad.SavingAndLoading
{
    public interface ISaveAndLoader
    {
        public void Save(object data);

        /// <returns>if save exist return loaded T, else return new T()</returns>
        public T TryLoad<T>() where T : new();

        /// <returns>if save exist return loaded T, else throw exception</returns>
        public T Load<T>();
        
        public bool Exist();

        public void DeleteSave();
    }
}