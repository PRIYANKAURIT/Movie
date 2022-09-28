namespace Movie.Model
{
    public class Movies
    {
        public int Id { get; set; }
        public string? mov_title { get; set; }
        public int mov_year { get; set; }
        public int mov_time { get; set; }
        public string? mov_lang { get; set; }
        public DateTime mov_dt_rel { get; set; }
        public string? mov_rel_country { get; set; }
        public int createdBy { get; set; }
        public DateTime createDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public int modifiedBy { get; set; }
        public bool isDeleted { get; set; }
        public int act_id { get; set; }
        public int dir_id { get; set; }
        public int gen_id { get; set; }
        public int rev_id { get; set; }

    }

    public class mymovie
    {
        public string? mov_title { get; set; }
        public string? Actor_Name { get; set; }
        public string? Director_Name { get; set; }
        public string? gen_title { get; set; }
        public int rev_stars { get; set; }
    }

    public class moviedetails
    {
        public int Id { get; set; }
        public string? mov_title { get; set; }
        public int mov_year { get; set; }
        public int mov_time { get; set; }
        public string? mov_lang { get; set; }
        public DateTime mov_dt_rel { get; set; }
        public string? mov_rel_country { get; set; }
        public int createdBy { get; set; }
        public DateTime createDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public int modifiedBy { get; set; }
        public bool isDeleted { get; set; }
        public int act_id { get; set; }
        public int dir_id { get; set; }
        public int gen_id { get; set; }
        public int rev_id { get; set; }
        public List<MovieActor> MA { get; set; }
        public List<MovieDirector> MD { get; set; }
        public List<MovieGeners> MG { get; set; } 
        public List<MovieRatingReviwer> MR { get; set; }
    }
}



