using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Model;
using Movie.Repository;
using Movie.Repository.Interface;

namespace Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger logger;
        public IMoviesRepository MoviesRepo;

        public MovieController(IConfiguration configuartion, ILoggerFactory loggerFactory, IMoviesRepository MoviesRepo)
        {
            this.logger = loggerFactory.CreateLogger<MovieController>();
            this.MoviesRepo = MoviesRepo;
        }
        [HttpGet("GetAllMoviesDetails")]
        public async Task<ActionResult> GetAllMoviesDetails()
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(string.Format($"MovieController-Get:Calling GetAllMoviesDetails."));
            var mov = await MoviesRepo.GetAllMoviesDetails();
            if (mov.Count == 0)
            {
                baseResponseStatus.StatusCode = StatusCodes.Status404NotFound.ToString();
                baseResponseStatus.StatusMessage = "Data not found";
            }
            else
            {
                baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponseStatus.StatusMessage = "All Data feached Successfully";
                baseResponseStatus.ResponseData = mov;
            }
            return Ok(baseResponseStatus);
        }

        [HttpGet("GetByMoviesId")]
        public async Task<IActionResult> GetByMoviesId(int id)
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(string.Format($"MovieController-Get:Calling GetByMoviesId."));
            var bank = await MoviesRepo.GetByMoviesId(id);
            if (bank == null)
            {
                baseResponseStatus.StatusCode = StatusCodes.Status404NotFound.ToString();
                baseResponseStatus.StatusMessage = "Data not found";
            }
            else
            {
                baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponseStatus.StatusMessage = "All Data feached Successfully";
                baseResponseStatus.ResponseData = bank;
            }
            return Ok(baseResponseStatus);
        }

        [HttpPost("CreateMoviesDetails")]
        public async Task<IActionResult> CreateMoviesDetails(moviedetails mdetails)
        {
            {
                BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
                logger.LogDebug(String.Format($"MovieController-CreateCompanyBankAccountDetails:Calling By CreateCompanyBankAccountDetails action."));
                if (mdetails != null)
                {
                    var Execution = await MoviesRepo.CreateMoviesDetails(mdetails);
                    if (Execution == -1)
                    {
                        var returnmsg = string.Format($"Record Is Already saved With ID{mdetails.Id}");
                        logger.LogDebug(returnmsg);
                        baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                        baseResponseStatus.StatusMessage = returnmsg;
                        return Ok(baseResponseStatus);
                    }
                    else if (Execution >= 1)
                    {
                        var rtnmsg = string.Format("Record added successfully..");
                        logger.LogInformation(rtnmsg);
                        logger.LogDebug(string.Format("CompanyBankAccountController-CreateCompanyBankAccountDetails:Calling By CreateCompanyBankAccountDetails action."));
                        baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                        baseResponseStatus.StatusMessage = rtnmsg;
                        baseResponseStatus.ResponseData = Execution;
                        return Ok(baseResponseStatus);
                    }
                    else
                    {
                        var rtnmsg1 = string.Format("Error while Adding");
                        logger.LogError(rtnmsg1);
                        baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                        baseResponseStatus.StatusMessage = rtnmsg1;
                        return Ok(baseResponseStatus);
                    }

                }
                else
                {
                    var returnmsg = string.Format("Record added successfully..");
                    logger.LogDebug(returnmsg);
                    baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponseStatus.StatusMessage = returnmsg;
                    return Ok(baseResponseStatus);
                }
            }
        }

        [HttpPut("UpdateMoviesDetails")]
        public async Task<IActionResult> UpdateMoviesDetails(moviedetails mdetails)
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(String.Format($"MovieController-UpdateMoviesDetails:Calling By UpdateMoviesDetails  action."));
            if (mdetails != null)
            {
                var Execution = await MoviesRepo.UpdateMoviesDetails(mdetails);
                if (Execution == -1)
                {
                    var returnmsg = string.Format($"Record Is Already saved With ID{mdetails.Id}");
                    logger.LogDebug(returnmsg);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = returnmsg;
                    return Ok(baseResponseStatus);
                }
                else if (Execution >= 1)
                {
                    var rtnmsg = string.Format("Record update successfully..");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format("CompanyBankAccountController-UpdateCompanyBankAccountDetails:Calling By UpdateCompanyBankAccountDetails action."));
                    baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponseStatus.StatusMessage = rtnmsg;
                    baseResponseStatus.ResponseData = Execution;
                    return Ok(baseResponseStatus);
                }
                else
                {
                    var rtnmsg1 = string.Format("Error while Adding");
                    logger.LogError(rtnmsg1);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = rtnmsg1;
                    return Ok(baseResponseStatus);
                }

            }
            else
            {
                var returnmsg = string.Format("Record added successfully..");
                logger.LogDebug(returnmsg);
                baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponseStatus.StatusMessage = returnmsg;
                return Ok(baseResponseStatus);
            }
        }
    }
}

