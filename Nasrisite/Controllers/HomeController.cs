using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nasrisite.Models;
using Nasrisite.MyModel;

namespace Nasrisite.Controllers
{


    public class HomeController : Controller
    {

        SWENEntities DBentities = new SWENEntities();
      

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ParticipantInfo()
        {
            //FillDropListForParticipent();
            return View();
        }

        public ActionResult SaveparticipantInfo(participantmember participant)
        {
            //FillDropListForParticipent();

            if (ModelState.IsValid)
            {
                var CheckEmailExist = (from i in DBentities.participants
                                       where i.Email.ToLower() == participant.Email.ToLower()
                                       select i).FirstOrDefault();
                if (CheckEmailExist != null)
                {
                    Session["userID"] = CheckEmailExist.ID;
                    return RedirectToAction("classification", "Home");
                }
                else
                {
                    participant newparticipant = new participant();
                    newparticipant.Email = participant.Email;
                    newparticipant.JobTitle = participant.JobTitle;
                    newparticipant.Experience = participant.Experience;
                    DBentities.participants.Add(newparticipant);
                    DBentities.SaveChanges();
                    Session["userID"] = newparticipant.ID;

                    return RedirectToAction("classification", "Home");

                }
            }
            return View("participantInfo", participant);
        }

        public ActionResult Classification()
        {
            ViewBag.ClassificationID = new SelectList(DBentities.Categories, "ID", "Description");
            if (Session["userID"] == null)
            {
                //FillDropListForParticipent();
                return View("participantInfo");
            }
            else
            {
                int UserId = Convert.ToInt32(Session["userID"]);


                participant p = GetParticipantInfo(UserId);
                ViewBag.ParticipantEmail = p.Email;

                int CountOfLabeledReviews = GetCountOfLabeledReviews(UserId);
                ViewBag.NoOfComments = CountOfLabeledReviews;
            
                UsersReview userreview = GetUserReviewForLabeling(UserId);
                ViewBag.NonFuncationalRequirments = GetNonFuncationalRequirment();
                ViewBag.ParticipantID = UserId;
                if (IsAllAnswered(UserId))
                {
                    ViewBag.message = "Great! you classified all reviews.";
                    ViewBag.UserReviewId = 0;
                    return View();
                }
                else
                {                   
                    ViewBag.UserReviewId = userreview.UserReviewId;                   
                    return View(userreview);
                }
                                       
            }

        }

        //All User review are classified
        private bool IsAllAnswered(int UserId)
        {
            List<UsersReview> canUse = new List<UsersReview>();
            canUse = DBentities.UsersReviews.Where(u => u.UserReviewUse == true).ToList();
            int CountOfUserReviews = canUse.Count();

            List<Result> results = new List<Result>();
            results = DBentities.Results.Where(u => u.ParticipantId == UserId).ToList();
            int CountOdAnswerdResultByUser = results.Count();

            if ( CountOdAnswerdResultByUser < CountOfUserReviews)
            {
                return false;
            }
            else
            {
                return true;
            }          
        }

        //Check if user review is answerd or not
        private bool IsAnswerd(int UserId, UsersReview userreview)
        {
            
            var result = DBentities.Results.Where(r => r.ParticipantId == UserId && r.UserReviewId == userreview.UserReviewId);
            if ( result.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Check if user review is answerd by Nasri Or Majdi
        private bool IsAnswerdByNasriOrMajd(UsersReview userreview)
        {
            int NasriId = 1;
            int MajdiID = 4;
            var result = DBentities.Results.Where(r => (r.ParticipantId == NasriId || r.ParticipantId == MajdiID) && r.UserReviewId == userreview.UserReviewId);
            if (result.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private UsersReview GetUserReviewForLabeling(int userId)
        {
            //int MajdiID = 4;
            //int NasriID = 1;
            CanUseAndRandom c = new CanUseAndRandom();
            c = GetRandomNumber();
            UsersReview user_review = GetReviewDetails(c.SelectedUserReview, c.canUse);
            bool isAnswerdByParticipant = IsAnswerd(userId, user_review);
            //bool isAnswerdByMajdi = IsAnswerd(MajdiID, user_review);
            //bool isAnswerdByNasri = IsAnswerd(NasriID, user_review);
            //bool total = isAnswerdByParticipant & isAnswerdByMajdi & isAnswerdByNasri;
            //bool res = IsAnswerdByNasriOrMajd(user_review);
            while (isAnswerdByParticipant)
            {
                Console.WriteLine("In -anaswerd " + c.SelectedUserReview);
                return GetUserReviewForLabeling(userId);
            }
            Console.WriteLine("Not answerd before "+c.SelectedUserReview);
            return user_review;
            
        }

        private CanUseAndRandom GetRandomNumber()
        {
            Random rand = new Random();
            List<UsersReview> canUse = new List<UsersReview>();
            canUse = canUseReviews();
            int SelectedUserReview = rand.Next(canUse.Count());
            CanUseAndRandom c = new CanUseAndRandom();
            c.SelectedUserReview = SelectedUserReview;
            c.canUse = canUse;
            return c;
        }

        private UsersReview GetReviewDetails(int selectedUserReview, List<UsersReview> canUse)
        {
            UsersReview ur = new UsersReview();
            ur = canUse[selectedUserReview];
            return ur;
        }


        //CanUse Flag == 0
        private List<UsersReview> canUseReviews()
        {
            List<UsersReview> canUse = new List<UsersReview>();
            canUse = DBentities.UsersReviews.Where(u => u.UserReviewUse == true).ToList();
            return canUse;
        }

        private List<NonFuncationalRequirments> GetNonFuncationalRequirment()
        {
            List<NonFuncationalRequirments> c = new List<NonFuncationalRequirments>();
            foreach(NonFuncationalRequirment cat in DBentities.NonFuncationalRequirments)
            {
                NonFuncationalRequirments c1 = new NonFuncationalRequirments();
                c1.NonFuncationalRequirmentID = cat.NonFuncationalRequirmentID;
                c1.NonFuncationalRequirment1 = cat.NonFuncationalRequirment1;
                c.Add(c1);
            }
           
            return c;
        }

        private int GetCountOfLabeledReviews(int userId)
        {
            List<Result> R = new List<Result>();
            R = DBentities.Results.Where(Labeled => Labeled.ParticipantId == userId).ToList();
            return R.Count();
        }


        // information of users 
        private participant GetParticipantInfo(int UserId)
        {
            participant p = DBentities.participants.Where(participant => participant.ID == UserId).FirstOrDefault(); ;
            return p;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "For any question, feel free to contact me anytime.";

            return View();
        }



        [HttpPost]       
        public ActionResult Answer(Answer answer)
        {
          if (ModelState.IsValid)
            {
                Result r = new Result();
                r.NonFuncationalReqID = answer.NonFuncationalRequirment1;
                r.UserReviewDateTime = DateTime.Now;
                r.ParticipantId = answer.ParticipantID;
                r.UserReviewId = answer.UserReviewId;
                DBentities.Results.Add(r);
                DBentities.SaveChanges();
            }
               return RedirectToAction("Classification");
          
        }

    }
}