namespace LR4
{
    public class IdRouteConstraint : IRouteConstraint
    {
        private int MinId { get; set; }
        private int MaxId { get; set; }
        public IdRouteConstraint(int minId, int maxId)
        {
            MinId = minId;
            MaxId = maxId;
        }
        public bool Match(
            HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection
            )
        {
            return Int32.TryParse(values[routeKey].ToString(), out int id) && MinId <= id && id <= MaxId;
        }
    }
}
