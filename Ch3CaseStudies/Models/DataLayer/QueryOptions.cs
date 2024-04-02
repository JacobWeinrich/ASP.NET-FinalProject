using System.Linq.Expressions;

namespace Ch3CaseStudies.Models.DataLayer
{
    public class QueryOptions<T>
    {
        // public properties for sorting and filtering
        public Expression<Func<T, Object>> OrderBy { get; set; } = null!;
        public Expression<Func<T, Object>> ThenOrderBy { get; set; } = null!;
        public Expression<Func<T, bool>> Where { get; set; } = null!;

        // private string array for include statements
        private string[] includes = Array.Empty<string>();

        // public write-only property for include statements - converts string to array
        public string Includes
        {
            set => includes = value.Replace(" ", "").Split(',');
        }

        // public get method for include statements - returns private string array or empty string array
        public string[] GetIncludes() => includes;

        // read-only properties 
        public bool HasWhere => Where != null;
        public bool HasOrderBy => OrderBy != null;
        public bool HasThenOrderBy => ThenOrderBy != null;
    }
}
