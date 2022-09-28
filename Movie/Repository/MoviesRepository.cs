using Dapper;
using Movie.Model;
using Movie.Repository.Interface;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;

namespace Movie.Repository
{
    public class MoviesRepository : BaseAsyncRepository, IMoviesRepository
    {
        public MoviesRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<mymovie>> GetAllMoviesDetails()
        {
            List<mymovie> movie = null;
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                await dbConnection.OpenAsync();
                var moviee = await dbConnection.QueryAsync<mymovie>(@"select m.mov_title,ma.act_fname+' '+ ma.act_lname as Actor_Name,dir_fname+' '+dir_lname as Director_Name,mg.gen_title,mr.rev_stars
                                                                    from movie m
                                                                    inner join movie_actor ma on m.Id = ma.Id
                                                                    inner join movie_director md on m.Id=md.Id
                                                                    inner join movie_geners mg on m.Id=mg.Id
                                                                    inner join movie_rating_reviwer mr on m.Id=mr.Id");
                movie = moviee.ToList();
            }
            return movie;
        }
        public async Task<mymovie> GetByMoviesId(int id)
        {
            mymovie movie = null;
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                await dbConnection.OpenAsync();
                var moviee = await dbConnection.QueryAsync<mymovie>(@"select m.mov_title,ma.act_fname+' '+ ma.act_lname as Actor_Name,dir_fname+' '+dir_lname as Director_Name,mg.gen_title,mr.rev_stars
                                                                    from movie m
                                                                    inner join movie_actor ma on m.Id = ma.Id
                                                                    inner join movie_director md on m.Id=md.Id
                                                                    inner join movie_geners mg on m.Id=mg.Id
                                                                    inner join movie_rating_reviwer mr on m.Id=mr.Id
                                                                    where m.Id =@id", new { id });
                movie = moviee.FirstOrDefault();
            }
            return movie;
        }

        public async Task<int> CreateMoviesDetails(moviedetails mdetails)
        {
            var query = @"insert into movie (mov_title,mov_year,mov_time,mov_lang,                            mov_dt_rel,mov_rel_country,createdBy,createDate,modifiedBy,
                            modifiedDate,isDeleted,act_id,dir_id,gen_id,rev_id) 
                            values(@mov_title, @mov_year, @mov_time, @mov_lang, @mov_dt_rel,
                            @mov_rel_country, @createdBy, @createDate, @modifiedBy, @modifiedDate,
                            @isDeleted, @act_id, @dir_id, @gen_id, @rev_id);
                            SELECT CAST(SCOPE_IDENTITY() as bigint); ";

            List<MovieActor> malist = new List<MovieActor>();
            malist = mdetails.MA.ToList();

            List<MovieDirector> mdlist = new List<MovieDirector>();
            mdlist = mdetails.MD.ToList();

            List<MovieGeners> mglist = new List<MovieGeners>();
            mglist = mdetails.MG.ToList();

            List<MovieRatingReviwer> mrlist = new List<MovieRatingReviwer>();
            mrlist = mdetails.MR.ToList();

            using (DbConnection dbConnection = sqlwriterConnection)
            {
                await dbConnection.OpenAsync();
                mdetails.createDate = DateTime.Now;
                mdetails.modifiedDate = DateTime.Now;

                int result = await dbConnection.ExecuteAsync(query, mdetails);

                foreach (var details in mdetails.MA)
                {
                    int re1 = await dbConnection.ExecuteAsync(@"insert into movie_actor
                                  (act_id,act_fname,act_lname,act_gender,act_role,Id) 
                                  values
                                  (@act_id,@act_fname,@act_lname,@act_gender,@act_role,@Id)"
                                  , details);

                    foreach (var details1 in mdetails.MD)
                    {
                        int re2 = await dbConnection.ExecuteAsync(@"
                                  insert into movie_director
                                  (dir_id,dir_fname,dir_lname,Id) 
                                  values (@dir_id,@dir_fname,@dir_lname,@Id)"
                                  , details1);

                        foreach (var details2 in mdetails.MG)
                        {
                            int re3 = await dbConnection.ExecuteAsync(@"
                                  insert into movie_geners
                                   (gen_id,gen_title,Id) 
                                  values  (@gen_id,@gen_title,@Id) "
                                  , details2);

                            foreach (var details3 in mdetails.MR)
                            {
                                int re4 = await dbConnection.ExecuteAsync(@"
                                  insert into movie_rating_reviwer
                                  (rev_id,rev_stars,num_o_ratings,rev_name,Id)
                                  values (@rev_id,@rev_stars,@num_o_ratings,@rev_name,@Id) "
                                  , details3);
                            }
                        }
                    }

                }
                return Convert.ToInt32(result);

            }
        }

        public async Task<int> UpdateMoviesDetails(moviedetails mdetails)
        {
            var query = @"update movie set  mov_title=@mov_title,mov_year=@mov_year,mov_time=@mov_time,
                                            mov_lang=@mov_lang,mov_dt_rel=@mov_dt_rel,
                                            mov_rel_country=@mov_rel_country,createdBy=@createdBy,
                                            createDate=@createDate,modifiedBy=@modifiedBy,
                                            modifiedDate=@modifiedDate,isDeleted=@isDeleted,
                                            act_id=@act_id,dir_id=@dir_id,gen_id=@gen_id,
                                            rev_id=@rev_id
                                            where Id=@Id";

            List<MovieActor> malist = new List<MovieActor>();
            malist = mdetails.MA.ToList();

            List<MovieDirector> mdlist = new List<MovieDirector>();
            mdlist = mdetails.MD.ToList();

            List<MovieGeners> mglist = new List<MovieGeners>();
            mglist = mdetails.MG.ToList();

            List<MovieRatingReviwer> mrlist = new List<MovieRatingReviwer>();
            mrlist = mdetails.MR.ToList();

            using (DbConnection dbConnection = sqlwriterConnection)
            {
                await dbConnection.OpenAsync();
                mdetails.createDate = DateTime.Now;
                mdetails.modifiedDate = DateTime.Now;

                int result = await dbConnection.ExecuteAsync(query, mdetails);

                foreach (var details in mdetails.MA)
                {
                    int re1 = await dbConnection.ExecuteAsync(@"update movie_actor set
                                  act_fname=@act_fname,act_lname=@act_lname,act_gender=@act_gender,
                                  act_role=@act_role,Id=@Id where act_id=@act_id"
                                  , details);

                    foreach (var details1 in mdetails.MD)
                    {
                        int re2 = await dbConnection.ExecuteAsync(@"
                                  update movie_director set
                                  dir_fname=@dir_fname,dir_lname=@dir_lname,Id=@Id
                                  where dir_id=@dir_id"
                                  , details1);

                        foreach (var details2 in mdetails.MG)
                        {
                            int re3 = await dbConnection.ExecuteAsync(@"
                                  update movie_geners set
                                  gen_title=@gen_title,Id=@Id 
                                  where gen_id=@gen_id"
                                  , details2);

                            foreach (var details3 in mdetails.MR)
                            {
                                int re4 = await dbConnection.ExecuteAsync(@"
                                  update movie_rating_reviwer set
                                  rev_stars=@rev_stars,num_o_ratings=@num_o_ratings,rev_name=@rev_name,Id=@Id
                                  where rev_id=@rev_id"
                                  , details3);
                            }
                        }
                    }

                }
                return Convert.ToInt32(result);

            }
        }
    }
}
/*{
  "id": 0,
  "mov_title": "singham",
  "mov_year": 2012,
  "mov_time": 9,
  "mov_lang": "hindi",
  "mov_dt_rel": "2022-09-28T09:10:46.504Z",
  "mov_rel_country": "india",
  "createdBy": 0,
  "createDate": "2022-09-28T09:10:46.504Z",
  "modifiedDate": "2022-09-28T09:10:46.504Z",
  "modifiedBy": 0,
  "isDeleted": true,
  "act_id": 1,
  "dir_id": 1,
  "gen_id": 1,
  "rev_id": 1,
  "ma": [
    {
      "act_id": 1,
      "act_fname": "ajay",
      "act_lname": "devgan",
      "act_gender": "male",
      "act_role": "lead",
      "id": 0
    }
  ],
  "md": [
    {
      "dir_id": 1,
      "dir_fname": "rohit",
      "dir_lname": "shetty",
      "id": 1
    }
  ],
  "mg": [
    {
      "gen_id": 1,
      "gen_title": "action",
      "id": 1
    }
  ],
  "mr": [
    {
      "rev_id": 1,
      "rev_stars": 5,
      "num_o_ratings": 1554,
      "rev_name": "good",
      "id": 1
    }
  ]
}*/

/*foreach (var act in mdetails)
{
    int result1 = await dbConnection.ExecuteAsync(@"
                                  insert into movie_actor
                                  (act_id,act_fname,act_lname,act_gender,act_role,Id) 
                                  values (@act_id,@act_fname,@act_lname,@act_gender,@act_role,@Id)",
                  resultResult);
}
if (resultResult != 0)
{
    int result2 = await dbConnection.ExecuteAsync(@"
                                  insert into movie_director
                                  (dir_id,dir_fname,dir_lname,Id) 
                                  values (@dir_id,@dir_fname,@dir_lname,@Id)",
                  new { dir_id = resultResult });
}
if (resultResult != 0)
{
    int result3 = await dbConnection.ExecuteAsync(@"
                                  insert into movie_geners
                                   (gen_id,gen_title,Id) 
                                  values  (@gen_id,@gen_title,@Id) ",
                  new { gen_id = resultResult });
}
if (resultResult != 0)
{
    int result4 = await dbConnection.ExecuteAsync(@"
                                  insert into movie_rating_reviwer
                                  (rev_id,rev_stars,num_o_ratings,rev_name,Id)
                                  values (@rev_id,@rev_stars,@num_o_ratings,@rev_name,@Id) ",
                  new { rev_id = resultResult });
}
return resultResult;*/