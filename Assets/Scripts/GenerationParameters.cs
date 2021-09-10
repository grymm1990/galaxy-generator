public class GenerationParameters
{
    public int seed;
    public float rotation;
    public int arms;
    public StarType[] starTypes;

    public GenerationParameters(int seed, float rotation, int arms, StarType[] starTypes)
    {
        this.seed = seed;
        this.rotation = rotation;
        this.arms = arms;
        this.starTypes = starTypes;
    }
}
