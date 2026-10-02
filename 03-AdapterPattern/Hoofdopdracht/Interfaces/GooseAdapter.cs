namespace AdapterPattern.Interfaces
{
    internal class GooseAdapter : Duck
    {
        private Goose goose;
        public GooseAdapter(Goose goose)
        {
            this.goose = goose;
        }
        public void Quack()
        {
            goose.Honk();
        }

        public void Fly()
        {
            goose.Fly();
        }
    }
}
