using Movie.Model;

namespace Movie.Repository.Interface
{
    public interface IMoviesRepository
    {
        public Task<List<mymovie>> GetAllMoviesDetails();
        public Task<mymovie> GetByMoviesId(int id);
        public Task<int> CreateMoviesDetails(moviedetails mdetails);
        public Task<int> UpdateMoviesDetails(moviedetails mdetails);

    }
}
