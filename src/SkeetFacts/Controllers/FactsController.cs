using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SkeetFacts.Models;
using SkeetFacts.Models.Indexes;

namespace SkeetFacts.Controllers
{
    public class FactsController : RavenController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var fact = RavenSession.Query<Fact, Facts_QueryIndex>()
                                    .Customize(x => x.RandomOrdering())
                                    .Take(1)
                                    .FirstOrDefault();

            return View(fact);
        }

        [HttpGet]
        public string Create()
        {
            if (RavenSession.Query<Fact, Facts_QueryIndex>().Any())
                return "Already populated.";

            var facts = new List<Fact> 
            { 
                new Fact { Created = DateTime.Now.AddHours(-1), Content = "Jon Skeet is immutable. If something's going to change, it's going to have to be the rest of the universe." },
                new Fact { Created = DateTime.Now.AddHours(-2), Content = "Anonymous methods and anonymous types are really all called Jon Skeet. They just don't like to boast." },
                new Fact { Created = DateTime.Now.AddHours(-3),Content = "Jon Skeet's code doesn't follow a coding convention. It is the coding convention." },
                new Fact { Created = DateTime.Now.AddHours(-4),Content = "Users don't mark Jon Skeet's answers as accepted. The universe accepts them out of a sense of truth and justice." },
                new Fact { Created = DateTime.Now.AddHours(-5),Content = "Jon Skeet can divide by zero." },
                new Fact { Created = DateTime.Now.AddHours(-6),Content = "Jon Skeet's SO reputation is only as modest as it is because of integer overflow (SQL Server does not have a datatype large enough)." },
                new Fact { Created = DateTime.Now.AddHours(-7),Content = "Jon Skeet is the only top 100 SO user who is human. The others are bots that he coded to pass the time between questions." },
                new Fact { Created = DateTime.Now.AddHours(-8),Content = "Jon Skeet coded his last project entirely in Microsoft Paint, just for the challenge." },
                new Fact { Created = DateTime.Now.AddHours(-9),Content = "When Jon Skeet's code fails to compile the compiler apologises." },
                new Fact { Created = DateTime.Now.AddHours(-10),Content = "Jon Skeet does not use revision control software. None of his code has ever needed revision." },
                new Fact { Created = DateTime.Now.AddHours(-11),Content = "When you search for \"guru\" on Google it says \"Did you mean Jon Skeet?\"" },
                new Fact { Created = DateTime.Now.AddHours(-12),Content = "There are two types of programmers: good programmers, and those that are not Jon Skeet." },
                new Fact { Created = DateTime.Now.AddHours(-13),Content = "Jon Skeet once answered one of my questions 42 seconds before I asked it. It is my belief that he employed a super computer and Infinite Improbability Drive technology to achieve this result." },
                new Fact { Created = DateTime.Now.AddHours(-14),Content = "When Jon Skeet points to null, null quakes in fear." },
                new Fact { Created = DateTime.Now.AddHours(-15),Content = "Jon Skeet has root access to your system." },
                new Fact { Created = DateTime.Now.AddHours(-16),Content = "Jon Skeet has more \"Nice Answer\" badges than you have badges." },
                new Fact { Created = DateTime.Now.AddHours(-17),Content = "Jon Skeet does not sleep.. He waits." },
                new Fact { Created = DateTime.Now.AddHours(-18),Content = "Google is Jon Skeet behind a proxy." },
                new Fact { Created = DateTime.Now.AddHours(-19),Content = "Jon Skeet can code in Perl and make it look like Java." },
                new Fact { Created = DateTime.Now.AddHours(-20),Content = "Jon Skeet can stop an infinite loop just by thinking about it." },
                new Fact { Created = DateTime.Now.AddHours(-21),Content = "Jon Skeet doesn't need a debugger, he just stares down the bug until the code confesses." },
                new Fact { Created = DateTime.Now.AddHours(-22),Content = "Jon Skeet once wrote an entire operating system in his sleep on a Treo with no battery, powered only by the force of his will." },
                new Fact { Created = DateTime.Now.AddHours(-23),Content = "There is no 'CTRL' button on Jon Skeet's computer. Jon Skeet is always in control." },
                new Fact { Created = DateTime.Now.AddHours(-24),Content = "Jon Skeet does not run his programs. He just whispers \"you better run\". And it runs." },
                new Fact { Created = DateTime.Now.AddHours(-25),Content = "Jon Skeet does not \"Abort, Retry, Ignore\". Ever." },
                new Fact { Created = DateTime.Now.AddHours(-26),Content = "God said: 'Let there be light,' only so he could see what Jon Skeet was up to." },
                new Fact { Created = DateTime.Now.AddHours(-27),Content = "Jon Skeet's keyboard doesn't have F1 key, the computer asks for help from him." },
                new Fact { Created = DateTime.Now.AddHours(-28),Content = "When Jon Skeet presses Ctrl+Alt+Delete, worldwide computers restart is initiated. The same goes for format." },
                new Fact { Created = DateTime.Now.AddHours(-29),Content = "Jon Skeet uses Visual Studio to burn CD's." },
                new Fact { Created = DateTime.Now.AddHours(-30),Content = "Jon Skeet dreams in 1 and 0. When 2 shows up, it is a nightmare. But again that's only in theory. 2 doesn't exist for Jon." },
                new Fact { Created = DateTime.Now.AddHours(-31),Content = "Jon Skeet's heart rate is 5GHz." },
                new Fact { Created = DateTime.Now.AddHours(-32),Content = "Nobody has EVER dared to close the <JonSkeet> tag." },
                new Fact { Created = DateTime.Now.AddHours(-33),Content = "Seventh normal form (7NF) for database normalization IS Jon Skeet." },
                new Fact { Created = DateTime.Now.AddHours(-34),Content = "Compatibility doesn't exist in Jon Skeet's dictionary. He can easily work in Microsoft Office in Linux on a Mac." },
                new Fact { Created = DateTime.Now.AddHours(-35),Content = "When Jon Skeet is programming the Garbage Collector rests. The objects know when to destroy themselves." },
                new Fact { Created = DateTime.Now.AddHours(-36),Content = "Jon Skeet's styling is connected to a .css file." },
                new Fact { Created = DateTime.Now.AddHours(-37),Content = "Jon Skeet uses Visual Studio to burn CD's." },
                new Fact { Created = DateTime.Now.AddHours(-38),Content = "Jon Skeet doesn't need delegates, he does all the work himself." },
                new Fact { Created = DateTime.Now.AddHours(-39),Content = "Jon Skeet doesn't call a background worker, background workers call Jon Skeet." },
                new Fact { Created = DateTime.Now.AddHours(-40),Content = "Jon Skeet doesn't write books, the words assemble themselves out of fear." },
                new Fact { Created = DateTime.Now.AddHours(-41),Content = "Jon Skeet can solve the travelling salesman in O(1)." },
                new Fact { Created = DateTime.Now.AddHours(-42),Content = "When Jon Skeet throws an exception, nothing can catch it." }
            };

            // Naughtily overriding the 30 session limit
            RavenSession.Advanced.MaxNumberOfRequestsPerSession = 100;

            foreach(var fact in facts)
            {
                RavenSession.Store(fact);
            }

            return "Creating...";
        }
    }
}
