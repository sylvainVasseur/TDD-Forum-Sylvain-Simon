using forum_api.DataAccess.DataObjects;

namespace forum_api.Services
{
    public interface IWordFilterService
    {
        string WordFilterSentence(string sentence);
    }
}