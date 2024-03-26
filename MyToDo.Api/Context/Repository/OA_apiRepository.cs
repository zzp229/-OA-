namespace MyToDo.Api.Context.Repository
{
    public class OA_apiRepository : Repository<OA_api>, IRepository<OA_api>
    {
        public OA_apiRepository(MyToDoContext dbContext) : base(dbContext)
        {
        }
    }
}
