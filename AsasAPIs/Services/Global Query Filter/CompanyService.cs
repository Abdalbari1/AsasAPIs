namespace AsasAPIs.Global_Query_Filter
{
    public class CompanyService : ICompanyService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentCompanyId()
        {

            var claim = _httpContextAccessor.HttpContext?.User.FindFirst("CompanyId")?.Value;

            return int.TryParse(claim, out int comId) ? comId : 0;
        }
    }
}
