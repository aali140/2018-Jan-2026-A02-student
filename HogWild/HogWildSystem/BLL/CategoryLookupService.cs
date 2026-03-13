using HogWildSystem.DAL;
using BYSResults;
using HogWildSystem.ViewModels;

namespace HogWildSystem.BLL
{
    public class CategoryLookupService
    {
        #region Fields
        //  hog wild context
        private readonly HogWildContext _hogWildContext;
        #endregion


        //  Constructor for the CategoryLookupService CLass
        internal CategoryLookupService(HogWildContext hogWildContext)
        {
            //  initialize the _hogWildContext field with the provided
            //      HogWildContext instance
            _hogWildContext = hogWildContext;
        }

        //  get the lookup
        public Result<List<LookupView>> GetLookupView(string categoryName)
        {
            //	Create a Result container that will hold either a 
            //		CustomerSeearchView object(s) on success or anu accumulated errors on failure
            var result = new Result<List<LookupView>>();

            #region Business Rules
            //  These are processing rules that need to be satisfied for valid data
            //      rule:   category name cannot be empty
            //      rule:   RemoveFromViewFlag must be false (not soft deleted)

            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return result.AddError(new Error("Missing Information",
                    "Please provide a valid category name"));
            }
            #endregion

            var lookups = _hogWildContext.Lookups
                .Where(l => l.Category.CategoryName == categoryName
                            && !l.RemoveFromViewFlag)
                .Select(l => new LookupView
                {
                    LookupID = l.LookupID,
                    CategoryID = l.CategoryID,
                    Name = l.Name,
                    RemoveFromViewFlag = l.RemoveFromViewFlag
                })
                .OrderBy(l => l.Name)
                .ToList();

            //  if not lookups was found with the category name
            if (lookups == null || lookups.Count == 0)
            {
                return result.AddError(new Error("No Lookups",
                    $"No lookups for category name '{categoryName}' was found"));
            }
            //  return the result
            return result.WithValue(lookups);
        }
    }
}
