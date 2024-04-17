using Microsoft.AspNetCore.Components;

namespace Movieasy.Application.IntegrationTests
{
    public static class GlobalData
    {
        public static readonly string SearchTerm = "test search term";
        public static readonly int PageNumber = 2;
        public static readonly int PageSize = 12;
        public static readonly string SortOrder = "desc";
        public static readonly string SortColumn = "random";
    }
}
