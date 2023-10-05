namespace EnglishHelperService.Business
{
    /// <summary>
    /// Generic shuffle helper class for any list
    /// </summary>
    public class ShuffleHandler
    {
        /// <summary>
        /// Shuffle any list
        /// </summary>
        public void Shuffle<T>(List<T> list)
        {
            Random random = new Random();
            int currentIndex = list.Count;
            while (currentIndex > 1)
            {
                currentIndex--;
                int randomIndex = random.Next(currentIndex + 1);
                T currentValue = list[randomIndex];
                list[randomIndex] = list[currentIndex];
                list[currentIndex] = currentValue;
            }
        }
    }
}

