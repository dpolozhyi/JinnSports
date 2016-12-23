using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.Parser.App.NamingMatcher
{
    public class NamingMatcher
    {   
        public NamingMatcher(IUnitOfWork unit)
        {
            this.Unit = unit;
        }

        public IUnitOfWork Unit { get; set; }

        /// <summary>
        /// Returns Team with given name in case this is exist in DB or it is brand new Team. In other cases adds naming conflicts to DB.
        /// </summary>
        /// <param name="inputTeam"></param>
        /// <returns></returns>
        public Team ResolveNaming(Team inputTeam) //add typisation and locals
        {
            IEnumerable<Team> teams = this.Unit.GetRepository<Team>().Get((t) => t.SportType.Name == inputTeam.SportType.Name);

            Team simpleCheckTeam = this.SimpleCheck(teams, inputTeam.Name);
            if (simpleCheckTeam != null)
            {
                return simpleCheckTeam;
            }

            string preparedInputName = inputTeam.Name.ToLower().Replace(".", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty);
            List<Team> matches = new List<Team>();

            double comparingResult = this.Compairing(teams, preparedInputName, out matches);

            if (comparingResult == 100 && matches.Count == 1)
            {
                TeamName newName = new TeamName();
                newName.Name = inputTeam.Name;
                matches[0].Names.Add(newName);
                this.Unit.GetRepository<Team>().Update(matches[0]);
                this.Unit.SaveChanges();
                return matches[0];
            }
            else if (comparingResult < 50)
            {
                this.Unit.GetRepository<Team>().Insert(inputTeam);
                this.Unit.SaveChanges();
                return inputTeam;
            }
            else
            {
                foreach (Team team in matches)
                {
                    Conformity conf = new Conformity();
                    conf.InputTeam = inputTeam;
                    conf.ExistedTeam = team;
                    this.Unit.GetRepository<Conformity>().Insert(conf);
                }

                this.Unit.SaveChanges();
                return null;
            }
        }

        private Team SimpleCheck(IEnumerable<Team> teams, string inputName)
        {
            foreach (Team team in teams)
            {
                foreach (TeamName teamName in team.Names)
                {
                    if (teamName.Name == inputName)
                    {
                        //exist in db;
                        return team;
                    }
                }
            }
            return null;
        }

        private double Compairing(IEnumerable<Team> teams, string inputName, out List<Team> matches)
        {
            List<Team> tempMatches = new List<Team>();
            double bestSuit = 0;

            foreach (Team team in teams)
            {
                foreach (TeamName teamName in team.Names)
                {
                    string preparedBaseName = teamName.Name.ToLower().Replace("-", string.Empty).Replace(".", string.Empty).Replace(" ", string.Empty);
                    double numberOfSuitableLetters = this.LiteralComparing(preparedBaseName, inputName);

                    if (numberOfSuitableLetters == 0)
                    {
                        continue;
                    }

                    double currentSuit;

                    if (preparedBaseName.Length > inputName.Length)
                    {
                        currentSuit = numberOfSuitableLetters / inputName.Length * 100;
                    }
                    else
                    {
                        currentSuit = numberOfSuitableLetters / preparedBaseName.Length * 100;
                    }

                    if (currentSuit > bestSuit && currentSuit > 50)
                    {
                        tempMatches.Clear();
                        tempMatches.Add(team);
                        bestSuit = currentSuit;
                    }
                    else if (currentSuit == bestSuit && currentSuit > 50)
                    {
                        tempMatches.Add(team);
                    } 
                }
            }
            matches = tempMatches;
            return bestSuit;
        }

        private int LiteralComparing(string baseName, string inputName)
        {
            int comparingResult = 0;
            int reversedComparingResult = 0;

            StringBuilder comparer = new StringBuilder();

            for (int i = 0; i < inputName.Length; i++)
            {
                comparer.Append(inputName[i]);

                if (!baseName.Contains(comparer.ToString()))
                {
                    comparer.Remove(comparer.Length - 1, 1);
                    break;
                }
            }

            comparingResult = comparer.Length;
            comparer.Clear();

            for (int i = inputName.Length - 1; i >= 0; i--)
            {
                comparer.Insert(0, inputName[i]);

                if (!baseName.Contains(comparer.ToString()))
                {
                    comparer.Remove(0, 1);
                    break;
                }
            }

            reversedComparingResult = comparer.Length;

            return Math.Max(comparingResult, reversedComparingResult);
        }
    }
}
