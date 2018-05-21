namespace SocialNetworkGraph
{
    interface IGraphVertex
    {
        int ID { get; }

        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}