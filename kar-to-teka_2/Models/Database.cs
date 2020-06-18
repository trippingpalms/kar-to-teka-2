using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kar_to_teka_2.Models;
using kar_to_teka_2.ViewModels;
using MongoDB.Bson;

namespace kar_to_teka_2.Models
{
    public sealed class Database
    {
        // Singleton mechanism (lines: 12 to 29).
        private static Database databaseInstance = null;
        private static readonly object databaseLock = new object();

        public static Database Instance
        {
            get
            {
                lock (databaseLock)
                {
                    if (databaseInstance == null)
                    {
                        databaseInstance = new Database();
                    }

                    return databaseInstance;
                }
            }
        }

        // Rest of the class.
        private MongoClient mongoClient;
        private IMongoDatabase mongoDatabase;
        private IMongoCollection<Crime> databaseCrimes;
        private IMongoCollection<Criminal> databaseCriminals;

        private Database()
        {
            mongoClient = new MongoClient();
            mongoDatabase = mongoClient.GetDatabase("Kartoteka");

            databaseCrimes = mongoDatabase.GetCollection<Crime>("Crimes");
            databaseCriminals = mongoDatabase.GetCollection<Criminal>("Criminals");
        }

        public void AddCrime(AddCrimeViewModel addCrimeViewModel)
        {
            Crime crime = new Crime();

            crime.Paragraph = addCrimeViewModel.Paragraph;
            crime.Name = addCrimeViewModel.Name;
            crime.Description = addCrimeViewModel.Description;

            databaseCrimes.InsertOne(crime);
        }
        public void AssignCrime(AssignCrimeViewModel assignCrimeViewModel)
        {
            Criminal criminal = assignCrimeViewModel.Criminal;
            CommittedCrime committedCrime = new CommittedCrime();
            
            committedCrime.Crime = assignCrimeViewModel.Crime;
            committedCrime.Description = assignCrimeViewModel.Description;
            committedCrime.ImprisonmentDate = assignCrimeViewModel.ImprisonmentDate;
            committedCrime.ImprisonmentLength = assignCrimeViewModel.ImprisonmentLength;

            if (criminal.CommittedCrimes == null)
            {
                criminal.CommittedCrimes = new List<CommittedCrime>();
            }
            criminal.CommittedCrimes.Add(committedCrime);
            databaseCriminals.FindOneAndReplace(x => x._id == criminal._id, criminal);
        }
        public void UnassignCrime(UnassignCrimeViewModel unassignCrimeViewModel)
        {
            Criminal criminal = unassignCrimeViewModel.Criminal;

            databaseCriminals.FindOneAndReplace(x => x._id == criminal._id, criminal);
        }
        public List<Crime> GetCrimes()
        {
            return databaseCrimes.Find(x => !String.IsNullOrEmpty(x.Name)).ToList<Crime>();
        }

        public void AddCriminal(AddCriminalViewModel addCriminalViewModel)
        {
            Criminal criminal = new Criminal();

            criminal.Name = addCriminalViewModel.Name;
            criminal.Surname = addCriminalViewModel.Surname;
            criminal.Nickname = addCriminalViewModel.Nickname;
            criminal.Residence = addCriminalViewModel.Residence;
            criminal.BirthPlace = addCriminalViewModel.BirthPlace;
            criminal.BirthDate = addCriminalViewModel.BirthDate;
            criminal.Wanted = addCriminalViewModel.Wanted;
            criminal.CommittedCrimes = new List<CommittedCrime>();

            databaseCriminals.InsertOne(criminal);
        }
        public void EditCriminal(EditCriminalViewModel editCriminalViewModel)
        {
            Criminal criminal = new Criminal();

            criminal._id = editCriminalViewModel.Criminal._id;
            criminal.Name = editCriminalViewModel.Name;
            criminal.Surname = editCriminalViewModel.Surname;
            criminal.Nickname = editCriminalViewModel.Nickname;
            criminal.Residence = editCriminalViewModel.Residence;
            criminal.BirthPlace = editCriminalViewModel.BirthPlace;
            criminal.BirthDate = editCriminalViewModel.BirthDate;
            criminal.Wanted = editCriminalViewModel.Wanted;
            criminal.CommittedCrimes = editCriminalViewModel.Criminal.CommittedCrimes;

            databaseCriminals.FindOneAndReplace(x => x._id == editCriminalViewModel.Criminal._id, criminal);
        }
        public void DeleteCriminal(EditCriminalViewModel editCriminalViewModel)
        {
            Criminal criminal = editCriminalViewModel.Criminal;

            databaseCriminals.DeleteOne(x => x._id == criminal._id);
        }
        public List<Criminal> GetCriminals()
        {
            return databaseCriminals.Find(x => !String.IsNullOrEmpty(x.Name) && !String.IsNullOrEmpty(x.Surname)).ToList<Criminal>();
        }
        public void AddCriminalPhoto(AddCriminalPhotoViewModel addCriminalPhotoViewModel)
        {
            Criminal criminal = new Criminal();

            criminal._id = addCriminalPhotoViewModel.Criminal._id;
            criminal.Name = addCriminalPhotoViewModel.Criminal.Name;
            criminal.Surname = addCriminalPhotoViewModel.Criminal.Surname;
            criminal.Nickname = addCriminalPhotoViewModel.Criminal.Nickname;
            criminal.Residence = addCriminalPhotoViewModel.Criminal.Residence;
            criminal.BirthPlace = addCriminalPhotoViewModel.Criminal.BirthPlace;
            criminal.BirthDate = addCriminalPhotoViewModel.Criminal.BirthDate;
            criminal.Wanted = addCriminalPhotoViewModel.Criminal.Wanted;
            criminal.CommittedCrimes = addCriminalPhotoViewModel.Criminal.CommittedCrimes;
            criminal.Image = addCriminalPhotoViewModel.Image;

            databaseCriminals.FindOneAndReplace(x => x._id == addCriminalPhotoViewModel.Criminal._id, criminal);
        }

        public List<string> GetStatistics()
        {
            List<string> listOfStatistics = new List<string>();
            List<Criminal> listOfCriminals;

            long numberOfCrimes = databaseCrimes.Count(x => !String.IsNullOrEmpty(x.Name));
            listOfStatistics.Add(Convert.ToString(numberOfCrimes));
            long numberOfCriminals = databaseCriminals.Count(x => !String.IsNullOrEmpty(x.Name) && !String.IsNullOrEmpty(x.Surname));
            listOfStatistics.Add(Convert.ToString(numberOfCriminals));
            long numberOfCommittedCrimes = 0;
            listOfCriminals = databaseCriminals.Find(x => !String.IsNullOrEmpty(x.Name) && !String.IsNullOrEmpty(x.Surname)).ToList<Criminal>();
            foreach (var item in listOfCriminals)
            {
                numberOfCommittedCrimes += item.CommittedCrimes.Count;
            }
            listOfStatistics.Add(Convert.ToString(numberOfCommittedCrimes));

            return listOfStatistics;
        }
    }
}
