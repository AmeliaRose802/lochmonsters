namespace RandomGenerator.Scripts
{ 
    public struct Tuple<T, T1>
    {
        private readonly T m_item1;
        private readonly T1 m_item2;

        public T Item1
        {
            get { return m_item1; }
        }

        public T1 Item2
        {
            get { return m_item2; }
        }

        public Tuple(T item1, T1 item2)
        {
            m_item1 = item1;
            m_item2 = item2;
        }
    }

    public struct Tuple<T, T1, T2>
    {
        private readonly T m_item1;
        private readonly T1 m_item2;
        private readonly T2 m_item3;

        public T Item1
        {
            get { return m_item1; }
        }

        public T1 Item2
        {
            get { return m_item2; }
        }

        public T2 Item3
        {
            get { return m_item3; }
        }

        public Tuple(T item1, T1 item2, T2 item3)
        {
            m_item1 = item1;
            m_item2 = item2;
            m_item3 = item3;
        }
    }
}